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
    
    public partial class m_chitietguimedia
    {
        public int id { get; set; }
        public Nullable<int> id_thongbao { get; set; }
        public Nullable<int> trangthai { get; set; }
        public Nullable<int> idtaikhoannhan { get; set; }
        public Nullable<System.DateTime> ngaygui { get; set; }
        public string theloai { get; set; }
    
        public virtual m_thongbao m_thongbao { get; set; }
    }
}
