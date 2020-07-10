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

namespace network_programming_midterm
{
    class Global
    {
        public static TcpClient client = default(TcpClient);
        public static Thread clientThread = default(Thread);
        public static NetworkStream stream = default(NetworkStream);
        public static Queue<string> dataQueue = new Queue<string>();
        public static Thread updateDisplayBox = default(Thread);
        // Paths
        public static string recordIndexPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Records\index.txt";    // Contains index of the last element added in database
        public static string recordDbPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Records\records.xlsx";    // Excel file storing Translation history
        // User's credentiality (in this application, just assign it to username which user used to logging in)
        public static string username = string.Empty;
    }
}
