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
        public static string recordIndexPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Records\index.txt";
        public static string recordDbPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Records\records.xlsx";
        public static Queue<string> encodedWords = new Queue<string>();
        public static Queue<string> definitions = new Queue<string>();
        public static Thread updateDisplayBox = default(Thread);
    }
}
