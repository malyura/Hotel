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
    public partial class ClientsForm : Form
    {
        ErrorProvider er = new ErrorProvider();
        ГостиницаContext context = new ГостиницаContext();
        public ClientsForm()
        {
            InitializeComponent();
            AddClients();
        }


        private void AddClients()
        {
            int i = 0;
            foreach (var cl in context.Клиенты)
            {
                dataGridClients.Rows.Add(++i,cl.Фамилия,cl.Имя,cl.Отчество,cl.НомерПаспорта,cl.Регистрация,cl.Гражданство);
            }
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            bool b = true;
            foreach (Control c in groupBox1.Controls)                    //проверка на пустые поля
                if (c.Text == "")
                    b = false;
            if (!b)
            {
                foreach (Control c in groupBox1.Controls)
                {
                    if (c.Text == String.Empty)
                        er.SetError(c, "Пустая строка!");
                }
            }
            else
            {
                if (context.Клиенты.SingleOrDefault(c => c.НомерПаспорта == textBoxPassport.Text) == null)
                {
                    Клиенты cl = new Клиенты
                    {
                        Фамилия = textBoxSurname.Text,
                        Имя = textBoxName.Text,
                        Отчество = textBoxPatr.Text,
                        НомерПаспорта = textBoxPassport.Text,
                        Регистрация = textBoxReg.Text,
                        Гражданство = textBoxNation.Text,
                    };
                    context.Клиенты.Add(cl);
                    context.SaveChanges();
                    dataGridClients.Rows.Clear();
                    AddClients();
                }
                else
                    MessageBox.Show("Клиент с таким номером паспорта уже есть в базе!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBox_Leave(object sender, EventArgs e)    // при потере фокуса убираем предупреждение
        {
            er.SetError((Control)sender, "");
        }

        private void textBoxPassport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= 65 && e.KeyChar <= 90) && !Char.IsDigit(e.KeyChar) && e.KeyChar != 8)             // Если это не латинская заглавная, не цифра и не backspace
            {
                e.Handled = true;
                er.SetError((TextBox)sender, "Недопустимый символ!");
                er.RightToLeft = true;
            }
            else
                er.SetError((TextBox)sender, "");
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
