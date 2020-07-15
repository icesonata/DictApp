using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography;

namespace ClientDictApp
{
    class Global
    {
        // Packet size convention (byte unit)
        public static int PKTSZ = 10000;
        //
        public static string serverIP = "localhost";
        public static int port = 8080;
        public static TcpClient client = default(TcpClient);
        public static Thread clientThread = default(Thread);
        public static NetworkStream stream = default(NetworkStream);
        public static Queue<string> dataQueue = new Queue<string>();
        public static Thread updateDisplayBox = default(Thread);
        // Paths
        public static string recordIndexPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\records\index.txt";    // Contains index of the last element added in database
        public static string recordDbPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\records\records.xlsx";    // Excel file storing Translation history
        // User's credentiality (in this application, just assign it to username which user used to logging in)
        public static string username = string.Empty;
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
