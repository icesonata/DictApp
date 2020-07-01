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
        public string request = "";
        public string content = "";
        public string timestamp = "";
        public string username = "";
        public Data() { }
        public Data(string request, string content, string timestamp, string username)
        {
            this.request = request;
            this.content = content;
            this.timestamp = timestamp;
            this.username = username;
        }
        public Data(string serialized)
        {
            Data deserialized = JsonSerializer.Deserialize<Data>(serialized);
            this.request = deserialized.request;
            this.content = deserialized.content;
            this.timestamp = deserialized.timestamp;
            this.username = deserialized.username;
        }
    }
}
