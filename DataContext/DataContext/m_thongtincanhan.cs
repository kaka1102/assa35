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
    
    public partial class m_thongtincanhan
    {
        public int id { get; set; }
        public string hovaten { get; set; }
        public Nullable<bool> gioitinh { get; set; }
        public Nullable<System.DateTime> ngaysinh { get; set; }
        public string email { get; set; }
        public string diachi { get; set; }
        public string sodienthoai { get; set; }
        public string chucvu { get; set; }
        public string cmthochieu { get; set; }
        public string noicap { get; set; }
        public Nullable<System.DateTime> ngaycap { get; set; }
        public Nullable<System.DateTime> ngayhethan { get; set; }
        public string sothenhabao { get; set; }
        public string fileanhthe { get; set; }
        public string anhcmtmattruoc { get; set; }
        public string anhcmtmatsau { get; set; }
        public Nullable<int> trangthai { get; set; }
        public Nullable<int> dangkyxe { get; set; }
        public Nullable<int> dangkyphong { get; set; }
        public Nullable<int> idtaikhoan { get; set; }
        public string yeucauthem { get; set; }
        public string lydo { get; set; }
    
        public virtual m_taikhoan m_taikhoan { get; set; }
    }
}