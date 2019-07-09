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

namespace DataContext.AdminBusiness
{
    public class SpeakerBusiness
    {
        SystemMess SysMess = new SystemMess();
        ASS_35Entities entity = new ASS_35Entities();
        SessionUser session = (SessionUser)HttpContext.Current.Session["uSession"];

        public SystemMess LoadAllSpeaker(bool bsearch, string search)
        {

            var danhsach = entity.m_speaker.Where(a =>
                    (
                        !bsearch ? true :
                        (
                           (a.hoten.ToLower().Trim().Contains(search.ToLower().Trim())) ||
                           (a.chucvu.ToLower().Trim().Contains(search.ToLower().Trim())) ||
                           (a.noidung.ToLower().Trim().Contains(search.ToLower().Trim()))
                        )
                    ) && (a.trangthai == 3 || a.trangthai == 4) && session.idquyentaikhoan == 1
                   ).ToList().Select(a => new
                   {
                       a.id,
                       a.hoten,
                       a.avatar,
                       a.chucvu,
                       a.trangthai,
                       a.file_abstract,
                       a.presentation,
                       kieu = a.type,
                       type = (a.type == "GUESTSPEAKER") ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.GUESTSPEAKER, SystemMessageConst.systemmessage.GUESTSPEAKEREN) : (a.type == "KEYNOTESPEAKER" ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.KEYNOTESPEAKER, SystemMessageConst.systemmessage.KEYNOTESPEAKEREN) : (a.type == "PANELIST" ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.PANELIST, SystemMessageConst.systemmessage.PANELISTEN) : DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unknown, SystemMessageConst.systemmessage.UnknownEn))),
                       a.noidung,
                       a.ngaytao
                   }).OrderByDescending(a => a.ngaytao);
            SysMess.IsSuccess = true;
            SysMess.LstData = danhsach.Cast<object>().ToList();
            return SysMess;
        }

        public SystemMess BN_AddNewSpeaker(string hoten, string noidung, string chucvu, string type, string avatar, int trangthai, List<int> lstFile1, List<int> lstFile2)
        {

            var errorTen = new ValidateForm().CheckRequiredField(hoten, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Fullname, SystemMessageConst.systemmessage.FullnameEn), 3, 500);
            if (errorTen.error == null) { hoten = errorTen.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTen.error;
                return SysMess;
            }

            var errorGioithieu = new ValidateForm().CheckNonRequiredField(chucvu, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Position, SystemMessageConst.systemmessage.PositionEn), 3);
            if (errorGioithieu.error == null) { chucvu = errorGioithieu.valueName; }
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

            if (type != "GUESTSPEAKER" && type != "KEYNOTESPEAKER" && type != "PANELIST")
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Localtion, SystemMessageConst.systemmessage.LocaltionEn);
                return SysMess;
            }

            if (string.IsNullOrEmpty(avatar))
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Missingavatars, SystemMessageConst.systemmessage.MissingavatarsEn);
            }

            m_speaker sp = new m_speaker();

            sp.hoten = hoten;
            if(!string.IsNullOrEmpty(chucvu)){
                sp.chucvu = chucvu;
            }
            sp.avatar = avatar;

            int idItem = 0;
            for (int i = 0; i < lstFile1.Count; i++)
            {
                idItem = lstFile1[i];
                var check = entity.m_mediadetails.Where(m => m.id == idItem).FirstOrDefault();
                if (check != null)
                {
                    check.trangthai = 3;
                    sp.file_abstract = check.duongdanfile;
                }
            }

            for (int i = 0; i < lstFile2.Count; i++)
            {
                idItem = lstFile2[i];
                var check = entity.m_mediadetails.Where(m => m.id == idItem).FirstOrDefault();
                if (check != null)
                {
                    check.trangthai = 3;
                    sp.presentation = check.duongdanfile;
                }
            }
            sp.noidung = noidung;
            sp.type = type;
            sp.trangthai = trangthai;
            sp.ngaytao = DateTime.Now;
            entity.m_speaker.Add(sp);

            entity.SaveChanges();

            SysMess.IsSuccess = true;
            SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AddSuccess, SystemMessageConst.systemmessage.AddSuccessEn);
            return SysMess;
        }

        public SystemMess BN_DeleteSpeaker(int id)
        {
            var checkItem = entity.m_speaker.Where(m => m.id == id).FirstOrDefault();
            if (checkItem != null)
            {

                if (session.idquyentaikhoan == 1)
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


        public SystemMess BN_UpdateSpeaker(string hoten, string chucvu, string phienban, int id, int trangthai, string noidung, string avatar, List<int> lstFile1, List<int> lstFile2)
        {
            string msg = "";
            var checkExit = entity.m_speaker.Where(m => m.id == id).FirstOrDefault();

            if (checkExit == null)
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsNotExist, SystemMessageConst.systemmessage.IsNotExistEn);
                return SysMess;
            }

            var errorTen = new ValidateForm().CheckRequiredField(hoten, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Fullname, SystemMessageConst.systemmessage.FullnameEn), 3, 500);
            if (errorTen.error == null) { hoten = errorTen.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorTen.error;
                return SysMess;
            }

            var errorGioithieu = new ValidateForm().CheckNonRequiredField(chucvu, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Position, SystemMessageConst.systemmessage.PositionEn), 3);
            if (errorGioithieu.error == null) { chucvu = errorGioithieu.valueName; }
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

            if (phienban != "GUESTSPEAKER" && phienban != "KEYNOTESPEAKER" && phienban != "PANELIST")
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Localtion, SystemMessageConst.systemmessage.LocaltionEn);
                return SysMess;
            }

            int idItem = 0;
            if (session.idquyentaikhoan == 1)
            {
                checkExit.hoten = hoten;
                if(!string.IsNullOrEmpty(chucvu)){
                    checkExit.chucvu = chucvu;
                }
                checkExit.noidung = noidung;
                checkExit.type = phienban;
                checkExit.trangthai = trangthai;
                if (!string.IsNullOrEmpty(avatar))
                {
                    checkExit.avatar = avatar;
                }
                if (lstFile1.Count > 0)
                {
                    idItem = lstFile1[0];
                    var check = entity.m_mediadetails.Where(m => m.id == idItem).FirstOrDefault();
                    if (check != null)
                    {
                        check.trangthai = 3;
                        checkExit.file_abstract = check.duongdanfile;
                    }
                }
                if (lstFile2.Count > 0)
                {
                    idItem = lstFile2[0];
                    var check = entity.m_mediadetails.Where(m => m.id == idItem).FirstOrDefault();
                    if (check != null)
                    {
                        check.trangthai = 3;
                        checkExit.presentation = check.duongdanfile;
                    }
                }

                entity.SaveChanges();

                SysMess.IsSuccess = true;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EditSuccess, SystemMessageConst.systemmessage.EditSuccessEn);
                return SysMess;
            }
            else
            {
                msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Failed_to_update_post_information, SystemMessageConst.systemmessage.Failed_to_update_post_informationEn);
                SysMess.IsSuccess = false;
                SysMess.Message = msg;
                return SysMess;
            }
        }
    }
}