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
    public partial class AddResFromBookingForm : Form
    {
        ErrorProvider er = new ErrorProvider();
        ГостиницаContext context = new ГостиницаContext();
        int dayQuanty;
        public AddResFromBookingForm()
        {
            InitializeComponent();
        }

        private void AddResFromBookingForm_Load(object sender, EventArgs e)
        {
            dateTimePickerIn.Value = FromGridBooking.DateIn; 
            dateTimePickerOut.Value = FromGridBooking.DateOut;
            dayQuanty = (dateTimePickerOut.Value - dateTimePickerIn.Value).Days;
            textBoxRoom.Text = FromGridBooking.NumRoom.ToString();
            textBoxSurname.Text = FromGridBooking.Surname;
            textBoxName.Text = FromGridBooking.Name;
            textBoxPatronymic.Text = FromGridBooking.Patronymic;
            textBoxPrice.Text = context.Номера.Where(c => c.НомерКомнаты == FromGridBooking.NumRoom).             //автомат. подсчет стоимости проживания
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

            Проживание res = new Проживание
            {
                ДатаЗаселения = dateTimePickerIn.Value,
                ДатаВыселения = dateTimePickerOut.Value,
                Стоимость = Convert.ToDouble(textBoxPrice.Text),
                ДатаОплаты = DateTime.Today,
                КодНомера = context.Номера.Where(c => c.НомерКомнаты == FromGridBooking.NumRoom).Select(c => c.КодНомера).Single(),
                КодКлиента = cl.КодКлиента,
                КодБронирования = FromGridBooking.IdBooking
                                                          
            };
            return res;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            bool b = true;
            foreach (Control c in Controls)                    //проверка на пустые поля
                if (c.Text == "")
                    b = false;
            if (!b)
            {
                foreach (Control c in Controls)
                {
                    if (c.Text == String.Empty && c != textBoxRoom)
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
            if (!(e.KeyChar >= 65 && e.KeyChar <= 90) && !Char.IsDigit(e.KeyChar) && e.KeyChar != 8)             // Если это не латинская заглавная, не цифра и не backspace
            {
                e.Handled = true;
                er.SetError((TextBox)sender, "Недопустимый символ!");
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
    }
}
