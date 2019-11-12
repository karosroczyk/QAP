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
            chart1.Titles.Add("Prawdopodobieństwo wylosowania danego osobnika w metodzie ruletki");

            /*foreach (var elem in tab)
            {
                chart1.Series["Series1"].Points.AddXY(elem.ToString(), elem);
            }*/
            chart1.Series["Series1"].Points.AddXY("Osobnik 1", "373");
            chart1.Series["Series1"].Points.AddXY("Osobnik 2", "254");
            chart1.Series["Series1"].Points.AddXY("Osobnik 3", "156");
            chart1.Series["Series1"].Points.AddXY("Osobnik 4", "202");
            chart1.Series["Series1"].Points.AddXY("Osobnik 5", "10");


        }

        private void Chart1_Click(object sender, EventArgs e)
        {
        }

    }
}
