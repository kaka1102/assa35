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
    
    public partial class m_chitietsukienhop
    {
        public int id { get; set; }
        public string ten { get; set; }
        public int idsukienhop { get; set; }
        public string diadiem { get; set; }
        public System.TimeSpan thoigianbatdau { get; set; }
        public Nullable<System.TimeSpan> thoigianketthuc { get; set; }
        public Nullable<System.DateTime> ngayhop { get; set; }
        public string noidung { get; set; }
        public Nullable<int> trangthai { get; set; }
        public Nullable<int> idnguoitao { get; set; }
        public Nullable<int> idnguoiduyet { get; set; }
        public string lydotuchoi { get; set; }
    
        public virtual m_sukienhop m_sukienhop { get; set; }
    }
}
