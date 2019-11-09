using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wukresymetodselekcji
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.Titles.Add("Prawdopodobieństwo wylosowania danego osobnika w metodzie rankingowej");

            /*foreach (var elem in tab)
            {
                chart1.Series["Series1"].Points.AddXY(elem.ToString(), elem);
            }*/
            chart1.Series["Series1"].Points.AddXY("Osobnik 1", "1");
            chart1.Series["Series1"].Points.AddXY("Osobnik 2", "2");
            chart1.Series["Series1"].Points.AddXY("Osobnik 3", "3");
            chart1.Series["Series1"].Points.AddXY("Osobnik 4", "4");
            chart1.Series["Series1"].Points.AddXY("Osobnik 5", "5");


        }

        private void Chart1_Click(object sender, EventArgs e)
        {
        }
    }
}
