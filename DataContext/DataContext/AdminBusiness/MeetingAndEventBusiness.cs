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
using DataContext.DAL;
using System.Globalization;
using System.Data.Entity.Core.Objects;

namespace DataContext.AdminBusiness
{
    [CheckSessionUser]
    public class MeetingAndEventBusiness
    {
        SystemMess SysMess = new SystemMess();
        ASS_35Entities entity = new ASS_35Entities();
        SessionUser session = (SessionUser)HttpContext.Current.Session["uSession"];
        //public ReturnMeetingAndEvent LoadAllMeetingAndEvent(bool bsearch, string search, int trangthai, int CurentPage)
        //{
        //    try
        //    {
        //        string today = DateTime.Now.ToString("yyyy-MM-dd");
        //        DateTime date = DateTime.Parse(today);
        //        var session = (SessionUser)HttpContext.Current.Session["uSession"];
        //        int stSearch = 0;
        //        if (bsearch == true)
        //        {
        //            stSearch = 1;
        //        }
        //        ReturnMeetingAndEvent rt = new DAL.ReturnMeetingAndEvent();
        //        List<MeetingEvent> lstVideo = new List<DAL.MeetingEvent>();

        //        ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

        //        var Result = entity.LOADALL_MEETING(search, stSearch, trangthai, today, session.id, session.idquyentaikhoan, CurentPage, 10, totalCount).ToList().All(a =>
        //        {
        //            MeetingEvent lst = new DAL.MeetingEvent();

        //            lst.id = a.idhop;
        //            lst.thoigiantao = a.thoigiantao.ToString("dd/MM/yyyy");
        //            lst.idnguoitao = a.idnguoitao;
        //            lst.tennguoitao = new AccountUntil().GetFullNameByIdTK(a.idnguoitao);
        //            lst.trangthai = a.trangthaihop;
        //            lst.idnguoiduyet = (a.idnguoiduyet != null) ? a.idnguoiduyet.Value : 0;
        //            lst.trangthaihienthi = (a.trangthaihop == 0) ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsDeleted, SystemMessageConst.systemmessage.IsDeletedEn) : (a.trangthaihop == 1 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Deactive, SystemMessageConst.systemmessage.DeactiveEn) : (a.trangthaihop == 2 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Pending, SystemMessageConst.systemmessage.PendingEn) : (a.trangthaihop == 3 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Active, SystemMessageConst.systemmessage.ActiveEn) : (a.trangthaihop == 4 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Save_Draft, SystemMessageConst.systemmessage.Save_DraftEn) : (a.trangthaihop == 5 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.RemoveOnSite, SystemMessageConst.systemmessage.RemoveOnSiteEn) : DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unknown, SystemMessageConst.systemmessage.UnknownEn))))));
        //            lst.tencuochop = a.tencuochop;
        //            lst.phienban = a.phienban;
        //            lst.lydotuchoi = a.lydotuchoi;
        //            lst.diadiem = a.diadiemhop;
        //            lst.thoigianduyet = (a.thoigianduyet == null ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn) : a.thoigianduyet.Value.ToString("dd/MM/yyyy"));
        //            lst.chitiet = entity.m_chitietsukienhop.Where(m => m.idsukienhop == a.idhop && m.trangthai.Value == 3 && m.ngayhop.Value.Equals(date) == true).ToList().Select(m => new DetailMeetingEvent
        //            {
        //                id = m.id,
        //                ten = m.ten,
        //                diadiem = m.diadiem,
        //                thoigianbatdau = m.thoigianbatdau.ToString(),
        //                thoigianketthuc = m.thoigianketthuc.ToString(),
        //                ngayhop = m.ngayhop.Value.ToString("dd/MM/yyyy"),
        //                noidung = m.noidung
        //            }).ToList();
        //            lstVideo.Add(lst);
        //            return true;
        //        });
        //        rt.data = lstVideo;
        //        rt.totalCount = int.Parse(totalCount.Value.ToString());
        //        return rt;
        //    }
        //    catch (Exception)
        //    {
        //        ReturnMeetingAndEvent rt = new DAL.ReturnMeetingAndEvent();
        //        rt.totalCount = 0;
        //        rt.data = null;
        //        return rt;
        //    }
        //}


        public ReturnMeetingAndEvent LoadAllMeetingAndEvent(bool bsearch, string search, int trangthai, int CurentPage)
        {
            try
            {
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime date = DateTime.Parse(today);
                var session = (SessionUser)HttpContext.Current.Session["uSession"];
                int stSearch = 0;
                if (bsearch == true)
                {
                    stSearch = 1;
                }
                ReturnMeetingAndEvent rt = new DAL.ReturnMeetingAndEvent();
                List<MeetingEvent> lstVideo = new List<DAL.MeetingEvent>();

                ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

                var Result = entity.SP_MANAGER_MEETING_EVENT(search, stSearch, trangthai, session.id, session.idquyentaikhoan, CurentPage, 10, totalCount).ToList().All(a =>
                {
                    MeetingEvent lst = new DAL.MeetingEvent();

                    lst.id = a.idhop;
                    lst.thoigiantao = a.thoigiantao.ToString("dd/MM/yyyy");
                    lst.idnguoitao = a.idnguoitao;
                    lst.tennguoitao = new AccountUntil().GetFullNameByIdTK(a.idnguoitao);
                    lst.trangthai = a.trangthaihop;
                    lst.idnguoiduyet = (a.idnguoiduyet != null) ? a.idnguoiduyet.Value : 0;
                    lst.trangthaihienthi = (a.trangthaihop == 0) ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsDeleted, SystemMessageConst.systemmessage.IsDeletedEn) : (a.trangthaihop == 1 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Deactive, SystemMessageConst.systemmessage.DeactiveEn) : (a.trangthaihop == 2 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Pending, SystemMessageConst.systemmessage.PendingEn) : (a.trangthaihop == 3 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Active, SystemMessageConst.systemmessage.ActiveEn) : (a.trangthaihop == 4 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Save_Draft, SystemMessageConst.systemmessage.Save_DraftEn) : (a.trangthaihop == 5 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.RemoveOnSite, SystemMessageConst.systemmessage.RemoveOnSiteEn) : DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unknown, SystemMessageConst.systemmessage.UnknownEn))))));
                    lst.tencuochop = a.tencuochop;
                    lst.phienban = a.phienban;
                    lst.lydotuchoi = a.lydotuchoi;
                    lst.ngayhop = a.ngayhop.Value.ToString("dd/MM/yyyy");
                    lst.diadiem = a.diadiemhop;
                    lst.thoigianduyet = (a.thoigianduyet == null ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn) : a.thoigianduyet.Value.ToString("dd/MM/yyyy"));
                    lst.chitiet = entity.m_chitietsukienhop.Where(m => m.idsukienhop == a.idhop && m.trangthai.Value == 3).ToList().Select(m => new DetailMeetingEvent
                    {
                        id = m.id,
                        ten = m.ten,
                        diadiem = m.diadiem,
                        thoigianbatdau = m.thoigianbatdau.ToString(),
                        thoigianketthuc = m.thoigianketthuc.ToString(),
                        ngayhop = m.ngayhop.Value.ToString("dd/MM/yyyy"),
                        noidung = m.noidung
                    }).ToList();
                    lstVideo.Add(lst);
                    return true;
                });
                rt.data = lstVideo;
                rt.totalCount = int.Parse(totalCount.Value.ToString());
                return rt;
            }
            catch (Exception)
            {
                ReturnMeetingAndEvent rt = new DAL.ReturnMeetingAndEvent();
                rt.totalCount = 0;
                rt.data = null;
                return rt;
            }
        }
        public SystemMess BN_DeleteMeetingAndEvent(int id)
        {
            bool statusDell = false;
            var checkItem = entity.m_sukienhop.Where(m => m.id == id).FirstOrDefault();
            if (checkItem != null)
            {
                var thongtintk = entity.m_taikhoan.Where(a => a.id == checkItem.idnguoitao).ToList().Select(a => new
                {
                    a.id,
                    a.email,
                    matkhau = a.matkhau,
                    tendaydu = new AccountUntil().GetFullNameByIdTK(a.id),
                }).FirstOrDefault();

                if (session.idquyentaikhoan == 1)
                {
                    statusDell = true;
                }
                else if (session.idquyentaikhoan != 1 && checkItem.idnguoitao == session.id && checkItem.trangthai != 3)
                {
                    statusDell = true;
                }
                else
                {
                    SysMess.IsSuccess = false;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteFail, SystemMessageConst.systemmessage.DeleteFailEn);
                }
                if (statusDell)
                {
                    checkItem.trangthai = 0;
                    entity.SaveChanges();
                    SysMess.IsSuccess = true;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteSuccess, SystemMessageConst.systemmessage.DeleteSuccessEn);

                    string mailto = thongtintk.email;
                    string subject = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HeaderEmail, ListMailAddress.ContentEmailSystem.HeaderEmailEn);
                    string body = "";
                    body = string.Format("<div style='padding: 30px;border: 1px solid green;border-radius: 5px;'>"
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HelloAccount, ListMailAddress.ContentEmailSystem.HelloAccountEn) + " <span style='color:red;'>{0}</span></label></div><br />"
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.DeleteMeeting.Content1, ListMailAddress.DeleteMeeting.ContentEn1) + "</label></div><br />"
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.DeleteMeeting.Content2, ListMailAddress.DeleteMeeting.ContentEn2) + "</label></div><br />"
                                    + "<ul>"
                                    + "<li  style='padding: 5px;'><b> {1} </b></li>"
                                    + "</ul> "
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.Topic.DeleteTopPic2, ListMailAddress.Topic.DeleteTopPicEn2) + "</label></div><br />"
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.Topic.DeleteTopPic3, ListMailAddress.Topic.DeleteTopPicEn3) + "</label></div><br />"
                                    + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.FooterEmail, ListMailAddress.ContentEmailSystem.FooterEmailEn)
                                    + "</div>",
                                    thongtintk.tendaydu, checkItem.tencuochop);

                    bool a = SendEmailUntil.SendMail(mailto, body, subject);
                }
                else
                {
                    SysMess.IsSuccess = false;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteFail, SystemMessageConst.systemmessage.DeleteFailEn);
                }
            }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DataIsEmpty, SystemMessageConst.systemmessage.DataIsEmptyEn);
            }
            return SysMess;
        }

        public SystemMess BN_UpdateMeetingAndEvent(string tencuochop, string diadiem, int phienban, int id, int trangthai, string lydo, string startDate)
        {
            string msg = "";
            var checkExit = entity.m_sukienhop.Where(m => m.id == id).FirstOrDefault();

            if (checkExit == null)
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsNotExist, SystemMessageConst.systemmessage.IsNotExistEn);
                return SysMess;
            }

            var errorTen = new ValidateForm().CheckRequiredField(tencuochop, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NameMeeting, SystemMessageConst.systemmessage.NameMeetingEn), 3);
            if (errorTen.error == null) { tencuochop = errorTen.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTen.error;
                return SysMess;
            }

            var errorGioithieu = new ValidateForm().CheckRequiredField(diadiem, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Place, SystemMessageConst.systemmessage.PlaceEn), 3);
            if (errorGioithieu.error == null) { diadiem = errorGioithieu.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorGioithieu.error;
                return SysMess;
            }
            if (string.IsNullOrEmpty(startDate))
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DateStartMeetingNotNull, SystemMessageConst.systemmessage.DateStartMeetingNotNullEn);
                return SysMess;
            }
            if (phienban != 1 && phienban != 2)
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Select_Languge, SystemMessageConst.systemmessage.Select_LangugeEn);
                return SysMess;
            }

            if (session.id == checkExit.idnguoitao && checkExit.trangthai != 3)
            {
                checkExit.tencuochop = tencuochop;
                checkExit.diadiem = diadiem;
                checkExit.phienban = phienban;
                checkExit.ngayhop = DateTime.Parse(startDate);
                if (session.idquyentaikhoan == 1)
                {
                    if (checkExit.trangthai != trangthai)
                    {
                        checkExit.trangthai = trangthai;
                        checkExit.idnguoiduyet = session.id;
                        checkExit.thoigianduyet = DateTime.Now;
                        if (trangthai == 1)
                        {
                            checkExit.lydotuchoi = (string.IsNullOrEmpty(lydo)) ? '"' + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizers, SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizersEn) + '"' : lydo;
                        }
                        else
                        {
                            checkExit.lydotuchoi = null;
                        }
                        //   msg = "Cập nhật thông tin và phê duyệt chủ đề thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Update_and_approve_topic_successfully, SystemMessageConst.systemmessage.Update_and_approve_topic_successfullyEn);
                    }
                    else
                    {
                        //  msg = "Cập nhật thông tin chủ đề thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Update_topic_successfully, SystemMessageConst.systemmessage.Update_topic_successfullyEn);
                    }
                }
                else
                {
                    if (checkExit.trangthai != trangthai && trangthai != 3)
                    {
                        checkExit.trangthai = trangthai;
                        //    msg = "Cập nhật thông tin và đổi trạng thái chủ đề thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Update_and_approve_topic_successfully, SystemMessageConst.systemmessage.Update_and_approve_topic_successfullyEn);
                    }
                    else
                    {
                        //  msg = "Cập nhật thông tin chủ đề thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Update_topic_successfully, SystemMessageConst.systemmessage.Update_topic_successfullyEn);
                    }
                }
            }
            else if (session.idquyentaikhoan == 1)
            {
                if (checkExit.trangthai != trangthai)
                {
                    checkExit.phienban = phienban;
                    checkExit.trangthai = trangthai;
                    checkExit.idnguoiduyet = session.id;
                    checkExit.thoigianduyet = DateTime.Now;
                    if (trangthai == 1)
                    {
                        checkExit.lydotuchoi = (string.IsNullOrEmpty(lydo)) ? '"' + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizers, SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizersEn) + '"' : lydo;
                        //   msg = "Từ chối phê duyệt chủ đề thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Refusal_to_approve_the_topic_successfully, SystemMessageConst.systemmessage.Refusal_to_approve_the_topic_successfullyEn);
                    }
                    else if (trangthai == 5)
                    {
                        checkExit.lydotuchoi = (string.IsNullOrEmpty(lydo)) ? '"' + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizers, SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizersEn) + '"' : lydo;
                        // msg = "Gỡ chủ đề khỏi website thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Remove_the_theme_from_the_website_successfully, SystemMessageConst.systemmessage.Remove_the_theme_from_the_website_successfullyEn);
                    }
                    else
                    {
                        checkExit.lydotuchoi = null;
                        // msg = "Thay đổi trạng thái chủ đề thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Successful_topic_change, SystemMessageConst.systemmessage.Successful_topic_changeEn);
                    }

                }
                else
                {
                    if (checkExit.phienban != phienban)
                    {
                        checkExit.phienban = phienban;
                        //   msg = "Cập nhật thông tin thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EditSuccess, SystemMessageConst.systemmessage.EditSuccessEn);
                    }
                    else
                    {
                        //  msg = "Bạn không thể thay đổi nội dung chủ đề";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.You_can_not_change_topic_content, SystemMessageConst.systemmessage.You_can_not_change_topic_contentEn);
                    }
                }
            }
            else
            {
                // msg = "Không thể cập nhật thông tin chủ đề";
                msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Failed_to_update_topic_information, SystemMessageConst.systemmessage.Failed_to_update_topic_informationEn);
            }

            entity.SaveChanges();

            SysMess.IsSuccess = true;
            SysMess.Message = msg;
            return SysMess;
        }

        public SystemMess BN_UpdateAllEvent(int id, int trangthai)
        {
            string msg = "";
            var checkExit = entity.m_sukienhop.Where(m => m.id == id).FirstOrDefault();

            if (checkExit == null)
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsNotExist, SystemMessageConst.systemmessage.IsNotExistEn);
                return SysMess;
            }
            if (session.id == checkExit.idnguoitao && session.idquyentaikhoan == 1)
            {
                checkExit.trangthai = 3;
                msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Successful_topic_change, SystemMessageConst.systemmessage.Successful_topic_changeEn);
            }
            else if (session.id == checkExit.idnguoitao && session.idquyentaikhoan != 1)
            {
                if (checkExit.trangthai != 3)
                {
                    checkExit.trangthai = 2;
                    msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Successful_topic_change, SystemMessageConst.systemmessage.Successful_topic_changeEn);
                }
            }
            else
            {
                msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Failed_to_update_topic_information, SystemMessageConst.systemmessage.Failed_to_update_topic_informationEn);
            }

            entity.SaveChanges();

            SysMess.IsSuccess = true;
            SysMess.Message = msg;
            return SysMess;
        }
        public SystemMess BN_AddNewMeetingEvent(string tencuochop, string diadiem, int phienban, string startDate, int trangthai)
        {

            var errorTen = new ValidateForm().CheckRequiredField(tencuochop, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Topic_name, SystemMessageConst.systemmessage.Topic_nameEn), 3);
            if (errorTen.error == null) { tencuochop = errorTen.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTen.error;
                return SysMess;
            }

            var errorGioithieu = new ValidateForm().CheckRequiredField(diadiem, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Introduce, SystemMessageConst.systemmessage.IntroduceEn), 3);
            if (errorGioithieu.error == null) { diadiem = errorGioithieu.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorGioithieu.error;
                return SysMess;
            }
            if (phienban != 1 && phienban != 2)
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Select_Languge, SystemMessageConst.systemmessage.Select_LangugeEn);
                return SysMess;
            }
            if (string.IsNullOrEmpty(startDate))
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DateStartMeetingNotNull, SystemMessageConst.systemmessage.DateStartMeetingNotNullEn);
                return SysMess;
            }
            m_sukienhop media = new m_sukienhop();
            media.tencuochop = tencuochop;
            media.diadiem = diadiem;
            media.thoigiantao = DateTime.Now;
            media.idnguoitao = session.id;
            media.trangthai = trangthai;
            if (trangthai == 3 && session.idquyentaikhoan == 1)
            {
                media.idnguoiduyet = session.id;
                media.thoigianduyet = DateTime.Now;
            }
            media.ngayhop = DateTime.Parse(startDate);
            media.phienban = phienban;

            entity.m_sukienhop.Add(media);

            entity.SaveChanges();

            SysMess.IsSuccess = true;
            SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AddSuccess, SystemMessageConst.systemmessage.AddSuccessEn);
            return SysMess;
        }

        public ReturnDetailMeeting LoadDetailMeetingByID(bool bsearch, string search, int trangthai, int CurentPage, int id)
        {
            try
            {
                var session = (SessionUser)HttpContext.Current.Session["uSession"];
                int stSearch = 0;
                if (bsearch == true)
                {
                    stSearch = 1;
                }
                ReturnDetailMeeting rt = new DAL.ReturnDetailMeeting();
                List<DetailMeetingEvent> lstVideo = new List<DAL.DetailMeetingEvent>();

                ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

                var Result = entity.LOAD_DETAIL_MEETING(search, stSearch, trangthai, id, session.id, session.idquyentaikhoan, CurentPage, 10, totalCount).ToList().All(a =>
                {
                    DetailMeetingEvent lst = new DAL.DetailMeetingEvent();

                    lst.id = a.id;
                    lst.idsukienhop = a.idsukienhop;
                    lst.ten = a.ten;
                    lst.diadiem = a.diadiem;
                    //lst.thoigianbatdau = a.thoigianbatdau.ToString();
                    //lst.thoigianketthuc = a.thoigianketthuc.ToString();
                    lst.thoigianbatdau = DisplayUntils.TimeSpan_PmAmFormat(a.thoigianbatdau);
                    lst.thoigianketthuc = DisplayUntils.TimeSpan_PmAmFormat(a.thoigianketthuc);
                    lst.ngayhop = a.ngayhop.Value.ToString("dd/MM/yyyy");
                    lst.noidung = a.noidung;
                    lst.trangthai = a.trangthai.Value;
                    lst.trangthaihienthi = (a.trangthai.Value == 0) ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsDeleted, SystemMessageConst.systemmessage.IsDeletedEn) : (a.trangthai.Value == 1 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Deactive, SystemMessageConst.systemmessage.DeactiveEn) : (a.trangthai.Value == 2 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Pending, SystemMessageConst.systemmessage.PendingEn) : (a.trangthai.Value == 3 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Active, SystemMessageConst.systemmessage.ActiveEn) : (a.trangthai.Value == 4 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Save_Draft, SystemMessageConst.systemmessage.Save_DraftEn) : (a.trangthai.Value == 5 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.RemoveOnSite, SystemMessageConst.systemmessage.RemoveOnSiteEn) : DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unknown, SystemMessageConst.systemmessage.UnknownEn))))));
                    lst.idnguoiduyet = (a.idnguoiduyet != null) ? a.idnguoiduyet.Value : 0;
                    lst.idnguoitao = a.idnguoitao.Value;
                    lst.lydotuchoi = a.lydotuchoi;
                    lstVideo.Add(lst);
                    return true;
                });
                rt.data = lstVideo;
                rt.totalCount = int.Parse(totalCount.Value.ToString());
                return rt;
            }
            catch (Exception)
            {
                ReturnDetailMeeting rt = new DAL.ReturnDetailMeeting();
                rt.totalCount = 0;
                rt.data = null;
                return rt;
            }
        }

        public SystemMess BN_AddNewInforMeeting(string ten, string diadiem, string noidung, string startDate, string startTime, string endTime, int id, int trangthai)
        {

            var errorTen = new ValidateForm().CheckRequiredField(ten, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NameMeeting, SystemMessageConst.systemmessage.NameMeetingEn), 3);
            if (errorTen.error == null) { ten = errorTen.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTen.error;
                return SysMess;
            }

            var errorGioithieu = new ValidateForm().CheckRequiredField(diadiem, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Place, SystemMessageConst.systemmessage.PlaceEn), 3);
            if (errorGioithieu.error == null) { diadiem = errorGioithieu.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorGioithieu.error;
                return SysMess;
            }
            var errorNoiDung = new ValidateForm().Check_CKEditor_Requite2(noidung, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Content, SystemMessageConst.systemmessage.ContentEn), 1);
            if (errorNoiDung.error == null) { noidung = errorNoiDung.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorNoiDung.error;
                return SysMess;
            }
            if (string.IsNullOrEmpty(startDate))
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DateStartMeetingNotNull, SystemMessageConst.systemmessage.DateStartMeetingNotNullEn);
                return SysMess;
            }
            if (string.IsNullOrEmpty(startTime))
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.TimeStartNotNull, SystemMessageConst.systemmessage.TimeStartNotNullEn);
                return SysMess;
            }
            if (string.IsNullOrEmpty(endTime))
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.TimeEndNotNull, SystemMessageConst.systemmessage.TimeEndNotNullEn);
                return SysMess;
            }
            if (ValidateForm.IsValidDateFormat(startDate) == false)
            {
                SysMess.IsSuccess = false;
                SysMess.Message = String.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage("Ngày họp", "Start date"));
                return SysMess;
            }
            TimeSpan thoigianbatdau;
            TimeSpan thoigianketthuc;
            //check thời gian bắt đầu
            if (ValidateForm.IsValidTimeFormat(startTime))
            {
                thoigianbatdau = DateTime.ParseExact(
                    startTime,
                    "h:mm tt",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None).TimeOfDay;
            }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = String.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.TimeNotCorrectFormat, SystemMessageConst.ValidateConst.TimeNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage("Thời gian bắt đầu ", "Time start "));
                return SysMess;
            }
            //check thời gian kết thúc
            if (ValidateForm.IsValidTimeFormat(endTime))
            {
                thoigianketthuc = DateTime.ParseExact(
                   endTime,
                   "h:mm tt",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None).TimeOfDay;
            }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = String.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.TimeNotCorrectFormat, SystemMessageConst.ValidateConst.TimeNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage("Thời gian kết thúc ", "Time end "));
                return SysMess;
            }

            if (thoigianketthuc <= thoigianbatdau && thoigianketthuc != DateTime.ParseExact(
                   "11:45 PM",
                   "h:mm tt",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None).TimeOfDay)
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.EndDateNotCorrect, SystemMessageConst.ValidateConst.EndDateNotCorrectEn);
                return SysMess;
            }

            m_chitietsukienhop media = new m_chitietsukienhop();
            media.ten = ten;
            media.idsukienhop = id;
            media.diadiem = diadiem;
            media.thoigianbatdau = thoigianbatdau;
            media.thoigianketthuc = thoigianketthuc;
            media.ngayhop = DateTime.Parse(startDate);
            media.noidung = noidung;

            media.idnguoitao = session.id;
            media.trangthai = trangthai;
            if (trangthai == 3 && session.idquyentaikhoan == 1)
            {
                media.idnguoiduyet = session.id;
            }

            entity.m_chitietsukienhop.Add(media);

            entity.SaveChanges();

            SysMess.IsSuccess = true;
            SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AddSuccess, SystemMessageConst.systemmessage.AddSuccessEn);
            return SysMess;
        }


        public SystemMess BN_DeleteInforMeeting(int id)
        {
            bool statusDell = false;
            var checkItem = entity.m_chitietsukienhop.Where(m => m.id == id).FirstOrDefault();
            if (checkItem != null)
            {

                if (session.idquyentaikhoan == 1)
                {
                    statusDell = true;
                }
                else if (session.idquyentaikhoan != 1 && checkItem.idnguoitao == session.id && checkItem.trangthai != 3)
                {
                    statusDell = true;
                }
                else
                {
                    SysMess.IsSuccess = false;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteFail, SystemMessageConst.systemmessage.DeleteFailEn);
                }
                if (statusDell)
                {
                    checkItem.trangthai = 0;
                    entity.SaveChanges();
                    SysMess.IsSuccess = true;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteSuccess, SystemMessageConst.systemmessage.DeleteSuccessEn);
                }
                else
                {
                    SysMess.IsSuccess = false;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteFail, SystemMessageConst.systemmessage.DeleteFailEn);
                }
            }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DataIsEmpty, SystemMessageConst.systemmessage.DataIsEmptyEn);
            }
            return SysMess;
        }

        public SystemMess BN_UpdateInforMeeting(string ten, string diadiem, string lydo, string noidung, string startDate, string startTime, string endTime, int id, int trangthai)
        {
            string msg = "";
            var checkExit = entity.m_chitietsukienhop.Where(m => m.id == id).FirstOrDefault();

            if (checkExit == null)
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsNotExist, SystemMessageConst.systemmessage.IsNotExistEn);
                return SysMess;
            }

            var errorTen = new ValidateForm().CheckRequiredField(ten, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NameMeeting, SystemMessageConst.systemmessage.NameMeetingEn), 3);
            if (errorTen.error == null) { ten = errorTen.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTen.error;
                return SysMess;
            }

            var errorGioithieu = new ValidateForm().CheckRequiredField(diadiem, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Place, SystemMessageConst.systemmessage.PlaceEn), 3);
            if (errorGioithieu.error == null) { diadiem = errorGioithieu.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorGioithieu.error;
                return SysMess;
            }
            var errorNoiDung = new ValidateForm().Check_CKEditor_Requite2(noidung, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Content, SystemMessageConst.systemmessage.ContentEn), 1);
            if (errorNoiDung.error == null) { noidung = errorNoiDung.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorNoiDung.error;
                return SysMess;
            }
            if (string.IsNullOrEmpty(startDate))
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DateStartMeetingNotNull, SystemMessageConst.systemmessage.DateStartMeetingNotNullEn);
                return SysMess;
            }
            if (string.IsNullOrEmpty(startTime))
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.TimeStartNotNull, SystemMessageConst.systemmessage.TimeStartNotNullEn);
                return SysMess;
            }
            if (string.IsNullOrEmpty(endTime))
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.TimeEndNotNull, SystemMessageConst.systemmessage.TimeEndNotNullEn);
                return SysMess;
            }
            DateTime _starDate;
            if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out _starDate))
            {
                checkExit.ngayhop = _starDate;
            }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = String.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage("Ngày họp", "Start date"));
                return SysMess;
            }
            //if (ValidateForm.IsValidDateFormat(startDate) == false)
            //{
            //    
            //}
            TimeSpan thoigianbatdau;
            TimeSpan thoigianketthuc;
            //check thời gian bắt đầu
            if (ValidateForm.IsValidTimeFormat(startTime))
            {
                thoigianbatdau = DateTime.ParseExact(
                    startTime,
                    "h:mm tt",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None).TimeOfDay;
            }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = String.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.TimeNotCorrectFormat, SystemMessageConst.ValidateConst.TimeNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage("Thời gian bắt đầu ", "Time start "));
                return SysMess;
            }
            //check thời gian kết thúc
            if (ValidateForm.IsValidTimeFormat(endTime))
            {
                thoigianketthuc = DateTime.ParseExact(
                   endTime,
                   "h:mm tt",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None).TimeOfDay;
            }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = String.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.TimeNotCorrectFormat, SystemMessageConst.ValidateConst.TimeNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage("Thời gian kết thúc ", "Time end "));
                return SysMess;
            }

            if (thoigianketthuc <= thoigianbatdau && thoigianketthuc != DateTime.ParseExact(
                  "11:45 PM",
                  "h:mm tt",
                  CultureInfo.InvariantCulture,
                  DateTimeStyles.None).TimeOfDay)
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.EndDateNotCorrect, SystemMessageConst.ValidateConst.EndDateNotCorrectEn);
                return SysMess;
            }


            if (session.id == checkExit.idnguoitao || session.idquyentaikhoan == 1)
            {
                checkExit.ten = ten;
                checkExit.diadiem = diadiem;
                checkExit.noidung = noidung;
               
                checkExit.thoigianbatdau = thoigianbatdau;
                checkExit.thoigianketthuc = thoigianketthuc;
                checkExit.trangthai = 3;
                msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Update_post_successfully, SystemMessageConst.systemmessage.Update_post_successfullyEn);
                entity.SaveChanges();
            }
            else
            {
                msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Failed_to_update_topic_information, SystemMessageConst.systemmessage.Failed_to_update_topic_informationEn);
            }

         

            SysMess.IsSuccess = true;
            SysMess.Message = msg;
            return SysMess;
        }
    }
}