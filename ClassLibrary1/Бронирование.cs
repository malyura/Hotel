//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Бронирование
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Бронирование()
        {
            this.Проживание = new HashSet<Проживание>();
        }
    
        public int КодБронирования { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public System.DateTime ДатаБронирования { get; set; }
        public System.DateTime ДатаЗаселения { get; set; }
        public System.DateTime ДатаВыселения { get; set; }
        public string Примечание { get; set; }
        public int КодНомера { get; set; }
    
        public virtual Номера Номера { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Проживание> Проживание { get; set; }
    }
}
