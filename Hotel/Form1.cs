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
using System.Xml.Linq;
using System.IO;
using NLog;
using Microsoft.Office.Interop.Excel;

namespace Hotel
{
    public partial class HotelForm : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        ГостиницаContext context = new ГостиницаContext();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        public HotelForm()
        { 
            logger.Info("Запуск программы");
            InitializeComponent();
            saveFileDialog.Filter = "Книга Excel(*.xlsx)|*.xlsx|All files(*.*)|*.*";
            dataGridBooking.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridResidents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                GetResidents();
                GetBooking();
            }
            catch (Exception msg)
            {
                logger.Debug(msg);
                MessageBox.Show("База данных недоступна!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           
        }

        private void GetResidents()
        {
            var residents = from p in context.Проживание
                         join z in context.Номера on p.КодНомера equals z.КодНомера
                         join k in context.Категория on z.КодКатегории equals k.КодКатегории
                         join c in context.Клиенты on p.КодКлиента equals c.КодКлиента
                         where p.ДатаВыселения >= DateTime.Today
                         select new
                         {
                             НомерКомнаты = z.НомерКомнаты,
                             Категория = k.Категория1,
                             Заезд = p.ДатаЗаселения,
                             Выезд = p.ДатаВыселения,
                             Оплата = p.Стоимость,
                             ДатаОплаты = p.ДатаОплаты,
                             Фамилия = c.Фамилия,
                             Имя = c.Имя,
                             Отчество = c.Отчество
                         };

            
            foreach (var r in residents)
            {
                dataGridResidents.Rows.Add(new object[] { r.НомерКомнаты,r.Категория,r.Заезд.ToShortDateString(),r.Выезд.ToShortDateString(),
                                                          r.Оплата,r.ДатаОплаты.ToShortDateString(),r.Фамилия + " " + r.Имя + " " + r.Отчество});
            }
           
        }

        private void GetBooking()
        {

            var booking = from c in context.Бронирование
                         join z in context.Номера on c.КодНомера equals z.КодНомера
                         where c.ДатаЗаселения >= DateTime.Today          
                         select new
                         {
                             НомерКомнаты = z.НомерКомнаты,
                             ФИО = c.Фамилия + " " + c.Имя + " " + c.Отчество,
                             Заезд = c.ДатаЗаселения,
                             Выезд = c.ДатаВыселения,
                             ДатаЗаписи = c.ДатаБронирования,
                         };


            foreach (var o in booking)
            {
                dataGridBooking.Rows.Add(new object[] { o.НомерКомнаты, o.ФИО, o.Заезд.ToShortDateString(),
                                                        o.Выезд.ToShortDateString(), o.ДатаЗаписи.ToShortDateString() });

            }
         
        }

        private void butBooking_Click(object sender, EventArgs e)
        {
            AddBookingForm addBooking = new AddBookingForm();
            if (addBooking.ShowDialog() == DialogResult.OK)
            {
                context.Бронирование.Add(addBooking.AddBooking());
                context.SaveChanges();
                dataGridBooking.Rows.Clear();
                GetBooking();
            }

            if (addBooking.DialogResult == DialogResult.Cancel)
                return;
        }


        private void GetFromGridBooking()    //получаем данные из выделенной строки таблицы dataGridBooking
        {
            string[] s = dataGridBooking.CurrentRow.Cells[1].Value.ToString().Split(' ');
            string surName = s[0], name = s[1], patr = s[2];
            DateTime dateBooking = DateTime.Parse(dataGridBooking.CurrentRow.Cells[4].Value.ToString());

            FromGridBooking.Surname = surName;
            FromGridBooking.Name = name;
            FromGridBooking.Patronymic = patr;
            FromGridBooking.NumRoom = Convert.ToInt32(dataGridBooking.CurrentRow.Cells[0].Value);
            FromGridBooking.DateIn = DateTime.Parse(dataGridBooking.CurrentRow.Cells[2].Value.ToString());
            FromGridBooking.DateOut = DateTime.Parse(dataGridBooking.CurrentRow.Cells[3].Value.ToString());
            FromGridBooking.IdBooking = context.Бронирование.Where(b => b.Фамилия == surName && b.Имя == name && b.Отчество == patr &&
                                                        b.ДатаБронирования == dateBooking).Select(b => b.КодБронирования).First();
        }
        



        private void butAddResFromBooking_Click(object sender, EventArgs e)         //оформление проживания по записи о бронировании
        {
            if (dataGridBooking.CurrentRow != null)
            {
                GetFromGridBooking();
                AddResFromBookingForm res = new AddResFromBookingForm();
                if (context.Проживание.Where(p => p.КодБронирования == FromGridBooking.IdBooking).Count() == 0)  //для предотвращения повторного оформления на проживание
                {
                   
                    if (res.ShowDialog() == DialogResult.OK)
                    {
                        context.Проживание.Add(res.AddResidance());
                        context.SaveChanges();
                        dataGridResidents.Rows.Clear();
                        GetResidents();
                    }
                }
                else
                    MessageBox.Show("Клиент уже оформлен на проживание!","Внимание!",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

                if (res.DialogResult == DialogResult.Cancel)
                    return;
            }
            
        }


        private void butAddResident_Click(object sender, EventArgs e)
        {
            AddResidentForm addRes = new AddResidentForm();
            if (addRes.ShowDialog() == DialogResult.OK)
            {
                context.Проживание.Add(addRes.AddResidance());
                context.SaveChanges();
                dataGridResidents.Rows.Clear();
                GetResidents();
                GetSumPrice(graphic1.y);
            }
            if (addRes.DialogResult == DialogResult.Cancel)
                return;
        }

        #region события и метод для графика (для user control)
        private void GetSumPrice(decimal year)                   //Расчет выручки РТДП 40324.018.05
        {
            graphic1.v.Clear();
            for (int i = 1; i <= 12; i++)
            {
                Проживание res = new Проживание();
                double priceSum;
                if ((res = context.Проживание.Where(p => p.ДатаОплаты.Month == i && p.ДатаОплаты.Year == year).FirstOrDefault()) == null)
                    priceSum = 0;
                else
                    priceSum = context.Проживание.Where(p => p.ДатаОплаты.Month == i && p.ДатаОплаты.Year == year).Sum(p => p.Стоимость);
                graphic1.v.Add(priceSum);
            }
           
        }
        
        
        private void graphic1_Load(object sender, EventArgs e)
        {
            try
            {
                GetSumPrice(graphic1.y);
                graphic1.AddPoints();
            }
            catch (Exception msg)
            {
                logger.Debug(msg);
                MessageBox.Show("База данных недоступна!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void graphic1_YearChanged(object sender, SumInfoEventArgs e)
        {
            try
            {
                GetSumPrice(e.Year);
                graphic1.AddPoints();
            }
            catch(Exception msg)
            {
                logger.Debug(msg);
                MessageBox.Show("Данные не получены!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void graphic1_Click(object sender, EventArgs e)
        {
            try
            {
                GetSumPrice(graphic1.y);
                graphic1.AddPoints();
            }
            catch (Exception msg)
            {
                logger.Debug(msg);
                MessageBox.Show("Данные не получены!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion
        private void butDelBooking_Click(object sender, EventArgs e)            //удаление записи о бронировании РТДП 40324.018.04
        {
            if (dataGridBooking.CurrentRow != null)
            {
                GetFromGridBooking();
                if (context.Проживание.Where(p => p.КодБронирования == FromGridBooking.IdBooking).Count() == 0)
                {
                    DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                        return;
            
                    var v = context.Бронирование.Where(p => p.КодБронирования == FromGridBooking.IdBooking).Single();
                    context.Бронирование.Remove(v);
                    context.SaveChanges();
                    dataGridBooking.Rows.Clear();
                    GetBooking();
                }
                else
                    MessageBox.Show("Удаление брони невозможно, т.к. по этой записи уже оформлено проживание!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void GetFromGridResident()    //получаем данные из выделенной строки таблицы dataGridResidents
        {
            string[] s = dataGridResidents.CurrentRow.Cells[6].Value.ToString().Split(' ');
            string surName = s[0], name = s[1], patr = s[2];
            DateTime datePay = DateTime.Parse(dataGridResidents.CurrentRow.Cells[5].Value.ToString());
            int numRoom = Convert.ToInt32(dataGridResidents.CurrentRow.Cells[0].Value);
            int idResident = context.Проживание.Where(p => p.Клиенты.Фамилия == surName && p.Клиенты.Имя == name && p.Клиенты.Отчество == patr &&
                                                        p.ДатаОплаты == datePay && p.Номера.НомерКомнаты == numRoom).Select(p => p.КодПроживания).Single();

            FromGridRes.IdResident = idResident;
            FromGridRes.DateIn = DateTime.Parse(dataGridResidents.CurrentRow.Cells[2].Value.ToString());
            FromGridRes.DateOut = DateTime.Parse(dataGridResidents.CurrentRow.Cells[3].Value.ToString());
            FromGridRes.Price = Convert.ToInt32(dataGridResidents.CurrentRow.Cells[4].Value);
            FromGridRes.DatePay = datePay;
            FromGridRes.IdClient = context.Проживание.Single(p => p.КодПроживания == idResident).КодКлиента;
            FromGridRes.IdRoom = context.Проживание.Single(p => p.КодПроживания == idResident).КодНомера;
            FromGridRes.IdBooking = context.Проживание.Single(p => p.КодПроживания == idResident).КодБронирования;
                

            FromGridRes.Surname = surName;
            FromGridRes.Name = name;
            FromGridRes.Patronymic = patr;
            FromGridRes.Passport = context.Клиенты.Single(p => p.КодКлиента == FromGridRes.IdClient).НомерПаспорта;
            FromGridRes.Reg = context.Клиенты.Single(p => p.КодКлиента == FromGridRes.IdClient).Регистрация;
            FromGridRes.Nation = context.Клиенты.Single(p => p.КодКлиента == FromGridRes.IdClient).Гражданство;

            FromGridRes.Room = numRoom;
            FromGridRes.Category = dataGridResidents.CurrentRow.Cells[1].Value.ToString();       

        }
        private void butEditResident_Click(object sender, EventArgs e)
        {
            if (dataGridResidents.CurrentRow != null)
            {
                GetFromGridResident();
               

                EditResidentForm res = new EditResidentForm();

                if (res.ShowDialog() == DialogResult.OK)
                {
                    context.Проживание.Add(res.EditResident());
                    context.SaveChanges();
                    dataGridResidents.Rows.Clear();
                    GetResidents();
                    GetSumPrice(graphic1.y);
                }

                if (res.DialogResult == DialogResult.Cancel)
                {
                    context.Проживание.Add(res.AddOldResident());
                    context.SaveChanges();
                }
            }
        }

        private void butDelRes_Click(object sender, EventArgs e)
        {
            if (dataGridResidents.CurrentRow != null)
            {
                DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                GetFromGridResident();
                var r = context.Проживание.Where(p => p.КодПроживания == FromGridRes.IdResident).Single();
                context.Проживание.Remove(r);
                context.SaveChanges();
                dataGridResidents.Rows.Clear();
                GetResidents();
                GetSumPrice(graphic1.y);
            }
        }


        #region События для строки статуса

        private void graphic1_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "График выручки";
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Приложение \"Гостиница\"";
        }

        private void dataGridResidents_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Актуальные записи о проживании";
        }

        private void toolStripButton2_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Номерной фонд";
        }

        private void toolStripButton3_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Клиенты";
        }
        private void dataGridBooking_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Актуальные записи о бронировании";
        }

        private void butAddResident_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Заселение без предварительного бронирования";
        }

        private void butEditResident_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Редактирование выбранной записи о проживании";
        }

        private void butDelRes_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Удаление выбранной записи о проживании";
        }

        private void butAddResFromBooking_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Заселение по выбранной записи о бронировании";
        }

        private void butBooking_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Добавление записи о предварительном бронировании";
        }

        private void butDelBooking_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Удаление выбранной записи о бронировании";
        }

        private void xMLToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Экспорт записей о проживании в Excel";
        }
        private void ExportResToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = ExportResToolStripMenuItem.Text;
        }
        private void ImportResToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = ImportResToolStripMenuItem.Text;
        }
        private void toolStripButExit_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = toolStripButExit.Text;
        }
        #endregion

        private void toolStripButRooms_Click(object sender, EventArgs e)
        {
            RoomsForm room = new RoomsForm();
            room.ShowDialog();
        }

        private void toolStripButClients_Click(object sender, EventArgs e)
        {

            ClientsForm cl = new ClientsForm();
            cl.ShowDialog();
        }

        private void ExportResToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            //Номера room = new Номера();
            //Проживание res = new Проживание();
            //room = context.Номера.Where(n => n.КодНомера == res.КодНомера);
            XDocument doc = new XDocument(
                new XElement("Проживания",
                context.Проживание.ToList().Select(p =>
                   new XElement("Проживание",
                       new XElement("ДатаЗаселения", p.ДатаЗаселения.ToShortDateString()),
                       new XElement("ДатаВыселения", p.ДатаВыселения.ToShortDateString()),
                       new XElement("Стоимость", p.Стоимость),
                       new XElement("ДатаОплаты", p.ДатаОплаты.ToShortDateString()),
                       new XElement("Номер", context.Номера.Where(n => n.КодНомера == p.КодНомера).Select(f => f.НомерКомнаты)),
                       new XElement("Клиент", context.Клиенты.Where(n => n.КодКлиента == p.КодКлиента).Select(f => f.Фамилия + " " + f.Имя + " " + f.Отчество))
                      //new XElement("Номер", p.КодБронирования != null ? context.Бронирование.Where(n => n.КодБронирования == p.КодБронирования).Select(f => f.ДатаБронирования.ToShortTimeString()).ToString() : "без брони")
                       //new XElement("Номер", p.Номера.НомерКомнаты),
                       //new XElement("Клиент", p.Клиенты.Фамилия + " " + p.Клиенты.Имя + " " + p.Клиенты.Отчество),
                     //  new XElement("ДатаБронирования", p.КодБронирования == null ? "без брони" : p.Бронирование.ДатаБронирования.ToShortDateString())
                       ))));
            doc.Save("x.xml");
            string pathXml = Path.GetFullPath("x.xml");
            Microsoft.Office.Interop.Excel.Application exApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = exApp.Workbooks.OpenXML(pathXml, Type.Missing, LoadOption.PreserveChanges);
            string filename = saveFileDialog.FileName;
            workbook.SaveAs(filename);
            exApp.Quit();
            MessageBox.Show("Записи о проживании экспортированы в файл Excel", "Экспорт в Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
          
        }

        private void ImportResToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"x.xml"))
            {
                ImportXMLForm i = new ImportXMLForm();
                i.ShowDialog();
            }
            else
            {
                logger.Debug("XML-файл не найден!");
                MessageBox.Show("Файл не обнаружен!");
            }
        }

        private void toolStripButExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            logger.Info("Завершение программы");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm a = new AboutForm();
            a.ShowDialog();
        }

        
    }
}
