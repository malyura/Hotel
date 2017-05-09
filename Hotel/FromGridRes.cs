using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    static class FromGridRes
    {
        public static int IdResident { get; set; }
        public static DateTime DateIn { get; set; }
        public static DateTime DateOut { get; set; }
        public static int Price { get; set; }
        public static DateTime DatePay { get; set; }
        public static int IdClient { get; set; }
        public static int IdRoom { get; set; }
        public static int? IdBooking { get; set; }

        public static string Surname { get; set; }
        public static string Name { get; set; }
        public static string Patronymic { get; set; }
        public static string Passport { get; set; }
        public static string Reg { get; set; }
        public static string Nation { get; set; }

        public static int Room { get; set; }
        public static string Category { get; set; }
    
        

    }
}
