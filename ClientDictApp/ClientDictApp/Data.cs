using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;

namespace ClientDictApp
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
        // This constructor is for client/server request
        public Data(int code, string content, string dest = null, string src = null) {
            this.code = code;
            this.content = Global.GetEncrypted(content);
            this.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (string.IsNullOrEmpty(dest))
            {
                this.dest = GetRemoteIP();
            }
            else
            {
                this.dest = dest;
            }
            if (string.IsNullOrEmpty(src))
            {
                this.src = GetLocalIP();
            }
            else
            {
                this.src = src;
            }
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
        public static string GetRemoteIP()
        {
            string addr = ((IPEndPoint)Global.client.Client.RemoteEndPoint).Address.MapToIPv4().ToString();
            string port = ((IPEndPoint)Global.client.Client.RemoteEndPoint).Port.ToString();
            return string.Format("{0}:{1}", addr, port);
        }
        public static string GetLocalIP()
        {
            string addr = ((IPEndPoint)Global.client.Client.LocalEndPoint).Address.MapToIPv4().ToString();
            string port = ((IPEndPoint)Global.client.Client.LocalEndPoint).Port.ToString();
            return string.Format("{0}:{1}", addr, port);
        }
        public string GetSerialized()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}