using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;


namespace kolejnaprobachartu
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
            string filePath = @"C:\Users\Lenovo\Desktop\Inżynierka\Testy\wyniki.csv";
            var csv = new StringBuilder();
            var newline = "";

            chart1.Text = "Przebieg funkcji celu przy użyciu różnych metod mutacji";
            chart1.Titles.Add("Przebieg funkcji celu");
            //chart1.ChartAreas[0].AxisY.Minimum = 1650;
            chart1.ChartAreas[0].AxisX.Maximum = 30000;
            int k = 1;

            for (int j = 0; j < 20; j++)
            {
                for (int i = 0; i < 1; i++)
                {
                    final.Koncowa(i + 1, 2);
                    List<int> toChart_ = Final.toChart;
                    /*foreach (int elem in toChart_)
                    {
                        chart1.Series[i].Points.AddXY(k,elem);
                        //chart1.Series[0].IsValueShownAsLabel = true;
                        k++;
                    }*/
                    Final.RemoveAllElems();
                    k = 1;
                }
                Final.final +=  '\n';
                newline = string.Format("{0}", Final.final);
            }

            csv.AppendLine(newline);
            File.WriteAllText(filePath, csv.ToString());
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

        private void Prsztcisk_Click(object sender, EventArgs e)
        {

        }

        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
}
