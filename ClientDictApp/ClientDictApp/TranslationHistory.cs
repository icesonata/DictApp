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

namespace ClientDictApp
{
    public partial class TranslationHistory : Form
    {
        public TranslationHistory()
        {
            InitializeComponent();
        }

        private void TranslationHistory_Load(object sender, EventArgs e)
        {
            // open file excel records.xlsx
            Excel excel = new Excel(Global.recordDbPath, 1);

            int i, j, pos;
            string line, data1, data2;
            data1 = data2 = "";
            i = j = pos = 0;
            // open file index.txt and get indices of the last edited cell in the previous launched
            var filestream = new FileStream(Global.recordIndexPath, FileMode.Open, FileAccess.Read);
            try
            {
                using (var streamReader = new StreamReader(filestream, Encoding.UTF8))
                {
                    // get indices of the last edited cell in index.txt
                    line = streamReader.ReadLine();
                    pos = line.IndexOf('\t');
                    data1 = line.Substring(0, pos);
                    data2 = line.Substring(pos + 1);

                    streamReader.Close();
                }
                filestream.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error happens while reading translation history file.");
            }
            if (!Int32.TryParse(data1, out i))
            {
                MessageBox.Show("Error happened while converting Excel row index");
            }
            if (!Int32.TryParse(data2, out j))
            {
                MessageBox.Show("Error happened while converting Excel column index");
            }
            //
            for (int ii = 1; ii < i; ++ii)
            {

                if (excel.ReadCell(ii, j+2) == Global.username)
                {
                    ListViewItem listitem = new ListViewItem(excel.ReadCell(ii, j));
                    using (StringReader sr = new StringReader(excel.ReadCell(ii, j + 1)))
                    {
                        string li;
                        int jj = 0;
                        while ((li = sr.ReadLine()) != null)
                        {
                            if (jj == 0 && li == "Not found") { listitem.SubItems.Add(li); break; }
                            if (jj == 2)
                                listitem.SubItems.Add(li);
                            jj++;
                        }
                    }

                    translation_log.Items.Add(listitem);
                }
            }
            // close Excel file 
            excel.Close();
        }
    }
}
