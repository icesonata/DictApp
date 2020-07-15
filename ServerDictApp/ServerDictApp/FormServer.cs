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
using System.Security.Cryptography;

namespace network_programming_midterm_2
{
    public partial class FormServer : Form
    {
        public FormServer()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_Close);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            btn_shutdown.Enabled = false;
        }

        private void Form1_Close(object sender, EventArgs e)
        {
            closeConnections();
        }
        
        static int getDecimalValue(string data)
        {
            string base64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            int decValue = 0;
            int pos = 0;
            for (int i = 0; i < data.Length; i++)
            {
                pos = base64.IndexOf(data[i]);
                decValue += pos * Convert.ToInt32(Math.Pow(64, data.Length - i - 1));
            }
            return decValue;
        }

        static string getMean(int offset, int length)
        {
            try
            {
                // open dict file
                using (BinaryReader fs = new BinaryReader(new FileStream(Global.dictDbPath, FileMode.Open, FileAccess.Read)))
                {
                    byte[] b = new byte[length];
                    fs.BaseStream.Seek(offset, SeekOrigin.Begin);

                    fs.Read(b, 0, length);

                    string s = Encoding.UTF8.GetString(b);
                    fs.Close();
                    return s;
                }
            }
            catch (Exception)
            {
                return "Error happens while reading dictionary database";
            }
        }

        static string getTranslated(string search)
        {
            //preprocess
            search = search.Trim();
            // pre-init
            int pos = 0;
            string line, word, data, data1, data2;
            line = word = data = data1 = data2 = "";
            // open index file
            var filestream = new FileStream(Global.dictIndexPath, FileMode.Open, FileAccess.Read);
            try
            {
                using (var streamReader = new StreamReader(filestream, Encoding.UTF8))
                {
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        pos = line.IndexOf('\t');
                        word = line.Substring(0, pos);
                        data = line.Substring(pos + 1);

                        // word found matched word searched
                        if (search == word)
                        {
                            pos = data.IndexOf('\t');   // vị trí của kí tự tab thứ 2
                            data1 = data.Substring(0, pos);
                            data2 = data.Substring(pos + 1);
                            break;
                        }
                    }
                    streamReader.Close();
                }
                filestream.Close();
            }
                catch (Exception)
                {
                    Console.WriteLine("Error happens while reading file.");
                    return "Error";
                }
            // if the text is not in the database, error message will pop up
            if (search != word)
                return "Not found";

            string mean = getMean(getDecimalValue(data1), getDecimalValue(data2));

            return mean;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            try
            {
                // create and start serverThread
                Global.serverThread = new Thread(runServer);
                Global.serverThread.Start();
                // create and start updateHistory
                Global.updateHistory = new Thread(updateQueryHistory);
                Global.updateHistory.Start();
                //
                box_queries.Text += "Server started...\n";
                btn_start.Enabled = false;
                btn_shutdown.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        // doing listening to all new clients
        private void runServer()
        {
            try
            {
                Global.server.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            while (true)
            {
                try
                {
                    // check if there is any connection from client has status pending 
                    if (Global.server.Pending())
                    {
                        var client = Global.server.AcceptTcpClient();

                        //
                        Global.clients.Add(client);

                        NetworkStream stream = client.GetStream();
                        //
                        Global.streams.Add(stream);

                        //
                        Thread clientThread = new Thread(() => openClientStream(client, stream));
                        Global.clientThreads.Add(clientThread);
                        clientThread.Start();
                    }
                }
                catch (Exception)
                {
                    // do nothing
                    break;
                }

            }
        }
        // listen to coming data from client
        private void openClientStream(TcpClient client, NetworkStream stream)
        {
            while (true)
            {
                try
                {
                    // receive data and response from server
                    byte[] receiveBuffer = new byte[Global.PKTSZ];
                    while (!stream.DataAvailable && client.Connected)
                    {
                        // waiting and do nothing
                    }
                    stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                    // Get serialized data encrypted by Client
                    string serialized = Encoding.UTF8.GetString(receiveBuffer);
                    serialized = serialized.Replace("\0", string.Empty);

                    Data deserialized = new Data(serialized);

                    // string result = getTranslated(serialized);
                    string response = GetResponse(serialized).GetSerialized();

                    int byteCount = Encoding.UTF8.GetByteCount(response);
                    byte[] data = new byte[byteCount];
                    data = Encoding.UTF8.GetBytes(response);

                    stream.Write(data, 0, data.Length);

                    // store connection of client to history file   //////////////
                    string infoClient = DateTime.Now.ToString() + " " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.MapToIPv4().ToString() + ":" + ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString() + "; Sent: " + deserialized.content;
                    StreamWriter sw = File.AppendText(Global.historyQueriesPath);
                    // Only show to query text box if request code is 300 indicates translation request.
                    if(deserialized.code == 300)
                        Global.queries_history.Enqueue(infoClient + "\n");
                    sw.WriteLine(infoClient);
                    sw.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SERVER: " + ex.ToString());
                    //MessageBox.Show("Error happen while creating connection and network stream.");
                    return;
                }
            }
        }
        private Data GetResponse(string serialized)
        {
            Data deserialized = new Data(serialized);
            if (deserialized.code == 300)
            {
                return new Data(code: 302, content: getTranslated(deserialized.content), dest: deserialized.src, src: deserialized.dest);
            }
            else if (deserialized.code == 100)
            {
                Dictionary<string, string> UserInfo = JsonSerializer.Deserialize<Dictionary<string, string>>(deserialized.content);
                if (DB.Authentication(UserInfo["username"], UserInfo["password"]))
                {
                    return new Data(code: 102, content: UserInfo["username"], dest: deserialized.src, src: deserialized.dest);
                }
                return new Data(code: 104, content: UserInfo["username"], dest: deserialized.src, src: deserialized.dest);
            }
            else if (deserialized.code == 200)
            {
                Dictionary<string, string> UserInfo = JsonSerializer.Deserialize<Dictionary<string, string>>(deserialized.content);
                if (DB.Register(UserInfo["username"], UserInfo["password"]))
                {
                    return new Data(code: 202, content: UserInfo["username"], dest: deserialized.src, src: deserialized.dest);
                }
                return new Data(code: 204, content: UserInfo["username"], dest: deserialized.src, src: deserialized.dest);
            }
            else if (deserialized.code == 400)
            {
                if (Global.Capacity >= Global.MaxCapacity)
                    return new Data(code: 404, content: "", dest: deserialized.src, src: deserialized.dest, encrypted: false);
                Global.Capacity++;
                return new Data(code: 402, content: "", dest: deserialized.src, src: deserialized.dest, encrypted: false);
            }
            else if (deserialized.code == 500)
            {
                Global.Capacity--;
                return new Data(code: 502, content: "", dest: deserialized.src, src: deserialized.dest, encrypted: false);
            }
            MessageBox.Show("Error happened while processing user's request");
            return null;
        }
        private void updateQueryHistory()
        {
            // automatically update queries history from clients if there is any
            while (true)
            {
                if (Global.queries_history.Count() != 0)
                {
                    box_queries.Text += Global.queries_history.Dequeue();
                }
            }
        }

        private void box_queries_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_shutdown_Click(object sender, EventArgs e)
        {
            closeConnections();
        }

        private void closeConnections()
        {
            try
            {
                foreach (var client in Global.clients)
                {
                    if (client.Connected)
                    {
                        client.Close();
                    }
                }
                foreach (var clientThread in Global.clientThreads)
                {
                    if (clientThread.IsAlive)
                    {
                        clientThread.Abort();
                    }
                }
                // stop server from listening
                Global.server.Stop();

                // abort 2 thread that run serverThread and updateHistory then
                if (Global.serverThread != default(Thread))
                {
                    Global.serverThread.Abort();
                    Global.updateHistory.Abort();
                    // be careful Abort() method of Thread is risky and take a lot of your machine's resources
                }

                // enable "Start" button and disable "Shut down" button
                btn_start.Enabled = true;
                btn_shutdown.Enabled = false;

                // Reset capacity
                Global.Capacity = 0;
            }
            catch (Exception)
            {
                // do nothing
            }
        }
        private void btn_query_history_Click(object sender, EventArgs e)
        {
            QueryHistory quehis_form = new QueryHistory();
            quehis_form.Show();
        }
    }
}

// Note
// receiveBuffer must be smaller than PKTSZ bytes to be able to carry (hold) definition