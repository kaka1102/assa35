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
    
    public partial class m_cuochop
    {
        public int id { get; set; }
        public Nullable<System.DateTime> thoigiantao { get; set; }
        public Nullable<int> idnguoitao { get; set; }
        public string ghichu { get; set; }
        public Nullable<int> trangthai { get; set; }
        public Nullable<int> idnguoiduyet { get; set; }
        public Nullable<System.DateTime> thoigianduyet { get; set; }
        public string tencuochop { get; set; }
        public string noidung { get; set; }
        public Nullable<int> phienban { get; set; }
        public string lydotuchoi { get; set; }
        public System.DateTime ngaydienra { get; set; }
        public System.TimeSpan thoigianbatdau { get; set; }
        public System.TimeSpan thoigianketthuc { get; set; }
        public string avatar { get; set; }
        public string diadiemtochuc { get; set; }
    
        public virtual m_taikhoan m_taikhoan { get; set; }
        public virtual m_trangthai m_trangthai { get; set; }
    }
}
