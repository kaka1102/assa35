using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Until;

namespace DataContext.AdminBusiness
{
    public class VenueManagementBusiness
    {
        public string ValidateAddVenue(m_diadiemtochuc venue)
        {
            var result = new ValidateForm.dataInput();
            var check = new ValidateForm();
            result = check.CheckRequiredField(venue.tendiadiem, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Name_venue, SystemMessageConst.systemmessage.Name_venueEn), 3, 50);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckRequiredField(venue.noidung, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Content, SystemMessageConst.systemmessage.ContentEn), 5);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(venue.ghichu, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Note, SystemMessageConst.systemmessage.NoteEn), 5, 1000);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(venue.lydotuchoi, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Note, SystemMessageConst.systemmessage.NoteEn), 5, 1000);
            if (result.error != null)
            {
                return result.error;
            }
            return null;
        }

        public SystemMess AddVenue(m_diadiemtochuc venue)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateAddVenue(venue);
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
                    if (myRoleId != SystemMessageConst.RoleConst.Admin &&
                        myRoleId != SystemMessageConst.RoleConst.Banthuky)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    entities.m_diadiemtochuc.Add(venue);
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
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

        public SystemMess EditVenue(m_diadiemtochuc venue)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateAddVenue(venue);
                if (check != null)
                {
                    systemMess.IsSuccess = false;
                    systemMess.Message = check;
                    return systemMess;
                }
                using (var entities = new ASS_35Entities())
                {
                    var myId = AccountUntil.GetAccount().id;
                    var result =
                        entities.m_diadiemtochuc.FirstOrDefault(ob => ob.id == venue.id &&
                                                                      ob.trangthai != SystemMessageConst.StatusConst
                                                                          .Delete);

                    if (result == null)
                    {
                        systemMess.IsSuccess = false;
                       // systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    if (result.idnguoitao == myId && result.trangthai != SystemMessageConst.StatusConst.Active &&
                        result.trangthai != SystemMessageConst.StatusConst.Pending)
                    {
                        result.noidung = venue.noidung;
                        result.ghichu = venue.ghichu;
                        result.tendiadiem = venue.tendiadiem;
                        result.phienban = venue.phienban;
                        if (venue.avatar != null)
                        {
                            result.avatar = venue.avatar;
                        }
                    }
                    result.trangthai = venue.trangthai;
                    if (AccountUntil.GetAccount().idquyentaikhoan == SystemMessageConst.RoleConst.Admin)
                    {
                        result.lydotuchoi = venue.lydotuchoi;
                    }
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EditSuccess, SystemMessageConst.systemmessage.EditSuccessEn);
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

        public SystemMess EditStatusVenue(int? status,int id,string feedback)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myRoleId = AccountUntil.GetAccount().idquyentaikhoan;
                    if (myRoleId != SystemMessageConst.RoleConst.Admin)
                    {
                        systemMess.IsSuccess = false;
                       // systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    var result = entities.m_diadiemtochuc.FirstOrDefault(ob => ob.id == id && ob.trangthai != SystemMessageConst.StatusConst.Delete);

                    if (result == null)
                    {
                        systemMess.IsSuccess = false;
                        //systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    result.trangthai = status;
                    if (status == SystemMessageConst.StatusConst.Deactive && string.IsNullOrEmpty(feedback))
                    {
                        result.lydotuchoi = SystemMessageConst.systemmessage.RejectTopicMessage;
                    }
                    else
                    {
                        result.lydotuchoi = feedback;
                    }
                    
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                  //  systemMess.Message = SystemMessageConst.systemmessage.EditSuccess;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EditSuccess, SystemMessageConst.systemmessage.EditSuccessEn);
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

        public List<VenueInfomation_Result> GetListVenue(int status)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    int myId = AccountUntil.GetAccount().id;
                    int roleId = AccountUntil.GetAccount().idquyentaikhoan;
                    var result = entities.VenueInfomation(roleId, myId, status).OrderByDescending(ob => ob.thoigiantao).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public m_diadiemtochuc GetVenueById(int id)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myRoleId = AccountUntil.GetAccount().idquyentaikhoan;
                    if (myRoleId != SystemMessageConst.RoleConst.Admin &&
                        myRoleId != SystemMessageConst.RoleConst.Banthuky)
                    {
                        return null;
                    }
                    var result =
                        entities.m_diadiemtochuc.FirstOrDefault(
                            ob => ob.id == id && ob.trangthai != SystemMessageConst.StatusConst.Delete);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SystemMess DeleteVenue(int id)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myRoleId = AccountUntil.GetAccount().idquyentaikhoan;
                    var myId = AccountUntil.GetAccount().id;
                    if (myRoleId != SystemMessageConst.RoleConst.Admin && myRoleId != SystemMessageConst.RoleConst.Banthuky)
                    {
                        systemMess.IsSuccess = false;
                      //  systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    var result = entities.m_diadiemtochuc.FirstOrDefault(ob => ob.id == id);
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
                       // if (result.idnguoitao != myId)
                       // {
                            var director = entities.m_taikhoan.SingleOrDefault(ob => ob.id == result.idnguoitao);
                          //  SendEmailUntil.SendMailRemovePost(director.email,"Địa điểm : "+result.tendiadiem);
                            SendEmailUntil._SendMailRemoveLocation(director.email,  result.tendiadiem);
                     //   }
                    }
                    if (myRoleId == SystemMessageConst.RoleConst.Banthuky)
                    {
                        if (result.trangthai != SystemMessageConst.StatusConst.Active && result.idnguoitao == myId)
                        {
                            result.trangthai = SystemMessageConst.StatusConst.Delete;
                            entities.SaveChanges();
                        }
                    }
                    systemMess.IsSuccess = true;
               //     systemMess.Message = SystemMessageConst.systemmessage.DeleteSuccess;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteSuccess, SystemMessageConst.systemmessage.DeleteSuccessEn);
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