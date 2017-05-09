using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary1;

namespace Hotel
{
    public partial class RoomsForm : Form
    {
        ErrorProvider er = new ErrorProvider();
        ГостиницаContext context = new ГостиницаContext();
        public RoomsForm()
        {
            InitializeComponent();
            AddRooms();
            foreach (var c in context.Категория)
            {
                comboBoxCategory.Items.Add(c.Категория1);
                comboBoxCategory2.Items.Add(c.Категория1);
            }
        }


        private void AddRooms ()
        {
            var rooms = context.Номера.Join(context.Категория, n => n.КодКатегории, c => c.КодКатегории, (n, c) => new
            {
                Room = n.НомерКомнаты,
                Cat = c.Категория1,
                Quant = c.КолМест,
                Price = c.Цена
            });

            foreach (var r in rooms)
                dataGridRooms.Rows.Add(new object[] { r.Room, r.Cat, r.Quant, r.Price });
        }
        private void butOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void butAddRoom_Click(object sender, EventArgs e)
        {
            if (textBoxRoom.Text != "" && comboBoxCategory.Text != "")
            {
                int numRoom = Convert.ToInt32(textBoxRoom.Text);
                if (context.Номера.SingleOrDefault(n => n.НомерКомнаты == numRoom) == null)
                {
                    Номера room = new Номера
                    {
                        НомерКомнаты = numRoom,
                        КодКатегории = context.Категория.Single(c => c.Категория1 == comboBoxCategory.SelectedItem.ToString()).КодКатегории
                    };
                    context.Номера.Add(room);
                    context.SaveChanges();
                    dataGridRooms.Rows.Clear();
                    AddRooms();
                }
                else
                    er.SetError(textBoxRoom, "Такой номер уже есть!");
            }
            else
                foreach (Control c in groupBox1.Controls)
                {
                    if (c.Text == String.Empty)
                        er.SetError(c, "Пустая строка!");
                }

        }


        private void butSavePrice_Click(object sender, EventArgs e)
        {
            if (textBoxPrice.Text != "" && comboBoxCategory2.Text != "")
            {
                Категория cat = context.Категория.Single(c => c.Категория1 == comboBoxCategory2.SelectedItem.ToString());
                cat.Цена = Convert.ToInt32(textBoxPrice.Text);
                context.SaveChanges();
                dataGridRooms.Rows.Clear();
                AddRooms();
            }
            else
                foreach (Control c in groupBox2.Controls)
                {
                    if (c.Text == String.Empty)
                        er.SetError(c, "Пустая строка!");
                }
        }

        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)             // Если это не цифра и не backspace
            {
                e.Handled = true;
                er.SetError((TextBox)sender, "Недопустимый символ!");
            }
            else
                er.SetError((TextBox)sender, "");
        }


        private void textBox_Leave(object sender, EventArgs e)    // при потере фокуса убираем предупреждение
        {
            er.SetError((Control)sender, "");
        }
    }
}
