using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Security.Cryptography;

namespace ServerDictApp_1
{
    class Global
    {
        // Packet size convention (byte unit)
        public static int PKTSZ = 10000;
        //
        public static IPAddress DictServerAddr_1 = Dns.GetHostEntry("localhost").AddressList[0];
        public static int DictServerPort_1 = 9000;        // Comment this if you don't want to use neither Proxy nor Load Balancing
        //public static int DictServerPort_1 = 8080;      // Uncomment this if you don't want to use neither Proxy nor Load Balancing
        public static TcpListener server = new TcpListener(DictServerAddr_1, DictServerPort_1);
        //public static List<TcpListener> servers = new List<TcpListener>();    // multiple server  (feature locked)
        //public static TcpClient client = default(TcpClient);                  // handle only one client (feature locked)
        //public static NetworkStream stream = default(NetworkStream);
        public static List<TcpClient> clients = new List<TcpClient>();
        public static List<NetworkStream> streams = new List<NetworkStream>();
        public static List<Thread> clientThreads = new List<Thread>();
        public static Queue<string> queries_history = new Queue<string>();
        public static Thread serverThread = default(Thread);
        public static Thread updateHistory = default(Thread);
        // Paths
        public static string historyQueriesPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\history\" + "history.txt";
        public static string dictIndexPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\dict\" + "anhviet109K.index";
        public static string dictDbPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\dict\" + "anhviet109K.dict";
        // Load balancer feature section
        public static int Capacity = 0;
        public static int MaxCapacity = 2;
        // Cryptography section
        public static TripleDESCryptoServiceProvider tripleDESCrypto = default(TripleDESCryptoServiceProvider);
        public static string GetEncrypted(string cleartext)
        {
            Global.tripleDESCrypto = new TripleDESCryptoServiceProvider();
            byte[] byte_cleartext = Encoding.UTF8.GetBytes(cleartext);
            // 16 bytes key length
            Global.tripleDESCrypto.Key = Encoding.UTF8.GetBytes("Big3TeamProNT106");
            Global.tripleDESCrypto.Mode = CipherMode.ECB;
            string ciphertext = Convert.ToBase64String(
                Global.tripleDESCrypto.CreateEncryptor().TransformFinalBlock(
                    byte_cleartext, 0, byte_cleartext.Length));
            return ciphertext;
        }
        public static string GetDecrypted(string ciphertext)
        {
            Global.tripleDESCrypto = new TripleDESCryptoServiceProvider();
            byte[] byte_ciphertext = Convert.FromBase64String(ciphertext);
            // 16 bytes key length
            Global.tripleDESCrypto.Key = Encoding.UTF8.GetBytes("Big3TeamProNT106");
            Global.tripleDESCrypto.Mode = CipherMode.ECB;
            string cleartext = Encoding.UTF8.GetString(
                Global.tripleDESCrypto.CreateDecryptor().TransformFinalBlock(
                    byte_ciphertext, 0, byte_ciphertext.Length));
            return cleartext;
        }
    }
}
