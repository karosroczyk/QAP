using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace matody_wykresy_zapisywanie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //chart1.Titles.Add("Prawdopodobieństwo wylosowania danego osobnika w metodzie ruletki");

            /*foreach (var elem in tab)
            {
                chart1.Series["Series1"].Points.AddXY(elem.ToString(), elem);
            }*/
            chart1.Series["Series1"].Points.AddXY("Osobnik 1", "10");
            chart1.Series["Series1"].Points.AddXY("Osobnik 2", "136");
            chart1.Series["Series1"].Points.AddXY("Osobnik 3", "202");
            chart1.Series["Series1"].Points.AddXY("Osobnik 4", "254");
            chart1.Series["Series1"].Points.AddXY("Osobnik 5", "373");
        }

        private void Chart1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveChart = new SaveFileDialog();
            saveChart.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveChart.Title = "Save";
            saveChart.ShowDialog();
            if (saveChart.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveChart.OpenFile();
                this.chart1.SaveImage(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void SaveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }
    }
}
