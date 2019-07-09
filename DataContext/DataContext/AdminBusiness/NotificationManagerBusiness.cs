using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Socket;
using DataContext.Until;

namespace DataContext.AdminBusiness
{
    public class NotificationManagerBusiness
    {
        public string ValidateAddNotification(m_thongbao venue)
        {
            var result = new ValidateForm.dataInput();
            var check = new ValidateForm();
            result = check.CheckRequiredField(venue.tenthongbao, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Name_venue, SystemMessageConst.systemmessage.Name_venueEn), 3, 50);
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
            result = check.CheckNonRequiredField(venue.lido, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Reason, SystemMessageConst.systemmessage.ReasonEn),0,1000);
            if (result.error != null)
            {
                return result.error;
            }
            return null;
        }
        public m_thongbao GetNotifyById(int id)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myRoleId = AccountUntil.GetAccount().idquyentaikhoan;
                    var myId = AccountUntil.GetAccount().id;
                    if (myRoleId == SystemMessageConst.RoleConst.Banthuky)
                    {
                        var result =
                            entities.m_thongbao.FirstOrDefault(
                                ob => ob.id == id && ob.trangthai != SystemMessageConst.StatusConst.Delete && ob.idnguoitao == myId);
                        return result;
                    }
                    if (myRoleId == SystemMessageConst.RoleConst.Admin)
                    {
                        var result =
                            entities.m_thongbao.FirstOrDefault(
                                ob => ob.id == id && ob.trangthai != SystemMessageConst.StatusConst.Delete);
                        return result;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SystemMess AddNotification(m_thongbao venue,List<m_filethongbao> lstFile, string venueTarget)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateAddNotification(venue);
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
                    var listAccount =
                        entities.m_taikhoan.Where(ob => ob.trangthai == SystemMessageConst.StatusConst.Active && ob.m_quyentaikhoan.Any(x => x.idquyen == SystemMessageConst.RoleConst.Daibieu || x.idquyen == SystemMessageConst.RoleConst.Phongvien)).ToList();
                    //                    var listNotificaDetail = entities.m_chitietthongbao.ToList();
                    int _venueTarget;
                    Int32.TryParse(venueTarget, out _venueTarget);
                    venue.idnguoitao = myId;
                    venue.target = _venueTarget;
                    venue.thoigiantao = DateTime.Now;
                    if (myRoleId != SystemMessageConst.RoleConst.Admin &&
                        myRoleId != SystemMessageConst.RoleConst.Banthuky)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    if (myRoleId == SystemMessageConst.RoleConst.Admin)
                    {
                        venue.idnguoiduyet = myId;
                        venue.thoigianduyet = DateTime.Now;
                    }
                    entities.m_thongbao.Add(venue);
                    entities.SaveChanges();
                    if (lstFile.Count > 0)
                    {
                        foreach (var item in lstFile)
                        {
                            item.id_thongbao = venue.id;
                            entities.m_filethongbao.Add(item);
                        }
                        entities.SaveChanges();
                    }
                    if (venueTarget == "-1")
                    {
                        foreach (var item in listAccount)
                        {
                                    m_chitietthongbao notifiDetail = new m_chitietthongbao();
                                    notifiDetail.trangthai = SystemMessageConst.StatusConst.Active;
                                    notifiDetail.daxem = false;
                                    notifiDetail.ngaytao = DateTime.Now;
                                    notifiDetail.idthongbao = venue.id;
                                    notifiDetail.idtaikhoan = item.id;
                                    entities.m_chitietthongbao.Add(notifiDetail);
                        }
                    }
                    if (venueTarget == "-2")
                    {
                        foreach (var item in listAccount.Where(ob => ob.m_quyentaikhoan.Any(x => x.idquyen == SystemMessageConst.RoleConst.Phongvien)))
                        {
                                m_chitietthongbao notifiDetail = new m_chitietthongbao();
                                notifiDetail.trangthai = SystemMessageConst.StatusConst.Active;
                                notifiDetail.daxem = false;
                                notifiDetail.ngaytao = DateTime.Now;
                                notifiDetail.idthongbao = venue.id;
                                notifiDetail.idtaikhoan = item.id;
                                entities.m_chitietthongbao.Add(notifiDetail);
                        }
                    }
                    if (venueTarget == "-3")
                    {
                        foreach (var item in listAccount.Where(ob => ob.m_quyentaikhoan.Any(x => x.idquyen == SystemMessageConst.RoleConst.Daibieu)))
                        {
                                m_chitietthongbao notifiDetail = new m_chitietthongbao();
                                notifiDetail.trangthai = SystemMessageConst.StatusConst.Active;
                                notifiDetail.daxem = false;
                                notifiDetail.ngaytao = DateTime.Now;
                                notifiDetail.idthongbao = venue.id;
                                notifiDetail.idtaikhoan = item.id;
                                entities.m_chitietthongbao.Add(notifiDetail);
                        }
                    }
                    if (venueTarget != "-3" && venueTarget != "-2" && venueTarget != "-1")
                    {
                     var lstId =  venueTarget.Split(',');
                        for (int i = 0; i < lstId.Length - 1; i++)
                        {
                            int idAcc;
                            Int32.TryParse(lstId[i], out idAcc);
                                m_chitietthongbao notifiDetail = new m_chitietthongbao();
                                notifiDetail.trangthai = SystemMessageConst.StatusConst.Active;
                                notifiDetail.daxem = false;
                                notifiDetail.ngaytao = DateTime.Now;
                                notifiDetail.idthongbao = venue.id;
                                notifiDetail.idtaikhoan = idAcc;
                                entities.m_chitietthongbao.Add(notifiDetail);
                        }
                    }
                    entities.SaveChanges();
                    //gui socket
                    if (venue.trangthai == SystemMessageConst.StatusConst.Active && myRoleId == SystemMessageConst.RoleConst.Admin)
                    {
                        //kiem tra danh sach id can gui
                        var lstDetail = entities.m_chitietthongbao
                            .Where(ob => ob.idthongbao == venue.id &&
                                         ob.trangthai == SystemMessageConst.StatusConst.Active).ToList();
                        foreach (var item in lstDetail)
                        {
                            SocketProcessRequest.SocketRequest("1", item.idtaikhoan.ToString(), 2);
                        }
                    }
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
        public SystemMess EditNotification(m_thongbao venue,List<m_filethongbao> lstFile,string venueTarget)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateAddNotification(venue);
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
                    var listAccount =
                        entities.m_taikhoan.Where(ob => ob.trangthai == SystemMessageConst.StatusConst.Active).ToList();
                    var result =
                        entities.m_thongbao.FirstOrDefault(ob => ob.id == venue.id &&
                                                                      ob.trangthai != SystemMessageConst.StatusConst
                                                                          .Delete);

                    if (result == null && myRoleId != SystemMessageConst.RoleConst.Admin && myRoleId != SystemMessageConst.RoleConst.Banthuky)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    int _venueTarget;
                    Int32.TryParse(venueTarget, out _venueTarget);
                    if (result.trangthai != SystemMessageConst.StatusConst.Active)
                    {
                        result.target = _venueTarget;
                        result.noidung = venue.noidung;
                        result.ghichu = venue.ghichu;
                        result.tenthongbao = venue.tenthongbao;
                        result.lido = venue.lido;
                        if (lstFile.Count > 0)
                        {
                            foreach (var item in lstFile)
                            {
                                item.id_thongbao = venue.id;
                                entities.m_filethongbao.Add(item);
                            }
                        }
                        var listNotifiDetail = entities.m_chitietthongbao.Where(ob => ob.idthongbao == venue.id)
                            .ToList();
                        foreach (var item in listNotifiDetail)
                        {
                            entities.m_chitietthongbao.Remove(item);
                        }
                        entities.SaveChanges();
                        if (venueTarget == "-1")
                        {
                            foreach (var item in listAccount)
                            {
                                m_chitietthongbao notifiDetail = new m_chitietthongbao();
                                notifiDetail.trangthai = SystemMessageConst.StatusConst.Active;
                                notifiDetail.daxem = false;
                                notifiDetail.ngaytao = DateTime.Now;
                                notifiDetail.idthongbao = venue.id;
                                notifiDetail.idtaikhoan = item.id;
                                entities.m_chitietthongbao.Add(notifiDetail);
                            }
                        }
                        if (venueTarget == "-2")
                        {
                            foreach (var item in listAccount.Where(
                                ob => ob.m_quyentaikhoan.Any(x => x.idquyen == SystemMessageConst.RoleConst.Phongvien)))
                            {
                                m_chitietthongbao notifiDetail = new m_chitietthongbao();
                                notifiDetail.trangthai = SystemMessageConst.StatusConst.Active;
                                notifiDetail.daxem = false;
                                notifiDetail.ngaytao = DateTime.Now;
                                notifiDetail.idthongbao = venue.id;
                                notifiDetail.idtaikhoan = item.id;
                                entities.m_chitietthongbao.Add(notifiDetail);
                            }
                        }
                        if (venueTarget == "-3")
                        {
                            foreach (var item in listAccount.Where(
                                ob => ob.m_quyentaikhoan.Any(x => x.idquyen == SystemMessageConst.RoleConst.Daibieu)))
                            {
                                m_chitietthongbao notifiDetail = new m_chitietthongbao();
                                notifiDetail.trangthai = SystemMessageConst.StatusConst.Active;
                                notifiDetail.daxem = false;
                                notifiDetail.ngaytao = DateTime.Now;
                                notifiDetail.idthongbao = venue.id;
                                notifiDetail.idtaikhoan = item.id;
                                entities.m_chitietthongbao.Add(notifiDetail);
                            }
                        }
                        if (venueTarget != "-3" && venueTarget != "-2" && venueTarget != "-1")
                        {
                            var lstId = venueTarget.Split(',');
                            for (int i = 0; i < lstId.Length - 1; i++)
                            {
                                int idAcc;
                                Int32.TryParse(lstId[i], out idAcc);
                                m_chitietthongbao notifiDetail = new m_chitietthongbao();
                                notifiDetail.trangthai = SystemMessageConst.StatusConst.Active;
                                notifiDetail.daxem = false;
                                notifiDetail.ngaytao = DateTime.Now;
                                notifiDetail.idthongbao = venue.id;
                                notifiDetail.idtaikhoan = idAcc;
                                entities.m_chitietthongbao.Add(notifiDetail);
                            }
                        }
                        entities.SaveChanges();
                    }
                    if (venue.trangthai == SystemMessageConst.StatusConst.Active || venue.trangthai == SystemMessageConst.StatusConst.RemoveOnSite || venue.trangthai == SystemMessageConst.StatusConst.Deactive)
                    {
                        if (myRoleId == SystemMessageConst.RoleConst.Admin)
                        {
                            result.trangthai = venue.trangthai;
                            result.idnguoiduyet = myId;
                            result.thoigianduyet = DateTime.Now;

                        }
                        else
                        {
                            result.trangthai = SystemMessageConst.StatusConst.Deactive;
                        }
                       
                    }
                    else
                    {
                        result.trangthai = venue.trangthai;
                    }
                    entities.SaveChanges();
                    //gui socket
                    if (venue.trangthai == SystemMessageConst.StatusConst.Active && myRoleId == SystemMessageConst.RoleConst.Admin)
                    {
                        //kiem tra danh sach id can gui
                        var lstDetail = entities.m_chitietthongbao
                            .Where(ob => ob.idthongbao == venue.id &&
                                         ob.trangthai == SystemMessageConst.StatusConst.Active).ToList();
                        foreach (var item in lstDetail)
                        {
                            SocketProcessRequest.SocketRequest("1",item.idtaikhoan.ToString(),2);
                        }
                    }
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
        public List<NotificationManager_Result> GetListNotification(int status)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    int myId = AccountUntil.GetAccount().id;
                    int roleId = AccountUntil.GetAccount().idquyentaikhoan;
                    var result = entities.NotificationManager(roleId, myId, status).OrderByDescending(ob => ob.thoigiantao).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ListNotificationPerson_Result> GetMyNotificationNotRead()
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    int myId = AccountUntil.GetAccount().id;
                    var result = entities.ListNotificationPerson(myId).Where(ob => ob.daxem == false).OrderByDescending(ob => ob.thoigiantao).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<m_taikhoan> GetListAccountReporterPending()
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result =
                        entities.m_taikhoan.Where(
                            ob => ob.m_quyentaikhoan.Any(
                                x =>(x.idquyen == SystemMessageConst.RoleConst.Daibieu ||
                                      x.idquyen == SystemMessageConst.RoleConst.Phongvien) && x.trangthai == SystemMessageConst.StatusConst.Active && ob.id == x.idtaikhoan ) && ob.trangthai == SystemMessageConst.StatusConst.Pending && ob.isConfirmEmail == true).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int GetFirtRoleAccount(int idAcc)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result =
                        entities.m_quyentaikhoan.FirstOrDefault(ob => ob.idtaikhoan == idAcc &&
                                                                      ob.trangthai == SystemMessageConst.StatusConst.Active);
                    if (result == null)
                    {
                        return -1;
                    }
                    else
                    {
                        return result.idquyen.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public List<ListNotificationPerson_Result> GetListNotificationPerson()
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    int myId = AccountUntil.GetAccount().id;
                    var result = entities.ListNotificationPerson(myId).OrderByDescending(ob => ob.thoigianduyet).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<m_filethongbao> GetListFile()
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result =
                        entities.m_filethongbao.Where(ob => ob.trangthai != SystemMessageConst.StatusConst.Delete).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<m_filethongbao> GetListFileByNotifiId(int id)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result =
                        entities.m_filethongbao.Where(ob => ob.trangthai != SystemMessageConst.StatusConst.Delete && ob.id_thongbao == id).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public m_filethongbao GetFileById(int id)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result =
                        entities.m_filethongbao.FirstOrDefault(ob => ob.trangthai != SystemMessageConst.StatusConst.Delete && ob.id == id && ob.m_thongbao.trangthai != SystemMessageConst.StatusConst.Delete);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SystemMess ReadNotificaStatus(int id)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myId = AccountUntil.GetAccount().id;
                    var check = entities.m_chitietthongbao.FirstOrDefault(ob => ob.id == id && ob.idtaikhoan == myId && ob.trangthai == SystemMessageConst.StatusConst.Active && ob.daxem == false);
                    if (check == null)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message =
                            DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData,
                                SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    check.daxem = true;
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                    systemMess.Message = entities.m_chitietthongbao.Where(ob => ob.idtaikhoan == myId && ob.trangthai == SystemMessageConst.StatusConst.Active && ob.daxem == false && ob.m_thongbao.trangthai == SystemMessageConst.StatusConst.Active).ToList().Count.ToString();
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
        public SystemMess DeleteNotification(int id)
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
                        //systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    var result = entities.m_thongbao.FirstOrDefault(ob => ob.id == id && ob.trangthai != SystemMessageConst.StatusConst.Delete);
                    if (result == null)
                    {
                        systemMess.IsSuccess = false;
                       // systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    if (myRoleId == SystemMessageConst.RoleConst.Admin)
                    {
                        result.trangthai = SystemMessageConst.StatusConst.Delete;
                        entities.SaveChanges();
                     //   if (result.idnguoitao != myId)
                       // {
                            var director = entities.m_taikhoan.SingleOrDefault(ob => ob.id == result.idnguoitao);
                          //  SendEmailUntil.SendMailRemovePost(director.email, "Chủ đề : " + result.tenthongbao);
                            SendEmailUntil._SendMailRemoveNotification(director.email, result.tenthongbao);
                        //}
                    }
                    if (myRoleId == SystemMessageConst.RoleConst.Banthuky)
                    {
                        if (result.trangthai != SystemMessageConst.StatusConst.Active && result.idnguoitao == myId)
                        {
                            result.trangthai = SystemMessageConst.StatusConst.Delete;
                            entities.SaveChanges();

                            var director = entities.m_taikhoan.SingleOrDefault(ob => ob.id == result.idnguoitao);
                            SendEmailUntil._SendMailRemoveNotification(director.email, result.tenthongbao);
                        }
                    }
                    systemMess.IsSuccess = true;
                 //   systemMess.Message = SystemMessageConst.systemmessage.DeleteSuccess;
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
        public SystemMess DeleteNotificationFile(int id)
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
                    var result = entities.m_filethongbao.FirstOrDefault(ob => ob.id == id && ob.trangthai != SystemMessageConst.StatusConst.Delete);
                    if (result == null)
                    {
                        systemMess.IsSuccess = false;
                      //  systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    if (myRoleId == SystemMessageConst.RoleConst.Admin)
                    {
                        result.trangthai = SystemMessageConst.StatusConst.Delete;
                        entities.SaveChanges();
                    }
                    if (myRoleId == SystemMessageConst.RoleConst.Banthuky)
                    {
                        if (result.m_thongbao.idnguoitao == myId && result.trangthai != SystemMessageConst.StatusConst.Active)
                        {
                            result.trangthai = SystemMessageConst.StatusConst.Delete;
                            entities.SaveChanges();
                        }
                    }
                    systemMess.IsSuccess = true;
                   // systemMess.Message = SystemMessageConst.systemmessage.DeleteSuccess;
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

        public SystemMess EditStatusNotification(int? status, int id, string feedback)
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
                     //   systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    var result = entities.m_thongbao.FirstOrDefault(ob => ob.id == id && ob.trangthai != SystemMessageConst.StatusConst.Delete);

                    if (result == null)
                    {
                        systemMess.IsSuccess = false;
                       // systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    result.trangthai = status;
                    if (status == SystemMessageConst.StatusConst.Deactive && string.IsNullOrEmpty(feedback))
                    {
                        result.lido = SystemMessageConst.systemmessage.RejectTopicMessage;
                    }
                    else
                    {
                        result.lido = feedback;
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

        public SystemMess EditStatusFile(int? status, int id, string feedback)
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
                        //systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    var result = entities.m_filethongbao.FirstOrDefault(ob => ob.id == id && ob.trangthai != SystemMessageConst.StatusConst.Delete);
                   
                    if (result == null)
                    {
                        systemMess.IsSuccess = false;
                       // systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    if (result.m_thongbao.trangthai == SystemMessageConst.StatusConst.Delete)
                    {
                        systemMess.IsSuccess = false;
                     //   systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    result.trangthai = status;
                    if (status == SystemMessageConst.StatusConst.Deactive && string.IsNullOrEmpty(feedback))
                    {
                        result.lido = SystemMessageConst.systemmessage.RejectTopicMessage;
                    }
                    else
                    {
                        result.lido = feedback;
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
    }
}