using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ProxyDictApp
{
    class Global
    {
        // Packet size convention (byte unit)
        public static int PKTSZ = 10000; 
        // Main dictionary server address
        public static string DictAddr_0 = "localhost";
        public static int DictPort_0 = 8888;  // must be different from Proxy server's
        // Proxy server address
        public static IPAddress ProxyServerAddr = Dns.GetHostEntry("localhost").AddressList[0];
        public static int ProxyServerPort = 8080;
        // 
        public static TcpListener ProxyServer = new TcpListener(ProxyServerAddr, ProxyServerPort);
        public static Thread ProxyServerThread = default(Thread);
        // Mapping Client address to its TcpClient holding connection client - proxy at Proxy server
        public static Dictionary<string, TcpClient> Clients = new Dictionary<string, TcpClient>();
        public static List<Thread> ClientThreads = new List<Thread>();
        //public static Queue<byte[]> DataQueue = new Queue<byte[]>();
        public static TcpClient DictServer = default(TcpClient);
        public static Thread DictServerThread = default(Thread);
        public static NetworkStream DictServerNetStream = default(NetworkStream);
    }
}