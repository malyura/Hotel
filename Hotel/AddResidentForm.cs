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
    public partial class AddResidentForm : Form
    {
        ErrorProvider er = new ErrorProvider();
        ГостиницаContext context = new ГостиницаContext();
        int dayQuanty = 0;
        public AddResidentForm()
        {
            InitializeComponent();
            dateTimePickerIn.Value = DateTime.Today;
            dateTimePickerOut.Value = DateTime.Today.AddDays(1);
            foreach (var cat in context.Категория)
                comboBoxCategory.Items.Add(cat.Категория1);
            //comboBoxRoom.Items.Add("Выберите категорию");
        }

       

        private void butFind_Click(object sender, EventArgs e)               //поиск по номеру паспорта
        {
            Клиенты cl = new Клиенты();

            if ((cl = context.Клиенты.Where(c => c.НомерПаспорта == textBoxFind.Text).SingleOrDefault()) != null)
            {
                textBoxSurname.Text = cl.Фамилия;
                textBoxName.Text = cl.Имя;
                textBoxPatronymic.Text = cl.Отчество;
                textBoxPassport.Text = cl.НомерПаспорта;
                textBoxReg.Text = cl.Регистрация;
                textBoxNation.Text = cl.Гражданство;
                labelFind.Text = "Клиент найден!";
            }
            else
                labelFind.Text = "Клиент не найден!";
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxRoom.Items.Clear();
            if (comboBoxCategory.SelectedItem != null && dateTimePickerIn.Value < dateTimePickerOut.Value)
            {
                textBoxPrice.Clear();
                var room = from c in context.Номера
                           where c.Категория.Категория1 == comboBoxCategory.SelectedItem.ToString() &&
                            !((from b in context.Бронирование
                               where b.КодНомера == c.КодНомера &&
                                          ((dateTimePickerIn.Value >= b.ДатаЗаселения && dateTimePickerIn.Value < b.ДатаВыселения) ||
                                          (dateTimePickerIn.Value < b.ДатаВыселения && dateTimePickerOut.Value > b.ДатаЗаселения)) //!
                               select new { b.КодНомера }).FirstOrDefault() != null) &&
                             !((from p in context.Проживание
                                where p.КодНомера == c.КодНомера &&
                                          ((dateTimePickerIn.Value >= p.ДатаЗаселения && dateTimePickerIn.Value < p.ДатаВыселения) ||
                                           (dateTimePickerIn.Value < p.ДатаВыселения && dateTimePickerOut.Value > p.ДатаЗаселения))
                                select new { p.КодНомера }).FirstOrDefault() != null)

                           select new { Номер = c.НомерКомнаты, };

                foreach (var r in room)
                {
                    comboBoxRoom.Items.Add(r.Номер);
                }
                dayQuanty = (dateTimePickerOut.Value - dateTimePickerIn.Value).Days;
            }
            else
                comboBoxRoom.Items.Add("Данные не корректны");

        }

        private void comboBoxRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int room = Convert.ToInt32(comboBoxRoom.SelectedItem);
            textBoxPrice.Text = context.Номера.Where(c => c.НомерКомнаты == room).             //автомат. подсчет стоимости проживания
                                                     Select(c => c.Категория.Цена * dayQuanty).Single().ToString();
        }

        private Клиенты AddClient()
        {
            Клиенты cl = new Клиенты();
           
            if ((cl = context.Клиенты.Where(c => c.НомерПаспорта == textBoxPassport.Text).SingleOrDefault()) != null)
            {
                cl.Регистрация = textBoxReg.Text;
                context.SaveChanges();
                return cl;
            }
            else
            {
                cl = new Клиенты
                {
                    Фамилия = textBoxSurname.Text,
                    Имя = textBoxName.Text,
                    Отчество = textBoxPatronymic.Text,
                    НомерПаспорта = textBoxPassport.Text,
                    Регистрация = textBoxReg.Text,
                    Гражданство = textBoxNation.Text
                };
                context.Клиенты.Add(cl);
                context.SaveChanges();
                return cl;
            }
        }


        public Проживание AddResidance()
        {
            Клиенты cl = AddClient();
            int numRoom = Convert.ToInt32(comboBoxRoom.SelectedItem);
            Проживание res = new Проживание
            {
                ДатаЗаселения = dateTimePickerIn.Value,
                ДатаВыселения = dateTimePickerOut.Value,
                Стоимость = Convert.ToDouble(textBoxPrice.Text),
                ДатаОплаты = DateTime.Today,
                КодНомера = context.Номера.Where(c => c.НомерКомнаты == numRoom).Select(c => c.КодНомера).Single(),
                КодКлиента = cl.КодКлиента,
                КодБронирования = null
            };
            return res;
        }

        private void butOK_Click(object sender, EventArgs e)
        {

            bool b = true;
            foreach (Control c in Controls)                    //проверка на пустые поля
                if (c.Text == "" && c != textBoxFind)
                    b = false;
            if (!b)
            {
                foreach (Control c in Controls)
                {
                    if (c.Text == String.Empty && c != textBoxFind)
                        er.SetError(c, "Пустая строка!");
                }
            }
            else
                DialogResult = DialogResult.OK;
        }

        private void textBox_Leave(object sender, EventArgs e)    // при потере фокуса убираем предупреждение
        {
            er.SetError((Control)sender, "");
        }

        private void textBoxPassport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= 65 && e.KeyChar <= 90) && !Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13)             // Если это не латинская заглавная, не цифра и не backspace
            {
                e.Handled = true;
                er.SetError((TextBox)sender, "Недопустимый символ!");
                er.RightToLeft = true;
            }
            else
                er.SetError((TextBox)sender, "");
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

        private void butCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBoxFind_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                butFind_Click(sender,e);
            }
        }

      
    }
}
