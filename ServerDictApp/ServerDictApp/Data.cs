using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;

namespace ServerDictApp
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
        public Data(int code, string content, string dest, string src, bool encrypted = true)
        {
            this.code = code;
            if (encrypted)
            {
                this.content = Global.GetEncrypted(content);
            }
            else
            {
                this.content = content;
            }
            this.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.dest = dest;
            this.src = src;
        }
        // This constructor is for client/server response
        public Data(string serialized, bool encrypted = true)
        {
            Data deserialized = JsonSerializer.Deserialize<Data>(serialized);
            this.code = deserialized.code;
            if (encrypted && deserialized.code != 500 && deserialized.code != 400)
            {
                this.content = Global.GetDecrypted(deserialized.content);
            }
            else
            {
                this.content = deserialized.content;
            }
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