using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Hotel
{
    public partial class Graphic : UserControl
    {
     
        public decimal y { get; set; }
        public List<double> v = new List<double>();
        public List<string> month = new List<string> { "Янв", "Февр", "Март", "Апр", "Май", "Июнь", "Июль",
                                               "Авг", "Сент", "Окт", "Ноябрь", "Дек" };

        public Graphic()
        {
            InitializeComponent();
            numericUpDown1.Value = DateTime.Now.Year;
            y = numericUpDown1.Value;
        }

       public void AddPoints()
        {
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < 12; i++)
            {
                chart1.Series[0].Points.AddXY(month[i],v[i]);
            }
           
            foreach (var i in chart1.Series[0].Points)
                foreach (var j in i.YValues)
                    if (j != 0)
                        i.IsValueShownAsLabel = true;
            label3.Text = v.Sum().ToString() + " руб.";
        }

        

        public event EventHandler<SumInfoEventArgs> YearChanged;
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
      
            y = numericUpDown1.Value;
            if (YearChanged != null)
            {
                YearChanged.Invoke(this, new SumInfoEventArgs { Year = numericUpDown1.Value });
            }
        }
    }
}
