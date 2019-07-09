using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class ReturnMeetingAndEvent
    {
        public List<MeetingEvent> data { get; set; }
        public int totalCount { get; set; }
    }

    public class ReturnDetailMeeting
    {
        public List<DetailMeetingEvent> data { get; set; }
        public int totalCount { get; set; }
    }

    public class MeetingEvent
    {
        public int id { get; set; }
        public string thoigiantao { get; set; }
        public int idnguoitao { get; set; }
        public string tennguoitao { get; set; }
        public int trangthai { get; set; }
        public string trangthaihienthi { get; set; }
        public int idnguoiduyet { get; set; }
        public string thoigianduyet { get; set; }
        public string tencuochop { get; set; }
        public string ngayhop { get; set; }
        public int phienban { get; set; }
        public string lydotuchoi { get; set; }
        public string diadiem { get; set; }
        public List<DetailMeetingEvent> chitiet { get; set; }
    }

    public class DateMeeting
    {
        public string ngayhop { set; get; }
        public List<MeetingEvent> listhop { set; get; }
    }
    public class DetailMeetingEvent
    {
        public int id { get; set; }
        public string ten { get; set; }
        public int idsukienhop { get; set; }
        public string diadiem { get; set; }
        public string thoigianbatdau { get; set; }
        public string thoigianketthuc { get; set; }
        public string ngayhop { get; set; }
        public string noidung { get; set; }
        public int trangthai { get; set; }
        public int idnguoitao { get; set; }
        public int idnguoiduyet { get; set; }
        public string trangthaihienthi { get; set; }
        public string lydotuchoi { get; set; }
        public int sukienchinh { set; get; }
    }

    public class ReturnMeetingClient
    {
        public List<MeetingEvent> lstNgay1 { get; set; }
        public List<MeetingEvent> lstNgay2 { get; set; }
        public List<MeetingEvent> lstNgay3 { get; set; }
        public string DateToDate { get; set; }
        public string Date1 { get; set; }
        public string Date2 { get; set; }
        public string Date3 { get; set; }
    }
}