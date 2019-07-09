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
    [CheckSessionUser]
    public class ConferenceIntroductionBusiness
    {
        SystemMess SysMess = new SystemMess();
        ASS_35Entities entity = new ASS_35Entities();
        SessionUser session = (SessionUser)HttpContext.Current.Session["uSession"];


        public SystemMess LoadAllConferenceIntroduction(bool bsearch, string search)
        {
            var session = (SessionUser)HttpContext.Current.Session["uSession"];
            var danhsach = entity.m_baivietgioithieu.Where(a =>
                    (
                        !bsearch ? true :
                        (
                           (a.noidung.ToLower().Trim().Contains(search.ToLower().Trim())) ||
                           (a.tenmenu.ToLower().Trim().Contains(search.ToLower().Trim()))
                        )
                    ) && a.trangthai == 3
                   ).ToList().Select(a => new
                   {
                       a.id,
                       a.tenmenu,
                       a.noidung,
                       a.url,
                       ngaytao = a.ngaytao.Value.ToString("dd/MM/yyyy"),
                       ngay = a.ngaytao
                   }).OrderByDescending(a => a.ngay);
            SysMess.IsSuccess = true;
            SysMess.LstData = danhsach.Cast<object>().ToList();
            return SysMess;
        }

        public SystemMess BN_ConferenceIntroduction(string noidung, int id)
        {
            string msg = "";
            var checkExit = entity.m_baivietgioithieu.Where(m => m.id == id).FirstOrDefault();

            if (checkExit == null)
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsNotExist, SystemMessageConst.systemmessage.IsNotExistEn);
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

            if (session.idquyentaikhoan == 1)
            {
                checkExit.noidung = noidung;
                entity.SaveChanges();
                msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Update_post_successfully, SystemMessageConst.systemmessage.Update_post_successfullyEn);
            }
            else
            {
                msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Failed_to_update_post_information, SystemMessageConst.systemmessage.Failed_to_update_post_informationEn);
            }

            entity.SaveChanges();

            SysMess.IsSuccess = true;
            SysMess.Message = msg;
            return SysMess;
        }
    }
}