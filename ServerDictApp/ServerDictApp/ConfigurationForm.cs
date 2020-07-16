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

namespace ServerDictApp
{
    public partial class ConfigurationForm : Form
    {
        public ConfigurationForm()
        {
            InitializeComponent();
        }

        private void btn_set_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_address.Text) || string.IsNullOrWhiteSpace(txt_address.Text)
                || string.IsNullOrWhiteSpace(txt_port.Text) || string.IsNullOrEmpty(txt_port.Text))
            {
                MessageBox.Show("Address and port must not be null");
                return;
            }
            IPAddress addr = default(IPAddress);
            int port=0, max_capacity=0;
            if(!IPAddress.TryParse(txt_address.Text, out addr))
            {
                MessageBox.Show("Address was wrong");
                return;
            }
            if(!int.TryParse(txt_port.Text, out port))
            {
                MessageBox.Show("Port was wrong");
                return;
            }
            if(!int.TryParse(txt_capacity.Text, out max_capacity))
            {
                MessageBox.Show("Bad capacity input");
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
                Global.DictServerAddr = addr;
                Global.DictServerPort = port;
                Global.server = new TcpListener(addr, port);
                //
                MessageBox.Show("Configuration succeed");
                if (!string.IsNullOrEmpty(txt_name.Text))
                {
                    Global.SERVER_NAME = txt_name.Text;
                }
                if (Global.MaxCapacity <= 0)
                    Global.MaxCapacity = 2;
                Global.MaxCapacity = max_capacity;
                this.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Your address is not available at the moment.");
            }
        }
    }
}
