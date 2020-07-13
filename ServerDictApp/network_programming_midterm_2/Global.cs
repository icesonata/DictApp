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

namespace network_programming_midterm_2
{
    class Global
    {
        public static IPAddress DictServerAddr = Dns.GetHostEntry("localhost").AddressList[0];
        public static int DictServerPort = 8888;
        public static TcpListener server = new TcpListener(DictServerAddr, DictServerPort);
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
    }
}
