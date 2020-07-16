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
    public partial class ConfigureDictServer : Form
    {
        public ConfigureDictServer()
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
            string addr = txt_address.Text;
            int port;
            if (!int.TryParse(txt_port.Text, out port))
            {
                MessageBox.Show("Port was wrong");
                return;
            }
            try
            {
                using(TcpClient testConn = new TcpClient(addr, port))
                {
                    Global.DictServerAddr = addr;
                    Global.DictServerPort = port;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Server is not available at the moment");
                return;
            }
        }
    }
}
