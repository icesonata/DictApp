using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;

namespace network_programming_midterm_2
{
    class Data
    {
        public int code { get; set; }
        public string content { get; set; }
        public string timestamp { get; set; }
        // IP or username (Mostly IP)
        public string dest { get; set; }
        public string src { get; set; }
        public Data() { }
        // Different from Client site, there are multiple clients connect to server.
        // In the other word, "dest" and "src" addresses must be identified clearly
        // This constructor is for client/server request
        public Data(int code, string content, string dest, string src)
        {
            this.code = code;
            this.content = Global.GetEncrypted(content);
            this.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.dest = dest;
            this.src = src;
        }
        // This constructor is for client/server response
        public Data(string serialized)
        {
            Data deserialized = JsonSerializer.Deserialize<Data>(serialized);
            this.code = deserialized.code;
            this.content = Global.GetDecrypted(deserialized.content);
            this.timestamp = deserialized.timestamp;
            this.dest = deserialized.dest;
            this.src = deserialized.src;
        }
        public string GetSerialized()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}