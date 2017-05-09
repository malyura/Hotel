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
    public partial class EditResidentForm : Form
    {
        ErrorProvider er = new ErrorProvider();
        ГостиницаContext context = new ГостиницаContext();
        int dayQuanty = 0;
        public EditResidentForm()
        {
            InitializeComponent();
            if (context.Проживание.Where(c => c.КодКлиента == FromGridRes.IdClient).Count() > 1)
                context.Проживание.Remove(context.Проживание.Where(c => c.КодКлиента == FromGridRes.IdClient).First());
            var cl = context.Клиенты.Where(c => c.КодКлиента == FromGridRes.IdClient).Single();
            var pr = context.Проживание.Where(p => p.КодПроживания == FromGridRes.IdResident).Single();
            Бронирование br = new Бронирование();
            if ((br = context.Бронирование.Where(b => b.КодБронирования == FromGridRes.IdBooking).SingleOrDefault()) != null)
            {
                FromGridBooking.DateBook = br.ДатаБронирования;
                context.Бронирование.Remove(br);
            }
            
            context.Проживание.Remove(pr);
            context.Клиенты.Remove(cl);
           
            context.SaveChanges();
        }


        private void EditResidentForm_Load(object sender, EventArgs e)
        {
            textBoxSurname.Text = FromGridRes.Surname;
            textBoxName.Text = FromGridRes.Name;
            textBoxPatronymic.Text = FromGridRes.Patronymic;
            textBoxPassport.Text = FromGridRes.Passport;
            textBoxReg.Text = FromGridRes.Reg;
            textBoxNation.Text = FromGridRes.Nation;
            dateTimePickerIn.Value = FromGridRes.DateIn;
            dateTimePickerOut.Value = FromGridRes.DateOut;
            foreach (var cat in context.Категория)
                comboBoxCategory.Items.Add(cat.Категория1);
            comboBoxCategory.SelectedItem = FromGridRes.Category;
            textBoxPrice.Text = FromGridRes.Price.ToString();
            dateTimePickerPay.Value = FromGridRes.DatePay;
            FindFreeRoom();
            comboBoxRoom.SelectedItem = FromGridRes.Room;
        }

        private void FindFreeRoom()
        {
            if (comboBoxCategory.SelectedItem != null && dateTimePickerIn.Value < dateTimePickerOut.Value)
            {
                comboBoxRoom.Items.Clear();
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
                comboBoxRoom.Items.Clear();      
        }
       

        private void comboBoxRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int room = Convert.ToInt32(comboBoxRoom.SelectedItem);
            textBoxPrice.Text = context.Номера.Where(c => c.НомерКомнаты == room).             //автомат. подсчет стоимости проживания
                                                     Select(c => c.Категория.Цена * dayQuanty).Single().ToString();
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindFreeRoom();        
        }

        private Клиенты AddClientOldValues()
        {
            Клиенты cl = new Клиенты
            {
                Фамилия = FromGridRes.Surname,
                Имя = FromGridRes.Name,
                Отчество = FromGridRes.Patronymic,
                НомерПаспорта = FromGridRes.Passport,
                Гражданство = FromGridRes.Nation,
                Регистрация = FromGridRes.Reg,
            };
            context.Клиенты.Add(cl);
            context.SaveChanges();
            return cl; 
        }

        private int? AddBookValues()
        {
            Бронирование br = new Бронирование();
            if (FromGridRes.IdBooking != null)
            {
                br = new Бронирование
                {
                    Фамилия = FromGridRes.Surname,
                    Имя = FromGridRes.Name,
                    Отчество = FromGridRes.Patronymic,
                    ДатаБронирования = FromGridBooking.DateBook,
                    ДатаЗаселения = FromGridRes.DateIn,
                    ДатаВыселения = FromGridRes.DateOut,
                    КодНомера = FromGridRes.IdRoom,
                };
                context.Бронирование.Add(br);
                context.SaveChanges();
                return br.КодБронирования;
            }
            else
            {
                return null;
            }

        }

        public Проживание AddOldResident()
        {
            Клиенты cl = AddClientOldValues();
            int? br = AddBookValues();
            Проживание res = new Проживание
            {
                ДатаЗаселения = FromGridRes.DateIn,
                ДатаВыселения = FromGridRes.DateOut,
                Стоимость = FromGridRes.Price,
                ДатаОплаты = FromGridRes.DatePay,
                КодНомера = FromGridRes.IdRoom,
                КодКлиента = cl.КодКлиента,
                КодБронирования = br,
            };
            return res;
        }

        private Клиенты EditClient()
        {
            Клиенты cl = new Клиенты
            {
                Фамилия = textBoxSurname.Text,
                Имя = textBoxName.Text,
                Отчество = textBoxPatronymic.Text,
                НомерПаспорта = textBoxPassport.Text,
                Гражданство = textBoxNation.Text,
                Регистрация = textBoxReg.Text
            };
            context.Клиенты.Add(cl);
            context.SaveChanges();
            return cl; 
        }


        public Проживание EditResident()
        {
            Клиенты cl = EditClient();
            int? br = AddBookValues();
            int numRoom = Convert.ToInt32(comboBoxRoom.SelectedItem);
            Проживание res = new Проживание
            {
                ДатаЗаселения = dateTimePickerIn.Value,
                ДатаВыселения = dateTimePickerOut.Value,
                Стоимость = Convert.ToInt32(textBoxPrice.Text),
                ДатаОплаты = dateTimePickerPay.Value,
                КодНомера = context.Номера.Single(n => n.НомерКомнаты == numRoom).КодНомера,
                КодКлиента = cl.КодКлиента,
                КодБронирования = br
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
                    if (c.Text == String.Empty)
                        er.SetError(c, "Пустая строка!");
                }
            }
            else
                DialogResult = DialogResult.OK;

        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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
    }
}
