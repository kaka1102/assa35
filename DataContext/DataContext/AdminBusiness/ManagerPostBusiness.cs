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
    public class ManagerPostBusiness
    {
        SystemMess SysMess = new SystemMess();
        ASS_35Entities entity = new ASS_35Entities();
        SessionUser session = (SessionUser)HttpContext.Current.Session["uSession"];
        //public SystemMess LoadAllPost(bool bsearch, string search, int trangthai)
        //{
        //    var session = (SessionUser)HttpContext.Current.Session["uSession"];
        //    var danhsach = entity.m_baiviet.Where(a =>
        //            (
        //                !bsearch ? true :
        //                (
        //                   (a.tieude.ToLower().Trim().Contains(search.ToLower().Trim())) ||
        //                   (a.gioithieu.ToLower().Trim().Contains(search.ToLower().Trim())) ||
        //                   (a.noidung.ToLower().Trim().Contains(search.ToLower().Trim())) ||
        //                   (a.tennguoitao.ToLower().Trim().Contains(search.ToLower().Trim()))
        //                )
        //            ) && ((trangthai == 0) ? (a.trangthai != 0) : (a.trangthai.Value == trangthai)) &&
        //                ((session.idquyentaikhoan == 1) ? true : a.idnguoitao.Value == session.id)
        //           ).ToList().Select(a => new
        //           {
        //               a.id,
        //               a.tieude,
        //               a.gioithieu,
        //               a.noidung,
        //               a.avatar,
        //               ngaytao = a.ngaytao.Value.ToString("dd/MM/yyyy"),
        //               thoigiantao = a.ngaytao,
        //               idnguoitao = a.idnguoitao.Value,
        //               a.tennguoitao,
        //               a.trangthai,
        //               trangthaibaiviet = (a.trangthai == 0) ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsDeleted, SystemMessageConst.systemmessage.IsDeletedEn) : (a.trangthai == 1 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Deactive, SystemMessageConst.systemmessage.DeactiveEn) : (a.trangthai == 2 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Pending, SystemMessageConst.systemmessage.PendingEn) : (a.trangthai == 3 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Active, SystemMessageConst.systemmessage.ActiveEn) : (a.trangthai == 4 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Save_Draft, SystemMessageConst.systemmessage.Save_DraftEn) : (a.trangthai == 5 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.RemoveOnSite, SystemMessageConst.systemmessage.RemoveOnSiteEn) : DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unknown, SystemMessageConst.systemmessage.UnknownEn)))))),
        //               a.phienban,
        //               idnguoipheduyet = (a.idnguoipheduyet != null) ? a.idnguoipheduyet.Value : 0,
        //               tennguoipheduyet = new AccountUntil().GetFullNameByIdTK((a.idnguoipheduyet != null) ? a.idnguoipheduyet.Value : 0),
        //               thoigianpheduyet = (a.thoigianpheduyet == null ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn) : a.thoigianpheduyet.Value.ToString("dd/MM/yyyy")),
        //               a.lydo,
        //           }).OrderByDescending(a => a.thoigiantao);
        //    SysMess.IsSuccess = true;
        //    SysMess.LstData = danhsach.Cast<object>().ToList();
        //    return SysMess;
        //}
        public ReturnDataPost LoadAllPost(bool bsearch, string search, int trangthai, int CurentPage)
        {
            var session = (SessionUser)HttpContext.Current.Session["uSession"];
            int stSearch = 0;
            if (bsearch == true)
            {
                stSearch = 1;
            }
            ReturnDataPost rt = new DAL.ReturnDataPost();
            List<AllPost> lstVideo = new List<DAL.AllPost>();

            ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

            var Result = entity.LOADALL_POST(search, stSearch, trangthai, session.id, session.idquyentaikhoan, CurentPage, 10, totalCount).ToList().All(a =>
            {
                AllPost lst = new DAL.AllPost();
                lst.id = a.id;
                lst.tieude = a.tieude;
                lst.gioithieu = a.gioithieu;
                lst.noidung = a.noidung;
                lst.tacgia = a.tacgia;
                lst.avatar = a.avatar;
                lst.thoigianpheduyet = (a.thoigianpheduyet == null ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn) : a.thoigianpheduyet.Value.ToString("dd/MM/yyyy"));
                lst.tennguoitao = new AccountUntil().GetFullNameByIdTK((a.idnguoitao != null) ? a.idnguoitao.Value : 0);
                lst.ngaytao = a.ngaytao.Value.ToString("dd/MM/yyyy");
                lst.thoigiantao = a.ngaytao.Value;
                lst.phienban = a.phienban.Value;
                lst.trangthai = a.trangthai.Value;
                lst.trangthaibaiviet = (a.trangthai == 0) ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsDeleted, SystemMessageConst.systemmessage.IsDeletedEn) : (a.trangthai == 1 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Deactive, SystemMessageConst.systemmessage.DeactiveEn) : (a.trangthai == 2 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Pending, SystemMessageConst.systemmessage.PendingEn) : (a.trangthai == 3 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Active, SystemMessageConst.systemmessage.ActiveEn) : (a.trangthai == 4 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Save_Draft, SystemMessageConst.systemmessage.Save_DraftEn) : (a.trangthai == 5 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.RemoveOnSite, SystemMessageConst.systemmessage.RemoveOnSiteEn) : DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unknown, SystemMessageConst.systemmessage.UnknownEn))))));
                lst.lydo = a.lydo;
                lstVideo.Add(lst);
                return true;
            });
            rt.data = lstVideo;
            rt.totalCount = int.Parse(totalCount.Value.ToString());
            return rt;
        }
        public SystemMess BN_DeletePost(int id)
        {
            bool statusDell = false;
            var checkItem = entity.m_baiviet.Where(m => m.id == id).FirstOrDefault();
            if (checkItem != null)
            {
                var thongtintk = entity.m_taikhoan.Where(a => a.id == checkItem.idnguoitao).ToList().Select(a => new
                {
                    a.id,
                    a.email,
                    matkhau = a.matkhau,
                    //tendaydu = a.idloaitaikhoan == 1 ? a.m_thongtincanhan.Where(x => x.idtaikhoan == a.id).Select(x => x.hovaten).FirstOrDefault() : a.m_thongtincanhan.Where(x => x.idtaikhoan == a.id).Select(x => x.m_thongtincoquan.tencoquan).FirstOrDefault(),
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
                    //  SysMess.Message = SystemMessageConst.systemmessage.DeleteFail;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteFail, SystemMessageConst.systemmessage.DeleteFailEn);
                }
                if (statusDell)
                {
                    checkItem.trangthai = 0;
                    entity.SaveChanges();
                    SysMess.IsSuccess = true;
                    //    SysMess.Message = SystemMessageConst.systemmessage.DeleteSuccess;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteSuccess, SystemMessageConst.systemmessage.DeleteSuccessEn);

                    string mailto = thongtintk.email;
                    //    string subject = string.Format("THÔNG BÁO TỪ QUẢN TRỊ HỆ THỐNG");
                    string subject = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HeaderEmail, ListMailAddress.ContentEmailSystem.HeaderEmailEn);
                    string body = "";
                    body = string.Format("<div style='padding: 30px;border: 1px solid green;border-radius: 5px;'>"
                                     + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HelloAccount, ListMailAddress.ContentEmailSystem.HelloAccountEn) + " <span style='color:red;'>{0}</span></label></div><br />"
                                     + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.ThankForSend, ListMailAddress.ContentEmailSystem.ThankForSendEn) + "</label></div><br />"
                                     + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.Topic.DeletePost1, ListMailAddress.Topic.DeletePostEn1) + "</label></div><br />"
                                     + "<ul>"
                                     + "<li  style='padding: 5px;'><b> {1} </b></li>"
                                     + "</ul> "
                                     + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.Topic.DeleteTopPic2, ListMailAddress.Topic.DeleteTopPicEn2) + "</label></div><br />"
                                     + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.Topic.DeleteTopPic3, ListMailAddress.Topic.DeleteTopPicEn3) + "</label></div><br />"
                                     + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.FooterEmail, ListMailAddress.ContentEmailSystem.FooterEmailEn)
                                     + "</div>",
                                     thongtintk.tendaydu, checkItem.tieude);

                    bool a = SendEmailUntil.SendMail(mailto, body, subject);
                }
                else
                {
                    SysMess.IsSuccess = false;
                    //  SysMess.Message = SystemMessageConst.systemmessage.DeleteFail;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteFail, SystemMessageConst.systemmessage.DeleteFailEn);
                }
            }
            else
            {
                SysMess.IsSuccess = false;
                //  SysMess.Message = SystemMessageConst.systemmessage.DataIsEmpty;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DataIsEmpty, SystemMessageConst.systemmessage.DataIsEmptyEn);
            }
            return SysMess;
        }

        public SystemMess BN_AddNewPost(string tieude, string noidung,string tacgia, string gioithieu, int phienban, string avatar, int trangthai)
        {

            //  var errorTen = new ValidateForm().CheckRequiredField(tieude, "Tiêu đề ", 3, 500);
            var errorTen = new ValidateForm().CheckRequiredField(tieude, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Title, SystemMessageConst.systemmessage.TitleEn), 3, 500);
            if (errorTen.error == null) { tieude = errorTen.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTen.error;
                return SysMess;
            }

            //  var errorGioithieu = new ValidateForm().CheckRequiredField(gioithieu, "Giới thiệu ", 3);
            var errorGioithieu = new ValidateForm().CheckNonRequiredField(gioithieu, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Introduce, SystemMessageConst.systemmessage.IntroduceEn), 3);
            if (errorGioithieu.error == null) { gioithieu = errorGioithieu.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorGioithieu.error;
                return SysMess;
            }

            //  var errorNoiDung = new ValidateForm().Check_CKEditor_Requite2(noidung, "Nội dung", 1);
            var errorNoiDung = new ValidateForm().Check_CKEditor_Requite2(noidung, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Content, SystemMessageConst.systemmessage.ContentEn), 1);
            if (errorNoiDung.error == null) { noidung = errorNoiDung.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorNoiDung.error;
                return SysMess;
            }
            var errorTacgia = new ValidateForm().CheckNonRequiredField(tacgia, "Tác giả", 3);
            if (errorTacgia.error == null) { tacgia = errorTacgia.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTacgia.error;
                return SysMess;
            }
            if (phienban != 1 && phienban != 2)
            {
                SysMess.IsSuccess = false;
                //  SysMess.Message = "Mời bạn chọn phiên bản ngôn ngữ cho bài viết";
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Select_Languge, SystemMessageConst.systemmessage.Select_LangugeEn);
                return SysMess;
            }
            if (string.IsNullOrEmpty(avatar))
            {
                SysMess.IsSuccess = false;
                // SysMess.Message = "Bạn chưa chọn avatar cho bài viết";
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Not_Select_Avatar_Post, SystemMessageConst.systemmessage.Not_Select_Avatar_PostEn);
                return SysMess;
            }

            m_baiviet baiviet = new m_baiviet();
            baiviet.tieude = tieude;
            baiviet.gioithieu = gioithieu;
            baiviet.noidung = noidung;
            baiviet.tacgia = tacgia;
            baiviet.avatar = avatar;
            baiviet.ngaytao = DateTime.Now;
            baiviet.idnguoitao = session.id;
            baiviet.tennguoitao = (session.hovaten == '"' + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unknown, SystemMessageConst.systemmessage.UnknownEn) + '"') ? session.email : session.hovaten;
            baiviet.trangthai = trangthai;
            if (trangthai == 3 && session.idquyentaikhoan == 1)
            {
                baiviet.idnguoipheduyet = session.id;
                baiviet.thoigianpheduyet = DateTime.Now;
                baiviet.tennguoipheduyet = new AccountUntil().GetFullNameByIdTK(session.id);
            }
            baiviet.phienban = phienban;
            entity.m_baiviet.Add(baiviet);

            entity.SaveChanges();

            SysMess.IsSuccess = true;
            //SysMess.Message = SystemMessageConst.systemmessage.AddSuccess;
            SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AddSuccess, SystemMessageConst.systemmessage.AddSuccessEn);
            return SysMess;
        }

        public SystemMess BN_UpdatePost(string tieude, string gioithieu, string noidung,string tacgia, string lydo, int phienban, int id, int trangthai, string avatar)
        {
            string msg = "";
            var checkExit = entity.m_baiviet.Where(m => m.id == id).FirstOrDefault();

            if (checkExit == null)
            {
                SysMess.IsSuccess = false;
                // SysMess.Message = SystemMessageConst.systemmessage.IsNotExist;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsNotExist, SystemMessageConst.systemmessage.IsNotExistEn);
                return SysMess;
            }

            var errorTen = new ValidateForm().CheckRequiredField(tieude, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Title, SystemMessageConst.systemmessage.TitleEn), 3, 500);
            if (errorTen.error == null) { tieude = errorTen.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTen.error;
                return SysMess;
            }

            // var errorGioithieu = new ValidateForm().CheckRequiredField(gioithieu, "Giới thiệu ", 3);
            var errorGioithieu = new ValidateForm().CheckNonRequiredField(gioithieu, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Introduce, SystemMessageConst.systemmessage.IntroduceEn), 3);
            if (errorGioithieu.error == null) { gioithieu = errorGioithieu.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorGioithieu.error;
                return SysMess;
            }

            //    var errorNoiDung = new ValidateForm().Check_CKEditor_Requite2(noidung, "Nội dung", 1);
            var errorNoiDung = new ValidateForm().Check_CKEditor_Requite2(noidung, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Content, SystemMessageConst.systemmessage.ContentEn), 1);
            if (errorNoiDung.error == null) { noidung = errorNoiDung.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorNoiDung.error;
                return SysMess;
            }
            var errorTacgia = new ValidateForm().CheckNonRequiredField(tacgia, "Tác giả", 3);
            if (errorTacgia.error == null) { tacgia = errorTacgia.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTacgia.error;
                return SysMess;
            }

            if (phienban != 1 && phienban != 2)
            {
                SysMess.IsSuccess = false;
                //  SysMess.Message = "Mời bạn chọn phiên bản ngôn ngữ cho bài viết";
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Select_Languge, SystemMessageConst.systemmessage.Select_LangugeEn);
                return SysMess;
            }

            if (session.id == checkExit.idnguoitao.Value && checkExit.trangthai != 3)
            {
                checkExit.tieude = tieude;
                checkExit.gioithieu = gioithieu;
                checkExit.noidung = noidung;
                checkExit.tacgia = tacgia;
                checkExit.phienban = phienban;
                if (!string.IsNullOrEmpty(avatar))
                {
                    checkExit.avatar = avatar;
                }
                if (session.idquyentaikhoan == 1)
                {
                    if (checkExit.trangthai != trangthai)
                    {
                        checkExit.trangthai = trangthai;
                        checkExit.idnguoipheduyet = session.id;
                        checkExit.tennguoipheduyet = session.hovaten;
                        checkExit.thoigianpheduyet = DateTime.Now;
                        if (trangthai == 1)
                        {
                            checkExit.lydo = (string.IsNullOrEmpty(lydo)) ? '"' + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizers, SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizersEn) + '"' : lydo;
                        }
                        else
                        {
                            checkExit.lydo = null;
                        }
                        //  msg = "Cập nhật thông tin và phê duyệt bài viết thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Update_and_approve_post_successfully, SystemMessageConst.systemmessage.Update_and_approve_post_successfullyEn);
                    }
                    else
                    {
                        // msg = "Cập nhật thông tin bài viết thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Update_post_successfully, SystemMessageConst.systemmessage.Update_post_successfullyEn);
                    }
                }
                else
                {
                    if (checkExit.trangthai != trangthai && trangthai != 3)
                    {
                        checkExit.trangthai = trangthai;
                        //  msg = "Cập nhật thông tin và phê duyệt bài viết thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Update_and_approve_post_successfully, SystemMessageConst.systemmessage.Update_and_approve_post_successfullyEn);
                    }
                    else
                    {
                        //  msg = "Cập nhật thông tin bài viết thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Update_post_successfully, SystemMessageConst.systemmessage.Update_post_successfullyEn);
                    }
                }
            }
            else if (session.idquyentaikhoan == 1)
            {
                if (checkExit.trangthai != trangthai)
                {
                    checkExit.phienban = phienban;
                    checkExit.trangthai = trangthai;
                    checkExit.idnguoipheduyet = session.id;
                    checkExit.tennguoipheduyet = session.hovaten;
                    checkExit.thoigianpheduyet = DateTime.Now;
                    if (trangthai == 1)
                    {
                        checkExit.lydo = (string.IsNullOrEmpty(lydo)) ? '"' + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizers, SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizersEn) + '"' : lydo;
                        //  msg = "Từ chối phê duyệt bài viết thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Refusal_to_approve_the_post_successfully, SystemMessageConst.systemmessage.Refusal_to_approve_the_post_successfullyEn);
                    }
                    else if (trangthai == 5)
                    {
                        checkExit.lydo = (string.IsNullOrEmpty(lydo)) ? '"' + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizers, SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizersEn) + '"' : lydo;
                        // msg = "Gỡ bài viết khỏi website thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Remove_the_post_from_the_website_successfully, SystemMessageConst.systemmessage.Remove_the_post_from_the_website_successfullyEn);
                    }
                    else
                    {
                        checkExit.lydo = null;
                        //  msg = "Thay đổi trạng thái bài viết thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Successful_post_change, SystemMessageConst.systemmessage.Successful_post_changeEn);
                    }
                }
                else
                {

                    if (checkExit.phienban != phienban)
                    {
                        checkExit.phienban = phienban;
                        //    msg = "Cập nhật thông tin thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EditSuccess, SystemMessageConst.systemmessage.EditSuccessEn);
                    }
                    else
                    {
                        // msg = "Bạn không thể thay đổi nội dung bài viết";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.You_can_not_change_post_content, SystemMessageConst.systemmessage.You_can_not_change_post_contentEn);
                    }
                }
            }
            else
            {
                // msg = "Không thể cập nhật thông tin bài viết";
                msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Failed_to_update_post_information, SystemMessageConst.systemmessage.Failed_to_update_post_informationEn);
            }

            entity.SaveChanges();

            SysMess.IsSuccess = true;
            SysMess.Message = msg;
            return SysMess;
        }
    }
}