using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Until;

namespace DataContext.AdminBusiness
{
    public class ManagerContactBusiness
    {
        public string ValidateAddContact(m_lienhe venue)
        {
            var result = new ValidateForm.dataInput();
            var check = new ValidateForm();
//            result = check.CheckRequiredField(venue.tendonvi, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Unit_name, SystemMessageConst.systemmessage.Unit_nameEn), 3, 50);
//            if (result.error != null)
//            {
//                return result.error;
//            }
            result = check.CheckRequiredField(venue.noidung, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Content, SystemMessageConst.systemmessage.ContentEn), 5);
            if (result.error != null)
            {
                return result.error;
            }
//            result = check.CheckRequiredField(venue.diachi, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Address, SystemMessageConst.systemmessage.AddressEn), 5, 200);
//            if (result.error != null)
//            {
//                return result.error;
//            }
            return null;
        }
        public SystemMess AddContact(m_lienhe venue)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateAddContact(venue);
                if (check != null)
                {
                    systemMess.IsSuccess = false;
                    systemMess.Message = check;
                    return systemMess;
                }
                using (var entities = new ASS_35Entities())
                {
                    var myId = AccountUntil.GetAccount().id;
                    var myRoleId = AccountUntil.GetAccount().idquyentaikhoan;
                    venue.idnguoitao = myId;
                    venue.thoigiantao = DateTime.Now;
                    venue.trangthai = venue.trangthai;
                    if (myRoleId != SystemMessageConst.RoleConst.Admin)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    entities.m_lienhe.Add(venue);
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                  //  systemMess.Message = SystemMessageConst.systemmessage.AddSuccess;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AddSuccess, SystemMessageConst.systemmessage.AddSuccessEn);
                    return systemMess;
                }
            }
            catch (Exception ex)
            {
                systemMess.IsSuccess = false;
                systemMess.Message = ex.ToString();
                return systemMess;
            }
        }
        public SystemMess EditContact(m_lienhe venue)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateAddContact(venue);
                if (check != null)
                {
                    systemMess.IsSuccess = false;
                    systemMess.Message = check;
                    return systemMess;
                }
                using (var entities = new ASS_35Entities())
                {
                    var myRoleId = AccountUntil.GetAccount().idquyentaikhoan;
                    if (myRoleId != SystemMessageConst.RoleConst.Admin)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    var result =
                        entities.m_lienhe.FirstOrDefault(ob => ob.phienban == venue.phienban);
                    if (result == null)
                    {
                        entities.m_lienhe.Add(venue);
                        entities.SaveChanges();
                    
                    }
                    else
                    {
                        result.noidung = venue.noidung;
                        entities.SaveChanges();
                    }
                    systemMess.IsSuccess = true;
                    systemMess.Message =
                        DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EditSuccess,
                            SystemMessageConst.systemmessage.EditSuccessEn);
                    return systemMess;
                }
            }
            catch (Exception ex)
            {
                systemMess.IsSuccess = false;
                systemMess.Message = ex.ToString();
                return systemMess;
            }
        }
        //        public SystemMess EditContact(m_lienhe venue)
        //        {
        //            SystemMess systemMess = new SystemMess();
        //            try
        //            {
        //                var check = ValidateAddContact(venue);
        //                if (check != null)
        //                {
        //                    systemMess.IsSuccess = false;
        //                    systemMess.Message = check;
        //                    return systemMess;
        //                }
        //                using (var entities = new ASS_35Entities())
        //                {
        //                    var myRoleId = AccountUntil.GetAccount().idquyentaikhoan;
        //                    if (myRoleId != SystemMessageConst.RoleConst.Admin)
        //                    {
        //                        systemMess.IsSuccess = false;
        //                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
        //                        return systemMess;
        //                    }
        //                    var result =
        //                        entities.m_lienhe.FirstOrDefault(
        //                            ob => ob.id == venue.id && ob.trangthai != SystemMessageConst.StatusConst.Delete);
        //                    if (result == null)
        //                    {
        //                        systemMess.IsSuccess = false;
        //                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
        //                        return systemMess;
        //                    }
        //                    result.diachi = venue.diachi;
        //                    result.trangthai = venue.trangthai;
        //                    result.noidung = venue.noidung;
        //                    result.tendonvi = venue.tendonvi;
        //                    result.phienban = venue.phienban;
        //                    entities.SaveChanges();
        //                    systemMess.IsSuccess = true;
        //                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EditSuccess, SystemMessageConst.systemmessage.EditSuccessEn);
        //                    return systemMess;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                systemMess.IsSuccess = false;
        //                systemMess.Message = ex.ToString();
        //                return systemMess;
        //            }
        //        }
        public m_lienhe GetContactById(int id)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myRoleId = AccountUntil.GetAccount().idquyentaikhoan;
                    if (myRoleId != SystemMessageConst.RoleConst.Admin)
                    {
                        return null;
                    }
                    var result =
                        entities.m_lienhe.FirstOrDefault(
                            ob => ob.id == id && ob.trangthai != SystemMessageConst.StatusConst.Delete);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //        public List<ContactInfomation_Result> GetListContact(int status)
        //        {
        //            try
        //            {
        //                using (var entities = new ASS_35Entities())
        //                {
        //                    int myId = AccountUntil.GetAccount().id;
        //                    var result = entities.ContactInfomation(myId, status).ToList();
        //                    return result;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                return null;
        //            }
        //        }
                public m_lienhe GetContact(int language)
                {
                    try
                    {
                        using (var entities = new ASS_35Entities())
                        {
                            var result = entities.m_lienhe.FirstOrDefault(ob => ob.phienban == language);
                            return result;
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
        public SystemMess DeleteContact(int id)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myRoleId = AccountUntil.GetAccount().idquyentaikhoan;
                    var myId = AccountUntil.GetAccount().id;
                    if (myRoleId != SystemMessageConst.RoleConst.Admin)
                    {
                        systemMess.IsSuccess = false;
                     //   systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    var result = entities.m_lienhe.FirstOrDefault(ob => ob.id == id);
                    if (result == null)
                    {
                        systemMess.IsSuccess = false;
                        //systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    if (myRoleId == SystemMessageConst.RoleConst.Admin)
                    {
                        result.trangthai = SystemMessageConst.StatusConst.Delete;
                        entities.SaveChanges();
                    }
                    systemMess.IsSuccess = true;
                 //   systemMess.Message = SystemMessageConst.systemmessage.DeleteSuccess;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteSuccess, SystemMessageConst.systemmessage.DeleteSuccess);
                    return systemMess;
                }
            }
            catch (Exception ex)
            {
                systemMess.IsSuccess = false;
                systemMess.Message = ex.ToString();
                return systemMess;
            }
        }
    }
}