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
    public class MediaBusiness
    {
        SystemMess SysMess = new SystemMess();
        ASS_35Entities entity = new ASS_35Entities();
        SessionUser session = (SessionUser)HttpContext.Current.Session["uSession"];


        //public SystemMess LoadAllLibraryImages(bool bsearch, string search, int trangthai)
        //{
        //    var session = (SessionUser)HttpContext.Current.Session["uSession"];
        //    var danhsach = entity.m_media.Where(a =>
        //            (
        //                !bsearch ? true :
        //                (
        //                   (a.tenchude.ToLower().Trim().Contains(search.ToLower().Trim())) ||
        //                   (a.tennguoitao.ToLower().Trim().Contains(search.ToLower().Trim()))
        //                )
        //            ) && ((trangthai == 0) ? (a.trangthai != 0) : (a.trangthai.Value == trangthai)) && a.theloai == "images" &&
        //                ((session.idquyentaikhoan == 1) ? true : a.idnguoitao.Value == session.id)
        //           ).ToList().Select(a => new
        //           {
        //               a.id,
        //               a.tenchude,
        //               thoigian = a.thoigiantao.Value.ToString("dd/MM/yyyy"),
        //               thoigiantao = a.thoigiantao.Value,
        //               idnguoitao = a.idnguoitao.Value,
        //               a.tennguoitao,
        //               a.trangthai,
        //               trangthaitaikhoan = (a.trangthai == 0) ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsDeleted, SystemMessageConst.systemmessage.IsDeletedEn) : (a.trangthai == 1 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Deactive, SystemMessageConst.systemmessage.DeactiveEn) : (a.trangthai == 2 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Pending, SystemMessageConst.systemmessage.PendingEn) : (a.trangthai == 3 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Active, SystemMessageConst.systemmessage.ActiveEn) : (a.trangthai == 4 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Save_Draft, SystemMessageConst.systemmessage.Save_DraftEn) : (a.trangthai == 5 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.RemoveOnSite, SystemMessageConst.systemmessage.RemoveOnSiteEn) : DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unknown, SystemMessageConst.systemmessage.UnknownEn)))))),
        //               a.ghichu,
        //               a.gioithieu,
        //               idnguoipheduyet = (a.idnguoipheduyet != null) ? a.idnguoipheduyet.Value : 0,
        //               tennguoipheduyet = new AccountUntil().GetFullNameByIdTK((a.idnguoipheduyet != null) ? a.idnguoipheduyet.Value : 0),
        //               //  tennguoipheduyet = (a.tennguoipheduyet != null) ? a.tennguoipheduyet : DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn),
        //               thoigianpheduyet = (a.thoigianpheduyet == null ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn) : a.thoigianpheduyet.Value.ToString("dd/MM/yyyy")),
        //               phienban = a.phienban.Value,
        //               theloai = a.theloai,
        //               a.lydo,
        //               chitiet = entity.m_mediadetails.Where(m => m.idmedia.Value == a.id && m.trangthai.Value == 3).ToList().Select(m => new
        //               {
        //                   m.id,
        //                   m.duongdanfile,
        //                   ngaytao = m.ngaytao.Value.ToString("dd/MM/yyyy"),
        //                   m.tenfile
        //               })
        //           }).OrderByDescending(a => a.thoigiantao);
        //    SysMess.IsSuccess = true;
        //    SysMess.LstData = danhsach.Cast<object>().ToList();
        //    return SysMess;
        //}

        public ReturnDataDocument LoadAllLibraryImages(bool bsearch, string search, int trangthai, int CurentPage)
        {
            var session = (SessionUser)HttpContext.Current.Session["uSession"];
            int stSearch = 0;
            if (bsearch == true)
            {
                stSearch = 1;
            }
            ReturnDataDocument rt = new DAL.ReturnDataDocument();
            List<AllDocument> lstVideo = new List<DAL.AllDocument>();

            ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

            var Result = entity.LOADALL_MEDIA(search, stSearch, trangthai, "images", session.id, session.idquyentaikhoan, CurentPage, 10, totalCount).ToList().All(a =>
            {
                AllDocument lst = new DAL.AllDocument();

                lst.id = a.id;
                lst.tenchude = a.tenchude;
                lst.thoigian = a.thoigiantao.Value.ToString("dd/MM/yyyy");
                lst.thoigiantao = a.thoigiantao.Value;
                lst.tennguoitao = new AccountUntil().GetFullNameByIdTK((a.idnguoitao != null) ? a.idnguoitao.Value : 0);
                lst.trangthai = a.trangthai.Value;
                lst.trangthaihienthi = (a.trangthai == 0) ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsDeleted, SystemMessageConst.systemmessage.IsDeletedEn) : (a.trangthai == 1 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Deactive, SystemMessageConst.systemmessage.DeactiveEn) : (a.trangthai == 2 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Pending, SystemMessageConst.systemmessage.PendingEn) : (a.trangthai == 3 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Active, SystemMessageConst.systemmessage.ActiveEn) : (a.trangthai == 4 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Save_Draft, SystemMessageConst.systemmessage.Save_DraftEn) : (a.trangthai == 5 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.RemoveOnSite, SystemMessageConst.systemmessage.RemoveOnSiteEn) : DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unknown, SystemMessageConst.systemmessage.UnknownEn))))));
                lst.ghichu = a.ghichu;
                lst.gioithieu = a.gioithieu;
                lst.tennguoipheduyet = new AccountUntil().GetFullNameByIdTK((a.idnguoipheduyet != null) ? a.idnguoipheduyet.Value : 0);
                lst.thoigianpheduyet = (a.thoigianpheduyet == null ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn) : a.thoigianpheduyet.Value.ToString("dd/MM/yyyy"));
                lst.phienban = a.phienban.Value;
                lst.lydo = a.lydo;
                lst.chitiet = entity.m_mediadetails.Where(m => m.idmedia.Value == a.id && m.trangthai.Value == 3).ToList().Select(m => new ChiTietDocument
                {
                    id = m.id,
                    duongdanfile = m.duongdanfile,
                    ngaytao = m.ngaytao.Value.ToString("dd/MM/yyyy"),
                    tenfile = m.tenfile,
                    caption = m.caption
                }).ToList();
                lstVideo.Add(lst);
                return true;
            });
            rt.data = lstVideo;
            rt.totalCount = int.Parse(totalCount.Value.ToString());
            return rt;
        }
        public SystemMess BN_AddNewLibraryImages(string tenchude, string gioithieu, int phienban, int trangthai, List<ImageCaption> lstFile)
        {

            var errorTen = new ValidateForm().CheckRequiredField(tenchude, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Topic_name, SystemMessageConst.systemmessage.Topic_nameEn), 3, 500);
            if (errorTen.error == null) { tenchude = errorTen.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTen.error;
                return SysMess;
            }

            var errorGioithieu = new ValidateForm().CheckRequiredField(gioithieu, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Introduce, SystemMessageConst.systemmessage.IntroduceEn), 3);
            if (errorGioithieu.error == null) { gioithieu = errorGioithieu.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorGioithieu.error;
                return SysMess;
            }
            if (phienban != 1 && phienban != 2)
            {
                SysMess.IsSuccess = false;
                //    SysMess.Message = "Mời bạn chọn phiên bản ngôn ngữ cho chủ đề";
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Select_Languge, SystemMessageConst.systemmessage.Select_LangugeEn);
                return SysMess;
            }
            if (lstFile.Count <= 0)
            {
                SysMess.IsSuccess = false;
                //   SysMess.Message = "Bạn chưa chọn ảnh cho chủ đề này";
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Not_Select_Images, SystemMessageConst.systemmessage.Not_Select_ImagesEn);
                return SysMess;
            }

            m_media media = new m_media();
            media.tenchude = tenchude;
            media.thoigiantao = DateTime.Now;
            media.idnguoitao = session.id;
            media.luotxem = 0;
            media.trangthai = trangthai;
            if (trangthai == 3 && session.idquyentaikhoan == 1)
            {
                media.idnguoipheduyet = session.id;
                media.thoigianpheduyet = DateTime.Now;
                media.tennguoipheduyet = new AccountUntil().GetFullNameByIdTK(session.id);
            }
            media.gioithieu = gioithieu;
            media.phienban = phienban;
            media.theloai = "images";
            media.tennguoitao = (session.hovaten == '"' + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unknown, SystemMessageConst.systemmessage.UnknownEn) + '"') ? session.email : session.hovaten;
            entity.m_media.Add(media);
            
            
            for (int i = 0; i < lstFile.Count; i++)
            {
                ImageCaption idItem = lstFile[i];
                var check = entity.m_mediadetails.Where(m => m.id == idItem.id).FirstOrDefault();
                if (check != null)
                {
                    check.idmedia = media.id;
                    check.trangthai = 3;
                    check.caption = idItem.caption;
                    
                }
            }

            entity.SaveChanges();
            
            SysMess.IsSuccess = true;

            //  SysMess.Message = SystemMessageConst.systemmessage.AddSuccess;
            SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AddSuccess, SystemMessageConst.systemmessage.AddSuccessEn);
            return SysMess;
        }

        public SystemMess BN_DeleteLibaryImages(int id)
        {
            bool statusDell = false;
            var checkItem = entity.m_media.Where(m => m.id == id && m.theloai == "images").FirstOrDefault();
            if (checkItem != null)
            {
                var thongtintk = entity.m_taikhoan.Where(a => a.id == checkItem.idnguoitao).ToList().Select(a => new
                {
                    a.id,
                    a.email,
                    matkhau = a.matkhau,
                    // tendaydu = a.idloaitaikhoan == 1 ? a.m_thongtincanhan.Where(x => x.idtaikhoan == a.id).Select(x => x.hovaten).FirstOrDefault() : a.m_thongtincanhan.Where(x => x.idtaikhoan == a.id).Select(x => x.m_thongtincoquan.tencoquan).FirstOrDefault(),
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
                    //    SysMess.Message = SystemMessageConst.systemmessage.DeleteFail;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteFail, SystemMessageConst.systemmessage.DeleteFailEn);
                }
                if (statusDell)
                {
                    checkItem.trangthai = 0;
                    entity.SaveChanges();
                    SysMess.IsSuccess = true;
                    // SysMess.Message = SystemMessageConst.systemmessage.DeleteSuccess;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteSuccess, SystemMessageConst.systemmessage.DeleteSuccessEn);

                    string mailto = thongtintk.email;
                    //  string subject = string.Format("THÔNG BÁO TỪ QUẢN TRỊ HỆ THỐNG");
                    string subject = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HeaderEmail, ListMailAddress.ContentEmailSystem.HeaderEmailEn);
                    string body = "";
                    body = string.Format("<div style='padding: 30px;border: 1px solid green;border-radius: 5px;'>"
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HelloAccount, ListMailAddress.ContentEmailSystem.HelloAccountEn) + " <span style='color:red;'>{0}</span></label></div><br />"
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.ThankForSend, ListMailAddress.ContentEmailSystem.ThankForSendEn) + "</label></div><br />"
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.Topic.DeleteTopPic1, ListMailAddress.Topic.DeleteTopPicEn1) + "</label></div><br />"
                                    + "<ul>"
                                    + "<li  style='padding: 5px;'><b> {1} </b></li>"
                                    + "</ul> "
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.Topic.DeleteTopPic2, ListMailAddress.Topic.DeleteTopPicEn2) + "</label></div><br />"
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.Topic.DeleteTopPic3, ListMailAddress.Topic.DeleteTopPicEn3) + "</label></div><br />"
                                    + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.FooterEmail, ListMailAddress.ContentEmailSystem.FooterEmailEn)
                                    + "</div>",
                                    thongtintk.tendaydu, checkItem.tenchude);

                    bool a = SendEmailUntil.SendMail(mailto, body, subject);
                }
                else
                {
                    SysMess.IsSuccess = false;
                    //   SysMess.Message = SystemMessageConst.systemmessage.DeleteFail;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteFail, SystemMessageConst.systemmessage.DeleteFailEn);
                }
            }
            else
            {
                SysMess.IsSuccess = false;
                //   SysMess.Message = SystemMessageConst.systemmessage.DataIsEmpty;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DataIsEmpty, SystemMessageConst.systemmessage.DataIsEmptyEn);
            }
            return SysMess;
        }

        public SystemMess BN_UpdateLibraryImages(string tenchude, string gioithieu, int phienban, int id, int trangthai, string lydo, List<ImageCaption> lstFile, List<ImageCaption> lstDell)
        {
            string msg = "";
            var checkExit = entity.m_media.Where(m => m.id == id && m.theloai == "images").FirstOrDefault();

            if (checkExit == null)
            {
                SysMess.IsSuccess = false;
                // SysMess.Message = SystemMessageConst.systemmessage.IsNotExist;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsNotExist, SystemMessageConst.systemmessage.IsNotExistEn);
                return SysMess;
            }

            // var errorTen = new ValidateForm().CheckRequiredField(tenchude, "Tên chủ đề ", 3, 500);
            var errorTen = new ValidateForm().CheckRequiredField(tenchude, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Topic_name, SystemMessageConst.systemmessage.Topic_nameEn), 3, 500);
            if (errorTen.error == null) { tenchude = errorTen.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTen.error;
                return SysMess;
            }

            //var errorGioithieu = new ValidateForm().CheckRequiredField(gioithieu, "Giới thiệu ", 3);
            var errorGioithieu = new ValidateForm().CheckRequiredField(gioithieu, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Introduce, SystemMessageConst.systemmessage.IntroduceEn), 3);
            if (errorGioithieu.error == null) { gioithieu = errorGioithieu.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorGioithieu.error;
                return SysMess;
            }
            if (phienban != 1 && phienban != 2)
            {
                SysMess.IsSuccess = false;
                // SysMess.Message = "Mời bạn chọn phiên bản ngôn ngữ cho chủ đề";
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Select_Languge, SystemMessageConst.systemmessage.Select_LangugeEn);
                return SysMess;
            }
            
            if (session.id == checkExit.idnguoitao.Value && checkExit.trangthai != 3)
            {

                if (lstDell.Count > 0)
                {

                    ImageCaption idItemDell;
                    entity.m_mediadetails.Where(m => m.idmedia == id).ToList().Where(a => !(a.trangthai==3 && lstDell.Select(b=>b.id).Contains(a.id) )).All(a=> {
                        bool ck = new DeleteFileUntil().DeleteFileByPath(a.duongdanfile);
                        entity.m_mediadetails.Remove(a);
                        return true;
                    });
                    entity.m_mediadetails.Where(m => m.idmedia == id).ToList().Where(a => (a.trangthai == 3 && lstDell.Select(b => b.id).Contains(a.id))).All(a => {
                        var img_new = lstDell.Where(b => b.id == a.id).FirstOrDefault();
                        if (img_new != null)
                        {
                            a.caption = img_new.caption;
                        }
                        return true;
                    });
                    //for (int i = 0; i < lstDell.Count; i++)
                    //{
                    //    idItemDell = lstDell[i];
                    //    //var check = entity.m_mediadetails.Where(m => m.id == idItemDell.id).FirstOrDefault();
                    //    //if (check != null)
                    //    //{
                    //    //    bool ck = new DeleteFileUntil().DeleteFileByPath(check.duongdanfile);
                    //    //    entity.m_mediadetails.Remove(check);
                    //    //}
                    //    var checkDell = entity.m_mediadetails.Where(m => m.trangthai == 3 && idItemDell.id != m.id && m.idmedia == id).FirstOrDefault();
                    //    if (checkDell != null)
                    //    {
                    //        bool ck = new DeleteFileUntil().DeleteFileByPath(checkDell.duongdanfile);
                    //        entity.m_mediadetails.Remove(checkDell);
                    //    }

                    //}
                    //for (int k = 0; k < lstDell.Count; k++)
                    //{
                    //    idItemDell = lstDell[k];
                    //    var check = entity.m_mediadetails.Where(m => m.id == idItemDell.id && m.idmedia == id && m.trangthai == 3).FirstOrDefault();
                    //    if (check != null)
                    //    {
                    //        check.caption = idItemDell.caption;                            
                    //    }
                    //}
                    entity.SaveChanges();
                }else
                {
                    var ListCheck = entity.m_mediadetails.Where(a => a.idmedia == id && a.trangthai ==3).ToList();
                    for (int j = 0; j < ListCheck.Count; j++)
                    {
                        bool ck = new DeleteFileUntil().DeleteFileByPath(ListCheck[j].duongdanfile);
                        entity.m_mediadetails.Remove(ListCheck[j]);
                    }
                    entity.SaveChanges();
                }

                if (lstFile.Count > 0)
                {
                    
                    for (int i = 0; i < lstFile.Count; i++)
                    {
                      ImageCaption  idItem = lstFile[i];
                        var check = entity.m_mediadetails.Where(m => m.id == idItem.id).FirstOrDefault();
                        if (check != null)
                        {
                            check.idmedia = checkExit.id;
                            check.trangthai = 3;
                            check.caption = idItem.caption;
                        }
                    }
                }
                checkExit.tenchude = tenchude;
                checkExit.gioithieu = gioithieu;
                checkExit.phienban = phienban;

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
                    checkExit.idnguoipheduyet = session.id;
                    checkExit.tennguoipheduyet = session.hovaten;
                    checkExit.thoigianpheduyet = DateTime.Now;
                    if (trangthai == 1)
                    {
                        checkExit.lydo = (string.IsNullOrEmpty(lydo)) ? '"' + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizers, SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizersEn) + '"' : lydo;
                        //   msg = "Từ chối phê duyệt chủ đề thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Refusal_to_approve_the_topic_successfully, SystemMessageConst.systemmessage.Refusal_to_approve_the_topic_successfullyEn);
                    }
                    else if (trangthai == 5)
                    {
                        checkExit.lydo = (string.IsNullOrEmpty(lydo)) ? '"' + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizers, SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizersEn) + '"' : lydo;
                        // msg = "Gỡ chủ đề khỏi website thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Remove_the_theme_from_the_website_successfully, SystemMessageConst.systemmessage.Remove_the_theme_from_the_website_successfullyEn);
                    }
                    else
                    {
                        checkExit.lydo = null;
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

        //public SystemMess LoadAllLibraryVideo(bool bsearch, string search, int trangthai)
        //{
        //    var session = (SessionUser)HttpContext.Current.Session["uSession"];
        //    var danhsach = entity.m_media.Where(a =>
        //            (
        //                !bsearch ? true :
        //                (
        //                   (a.tenchude.ToLower().Trim().Contains(search.ToLower().Trim())) ||
        //                   (a.tennguoitao.ToLower().Trim().Contains(search.ToLower().Trim()))
        //                )
        //            ) && ((trangthai == 0) ? (a.trangthai != 0) : (a.trangthai.Value == trangthai)) && a.theloai == "video" &&
        //                ((session.idquyentaikhoan == 1) ? true : a.idnguoitao.Value == session.id)
        //           ).ToList().Select(a => new
        //           {
        //               a.id,
        //               a.tenchude,
        //               thoigian = a.thoigiantao.Value.ToString("dd/MM/yyyy"),
        //               thoigiantao = a.thoigiantao.Value,
        //               idnguoitao = a.idnguoitao.Value,
        //               a.tennguoitao,
        //               a.trangthai,
        //               trangthaitaikhoan = (a.trangthai == 0) ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsDeleted, SystemMessageConst.systemmessage.IsDeletedEn) : (a.trangthai == 1 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Deactive, SystemMessageConst.systemmessage.DeactiveEn) : (a.trangthai == 2 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Pending, SystemMessageConst.systemmessage.PendingEn) : (a.trangthai == 3 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Active, SystemMessageConst.systemmessage.ActiveEn) : (a.trangthai == 4 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Save_Draft, SystemMessageConst.systemmessage.Save_DraftEn) : (a.trangthai == 5 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.RemoveOnSite, SystemMessageConst.systemmessage.RemoveOnSiteEn) : DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unknown, SystemMessageConst.systemmessage.UnknownEn)))))),
        //               a.ghichu,
        //               a.gioithieu,
        //               idnguoipheduyet = (a.idnguoipheduyet != null) ? a.idnguoipheduyet.Value : 0,
        //               tennguoipheduyet = new AccountUntil().GetFullNameByIdTK((a.idnguoipheduyet != null) ? a.idnguoipheduyet.Value : 0),
        //             //  tennguoipheduyet = (a.tennguoipheduyet != null) ? a.tennguoipheduyet : DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn),
        //               thoigianpheduyet = (a.thoigianpheduyet == null ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn) : a.thoigianpheduyet.Value.ToString("dd/MM/yyyy")),
        //               phienban = a.phienban.Value,
        //               theloai = a.theloai,
        //               a.lydo,
        //               chitiet = entity.m_mediadetails.Where(m => m.idmedia.Value == a.id && m.trangthai.Value == 3).ToList().Select(m => new
        //               {
        //                   m.id,
        //                   m.duongdanfile,
        //                   ngaytao = m.ngaytao.Value.ToString("dd/MM/yyyy"),
        //                   m.tenfile
        //               })
        //           }).OrderByDescending(a => a.thoigiantao);
        //    SysMess.IsSuccess = true;
        //    SysMess.LstData = danhsach.Cast<object>().ToList();
        //    return SysMess;
        //}

        public ReturnDataDocument LoadAllLibraryVideo(bool bsearch, string search, int trangthai, int CurentPage)
        {
            var session = (SessionUser)HttpContext.Current.Session["uSession"];
            int stSearch = 0;
            if (bsearch == true)
            {
                stSearch = 1;
            }
            ReturnDataDocument rt = new DAL.ReturnDataDocument();
            List<AllDocument> lstVideo = new List<DAL.AllDocument>();

            ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

            var Result = entity.LOADALL_MEDIA(search, stSearch, trangthai, "video", session.id, session.idquyentaikhoan, CurentPage, 10, totalCount).ToList().All(a =>
            {
                AllDocument lst = new DAL.AllDocument();

                lst.id = a.id;
                lst.tenchude = a.tenchude;
                lst.thoigian = a.thoigiantao.Value.ToString("dd/MM/yyyy");
                lst.thoigiantao = a.thoigiantao.Value;
                lst.tennguoitao = new AccountUntil().GetFullNameByIdTK((a.idnguoitao != null) ? a.idnguoitao.Value : 0);
                lst.trangthai = a.trangthai.Value;
                lst.trangthaihienthi = (a.trangthai == 0) ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsDeleted, SystemMessageConst.systemmessage.IsDeletedEn) : (a.trangthai == 1 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Deactive, SystemMessageConst.systemmessage.DeactiveEn) : (a.trangthai == 2 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Pending, SystemMessageConst.systemmessage.PendingEn) : (a.trangthai == 3 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Active, SystemMessageConst.systemmessage.ActiveEn) : (a.trangthai == 4 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Save_Draft, SystemMessageConst.systemmessage.Save_DraftEn) : (a.trangthai == 5 ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.RemoveOnSite, SystemMessageConst.systemmessage.RemoveOnSiteEn) : DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unknown, SystemMessageConst.systemmessage.UnknownEn))))));
                lst.ghichu = a.ghichu;
                lst.gioithieu = a.gioithieu;
                lst.tennguoipheduyet = new AccountUntil().GetFullNameByIdTK((a.idnguoipheduyet != null) ? a.idnguoipheduyet.Value : 0);
                lst.thoigianpheduyet = (a.thoigianpheduyet == null ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn) : a.thoigianpheduyet.Value.ToString("dd/MM/yyyy"));
                lst.phienban = a.phienban.Value;
                lst.lydo = a.lydo;
                lst.chitiet = entity.m_mediadetails.Where(m => m.idmedia.Value == a.id && m.trangthai.Value == 3).ToList().Select(m => new ChiTietDocument
                {
                    id = m.id,
                    duongdanfile = m.duongdanfile,
                    ngaytao = m.ngaytao.Value.ToString("dd/MM/yyyy"),
                    tenfile = m.tenfile
                }).ToList();
                lstVideo.Add(lst);
                return true;
            });
            rt.data = lstVideo;
            rt.totalCount = int.Parse(totalCount.Value.ToString());
            return rt;
        }
        public SystemMess BN_AddNewLibraryVideo(string tenchude, string gioithieu, int phienban, string linkvideo, int trangthai)
        {

            // var errorTen = new ValidateForm().CheckRequiredField(tenchude, "Tên chủ đề ", 3, 500);
            var errorTen = new ValidateForm().CheckRequiredField(tenchude, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Topic_name, SystemMessageConst.systemmessage.Topic_nameEn), 3, 500);
            if (errorTen.error == null) { tenchude = errorTen.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTen.error;
                return SysMess;
            }
            //var errorGioithieu = new ValidateForm().CheckRequiredField(gioithieu, "Giới thiệu ", 3);
            var errorGioithieu = new ValidateForm().CheckRequiredField(gioithieu, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Introduce, SystemMessageConst.systemmessage.IntroduceEn), 3);
            if (errorGioithieu.error == null) { gioithieu = errorGioithieu.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorGioithieu.error;
                return SysMess;
            }
            if (phienban != 1 && phienban != 2)
            {
                SysMess.IsSuccess = false;
                //  SysMess.Message = "Mời bạn chọn phiên bản ngôn ngữ cho chủ đề";
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Select_Languge, SystemMessageConst.systemmessage.Select_LangugeEn);
                return SysMess;
            }

            var errorLink = new ValidateForm().CheckRequiteIframe(linkvideo, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Iframe, SystemMessageConst.systemmessage.IframeEn));
            if (errorLink.error == null) { linkvideo = errorLink.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorLink.error;
                return SysMess;
            }

            m_media media = new m_media();
            media.tenchude = tenchude;
            media.thoigiantao = DateTime.Now;
            media.idnguoitao = session.id;
            media.luotxem = 0;
            media.trangthai = trangthai;
            if (trangthai == 3 && session.idquyentaikhoan == 1)
            {
                media.idnguoipheduyet = session.id;
                media.thoigianpheduyet = DateTime.Now;
                media.tennguoipheduyet = new AccountUntil().GetFullNameByIdTK(session.id);
            }
            media.gioithieu = gioithieu;
            media.phienban = phienban;
            media.theloai = "video";
            media.tennguoitao = (session.hovaten == '"' + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unknown, SystemMessageConst.systemmessage.UnknownEn) + '"') ? session.email : session.hovaten;
            entity.m_media.Add(media);


            m_mediadetails m_details = new m_mediadetails();
            m_details.duongdanfile = linkvideo;
            m_details.trangthai = 3;
            m_details.kieufile = "video";
            m_details.idmedia = media.id;
            m_details.ngaytao = DateTime.Now;
            m_details.idtaikhoan = session.id;
            m_details.tenfile = "video-" + DisplayUntils.TaoMatKhau();
            entity.m_mediadetails.Add(m_details);

            entity.SaveChanges();

            SysMess.IsSuccess = true;
            //    SysMess.Message = SystemMessageConst.systemmessage.AddSuccess;
            SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AddSuccess, SystemMessageConst.systemmessage.AddSuccessEn);
            return SysMess;
        }

        public SystemMess BN_DeleteLibaryVideo(int id)
        {
            bool statusDell = false;
            var checkItem = entity.m_media.Where(m => m.id == id && m.theloai == "video").FirstOrDefault();
            if (checkItem != null)
            {
                var thongtintk = entity.m_taikhoan.Where(a => a.id == checkItem.idnguoitao).ToList().Select(a => new
                {
                    a.id,
                    a.email,
                    matkhau = a.matkhau,
                    //  tendaydu = a.idloaitaikhoan == 1 ? a.m_thongtincanhan.Where(x => x.idtaikhoan == a.id).Select(x => x.hovaten).FirstOrDefault() : a.m_thongtincanhan.Where(x => x.idtaikhoan == a.id).Select(x => x.m_thongtincoquan.tencoquan).FirstOrDefault(),
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
                    // SysMess.Message = SystemMessageConst.systemmessage.DeleteFail;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteFail, SystemMessageConst.systemmessage.DeleteFailEn);
                }
                if (statusDell)
                {
                    checkItem.trangthai = 0;
                    entity.SaveChanges();
                    SysMess.IsSuccess = true;
                    //   SysMess.Message = SystemMessageConst.systemmessage.DeleteSuccess;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteSuccess, SystemMessageConst.systemmessage.DeleteSuccessEn);

                    string mailto = thongtintk.email;
                    //  string subject = string.Format("THÔNG BÁO TỪ QUẢN TRỊ HỆ THỐNG");
                    string subject = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HeaderEmail, ListMailAddress.ContentEmailSystem.HeaderEmailEn);
                    string body = "";
                    body = string.Format("<div style='padding: 30px;border: 1px solid green;border-radius: 5px;'>"
                                      + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HelloAccount, ListMailAddress.ContentEmailSystem.HelloAccountEn) + " <span style='color:red;'>{0}</span></label></div><br />"
                                      + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.ThankForSend, ListMailAddress.ContentEmailSystem.ThankForSendEn) + "</label></div><br />"
                                      + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.Topic.DeleteTopPic1, ListMailAddress.Topic.DeleteTopPicEn1) + "</label></div><br />"
                                      + "<ul>"
                                      + "<li  style='padding: 5px;'><b> {1} </b></li>"
                                      + "</ul> "
                                      + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.Topic.DeleteTopPic2, ListMailAddress.Topic.DeleteTopPicEn2) + "</label></div><br />"
                                      + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.Topic.DeleteTopPic3, ListMailAddress.Topic.DeleteTopPicEn3) + "</label></div><br />"
                                      + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.FooterEmail, ListMailAddress.ContentEmailSystem.FooterEmailEn)
                                      + "</div>",
                                      thongtintk.tendaydu, checkItem.tenchude);

                    bool a = SendEmailUntil.SendMail(mailto, body, subject);
                }
                else
                {
                    SysMess.IsSuccess = false;
                    // SysMess.Message = SystemMessageConst.systemmessage.DeleteFail;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteFail, SystemMessageConst.systemmessage.DeleteFailEn);
                }
            }
            else
            {
                SysMess.IsSuccess = false;
                // SysMess.Message = SystemMessageConst.systemmessage.DataIsEmpty;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DataIsEmpty, SystemMessageConst.systemmessage.DataIsEmptyEn);
            }
            return SysMess;
        }
        public SystemMess BN_UpdateLibraryVideo(string tenchude, string gioithieu, int phienban, string linkvideo, int id, int trangthai, string lydo)
        {
            string msg = "";
            var checkExit = entity.m_media.Where(m => m.id == id && m.theloai == "video").FirstOrDefault();

            if (checkExit == null)
            {
                SysMess.IsSuccess = false;
                //    SysMess.Message = SystemMessageConst.systemmessage.IsNotExist;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsNotExist, SystemMessageConst.systemmessage.IsNotExistEn);
                return SysMess;
            }

            //    var errorTen = new ValidateForm().CheckRequiredField(tenchude, "Tên chủ đề ", 3, 500);
            var errorTen = new ValidateForm().CheckRequiredField(tenchude, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Topic_name, SystemMessageConst.systemmessage.Topic_nameEn), 3, 500);
            if (errorTen.error == null) { tenchude = errorTen.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTen.error;
                return SysMess;
            }

            //   var errorGioithieu = new ValidateForm().CheckRequiredField(gioithieu, "Giới thiệu ", 3);
            var errorGioithieu = new ValidateForm().CheckRequiredField(gioithieu, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Introduce, SystemMessageConst.systemmessage.IntroduceEn), 3);
            if (errorGioithieu.error == null) { gioithieu = errorGioithieu.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorGioithieu.error;
                return SysMess;
            }
            if (phienban != 1 && phienban != 2)
            {
                SysMess.IsSuccess = false;
                //      SysMess.Message = "Mời bạn chọn phiên bản ngôn ngữ cho chủ đề";
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Select_Languge, SystemMessageConst.systemmessage.Select_LangugeEn);
                return SysMess;
            }
            if (!string.IsNullOrEmpty(linkvideo))
            {
                var errorLink = new ValidateForm().CheckRequiteIframe(linkvideo, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Iframe, SystemMessageConst.systemmessage.IframeEn));
                if (errorLink.error == null) { linkvideo = errorLink.valueName; }
                else
                {
                    SysMess.IsSuccess = false;
                    SysMess.Message = errorLink.error;
                    return SysMess;
                }
            }


            if (session.id == checkExit.idnguoitao.Value && checkExit.trangthai != 3)
            {

                checkExit.tenchude = tenchude;
                checkExit.gioithieu = gioithieu;
                checkExit.phienban = phienban;
                if (!string.IsNullOrEmpty(linkvideo))
                {
                    var checkFile = entity.m_mediadetails.Where(z => z.idmedia == checkExit.id && z.kieufile == "video").ToList();
                    if (checkFile.Count > 0)
                    {
                        foreach (var item in checkFile)
                        {
                            entity.m_mediadetails.Remove(item);
                        }

                    }
                    m_mediadetails m_d = new m_mediadetails();
                    m_d.duongdanfile = linkvideo;
                    m_d.trangthai = 3;
                    m_d.kieufile = "video";
                    m_d.idmedia = checkExit.id;
                    m_d.ngaytao = DateTime.Now;
                    m_d.tenfile = "video-" + DisplayUntils.TaoMatKhau();
                    m_d.idtaikhoan = session.id;
                    entity.m_mediadetails.Add(m_d);

                    entity.SaveChanges();
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
                        //  msg = "Cập nhật thông tin và phê duyệt chủ đề thành công";
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
                        // msg = "Cập nhật thông tin và phê duyệt chủ đề thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Update_and_approve_topic_successfully, SystemMessageConst.systemmessage.Update_and_approve_topic_successfullyEn);
                    }
                    else
                    {
                        //   msg = "Cập nhật thông tin chủ đề thành công";
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
                    checkExit.idnguoipheduyet = session.id;
                    checkExit.tennguoipheduyet = session.hovaten;
                    checkExit.thoigianpheduyet = DateTime.Now;
                    if (trangthai == 1)
                    {
                        checkExit.lydo = (string.IsNullOrEmpty(lydo)) ? '"' + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizers, SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizersEn) + '"' : lydo;
                        //  msg = "Từ chối phê duyệt chủ đề thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Refusal_to_approve_the_topic_successfully, SystemMessageConst.systemmessage.Refusal_to_approve_the_topic_successfullyEn);
                    }
                    else if (trangthai == 5)
                    {
                        checkExit.lydo = (string.IsNullOrEmpty(lydo)) ? '"' + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizers, SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizersEn) + '"' : lydo;
                        //  msg = "Gỡ chủ đề khỏi website thành công";
                        msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Remove_the_theme_from_the_website_successfully, SystemMessageConst.systemmessage.Remove_the_theme_from_the_website_successfullyEn);
                    }
                    else
                    {
                        checkExit.lydo = null;
                        //   msg = "Thay đổi trạng thái chủ đề thành công";
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
                        //   msg = "Bạn không thể thay đổi nội dung chủ đề";
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
    }
}