using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text.Json;

namespace ProxyDictApp
{
    public partial class FormProxy : Form
    {
        public FormProxy()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(FormProxy_Closing);
            btn_turnoff.Enabled = false;
        }
        private void FormProxy_Closing(object sender, EventArgs e)
        {
            CloseConnection();
        }

        private void btn_turnon_Click(object sender, EventArgs e)
        {
            try
            {
                // Start running Proxy server in a new thread
                Global.ProxyServerThread = new Thread(ProxyServer);
                Global.ProxyServerThread.Start();
                //
                Global.DictServer = new TcpClient(Global.DictAddr_0, Global.DictPort_0);
                Global.DictServerNetStream = Global.DictServer.GetStream();
                //
                Global.DictServerThread = new Thread(CatchResponse);
                Global.DictServerThread.Start();
                //
                txtBox_query_history.Text += "Proxy server is running...\n";
                btn_turnoff.Enabled = true;
                btn_turnon.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void ProxyServer()
        {
            // This function will be use as a thread for listening to any new client coming
            // New client coming must have its own thread, thus, this thread will create new thread and new TcpClient for those client
            try
            {
                Global.ProxyServer.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            // listen to all client coming
            while (true)
            {
                try
                {
                    if (Global.ProxyServer.Pending())
                    {
                        var client = Global.ProxyServer.AcceptTcpClient();
                        //
                        string clientAddress = string.Format("{0}:{1}",((IPEndPoint)client.Client.RemoteEndPoint).Address.MapToIPv4().ToString(),((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString());
                        Global.Clients.Add(clientAddress, client);
                        //
                        Thread clientThread = new Thread(() => ClientThread(client, client.GetStream()));
                        Global.ClientThreads.Add(clientThread);
                        clientThread.Start();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void ClientThread(TcpClient client, NetworkStream stream)
        {
            // Catch every data coming from client
            while (true)
            {
                try
                {
                    byte[] receiveBuffer = new byte[Global.PKTSZ];
                    // wait for new data coming
                    while(!stream.DataAvailable) { }
                    stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                    // direct data from client to dictionary server
                    Global.DictServerNetStream.Write(receiveBuffer, 0, receiveBuffer.Length);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void CatchResponse()
        {
            while (true)
            {
                while (!Global.DictServerNetStream.DataAvailable) { }
                byte[] receiveBuffer = new byte[Global.PKTSZ];
                Global.DictServerNetStream.Read(receiveBuffer, 0, receiveBuffer.Length);

                string serialized = Encoding.UTF8.GetString(receiveBuffer);
                // get rid of null characters
                serialized = serialized.Replace("\0", string.Empty);

                Data deserialized = JsonSerializer.Deserialize<Data>(serialized);
                NetworkStream nwStream = Global.Clients[deserialized.dest].GetStream();
                nwStream.Write(receiveBuffer, 0, receiveBuffer.Length);
            }
        }
        private void btn_turnoff_Click(object sender, EventArgs e)
        {
            CloseConnection();
        }
        private void CloseConnection()
        {
            // Close Clients' connections
            foreach(var client in Global.Clients)
            {
                client.Value.Close();
            }
            // Abort Clients' threads
            foreach(var clientThread in Global.ClientThreads)
            {
                clientThread.Abort();
            }

            // Close connection to Dictionary server
            if (Global.DictServer != default(TcpClient))
                if (Global.DictServer.Connected)
                    Global.DictServer.Close();

            // Abort thread holding connection to Dictionary server
            if (Global.DictServerThread != default(Thread))
                if (Global.DictServerThread.IsAlive)
                    Global.DictServerThread.Abort();

            // Close Socket listener of Proxy server
            Global.ProxyServer.Stop();

            // Close Proxy Server's thread
            if (Global.ProxyServerThread != default(Thread))
                if (Global.ProxyServerThread.IsAlive)
                    Global.ProxyServerThread.Abort();

            //
            btn_turnon.Enabled = true;
            btn_turnoff.Enabled = false;
        }

        private void FormProxy_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }
    }
}

// Note:
// receiveBuffer must be larger than PKTSZ to be able to carry (hold) definition