using DataContext.Until;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataContext.DAL;
using DataContext.Constants;

namespace DataContext.AdminBusiness
{
    public class SlideBusiness
    {
        SystemMess SysMess = new SystemMess();
        ASS_35Entities entity = new ASS_35Entities();
        SessionUser session = (SessionUser)HttpContext.Current.Session["uSession"];
        public List<SlideInformation_Result> ListImage(int site)
        {
            var entities = new ASS_35Entities();
            try
            {
                int roleId = AccountUntil.GetAccount().idquyentaikhoan;
                var result = entities.SlideInformation(roleId, site).OrderByDescending(a => a.stt).ToList();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public SystemMess CheckThutu(int stt)
        {
            SystemMess mess = new SystemMess();
            mess.IsSuccess = false;
           var entities = new ASS_35Entities();
            try
            {
                var result = entities.m_slidetrangchu.Where(a => a.stt == stt).ToList().FirstOrDefault();
                if (result != null)
                {
                    mess.IsSuccess = false;
                    mess.Message = "Trùng số thứ tự";
                    return mess;
                }
                else
                {
                    mess.IsSuccess = true;
                    mess.Message = "Không bị trùng";
                    return mess;
                }
            }
            catch (Exception e)
            {
                mess.IsSuccess = false;
                mess.Message = "Something wrong!";
                return mess;
            }
        }

        public m_slidetrangchu GetImageById(int id)
        {
            try
            {
                var result = entity.m_slidetrangchu.Where(a => a.id == id).FirstOrDefault();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public SystemMess RemoveImage(int id)
        {
            var check = entity.m_slidetrangchu.Where(a => a.id == id).FirstOrDefault();
            if (check == null)
            {
                SysMess.IsSuccess = false;
                SysMess.Message = "Khong tim thay anh";
                return SysMess;
            }
            else
            {
                check.trangthai = 5;
                if (entity.SaveChanges() == 1)
                {
                    SysMess.IsSuccess = true;
                    SysMess.Message = "Remove successful";
                }
                else
                {
                    SysMess.IsSuccess = false;
                    SysMess.Message = "Can not remove image";
                }
                
                return SysMess;
            }
        }
        public SystemMess BN_AddImage(string tieude, string gioithieu, string avatar, int thutu, int phienban,int trangthai, string duongdan)
        {
            var errorTen = new ValidateForm().CheckRequiredField(tieude, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Title, SystemMessageConst.systemmessage.TitleEn),3,500);
            if (errorTen.error == null) { tieude = errorTen.valueName;}
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTen.error;
                return SysMess;
            }
            var errorGioithieu = new ValidateForm().CheckNonRequiredField(gioithieu, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Introduce, SystemMessageConst.systemmessage.IntroduceEn), 3);
            if (errorGioithieu.error == null) { gioithieu = errorGioithieu.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorGioithieu.error;
                return SysMess;
            }
            var errorDuongdan = new ValidateForm().CheckNonRequiteURL(duongdan,"Duong dan");
            if (errorDuongdan.error == null )
            {
                duongdan = errorDuongdan.valueName;
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
            // check thutu
            var check = entity.m_slidetrangchu.Where(a => a.stt == thutu && a.trangthai ==3).FirstOrDefault();
            if (check != null)
            {
                SysMess.IsSuccess = false;
                // SysMess.Message = "Bạn chưa chọn avatar cho bài viết";
                SysMess.Message = "Trung thu tu";
                return SysMess;
            }
            m_slidetrangchu slide = new m_slidetrangchu();
            slide.tenanh = tieude;
            slide.trichdan = gioithieu;
            slide.hinhanh = avatar;
            slide.stt = thutu;
            slide.trangthai = trangthai;
            slide.phienban = phienban;
            slide.ngaytao = DateTime.Now;
            slide.idnguoitao = session.id;
            slide.duongdan = duongdan;

            entity.m_slidetrangchu.Add(slide);
            entity.SaveChanges();
            SysMess.IsSuccess = true;
            //SysMess.Message = SystemMessageConst.systemmessage.AddSuccess;
            SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AddSuccess, SystemMessageConst.systemmessage.AddSuccessEn);
            return SysMess;
        }
        public SystemMess BN_EditImage(int id,string tieude, string gioithieu, string avatar, int thutu, int phienban, int trangthai, string duongdan)
        {
            var check = entity.m_slidetrangchu.Where(a => a.id == id && trangthai ==3).FirstOrDefault();
            if (check != null)
            {
                var errorTen = new ValidateForm().CheckRequiredField(tieude,
                    DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Title,
                        SystemMessageConst.systemmessage.TitleEn), 3, 500);
                if (errorTen.error == null)
                {
                    tieude = errorTen.valueName;
                }
                else
                {
                    SysMess.IsSuccess = false;
                    SysMess.Message = errorTen.error;
                    return SysMess;
                }
                var errorGioithieu = new ValidateForm().CheckNonRequiredField(gioithieu,
                    DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Introduce,
                        SystemMessageConst.systemmessage.IntroduceEn), 3);
                if (errorGioithieu.error == null)
                {
                    gioithieu = errorGioithieu.valueName;
                }
                else
                {
                    SysMess.IsSuccess = false;
                    SysMess.Message = errorGioithieu.error;
                    return SysMess;
                }
                var errorDuongdan = new ValidateForm().CheckNonRequiteURL(duongdan, "Duong dan");
                if (errorDuongdan.error == null)
                {
                    duongdan = errorDuongdan.valueName;
                }
                if (phienban != 1 && phienban != 2)
                {
                    SysMess.IsSuccess = false;
                    //  SysMess.Message = "Mời bạn chọn phiên bản ngôn ngữ cho bài viết";
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(
                        SystemMessageConst.systemmessage.Select_Languge,
                        SystemMessageConst.systemmessage.Select_LangugeEn);
                    return SysMess;
                }
                if (string.IsNullOrEmpty(avatar))
                {
                    avatar = check.hinhanh;
                }
                // check thutu
                var check1 = entity.m_slidetrangchu.Where(a => a.stt == thutu && a.id != check.id).FirstOrDefault();
                if (check1 != null)
                {
                    SysMess.IsSuccess = false;
                    // SysMess.Message = "Bạn chưa chọn avatar cho bài viết";
                    SysMess.Message = "Trung thu tu";
                    return SysMess;
                }

                check.tenanh = tieude;
                check.trichdan = gioithieu;
                check.hinhanh = avatar;
                check.stt = thutu;
                check.trangthai = trangthai;
                check.phienban = phienban;
                check.ngaytao = DateTime.Now;               
                check.idnguoisua = session.id;
                check.duongdan = duongdan;
                entity.SaveChanges();
                SysMess.IsSuccess = true;
                //SysMess.Message = SystemMessageConst.systemmessage.AddSuccess;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EditSuccess,
                    SystemMessageConst.systemmessage.EditSuccessEn);
                return SysMess;
            }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = "Seeking no data";
                return SysMess;
            }
        }

        public SystemMess DeleteImage(int id)
        {
            var check = entity.m_slidetrangchu.Where(a => a.id == id).ToList().FirstOrDefault();
            if (check == null)
            {
                SysMess.IsSuccess = false;
                SysMess.Message = "Counldn't find image";
                return SysMess;
            }
            else
            {
                check.trangthai = 0;
                if (entity.SaveChanges() == 1)
                {
                    SysMess.IsSuccess = true;
                    SysMess.Message = "Deleting image successfully";
                    return SysMess;
                }
                else
                {
                    SysMess.IsSuccess = false;
                    SysMess.Message = "Something goes wrong!!!";
                    return SysMess;
                }
            }
        }

    }
}