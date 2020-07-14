using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LoadBalancer
{
    class Global
    {
        // Packet size convention (byte unit)
        public static int PKTSZ = 10000;
        /// <summary>        
        // Proxy server address
        public static IPAddress LoadBalancerAddress = Dns.GetHostEntry("localhost").AddressList[0];
        public static int LoadBalancerPort = 8080;
        // dictionary server address 0
        public static string DictAddr = "localhost";
        public static int DictPort = 8888;  // must be different from Proxy server's
        // dictionary server address 1
        public static string DictAddr_1 = "localhost";
        public static int DictPort_1 = 9000;
        /// </summary>
        // 
        public static TcpListener LoadBalancerServer = new TcpListener(LoadBalancerAddress, LoadBalancerPort);
        public static Thread LoadBalancerThread = default(Thread);
        // Mapping Client address to its TcpClient holding connection client - proxy at Proxy server
        // Client-Server mapping: TKey = Client | TValue = Server
        public static Dictionary<TcpClient, TcpClient> ClientMapServer = new Dictionary<TcpClient, TcpClient>();
        // Client mapping: TKey = Client's address as a string | TValue = Client's TcpClient object
        public static Dictionary<string, NetworkStream> ClientMap = new Dictionary<string, NetworkStream>();
        //
        public static List<Thread> ClientThreads = new List<Thread>();
        public static Queue<byte[]> DataQueue = new Queue<byte[]>();
        //
        public static TcpClient DictServer = default(TcpClient);
        public static Thread DictServerThread = default(Thread);
        public static NetworkStream DictServerNetStream = default(NetworkStream);
        // Dictionary server 1
        public static TcpClient DictServer_1 = default(TcpClient);
        public static Thread DictServerThread_1 = default(Thread);
        public static NetworkStream DictServerNetStream_1 = default(NetworkStream);
        //
        public static Thread CheckConnections = default(Thread);
    }
}