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

namespace ClientDictApp
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            bool valid_flag = string.IsNullOrWhiteSpace(txt_username.Text) || string.IsNullOrEmpty(txt_username.Text)
                || string.IsNullOrEmpty(txt_password.Text) || string.IsNullOrWhiteSpace(txt_password.Text)
                || string.IsNullOrEmpty(txt_confirm_password.Text) || string.IsNullOrWhiteSpace(txt_confirm_password.Text);
            if (valid_flag)
            {
                MessageBox.Show("All fields must not be empty.");
            }
            else
            {
                if (txt_password.Text.Equals(txt_confirm_password.Text))
                {
                    SHA512 sha512 = SHA512.Create();
                    byte[] digest = sha512.ComputeHash(Encoding.UTF8.GetBytes(txt_password.Text));
                    Dictionary<string, string> userInfo = new Dictionary<string, string>()
                    {
                        {"username", txt_username.Text },
                        {"password", BitConverter.ToString(digest).Replace("-", string.Empty).ToLower() }
                    };
                    Data data = new Data(200, JsonSerializer.Serialize(userInfo));
                    Global.dataQueue.Enqueue(data.GetSerialized());
                }
                else
                {
                    MessageBox.Show("Password and Confirm password were not matched.");
                }
                // cleaning
                txt_username.Text = string.Empty;
                txt_password.Text = string.Empty;
                txt_confirm_password.Text = string.Empty;
            }
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txt_username;
        }
    }
}
