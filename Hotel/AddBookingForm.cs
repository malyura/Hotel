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
    public partial class AddBookingForm : Form
    {
        ErrorProvider er = new ErrorProvider();
        ГостиницаContext context = new ГостиницаContext();
        public AddBookingForm()
        {
            InitializeComponent();
            dateTimePickerIn.Value = DateTime.Today;
            dateTimePickerOut.Value = DateTime.Today.AddDays(1);
            foreach (var cat in context.Категория)
                comboBoxCategory.Items.Add(cat.Категория1);
           // comboBoxRoom.Items.Add("Выберите категорию");
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (textBoxSurname.Text != "" && textBoxName.Text != "" && textBoxPatronymic.Text != "" &&
                comboBoxCategory.Text != "" && comboBoxRoom.Text != "")
                DialogResult = DialogResult.OK;
            else
                foreach (Control c in Controls)
                {
                    if (c.Text == String.Empty && c != textBoxNote)
                        er.SetError(c, "Пустая строка!");
                }
        }


        //Фильтрация выводимых номеров (комнат) по датам заезда-выезда и категориям. Выводятся только свободные номера РТДП 40324.018.03
        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxRoom.Items.Clear();
            if (comboBoxCategory.SelectedItem != null && dateTimePickerIn.Value < dateTimePickerOut.Value)
            {
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
            }
            else
                comboBoxRoom.Items.Add("Данные не корректны");

        }

        public Бронирование AddBooking()
        {
            int numRoom = Convert.ToInt32(comboBoxRoom.SelectedItem);
            Бронирование booking = new Бронирование
            {
                Фамилия = textBoxSurname.Text,
                Имя = textBoxName.Text,
                Отчество = textBoxPatronymic.Text,
                ДатаБронирования = DateTime.Today,
                ДатаЗаселения = dateTimePickerIn.Value,
                ДатаВыселения = dateTimePickerOut.Value,
                Примечание = (textBoxNote.Text == "")? null : textBoxNote.Text,
                КодНомера = context.Номера.Where(s => s.НомерКомнаты == numRoom).
                                                  Select(s => s.КодНомера).Single()      
            };
            return booking;
        }

        private void textBox_Leave(object sender, EventArgs e)    // при потере фокуса убираем предупреждение
        {
            er.SetError((Control)sender, "");
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)   //предупреждение и запрет на ввод недопустимых символов
        {
            if (Char.IsDigit(e.KeyChar))             // Если это цифра
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
