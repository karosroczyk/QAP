using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mutacje_charty
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Final final = new Final();
        private void Form1_Load(object sender, EventArgs e)
        {
             chart1.Text = "Przebieg funkcji celu przy użyciu różnych metod selekcji";
             chart1.Titles.Add("Przebieg funkcji celu");
             chart1.ChartAreas[0].AxisY.Minimum = 5000;
             chart1.ChartAreas[0].AxisX.Maximum = 200;
             int k = 1;

             for (int i = 0; i < 8; i++)
             {
                 final.Koncowa(i + 1);
                 List<int> toChart_ = Final.toChart;
                 foreach (int elem in toChart_)
                 {
                     chart1.Series[i].Points.AddXY(k, elem);
                     //chart1.Series[0].IsValueShownAsLabel = true;
                     k++;
                 }
                 Final.RemoveAllElems();
                 k = 1;
             }
             MessageBox.Show(Final.final);
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
    }
}
