using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace network_programming_midterm
{
    class Data
    {
        public string code = "";
        public string content = "";
        public string timestamp = "";   // 
        // IP or username (Mostly IP)
        public string dest = "";
        public string src = "";
        public Data() { }
        public Data(string code, string content, string timestamp, string destination, string source)
        {
            this.code = code;
            this.content = content;
            this.timestamp = timestamp;
            this.dest = destination;
            this.src = source;
        }
        public Data(string serialized)
        {
            Data deserialized = JsonSerializer.Deserialize<Data>(serialized);
            this.code = deserialized.code;
            this.content = deserialized.content;
            this.timestamp = deserialized.timestamp;
            this.dest = deserialized.dest;
            this.src = deserialized.src;
        }
    }
}
