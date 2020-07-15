using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ServerDictApp
{
    public partial class QueryHistory : Form
    {
        public QueryHistory()
        {
            InitializeComponent();
        }

        private void QueryHistory_Load(object sender, EventArgs e)
        {
            using(FileStream fs = new FileStream(Global.historyQueriesPath, FileMode.Open, FileAccess.Read))
            {
                using(StreamReader sr = new StreamReader(fs))
                {
                    txtBox_query_history.Text = sr.ReadToEnd();
                }
            }
        }
    }
}
