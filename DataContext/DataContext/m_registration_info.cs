//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class m_registration_info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public m_registration_info()
        {
            this.m_regisActivity = new HashSet<m_regisActivity>();
        }
    
        public int id { get; set; }
        public string titlename { get; set; }
        public string titleOther { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string middleinitial { get; set; }
        public Nullable<int> gender { get; set; }
        public string country { get; set; }
        public string address { get; set; }
        public string organization { get; set; }
        public string department { get; set; }
        public string position { get; set; }
        public string telephone { get; set; }
        public string cellphone { get; set; }
        public string email { get; set; }
        public Nullable<int> mealprefer { get; set; }
        public string meal_other { get; set; }
        public string acc_per1 { get; set; }
        public string acc_per2 { get; set; }
        public string acc_per3 { get; set; }
        public Nullable<int> idRoomOrder { get; set; }
        public string acc_spec { get; set; }
        public string arr_con { get; set; }
        public string conarr_airline { get; set; }
        public string conarr_flight { get; set; }
        public Nullable<System.DateTime> conarr_date { get; set; }
        public Nullable<System.TimeSpan> conarr_time { get; set; }
        public string dep_con { get; set; }
        public string condep_airline { get; set; }
        public string condep_flight { get; set; }
        public Nullable<System.DateTime> condep_date { get; set; }
        public Nullable<System.TimeSpan> condep_time { get; set; }
        public Nullable<int> trangthai { get; set; }
        public Nullable<int> idtaikhoan { get; set; }
        public Nullable<System.DateTime> regis_date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<m_regisActivity> m_regisActivity { get; set; }
    }
}
