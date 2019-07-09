using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Until;
using DataContext;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System.Data.SqlClient;

namespace DataContext.AdminBusiness
{
    [CheckSessionUser]
    public class StatisticalBusiness
    {
        SystemMess SysMess = new SystemMess();
        ASS_35Entities entity = new ASS_35Entities();
        SessionUser session = (SessionUser)HttpContext.Current.Session["uSession"];
        public List<string> headerColumns = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        
        public static class SpreadsheetLightExport
        {
            public static InlineString CreateStringInline(string text)
            {
                InlineString inlineString = new InlineString();
                Text t = new Text();
                t.Text = text;

                inlineString.AppendChild(t);

                return inlineString;
            }
        }
        public class SLStyleExport
        {
            public SLStyle stHeader;
            public SLStyle stColTextTitle;
            public SLStyle stColDefault;
            public SLStyle stColTextSTT;
            public SLStyle stColTextSTTBold;
            public SLStyle stColPriceBold;
            public SLStyle stColPrice;
            public SLStyle stColTextTitleLeft;
            public SLStyle stColTextTitleLeftBold;
            public SLStyle stColTextTitleCenter;
            public SLStyle stColTextTitleFooterBold;
            SLDocument sl;
            public SLStyleExport(SLDocument sl)
            {
                this.sl = sl;
                this.styleHeader();
                this.styleColTextTitle();
                this.styleColDefault();
                this.styleColTextSTT();
                this.styleColTextSTTBold();
                this.styleColPriceBold();
                this.styleColPrice();
                this.styleColTextTitleLeft();
                this.styleColTextTitleLeftBold();
                this.styleColTextTitleCenter();
                this.styleColTextTitleFooterBold();
            }
            void styleHeader()
            {
                stHeader = sl.CreateStyle();
                stHeader.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                stHeader.SetVerticalAlignment(VerticalAlignmentValues.Center);
                stHeader.SetWrapText(true);
                stHeader.Font.FontName = "Times New Roman";
                stHeader.Font.FontSize = 8;
                stHeader.Font.FontColor = System.Drawing.Color.Black;
                stHeader.Font.Bold = true;
                stHeader.Font.Italic = false;
                stHeader.Font.Strike = false;
                stHeader.Font.Underline = UnderlineValues.None;
                //stHeader.Border.Outline = true;
                stHeader.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
                stHeader.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
                stHeader.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
                stHeader.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
            }
            void styleColTextTitle()
            {
                stColTextTitle = this.sl.CreateStyle();
                stColTextTitle.Alignment.Horizontal = HorizontalAlignmentValues.Left;
                stColTextTitle.SetVerticalAlignment(VerticalAlignmentValues.Center);
                stColTextTitle.SetWrapText(true);
                stColTextTitle.Font.FontName = "Times New Roman";
                stColTextTitle.Font.FontSize = 8;
                stColTextTitle.Font.FontColor = System.Drawing.Color.Black;
                stColTextTitle.Font.Bold = true;
                stColTextTitle.Font.Italic = false;
                stColTextTitle.Font.Strike = false;
                stColTextTitle.Font.Underline = UnderlineValues.None;
                //stHeader.Border.Outline = true;
                stColTextTitle.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
                stColTextTitle.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
                stColTextTitle.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
                stColTextTitle.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
            }
            void styleColDefault()
            {
                stColDefault = this.sl.CreateStyle();
                stColDefault.Alignment.Horizontal = HorizontalAlignmentValues.Left;
                stColDefault.SetVerticalAlignment(VerticalAlignmentValues.Center);
                stColDefault.SetWrapText(false);
                stColDefault.Font.FontName = "Times New Roman";
                stColDefault.Font.FontSize = 8;
                stColDefault.Font.FontColor = System.Drawing.Color.Black;
                stColDefault.Font.Bold = false;
                stColDefault.Font.Italic = false;
                stColDefault.Font.Strike = false;
                stColDefault.Font.Underline = UnderlineValues.None;
                //stHeader.Border.Outline = true;
                stColDefault.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
                stColDefault.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
                stColDefault.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
                stColDefault.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
            }
            void styleColTextSTT()
            {
                stColTextSTT = this.sl.CreateStyle();
                stColTextSTT.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                stColTextSTT.SetVerticalAlignment(VerticalAlignmentValues.Center);
                stColTextSTT.Font.FontName = "Times New Roman";
                stColTextSTT.Font.FontSize = 8;

                stColTextSTT.Font.FontColor = System.Drawing.Color.Black;
                //stHeader.Border.Outline = true;
                stColTextSTT.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
                stColTextSTT.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
                stColTextSTT.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
                stColTextSTT.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
            }
            void styleColTextSTTBold()
            {
                stColTextSTTBold = this.sl.CreateStyle();
                stColTextSTTBold.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                stColTextSTTBold.SetVerticalAlignment(VerticalAlignmentValues.Center);
                stColTextSTTBold.Font.FontName = "Times New Roman";
                stColTextSTTBold.Font.FontSize = 8;
                stColTextSTTBold.Font.Bold = true;
                stColTextSTTBold.Font.FontColor = System.Drawing.Color.Black;
                //stHeader.Border.Outline = true;
                stColTextSTTBold.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
                stColTextSTTBold.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
                stColTextSTTBold.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
                stColTextSTTBold.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
            }
            void styleColPriceBold()
            {
                stColPriceBold = this.sl.CreateStyle();
                stColPriceBold.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                stColPriceBold.SetVerticalAlignment(VerticalAlignmentValues.Center);
                stColPriceBold.Font.FontName = "Times New Roman";
                stColPriceBold.Font.FontSize = 8;
                stColPriceBold.Font.FontColor = System.Drawing.Color.Black;
                stColPriceBold.Font.Bold = true;
                //stHeader.Border.Outline = true;
                stColPriceBold.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
                stColPriceBold.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
                stColPriceBold.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
                stColPriceBold.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
            }
            void styleColPrice()
            {
                stColPrice = this.sl.CreateStyle();
                stColPrice.Alignment.Horizontal = HorizontalAlignmentValues.Right;
                stColPrice.SetVerticalAlignment(VerticalAlignmentValues.Center);
                stColPrice.Font.FontName = "Times New Roman";
                stColPrice.Font.FontSize = 8;
                stColPrice.Font.FontColor = System.Drawing.Color.Black;
                //stHeader.Border.Outline = true;
                stColPrice.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
                stColPrice.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
                stColPrice.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
                stColPrice.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
            }
            void styleColTextTitleLeft()
            {
                stColTextTitleLeft = this.sl.CreateStyle();
                stColTextTitleLeft.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                stColTextTitleLeft.SetVerticalAlignment(VerticalAlignmentValues.Center);
                stColTextTitleLeft.SetWrapText(true);
                stColTextTitleLeft.Font.FontName = "Times New Roman";
                stColTextTitleLeft.Font.FontSize = 10;
                stColTextTitleLeft.Font.FontColor = System.Drawing.Color.Black;
            }
            void styleColTextTitleLeftBold()
            {
                stColTextTitleLeftBold = this.sl.CreateStyle();
                stColTextTitleLeftBold.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                stColTextTitleLeftBold.SetVerticalAlignment(VerticalAlignmentValues.Center);
                stColTextTitleLeftBold.SetWrapText(true);
                stColTextTitleLeftBold.Font.FontName = "Times New Roman";
                stColTextTitleLeftBold.Font.FontSize = 10;
                stColTextTitleLeftBold.Font.FontColor = System.Drawing.Color.Black;
                stColTextTitleLeftBold.Font.Bold = true;
            }
            void styleColTextTitleCenter()
            {
                stColTextTitleCenter = this.sl.CreateStyle();
                stColTextTitleCenter.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                stColTextTitleCenter.SetVerticalAlignment(VerticalAlignmentValues.Center);
                stColTextTitleCenter.SetWrapText(true);
                stColTextTitleCenter.Font.FontName = "Times New Roman";
                stColTextTitleCenter.Font.FontSize = 13;
                stColTextTitleCenter.Font.FontColor = System.Drawing.Color.Black;
                stColTextTitleCenter.Font.Bold = true;
            }
            void styleColTextTitleFooterBold()
            {
                stColTextTitleFooterBold = this.sl.CreateStyle();
                stColTextTitleFooterBold.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                stColTextTitleFooterBold.SetVerticalAlignment(VerticalAlignmentValues.Center);
                stColTextTitleFooterBold.SetWrapText(true);
                stColTextTitleFooterBold.Font.FontName = "Times New Roman";
                stColTextTitleFooterBold.Font.FontSize = 10;
                stColTextTitleFooterBold.Font.FontColor = System.Drawing.Color.Black;
                stColTextTitleFooterBold.Font.Bold = true;
            }
        }




        //// new 

        public SystemMess BN_LoadReporter(int loaitaikhoan, string tendoan, string tendaibieu, int dkphong, int dkxe, DateTime dDKStart, DateTime dDKEnd, bool ckDStart, bool ckDEnd)
        {

            var session = (SessionUser)HttpContext.Current.Session["uSession"];
            var danhsach = entity.m_taikhoan.ToList().Where(a =>
                       a.trangthai.Value == 3 && (entity.m_quyentaikhoan.Where(m => m.idtaikhoan.Value == a.id && m.trangthai == 3 && m.idquyen == 4).Any() == true) &&
                       ((loaitaikhoan != 0) ? a.idloaitaikhoan.Value == loaitaikhoan : true) &&
                       ((!string.IsNullOrEmpty(tendoan) && (a.idcoquan != null)) ? (a.m_thongtincoquan.tencoquan.ToLower().Trim().Contains(tendoan.ToLower().Trim())) : true) &&
                        (entity.m_thongtincanhan.Where(m => m.idtaikhoan == a.id && m.trangthai == 3 &&
                            ((!string.IsNullOrEmpty(tendaibieu)) ? (m.hovaten.ToLower().Trim().Contains(tendaibieu)) : true) &&
                            ((dkphong == 1 && m.dangkyphong != null) ? m.dangkyphong == dkphong : true) &&
                            ((dkxe == 1 && m.dangkyxe != null) ? m.dangkyxe == dkxe : true)
                            ).Any() == true) &&

                       ((ckDStart == true && ckDEnd == true) ? ((CheckDateTime.ValidateDate_Start(a.ngaytao.Value.ToString("dd/MM/yyyy"), dDKStart) == true) && (CheckDateTime.ValidateDate_End(a.ngaytao.Value.ToString("dd/MM/yyyy"), dDKEnd) == true)) : true) &&
                       ((ckDStart == true && ckDEnd == false) ? (CheckDateTime.ValidateDate_Start(a.ngaytao.Value.ToString("dd/MM/yyyy"), dDKStart) == true) : true) &&
                       ((ckDStart == false && ckDEnd == true) ? (CheckDateTime.ValidateDate_End(a.ngaytao.Value.ToString("dd/MM/yyyy"), dDKEnd) == true) : true)
                       ).Select(a => new
                   {
                       a.id,
                       email = ((a.email != null) ? a.email : "không xác định"),
                       hovaten = new AccountUntil().GetFullNameByIdTK(a.id),
                       ngaytao = ((a.ngaytao != null) ? a.ngaytao.Value.ToString("dd/MM/yyyy") : "không xác định"),
                       tenloaitaikhoan = ((a.idloaitaikhoan != null) ? a.m_loaitaikhoan.tenloaitaikhoan : "không xác định"),
                       tencoquan = ((a.idcoquan != null) ? (a.m_thongtincoquan.tencoquan) : "không xác định"),
                       diachicoquan = ((a.idcoquan != null) ? (a.m_thongtincoquan.diachicoquan) : "không xác định"),
                       emailcoquan = ((a.idcoquan != null) ? (a.m_thongtincoquan.emailcoquan) : "không xác định"),
                       sodienthoai = ((a.idcoquan != null) ? (a.m_thongtincoquan.sodienthoai) : "không xác định"),
                       thongtincanhan = entity.m_thongtincanhan.Where(m => m.idtaikhoan == a.id && m.trangthai == 3 && ((!string.IsNullOrEmpty(tendaibieu)) ? (m.hovaten.ToLower().Trim().Contains(tendaibieu)) : true) && ((dkphong == 1 && m.dangkyphong != null) ? m.dangkyphong == dkphong : true) && ((dkxe == 1 && m.dangkyxe != null) ? m.dangkyxe == dkxe : true)).ToList().Select(m => new
                       {
                           m.id,
                           m.hovaten,
                           m.chucvu,
                           m.email,
                           m.diachi,
                           m.sodienthoai,
                           dangkyxe = (m.dangkyxe == null || m.dangkyxe == 0) ? "Không" : "Có",
                           dangkyphong = (m.dangkyphong == null || m.dangkyphong == 0) ? "Không" : "Có",
                       })
                   }).OrderByDescending(a => a.id);


            SysMess.IsSuccess = true;
            SysMess.LstData = danhsach.Cast<object>().ToList();
            return SysMess;
        }

        public SystemMess BN_CreateStatisticalReporter(List<Statistical.Reporter> danhsach)
        {
           
            if (danhsach.Count() > 0)
            {
                int totalRow = danhsach.Count();
                int countChild = 0;

                SLDocument sl = new SLDocument();

                SLStyleExport style = new SLStyleExport(sl);// tạo style
                SLStyle stHeader = style.stHeader;
                SLStyle stColTextTitle = style.stColTextTitle;
                SLStyle stColDefault = style.stColDefault;
                SLStyle stColTextSTT = style.stColTextSTT;
                SLStyle stColTextSTTBold = style.stColTextSTTBold;
                SLStyle stColPriceBold = style.stColPriceBold;
                SLStyle stColPrice = style.stColPrice;
                SLStyle stColTextTitleLeft = style.stColTextTitleLeft;
                SLStyle stColTextTitleLeftBold = style.stColTextTitleLeftBold;
                SLStyle stColTextTitleCenter = style.stColTextTitleCenter;
                SLStyle stColTextTitleFooterBold = style.stColTextTitleFooterBold;

                sl.MergeWorksheetCells("A3", "I3");
                //sl.SetCellValue(3, 1, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", "THỐNG KÊ PHÓNG VIÊN(ĐOÀN PHÓNG VIÊN) THAM GIA HỘI NGHỊ")));
                sl.SetCellValue(3, 1, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.TitleReporters, SystemMessageConst.systemmessage.TitleReportersEn))));
                sl.SetCellStyle(3, 1, 3, 1, stColTextTitleCenter);

                int startTable = 8;

                sl.MergeWorksheetCells(string.Format("A{0}", startTable), string.Format("A{0}", startTable + 2));
                sl.SetCellValue(startTable, 1, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_STT, SystemMessageConst.systemmessage.Rp_STTEn))));
                sl.MergeWorksheetCells(string.Format("B{0}", startTable), string.Format("B{0}", startTable + 2));
                sl.SetCellValue(startTable, 2, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_AccountEmail, SystemMessageConst.systemmessage.Rp_AccountEmailEn))));
                sl.MergeWorksheetCells(string.Format("C{0}", startTable), string.Format("C{0}", startTable + 2));
                sl.SetCellValue(startTable, 3, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Namecompany, SystemMessageConst.systemmessage.Rp_NamecompanyEn))));
                sl.MergeWorksheetCells(string.Format("D{0}", startTable), string.Format("D{0}", startTable + 2));
                sl.SetCellValue(startTable, 4, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Datecreated, SystemMessageConst.systemmessage.Rp_DatecreatedEn))));
                sl.MergeWorksheetCells(string.Format("E{0}", startTable), string.Format("E{0}", startTable + 2));
                sl.SetCellValue(startTable, 5, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_TypeAccount, SystemMessageConst.systemmessage.Rp_TypeAccountEn))));
                sl.MergeWorksheetCells(string.Format("F{0}", startTable), string.Format("F{0}", startTable + 2));
                sl.SetCellValue(startTable, 6, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Emaicompany, SystemMessageConst.systemmessage.Rp_Emaicompany))));
                sl.MergeWorksheetCells(string.Format("G{0}", startTable), string.Format("G{0}", startTable + 2));
                sl.SetCellValue(startTable, 7, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Addconpany, SystemMessageConst.systemmessage.Rp_AddconpanyEn))));
                sl.MergeWorksheetCells(string.Format("H{0}", startTable), string.Format("H{0}", startTable + 2));
                sl.SetCellValue(startTable, 8, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Phonecompany, SystemMessageConst.systemmessage.Rp_PhonecompanyEn))));

                int ii = (startTable + 3);

                sl.SetCellStyle(startTable, 1, (startTable + 2), 8, stHeader);// style header
                sl.SetCellStyle((startTable + 3), 1, (totalRow + (startTable + 3)), 8, stColTextSTT);

                sl.SetColumnWidth(1, 10);
                sl.SetColumnWidth(2, 20);
                sl.SetColumnWidth(3, 30);
                sl.SetColumnWidth(4, 20);
                sl.SetColumnWidth(5, 20);
                sl.SetColumnWidth(6, 20);
                sl.SetColumnWidth(7, 20);
                sl.SetColumnWidth(8, 20);
                sl.SetColumnWidth(9, 20);
                sl.SetColumnWidth(10, 20);

                int startTableChildren = 0;

                for (int i = 0; i < danhsach.Count(); i++)
                {
                 
                    var objToInsert = danhsach.ElementAt(i);
                 
                    for (int icolumn = 1; icolumn <= headerColumns.Count(); icolumn++)
                    {
                        bool addnew = true;
                        string value = "";

                        string column = headerColumns.ElementAt(icolumn - 1);
                        if (column.Equals("A"))
                            value = string.Format("{0}", i + 1);
                        if (column.Equals("B"))
                            value = string.Format("{0}", objToInsert.email);
                        if (column.Equals("C"))
                            value = string.Format("{0}", objToInsert.tencoquan);
                        if (column.Equals("D"))
                            value = string.Format("{0}", objToInsert.ngaytao);
                        if (column.Equals("E"))
                            value = string.Format("{0}", objToInsert.tenloaitaikhoan);
                        if (column.Equals("F"))
                            value = string.Format("{0}", objToInsert.emailcoquan);
                        if (column.Equals("G"))
                            value = string.Format("{0}", objToInsert.diachicoquan);
                        if (column.Equals("H"))
                            value = string.Format("{0}", objToInsert.sodienthoai);

                        if (addnew)
                        {

                            startTableChildren = ii + 2;
                            sl.SetCellValue(ii, icolumn, value);

                            //// thong tin thanh vien
                            if (objToInsert.thongtincanhan.Count() > 0)
                            {
                                int iii = (startTableChildren + 3);

                                countChild = objToInsert.thongtincanhan.Count();
                                int totalRowChildren = objToInsert.thongtincanhan.Count();


                                sl.SetCellStyle(startTableChildren, 1, (iii + countChild), 2, stHeader);// style header
                                sl.SetCellStyle(startTableChildren, 1, (iii + countChild), 2, stColTextSTT);
                                sl.MergeWorksheetCells(string.Format("A{0}", startTableChildren), string.Format("B{0}", (iii + countChild)));
                                sl.SetCellValue((startTableChildren), 1, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_ListMember, SystemMessageConst.systemmessage.Rp_ListMemberEn))));


                                sl.MergeWorksheetCells(string.Format("C{0}", startTableChildren), string.Format("C{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 3, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_STT, SystemMessageConst.systemmessage.Rp_STTEn))));
                                sl.MergeWorksheetCells(string.Format("D{0}", startTableChildren), string.Format("D{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 4, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Fullname, SystemMessageConst.systemmessage.Rp_FullnameEn))));
                                sl.MergeWorksheetCells(string.Format("E{0}", startTableChildren), string.Format("E{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 5, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Position, SystemMessageConst.systemmessage.Rp_PositionEn))));
                                sl.MergeWorksheetCells(string.Format("F{0}", startTableChildren), string.Format("F{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 6, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Phonecompany, SystemMessageConst.systemmessage.Rp_PhonecompanyEn))));
                                sl.MergeWorksheetCells(string.Format("G{0}", startTableChildren), string.Format("G{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 7, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Email, SystemMessageConst.systemmessage.Rp_EmailEn))));
                                sl.MergeWorksheetCells(string.Format("H{0}", startTableChildren), string.Format("H{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 8, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Address, SystemMessageConst.systemmessage.Rp_AddressEn))));
                                sl.MergeWorksheetCells(string.Format("I{0}", startTableChildren), string.Format("I{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 9, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Registerroom, SystemMessageConst.systemmessage.Rp_RegisterroomEn))));
                                sl.MergeWorksheetCells(string.Format("J{0}", startTableChildren), string.Format("J{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 10, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Registervehicle, SystemMessageConst.systemmessage.Rp_RegistervehicleEn))));

                                sl.SetCellStyle(startTableChildren, 3, (startTableChildren + 2), 10, stHeader);// style header
                                sl.SetCellStyle((startTableChildren + 3), 3, (totalRowChildren + (startTableChildren + 3)), 10, stColTextSTT);

                                for (int j = 0; j < objToInsert.thongtincanhan.Count(); j++)
                                {
                                    var objToInsertChildren = objToInsert.thongtincanhan.ElementAt(j);

                                    for (int iclChil = 3; iclChil <= headerColumns.Count(); iclChil++)
                                    {
                                        bool addChildren = true;
                                        string valueChildren = "";

                                        string columnChildren = headerColumns.ElementAt(iclChil - 1);
                                        if (columnChildren.Equals("C"))
                                            valueChildren = string.Format("{0}", j + 1);
                                        if (columnChildren.Equals("D"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.hovaten);
                                        if (columnChildren.Equals("E"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.chucvu);
                                        if (columnChildren.Equals("F"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.sodienthoai);
                                        if (columnChildren.Equals("G"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.email);
                                        if (columnChildren.Equals("H"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.diachi);
                                        if (columnChildren.Equals("I"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.dangkyphong);
                                        if (columnChildren.Equals("J"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.dangkyxe);

                                        if (addChildren)
                                        {
                                            sl.SetCellValue(iii, iclChil, valueChildren);
                                        }
                                    }
                                    iii++;
                                }
                            }
                            /// HẾT
                        }
                    }
                    ii = ii + countChild + startTable;
                }

                SLTable tbl = sl.CreateTable(string.Format("A{0}", (startTable)), string.Format("J{0}", (totalRow + (startTable + 1))));

                tbl.SetTableStyle(SLTableStyleTypeValues.Medium28);
                sl.InsertTable(tbl);


                string tenbaocao = string.Format("THONG_KE_PHONG_VIEN_(DOAN_PHONG_VIEN)_NGAY_{0:dd_MM_yyyy}.xlsx", DateTime.Now);
                string path = UrlConstant.UrlConst.StatisticalFolder + tenbaocao;
                bool checkFile = new DeleteFileUntil().DeleteFileByPath(path);

                var a = HttpContext.Current.Server.MapPath("~" + UrlConstant.UrlConst.StatisticalFolder) + tenbaocao;

                sl.SaveAs(a);

                SysMess.IsSuccess = true;
              //  SysMess.Message = SystemMessageConst.systemmessage.IsCreateReportSuccess;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsCreateReportSuccess, SystemMessageConst.systemmessage.IsCreateReportSuccessEn);
                SysMess.ReturnValue = path;
            }
            else
            {
                SysMess.IsSuccess = true;
                //SysMess.Message = SystemMessageConst.systemmessage.IsNotExist;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsNotExist, SystemMessageConst.systemmessage.IsNotExistEn);
            }
            return SysMess;
        }

        public SystemMess BN_LoadDelegate(int loaitaikhoan, string tendoan, string tendaibieu, int dkphong, int dkxe, DateTime dDKStart, DateTime dDKEnd, bool ckDStart, bool ckDEnd)
        {

            var session = (SessionUser)HttpContext.Current.Session["uSession"];
            var danhsach = entity.m_taikhoan.ToList().Where(a =>
                       a.trangthai.Value == 3 && (entity.m_quyentaikhoan.Where(m => m.idtaikhoan.Value == a.id && m.trangthai == 3 && m.idquyen == 5).Any() == true) &&
                       ((loaitaikhoan != 0) ? a.idloaitaikhoan.Value == loaitaikhoan : true) &&
                       ((!string.IsNullOrEmpty(tendoan) && (a.idcoquan != null)) ? (a.m_thongtincoquan.tencoquan.ToLower().Trim().Contains(tendoan.ToLower().Trim())) : true) &&
                        (entity.m_thongtincanhan.Where(m => m.idtaikhoan == a.id && m.trangthai == 3 &&
                            ((!string.IsNullOrEmpty(tendaibieu)) ? (m.hovaten.ToLower().Trim().Contains(tendaibieu)) : true) &&
                            ((dkphong == 1 && m.dangkyphong != null) ? m.dangkyphong == dkphong : true) &&
                            ((dkxe == 1 && m.dangkyxe != null) ? m.dangkyxe == dkxe : true)
                            ).Any() == true) &&

                       ((ckDStart == true && ckDEnd == true) ? ((CheckDateTime.ValidateDate_Start(a.ngaytao.Value.ToString("dd/MM/yyyy"), dDKStart) == true) && (CheckDateTime.ValidateDate_End(a.ngaytao.Value.ToString("dd/MM/yyyy"), dDKEnd) == true)) : true) &&
                       ((ckDStart == true && ckDEnd == false) ? (CheckDateTime.ValidateDate_Start(a.ngaytao.Value.ToString("dd/MM/yyyy"), dDKStart) == true) : true) &&
                       ((ckDStart == false && ckDEnd == true) ? (CheckDateTime.ValidateDate_End(a.ngaytao.Value.ToString("dd/MM/yyyy"), dDKEnd) == true) : true)
                       ).Select(a => new
                       {
                           a.id,
                           email = ((a.email != null) ? a.email : "không xác định"),
                           hovaten = new AccountUntil().GetFullNameByIdTK(a.id),
                           ngaytao = ((a.ngaytao != null) ? a.ngaytao.Value.ToString("dd/MM/yyyy") : "không xác định"),
                           tenloaitaikhoan = ((a.idloaitaikhoan != null) ? a.m_loaitaikhoan.tenloaitaikhoan : "không xác định"),
                           tencoquan = ((a.idcoquan != null) ? (a.m_thongtincoquan.tencoquan) : "không xác định"),
                           diachicoquan = ((a.idcoquan != null) ? (a.m_thongtincoquan.diachicoquan) : "không xác định"),
                           emailcoquan = ((a.idcoquan != null) ? (a.m_thongtincoquan.emailcoquan) : "không xác định"),
                           sodienthoai = ((a.idcoquan != null) ? (a.m_thongtincoquan.sodienthoai) : "không xác định"),
                           thongtincanhan = entity.m_thongtincanhan.Where(m => m.idtaikhoan == a.id && m.trangthai == 3 && ((!string.IsNullOrEmpty(tendaibieu)) ? (m.hovaten.ToLower().Trim().Contains(tendaibieu)) : true) && ((dkphong == 1 && m.dangkyphong != null) ? m.dangkyphong == dkphong : true) && ((dkxe == 1 && m.dangkyxe != null) ? m.dangkyxe == dkxe : true)).ToList().Select(m => new
                           {
                               m.id,
                               m.hovaten,
                               m.chucvu,
                               m.email,
                               m.diachi,
                               m.sodienthoai,
                               dangkyxe = (m.dangkyxe == null || m.dangkyxe == 0) ? "Không" : "Có",
                               dangkyphong = (m.dangkyphong == null || m.dangkyphong == 0) ? "Không" : "Có",
                           })
                       }).OrderByDescending(a => a.id);


            SysMess.IsSuccess = true;
            SysMess.LstData = danhsach.Cast<object>().ToList();
            return SysMess;
        }

        public SystemMess BN_CreateStatisticalDelegate(List<Statistical.Reporter> danhsach)
        {

            if (danhsach.Count() > 0)
            {
                int totalRow = danhsach.Count();
                int countChild = 0;

                SLDocument sl = new SLDocument();

                SLStyleExport style = new SLStyleExport(sl);// tạo style
                SLStyle stHeader = style.stHeader;
                SLStyle stColTextTitle = style.stColTextTitle;
                SLStyle stColDefault = style.stColDefault;
                SLStyle stColTextSTT = style.stColTextSTT;
                SLStyle stColTextSTTBold = style.stColTextSTTBold;
                SLStyle stColPriceBold = style.stColPriceBold;
                SLStyle stColPrice = style.stColPrice;
                SLStyle stColTextTitleLeft = style.stColTextTitleLeft;
                SLStyle stColTextTitleLeftBold = style.stColTextTitleLeftBold;
                SLStyle stColTextTitleCenter = style.stColTextTitleCenter;
                SLStyle stColTextTitleFooterBold = style.stColTextTitleFooterBold;

                sl.MergeWorksheetCells("A3", "I3");
                sl.SetCellValue(3, 1, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.TitleDelegate, SystemMessageConst.systemmessage.TitleDelegateEn))));
                sl.SetCellStyle(3, 1, 3, 1, stColTextTitleCenter);

                int startTable = 8;

                sl.MergeWorksheetCells(string.Format("A{0}", startTable), string.Format("A{0}", startTable + 2));
                sl.SetCellValue(startTable, 1, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_STT, SystemMessageConst.systemmessage.Rp_STTEn))));
                sl.MergeWorksheetCells(string.Format("B{0}", startTable), string.Format("B{0}", startTable + 2));
                sl.SetCellValue(startTable, 2, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_AccountEmail, SystemMessageConst.systemmessage.Rp_AccountEmailEn))));
                sl.MergeWorksheetCells(string.Format("C{0}", startTable), string.Format("C{0}", startTable + 2));
                sl.SetCellValue(startTable, 3, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Namecompany, SystemMessageConst.systemmessage.Rp_NamecompanyEn))));
                sl.MergeWorksheetCells(string.Format("D{0}", startTable), string.Format("D{0}", startTable + 2));
                sl.SetCellValue(startTable, 4, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Datecreated, SystemMessageConst.systemmessage.Rp_DatecreatedEn))));
                sl.MergeWorksheetCells(string.Format("E{0}", startTable), string.Format("E{0}", startTable + 2));
                sl.SetCellValue(startTable, 5, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_TypeAccount, SystemMessageConst.systemmessage.Rp_TypeAccountEn))));
                sl.MergeWorksheetCells(string.Format("F{0}", startTable), string.Format("F{0}", startTable + 2));
                sl.SetCellValue(startTable, 6, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Emaicompany, SystemMessageConst.systemmessage.Rp_EmaicompanyEn))));
                sl.MergeWorksheetCells(string.Format("G{0}", startTable), string.Format("G{0}", startTable + 2));
                sl.SetCellValue(startTable, 7, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Addconpany, SystemMessageConst.systemmessage.Rp_AddconpanyEn))));
                sl.MergeWorksheetCells(string.Format("H{0}", startTable), string.Format("H{0}", startTable + 2));
                sl.SetCellValue(startTable, 8, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Phonecompany, SystemMessageConst.systemmessage.Rp_PhonecompanyEn))));

                int ii = (startTable + 3);

                sl.SetCellStyle(startTable, 1, (startTable + 2), 8, stHeader);// style header
                sl.SetCellStyle((startTable + 3), 1, (totalRow + (startTable + 3)), 8, stColTextSTT);

                sl.SetColumnWidth(1, 10);
                sl.SetColumnWidth(2, 20);
                sl.SetColumnWidth(3, 30);
                sl.SetColumnWidth(4, 20);
                sl.SetColumnWidth(5, 20);
                sl.SetColumnWidth(6, 20);
                sl.SetColumnWidth(7, 20);
                sl.SetColumnWidth(8, 20);
                sl.SetColumnWidth(9, 20);
                sl.SetColumnWidth(10, 20);

                int startTableChildren = 0;

                for (int i = 0; i < danhsach.Count(); i++)
                {

                    var objToInsert = danhsach.ElementAt(i);

                    for (int icolumn = 1; icolumn <= headerColumns.Count(); icolumn++)
                    {
                        bool addnew = true;
                        string value = "";

                        string column = headerColumns.ElementAt(icolumn - 1);
                        if (column.Equals("A"))
                            value = string.Format("{0}", i + 1);
                        if (column.Equals("B"))
                            value = string.Format("{0}", objToInsert.email);
                        if (column.Equals("C"))
                            value = string.Format("{0}", objToInsert.tencoquan);
                        if (column.Equals("D"))
                            value = string.Format("{0}", objToInsert.ngaytao);
                        if (column.Equals("E"))
                            value = string.Format("{0}", objToInsert.tenloaitaikhoan);
                        if (column.Equals("F"))
                            value = string.Format("{0}", objToInsert.emailcoquan);
                        if (column.Equals("G"))
                            value = string.Format("{0}", objToInsert.diachicoquan);
                        if (column.Equals("H"))
                            value = string.Format("{0}", objToInsert.sodienthoai);

                        if (addnew)
                        {

                            startTableChildren = ii + 2;
                            sl.SetCellValue(ii, icolumn, value);

                            //// thong tin thanh vien
                            if (objToInsert.thongtincanhan.Count() > 0)
                            {
                                int iii = (startTableChildren + 3);

                                countChild = objToInsert.thongtincanhan.Count();
                                int totalRowChildren = objToInsert.thongtincanhan.Count();


                                sl.SetCellStyle(startTableChildren, 1, (iii + countChild), 2, stHeader);// style header
                                sl.SetCellStyle(startTableChildren, 1, (iii + countChild), 2, stColTextSTT);
                                sl.MergeWorksheetCells(string.Format("A{0}", startTableChildren), string.Format("B{0}", (iii + countChild)));
                                sl.SetCellValue((startTableChildren), 1, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_ListMember, SystemMessageConst.systemmessage.Rp_ListMemberEn))));


                                sl.MergeWorksheetCells(string.Format("C{0}", startTableChildren), string.Format("C{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 3, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_STT, SystemMessageConst.systemmessage.Rp_STTEn))));
                                sl.MergeWorksheetCells(string.Format("D{0}", startTableChildren), string.Format("D{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 4, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Fullname, SystemMessageConst.systemmessage.Rp_FullnameEn))));
                                sl.MergeWorksheetCells(string.Format("E{0}", startTableChildren), string.Format("E{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 5, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Position, SystemMessageConst.systemmessage.Rp_PositionEn))));
                                sl.MergeWorksheetCells(string.Format("F{0}", startTableChildren), string.Format("F{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 6, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Phonecompany, SystemMessageConst.systemmessage.Rp_PhonecompanyEn))));
                                sl.MergeWorksheetCells(string.Format("G{0}", startTableChildren), string.Format("G{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 7, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Email, SystemMessageConst.systemmessage.Rp_EmailEn))));
                                sl.MergeWorksheetCells(string.Format("H{0}", startTableChildren), string.Format("H{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 8, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Address, SystemMessageConst.systemmessage.Rp_AddressEn))));
                                sl.MergeWorksheetCells(string.Format("I{0}", startTableChildren), string.Format("I{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 9, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Registerroom, SystemMessageConst.systemmessage.Rp_RegisterroomEn))));
                                sl.MergeWorksheetCells(string.Format("J{0}", startTableChildren), string.Format("J{0}", startTableChildren + 2));
                                sl.SetCellValue(startTableChildren, 10, SpreadsheetLightExport.CreateStringInline(string.Format("{0}", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Rp_Registervehicle, SystemMessageConst.systemmessage.Rp_RegistervehicleEn))));

                                sl.SetCellStyle(startTableChildren, 3, (startTableChildren + 2), 10, stHeader);// style header
                                sl.SetCellStyle((startTableChildren + 3), 3, (totalRowChildren + (startTableChildren + 3)), 10, stColTextSTT);

                                for (int j = 0; j < objToInsert.thongtincanhan.Count(); j++)
                                {
                                    var objToInsertChildren = objToInsert.thongtincanhan.ElementAt(j);

                                    for (int iclChil = 3; iclChil <= headerColumns.Count(); iclChil++)
                                    {
                                        bool addChildren = true;
                                        string valueChildren = "";

                                        string columnChildren = headerColumns.ElementAt(iclChil - 1);
                                        if (columnChildren.Equals("C"))
                                            valueChildren = string.Format("{0}", j + 1);
                                        if (columnChildren.Equals("D"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.hovaten);
                                        if (columnChildren.Equals("E"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.chucvu);
                                        if (columnChildren.Equals("F"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.sodienthoai);
                                        if (columnChildren.Equals("G"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.email);
                                        if (columnChildren.Equals("H"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.diachi);
                                        if (columnChildren.Equals("I"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.dangkyphong);
                                        if (columnChildren.Equals("J"))
                                            valueChildren = string.Format("{0}", objToInsertChildren.dangkyxe);

                                        if (addChildren)
                                        {
                                            sl.SetCellValue(iii, iclChil, valueChildren);
                                        }
                                    }
                                    iii++;
                                }
                            }
                            /// HẾT
                        }
                    }
                    ii = ii + countChild + startTable;
                }

                SLTable tbl = sl.CreateTable(string.Format("A{0}", (startTable)), string.Format("J{0}", (totalRow + (startTable + 1))));

                tbl.SetTableStyle(SLTableStyleTypeValues.Medium28);
                sl.InsertTable(tbl);


                string tenbaocao = string.Format("THONG_KE_DAI_BIEU_(DOAN_DAI_BIEU)_NGAY_{0:dd_MM_yyyy}.xlsx", DateTime.Now);
                string path = UrlConstant.UrlConst.StatisticalFolder + tenbaocao;
                bool checkFile = new DeleteFileUntil().DeleteFileByPath(path);

                var a = HttpContext.Current.Server.MapPath("~" + UrlConstant.UrlConst.StatisticalFolder) + tenbaocao;

                sl.SaveAs(a);

                SysMess.IsSuccess = true;
               // SysMess.Message = SystemMessageConst.systemmessage.IsCreateReportSuccess;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsCreateReportSuccess, SystemMessageConst.systemmessage.IsCreateReportSuccessEn);
                SysMess.ReturnValue = path;
            }
            else
            {
                SysMess.IsSuccess = true;
               // SysMess.Message = SystemMessageConst.systemmessage.IsNotExist;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsNotExist, SystemMessageConst.systemmessage.IsNotExistEn);
            }
            return SysMess;
        }
    }
}