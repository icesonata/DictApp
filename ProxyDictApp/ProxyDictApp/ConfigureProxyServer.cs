using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ProxyDictApp
{
    public partial class ConfigureProxyServer : Form
    {
        public ConfigureProxyServer()
        {
            InitializeComponent();
        }

        private void btn_set_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_address.Text) || string.IsNullOrWhiteSpace(txt_address.Text)
                || string.IsNullOrWhiteSpace(txt_port.Text) || string.IsNullOrEmpty(txt_port.Text))
            {
                MessageBox.Show("Address and port must not be null");
                return;
            }
            IPAddress addr;
            int port;
            if (!IPAddress.TryParse(txt_address.Text, out addr))
            {
                MessageBox.Show("Address was wrong");
                return;
            }
            if (!int.TryParse(txt_port.Text, out port))
            {
                MessageBox.Show("Port was wrong");
                return;
            }
            // Test connection on the address we inputed
            TcpListener testAddress = new TcpListener(addr, port);
            try
            {
                testAddress.Start();
                MessageBox.Show("Address can be used");
                testAddress.Stop();
                //
                Global.ProxyServerAddr = addr;
                Global.ProxyServerPort = port;
                Global.ProxyServer = new TcpListener(addr, port);
                if (!string.IsNullOrEmpty(txt_name.Text))
                {
                    Global.SERVER_NAME = txt_name.Text;
                }
                MessageBox.Show("Configuration succeed");
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Your address is not available at the moment.");
            }
        }
    }
}
