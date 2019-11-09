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
            chart1.Text = "Przebieg funkcji celu przy użyciu różnych metod selekcji";
            chart1.Titles.Add("Przebieg funkcji celu");
            chart1.ChartAreas[0].AxisY.Minimum = 1600;
            chart1.ChartAreas[0].AxisX.Maximum = 20000;
            int k = 1;

            for (int i = 0; i < 5; i++)
            {
                final.Koncowa(i+1);
                List<int> toChart_ = Final.toChart;
                foreach (int elem in toChart_)
                {
                    chart1.Series[i].Points.AddXY(k,elem);
                    //chart1.Series[0].IsValueShownAsLabel = true;
                    k++;
                }
                Final.RemoveAllElems();
                k = 1;
            }
        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void Prsztcisk_Click(object sender, EventArgs e)
        {

        }
    }
}
