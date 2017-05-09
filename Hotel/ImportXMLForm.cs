using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Hotel
{
    public partial class ImportXMLForm : Form
    {
        public ImportXMLForm()
        {
            InitializeComponent();
        }

        private void ImportXMLForm_Load(object sender, EventArgs e)
        {
            XDocument xmlDoc = XDocument.Load(@"x.xml");
            var res = from c in xmlDoc.Root.Descendants("Проживание")
                         select new
                         {
                             In = c.Element("ДатаЗаселения").Value,
                             Out = c.Element("ДатаВыселения").Value,
                             Price = c.Element("Стоимость").Value,
                             DatePrice = c.Element("ДатаОплаты").Value,
                             Room = c.Element("Номер").Value,
                             Client = c.Element("Клиент").Value,
                             //DateBook = c.Element("ДатаБронирования").Value
                         };

            foreach (var r in res)
            {
                dataGridView1.Rows.Add(new object[] {r.In,r.Out,r.Price,r.DatePrice,r.Room,r.Client});
            }
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
