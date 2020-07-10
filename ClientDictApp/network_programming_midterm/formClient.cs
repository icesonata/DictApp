using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.Json;
using System.Security.Cryptography;

namespace network_programming_midterm
{
    public partial class FormClient : Form
    {
        // Create 'login' form
        FormLogin login_form = new FormLogin();
        public FormClient()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_Close);
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // create new translation history file if it hasn't exist yet
            string workingDir = Environment.CurrentDirectory;
            string path = Directory.GetParent(workingDir).Parent.Parent.FullName + @"\Records\records.xlsx";
            if (!File.Exists(path))
            {
                Excel record = new Excel();
                record.CreateNewFile();
                record.CreateNewSheet();
                record.SaveAs($@"{path}");
                record.Close();
            }
            // establish connection with server
            string serverIP = "localhost";
            int port = 8080;
            try
            {
                // create TcpClient connect to server
                Global.client = new TcpClient(serverIP, port);
                Global.stream = Global.client.GetStream();
                // create Thread to handle network stream of client
                Global.clientThread = new Thread(runClient);
                //Global.dataQueue.Enqueue(txt_encoded.Text.Trim().ToLower());
                Global.clientThread.Start();
            }
            catch (Exception exception)
            {
                //MessageBox.Show("Error happened while connecting to server\nClosing application");
                MessageBox.Show(exception.ToString());
                Application.Exit();
            }
            // pop up Login form after connection to server has been established(could be reconfigured for any purpose afterwards)
            login_form.ShowDialog();
        }
        private void Form1_Close(object sender, EventArgs e)
        {

            if (Global.clientThread != default(Thread))
            {
                if (Global.client.Connected)
                {
                    //Global.stream.Close();
                    Global.client.Close();
                }
                if (Global.clientThread != default(Thread))
                {
                    Global.clientThread.Abort();
                }
            }
        } 
        // "Dịch" button
        private void button1_Click(object sender, EventArgs e)
        {
            // add encoded word from client to queue so as to send and get decoded meaning from server
            string encoded = txt_encoded.Text.Trim().ToLower();
            Data data = new Data(300, encoded);
            string encrypted_data = GetEncrypted(data.GetSerialized());
            Global.dataQueue.Enqueue(encrypted_data);
        }
        private void runClient()
        {
            while (true)
            {
                if(Global.dataQueue.Count != 0)
                {
                    getTranslated(Global.dataQueue.Dequeue());
                }
            }
        }
        
        private void getTranslated(string encoded_text)
        {
            if (Global.client != default(TcpClient))
            {
                if(Global.client.Connected && Global.stream != default(NetworkStream))
                {
                    int byteCount = Encoding.UTF8.GetByteCount(encoded_text);

                    byte[] data = new byte[byteCount];

                    data = Encoding.UTF8.GetBytes(encoded_text);
                    Global.stream.Write(data, 0, data.Length);

                    byte[] receiveBuffer = new byte[10000];
                    while (!Global.stream.DataAvailable)
                    {
                        // waiting for comming data
                    }    
                    Global.stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                    string serialized = Encoding.UTF8.GetString(receiveBuffer);
                    // delete null char
                    serialized = serialized.Replace("\0", string.Empty);
                    // get decrypted data
                    string decrypted_data = GetDecrypted(serialized);
                    ProcessingResponse(encoded_text, decrypted_data);
                    
                    Global.stream.Flush();
                }
            }
        }
        private void ProcessingResponse(string encoded, string serialized)
        {
            Data deserialized = JsonSerializer.Deserialize<Data>(serialized);
            if(deserialized.code == 302)
            {
                // load decoded text to definition box
                txt_decoded.Text = deserialized.content;
                // add encoded and decoded words to user translation history
                addToRecord(encoded, deserialized.content);
            } else if(deserialized.code == 102)
            {
                Global.username = deserialized.content;
                MessageBox.Show($"Login succeed\nWelcome, {Global.username}!");
                login_form.Close();
            } else if(deserialized.code == 202)
            {
                MessageBox.Show("Register succeed\nPlease relogin");
            } else if(deserialized.code == 104)
            {
                MessageBox.Show("Login unsucceed");
            } else if(deserialized.code == 204)
            {
                MessageBox.Show("Register unsucceed");
            }
        }
        static void addToRecord(string encoded, string decoded)
        {
            DateTime date = DateTime.Now;
            // open file index.txt and get indices of the last edited cell in the previous launched

            // check if file records.xlsx is opened by user
            if (!isFileOpen(Global.recordDbPath))
            {
                MessageBox.Show("Please close the file Excel before translating for recording purpose.");
                return;
            }
            // open file excel records.xlsx
            Excel excel = new Excel(Global.recordDbPath, 1);

            int i, j, pos;
            string line, data1, data2;
            data1 = data2 = "";
            i = j = pos = 0;
            var filestream = new FileStream(Global.recordIndexPath, FileMode.Open, FileAccess.Read);
            try
            {
                using (var streamReader = new StreamReader(filestream, Encoding.UTF8))
                {
                    // get indices of the last edited cell in index.txt
                    line = streamReader.ReadLine();
                    pos = line.IndexOf('\t');
                    data1 = line.Substring(0, pos);
                    data2 = line.Substring(pos + 1);

                    streamReader.Close();
                }
                filestream.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error happens while reading translation history file.");
            }


            if (!Int32.TryParse(data1, out i))
            {
                MessageBox.Show("Error happened while converting Excel row index");
            }
            if(!Int32.TryParse(data2, out j))
            {
                MessageBox.Show("Error happened while converting Excel column index");
            }
            // if word was found in dictionary database
            if (decoded != "Not found")
            {
                // write to 'Encoded word' field in excel table
                excel.WriteToCell(i, j, encoded);
                // write to 'Decoded word' field in excel table
                excel.WriteToCell(i, j + 1, decoded);
                // write and save excel file
                excel.Save();

                // update the indices to the last index where we have just updated in file index.txt 
                updateIndex(i + 1, j);
            }
            // close Excel file 
            excel.Close();
        }

        static void updateIndex(int i, int j)
        {
            string workingDir = Environment.CurrentDirectory;
            string path_index = Directory.GetParent(workingDir).Parent.Parent.FullName + @"\Records\index.txt";
            using (StreamWriter writer = new StreamWriter(path_index))
            {
                string line = i.ToString() + '\t' + j.ToString();
                writer.WriteLine(line);
                writer.Close();
            }
        }
        // Check filename is opening
        static bool isFileOpen(string path)
        {
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    stream.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void btn_translation_history_Click(object sender, EventArgs e)
        {
            // Configure translation history feature here
            // Using any element that can simulate data table as Excel performence such as DataGridView or ListView
            // ...
        }
        private string GetEncrypted(string cleartext)
        {
            // Implement encryption here
            // ...
            return cleartext;   // Change return value to whatever you want after implementation
        }
        private string GetDecrypted(string ciphertext)
        {
            // Implement decryption here
            // ...
            return ciphertext;  // Change return value to whatever you want after implementation
        }
    }
}
