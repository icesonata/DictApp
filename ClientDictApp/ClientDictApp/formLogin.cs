using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Security.Cryptography;

namespace network_programming_midterm
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void label_register_Click(object sender, EventArgs e)
        {
            FormRegister register = new FormRegister();
            register.ShowDialog();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            bool valid_flag = string.IsNullOrWhiteSpace(txt_username.Text) || string.IsNullOrEmpty(txt_username.Text)
                || string.IsNullOrEmpty(txt_password.Text) || string.IsNullOrWhiteSpace(txt_password.Text);
            if (valid_flag)
            {
                MessageBox.Show("All fields must not be empty.");
            }
            else
            {
                SHA512 sha512 = SHA512.Create();
                byte[] digest = sha512.ComputeHash(Encoding.UTF8.GetBytes(txt_password.Text));
                Dictionary<string, string> userInfo = new Dictionary<string, string>()
                {
                    {"username", txt_username.Text },
                    {"password", BitConverter.ToString(digest).Replace("-", string.Empty).ToLower() }
                };
                Data data = new Data(100, JsonSerializer.Serialize(userInfo));
                Global.dataQueue.Enqueue(data.GetSerialized());
                //
                if(!string.IsNullOrEmpty(Global.username))
                    this.Close();
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txt_username;
        }
    }
}
