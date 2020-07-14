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

namespace LoadBalancer
{
    public partial class LoadBalancer : Form
    {
        string DictServer_0_Address = "";
        string ClientDictServer_0_Address = "";
        string DictServer_1_Address = "";
        string ClientDictServer_1_Address = "";
        public LoadBalancer()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(FormLoadBalancer_Closing);
        }

        private void btn_turnon_Click(object sender, EventArgs e)
        {
            try
            {
                // Start running Load Balancer in a new thread
                Global.LoadBalancerThread = new Thread(LoadBalancerServer);
                Global.LoadBalancerThread.Start();

                ////////////// Start Dictionary server 0
                Global.DictServer = new TcpClient(Global.DictAddr, Global.DictPort);
                Global.DictServerNetStream = Global.DictServer.GetStream();
                // Start Dictionary server 0
                Global.DictServerThread = new Thread(CatchResponse_DictServer);
                //Global.DictServerThread.Start();  // Try not to run this at the first place to avoid machine's resources overconsumption

                ////////////// Start Dictionary server 1
                Global.DictServer_1 = new TcpClient(Global.DictAddr_1, Global.DictPort_1);
                Global.DictServerNetStream_1 = Global.DictServer_1.GetStream();
                // Start Dictionary server 1
                Global.DictServerThread_1 = new Thread(CatchResponse_DictServer_1);
                //Global.DictServerThread_1.Start();    // Try not to run this at the first place to avoid machine's resources overconsumption

                ////////////// Start running Check connections to clients
                Global.CheckConnections = new Thread(CheckClientConnections);
                Global.CheckConnections.Start();

                ////////////// Get addresses
                DictServer_0_Address = ((IPEndPoint)Global.DictServer.Client.RemoteEndPoint).Address.MapToIPv4().ToString() + ":" + ((IPEndPoint)Global.DictServer.Client.RemoteEndPoint).Port.ToString();
                DictServer_1_Address = ((IPEndPoint)Global.DictServer_1.Client.RemoteEndPoint).Address.MapToIPv4().ToString() + ":" + ((IPEndPoint)Global.DictServer_1.Client.RemoteEndPoint).Port.ToString();
                ClientDictServer_0_Address = ((IPEndPoint)Global.DictServer.Client.LocalEndPoint).Address.MapToIPv4().ToString() + ":" + ((IPEndPoint)Global.DictServer.Client.LocalEndPoint).Port.ToString(); ;
                ClientDictServer_1_Address = ((IPEndPoint)Global.DictServer.Client.LocalEndPoint).Address.MapToIPv4().ToString() + ":" + ((IPEndPoint)Global.DictServer.Client.LocalEndPoint).Port.ToString(); ;

                //////////////
                txtBox_query_log.Text += "Load Balancer is running...\n";
                btn_turnoff.Enabled = true;
                btn_turnon.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LoadBalancerServer()
        {
            try
            {
                Global.LoadBalancerServer.Start();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            while (true)
            {
                try
                {
                    if (Global.LoadBalancerServer.Pending())
                    {
                        // Accept client
                        var client = Global.LoadBalancerServer.AcceptTcpClient();
                        string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.MapToIPv4().ToString();
                        string clientPort = ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString();
                        string clientAddr = string.Format("{0}:{1}", clientIP, clientPort);

                        // Init
                        string serialized = "";
                        byte[] dataSend;
                        byte[] receiveBuffer = new byte[10000];
                        Data data;

                        //////////////////// Ask Dictionary server 0
                        data = new Data(
                            code: 400,
                            content: "",
                            dest: DictServer_0_Address,
                            src: ClientDictServer_0_Address
                            );
                        serialized = data.GetSerialized();
                        dataSend = Encoding.UTF8.GetBytes(serialized);
                        Global.DictServerNetStream.Write(dataSend, 0, dataSend.Length);
                        // Waiting for response from Dictionary server 0
                        while (!Global.DictServerNetStream.DataAvailable) { }
                        Global.DictServerNetStream.Read(receiveBuffer, 0, receiveBuffer.Length);

                        //
                        serialized = Encoding.UTF8.GetString(receiveBuffer);
                        serialized = serialized.Replace("\0", "");

                        if (ServerCanHandle(serialized))
                        {
                            Global.ClientMapServer.Add(client, Global.DictServer);
                            Global.ClientMap.Add(clientAddr, client.GetStream());
                            Thread clientThread = new Thread(() => ClientThread(client, client.GetStream()));
                            Global.ClientThreads.Add(clientThread);
                            clientThread.Start();
                            // Go to the next loop
                            //MessageBox.Show("Server 0");
                            if(Global.DictServerThread.ThreadState != ThreadState.Running)
                            {
                                Global.DictServerThread.Start();
                            }
                            continue;
                        }

                        //////////////////// Ask Dictionary server 1
                        data = new Data(
                                code: 400,
                                content: "",
                                dest: DictServer_1_Address,
                                src: ClientDictServer_1_Address
                                );
                        serialized = data.GetSerialized();
                        dataSend = Encoding.UTF8.GetBytes(serialized);
                        Global.DictServerNetStream_1.Write(dataSend, 0, dataSend.Length);
                        // Waiting for response from Dictionary server 0
                        while (!Global.DictServerNetStream_1.DataAvailable) { }
                        Global.DictServerNetStream_1.Read(receiveBuffer, 0, receiveBuffer.Length);

                        //
                        serialized = Encoding.UTF8.GetString(receiveBuffer);
                        serialized = serialized.Replace("\0", "");
                        if (ServerCanHandle(serialized))
                        {
                            Global.ClientMapServer.Add(client, Global.DictServer_1);
                            Global.ClientMap.Add(clientAddr, client.GetStream());
                            Thread clientThread = new Thread(() => ClientThread(client, client.GetStream()));
                            Global.ClientThreads.Add(clientThread);
                            clientThread.Start();
                            // Go to the next loop
                            //MessageBox.Show("Server 1");
                            if (Global.DictServerThread_1.ThreadState != ThreadState.Running)
                            {
                                Global.DictServerThread_1.Start();
                            }
                            continue;
                        }

                        //////////////////// Both server are overloaded at the moment, tell the client to be patient and come back later
                        data = new Data(
                            code: 404,
                            content: "",
                            dest: clientAddr,
                            src: ((IPEndPoint)client.Client.LocalEndPoint).Address.MapToIPv4().ToString() + ":" + ((IPEndPoint)client.Client.LocalEndPoint).Port.ToString()
                            );
                        serialized = data.GetSerialized();
                        dataSend = Encoding.UTF8.GetBytes(serialized);
                        NetworkStream stream = client.GetStream();
                        stream.Write(dataSend, 0, dataSend.Length);
                        // Close connection if connection was failed to established
                        client.Close();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private bool ServerCanHandle(string serialized)
        {
            // Processing response from server which indicates if server can handle new client
            Data deserialized = new Data(serialized);
            if (deserialized.code == 404)
            {
                return false;
            }
            else if (deserialized.code == 402)
            {
                return true;
            }
            MessageBox.Show("Error happened\nCan't handle new client.");
            return false;
        }
        private void ClientThread(TcpClient client, NetworkStream stream)
        {
            while (true)
            {
                try
                {
                    while (!stream.DataAvailable) { }
                    byte[] receiveBuffer = new byte[Global.PKTSZ];
                    stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                    //
                    //MessageBox.Show("From client: "+Encoding.UTF8.GetString(receiveBuffer).Replace("\0", ""));
                    
                    // Get corresponding server's network stream and redirect client's packet to server
                    NetworkStream svStream = Global.ClientMapServer[client].GetStream();
                    svStream.Write(receiveBuffer, 0, receiveBuffer.Length);
                    //svStream.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void CatchResponse_DictServer()
        {
            while (true)
            {
                try
                {
                    while (!Global.DictServerNetStream.DataAvailable) { }
                    byte[] receiveBuffer = new byte[Global.PKTSZ];
                    Global.DictServerNetStream.Read(receiveBuffer, 0, receiveBuffer.Length);
                    //MessageBox.Show("From server 0: " + Encoding.UTF8.GetString(receiveBuffer).Replace("\0", ""));
                    string serialized = Encoding.UTF8.GetString(receiveBuffer);
                    // Get rid of null characters
                    serialized = serialized.Replace("\0", string.Empty);
                    //
                    Data data = new Data(serialized);
                    // Ignore some cases
                    if (data.code == 502 || data.code == 402 || data.dest.Equals(ClientDictServer_0_Address) || data.dest.Equals(ClientDictServer_1_Address))
                        continue;
                    // Get network stream corresponding to client's address in "dest" field of the response packet
                    NetworkStream clStream = Global.ClientMap[data.dest];
                    // Redirect server's response packet to client via network stream we got above
                    clStream.Write(receiveBuffer, 0, receiveBuffer.Length);
                    //
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void CatchResponse_DictServer_1()
        {
            while (true)
            {
                try
                {
                    while (!Global.DictServerNetStream_1.DataAvailable) { }
                    byte[] receiveBuffer = new byte[Global.PKTSZ];
                    Global.DictServerNetStream_1.Read(receiveBuffer, 0, receiveBuffer.Length);
                    
                    //
                    //MessageBox.Show("From server 1: " + Encoding.UTF8.GetString(receiveBuffer).Replace("\0", ""));
                    
                    string serialized = Encoding.UTF8.GetString(receiveBuffer);
                    // Get rid of null characters
                    serialized = serialized.Replace("\0", string.Empty);
                    //
                    Data data = new Data(serialized);
                    // Ignore some cases
                    if (data.code == 502 || data.code == 402 || data.dest.Equals(ClientDictServer_0_Address) || data.dest.Equals(ClientDictServer_1_Address))
                        continue;
                    // Get network stream corresponding to client's address in "dest" field of the response packet
                    NetworkStream clStream = Global.ClientMap[data.dest];
                    // Redirect server's response packet to client via network stream we got above
                    clStream.Write(receiveBuffer, 0, receiveBuffer.Length);
                    //
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        // A thread check for client's corrupted connection to announce server
        private void CheckClientConnections()   
        {
            // Check all connection to client every second
            // If any connection is corrupted, announce server
            while (true)
            {
                foreach(var cl_map_sv in Global.ClientMapServer)
                {
                    // If connection to client is no longer available
                    if (!cl_map_sv.Key.Connected)
                    {
                        // Remind that Key is Client's TcpClient and Value is Dictionary Server's
                        string serverAddress = string.Format("{0}:{1}",
                            ((IPEndPoint)cl_map_sv.Value.Client.RemoteEndPoint).Address.MapToIPv4().ToString(),
                            ((IPEndPoint)cl_map_sv.Value.Client.RemoteEndPoint).Port.ToString()
                            );
                        string loadBalancerAddress = string.Format("{0}:{1}",
                            ((IPEndPoint)cl_map_sv.Value.Client.LocalEndPoint).Address.MapToIPv4().ToString(),
                            ((IPEndPoint)cl_map_sv.Value.Client.LocalEndPoint).Port.ToString()
                            );
                        Data data = new Data(code: 500, content: "", dest: serverAddress, src: loadBalancerAddress);
                        string serialized = data.GetSerialized();
                        byte[] dataSend = Encoding.UTF8.GetBytes(serialized);
                        // Send announcement to server
                        NetworkStream serverStream = cl_map_sv.Value.GetStream();
                        serverStream.Write(dataSend, 0, dataSend.Length);

                        // Close network stream we has just created after sending announcement to server
                        serverStream.Close();
                        // Remove Client which has corrupted connection from Load Balancer's mapping table
                        Global.ClientMapServer.Remove(cl_map_sv.Key);
                    }
                }
                Thread.Sleep(1000);
            }
        }
        private void btn_turnoff_Click(object sender, EventArgs e)
        {
            CloseConnection();
        }
        private void CloseConnection()
        {
            try
            {
                // Close Clients' connections at Client-Server mapping will also end up closing at ClientMap
                foreach (var client in Global.ClientMapServer)
                {
                    client.Key.Close();
                }
                // Abort Clients' threads
                foreach (var clientThread in Global.ClientThreads)
                {
                    clientThread.Abort();
                }

                // Close connection to Dictionary server 0
                if (Global.DictServer != default(TcpClient))
                    if (Global.DictServer.Connected)
                        Global.DictServer.Close();

                // Abort thread holding connection to Dictionary server
                if (Global.DictServerThread != default(Thread))
                    if (Global.DictServerThread.IsAlive)
                        Global.DictServerThread.Abort();

                // Close connection to Dictionary server 1
                if (Global.DictServer_1 != default(TcpClient))
                    if (Global.DictServer_1.Connected)
                        Global.DictServer_1.Close();

                // Abort thread holding connection to Dictionary server 1
                if (Global.DictServerThread_1 != default(Thread))
                    if (Global.DictServerThread_1.IsAlive)
                        Global.DictServerThread_1.Abort();

                // Close Load Balancer Listener 
                Global.LoadBalancerServer.Stop();

                // Close Load Balancer Server's thread
                if (Global.LoadBalancerThread != default(Thread))
                    if (Global.LoadBalancerThread.IsAlive)
                        Global.LoadBalancerThread.Abort();

                // Close CheckConnections thread
                if (Global.CheckConnections != default(Thread))
                    if (Global.CheckConnections.IsAlive)
                        Global.CheckConnections.Abort();

                //
                btn_turnon.Enabled = true;
                btn_turnoff.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadBalancer_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            btn_turnoff.Enabled = false;
            btn_turnon.Enabled = true;
        }
        private void FormLoadBalancer_Closing(object sender, EventArgs e)
        {
            CloseConnection();
        }
    }
}

// Load Balancer doesn't have encryption feature.
// We don't need to close NetworkStream because Closing TcpClient will handle the rest network stream for us.