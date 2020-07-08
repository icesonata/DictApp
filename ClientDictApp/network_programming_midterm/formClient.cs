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

namespace network_programming_midterm
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_Close);
            CheckForIllegalCrossThreadCalls = false;

            // pop up Login form at the beginning (could be reconfigured for any purpose afterwards)
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
        private void button1_Click(object sender, EventArgs e)
        {
            if (Global.clientThread == default(Thread))
            {
                string serverIP = "localhost";
                int port = 8080;
                try
                {
                    // create TcpClient connect to server
                    Global.client = new TcpClient(serverIP, port);
                    Global.stream = Global.client.GetStream();
                    // create Thread to handle network stream of client
                    Global.clientThread = new Thread(runClient);
                    Global.encodedWords.Enqueue(txt_encoded.Text.Trim().ToLower());
                    Global.clientThread.Start();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error happened in connecting to server.");
                    //MessageBox.Show(exception.ToString());
                }

            }
            else
            {
                Global.encodedWords.Enqueue(txt_encoded.Text.Trim().ToLower());
            }
        }
        private void runClient()
        {
            while (true)
            {
                if(Global.encodedWords.Count != 0)
                {
                    getTranslated(Global.encodedWords.Dequeue());
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
                    //NetworkStream stream = Global.client.GetStream();
                    Global.stream.Write(data, 0, data.Length);

                    byte[] receiveBuffer = new byte[10000];
                    while (!Global.stream.DataAvailable)
                    {
                        // waiting for comming data
                    }    
                    Global.stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                    string decoded_text = Encoding.UTF8.GetString(receiveBuffer);
                    // delete null char
                    decoded_text = decoded_text.Replace("\0", string.Empty);
                    // load decoded text to definition box
                    txt_decoded.Text = decoded_text;

                    addToRecord(encoded_text, decoded_text);
                    
                    Global.stream.Flush();
                }

            }
        }

        static void addToRecord(string encoded, string decoded)
        {
            DateTime date = DateTime.Now;
            // open file index.txt and get indices of the last edited cell in the previous launched

            // check if file records.xlsx is opened by user
            if (!isFileOpen(Global.recordDbPath))
            {
                MessageBox.Show("Please close file Excel before translating for recording purpose.");
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
                MessageBox.Show("Error happens in reading file.");
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

        }
    }
}
