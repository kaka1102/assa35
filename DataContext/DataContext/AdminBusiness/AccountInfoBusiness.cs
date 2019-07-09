using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Until;

namespace DataContext.AdminBusiness
{

    public class AccountInfoBusiness
    {
        public string ValidateEditMember(m_thongtincanhan personInfo)
        {
            var result = new ValidateForm.dataInput();
            var check = new ValidateForm();
            //  result = check.CheckRequiredField(personInfo.hovaten, "Họ và tên", 5, 25);
            result = check.CheckRequiredField(personInfo.hovaten,
                DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Fullname,
                    SystemMessageConst.systemmessage.FullnameEn), 5, 25);
            if (result.error != null)
            {
                return result.error;
            }
            //  result = check.CheckRequiteEmail(personInfo.email, "Email cá nhân");
            result = check.CheckRequiteEmail(personInfo.email,
                DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Email,
                    SystemMessageConst.systemmessage.EmailEn));
            if (result.error != null)
            {
                return result.error;
            }
            //   result = check.CheckRequitePhone(personInfo.sodienthoai, "Số điện thoại cá nhân");
            result = check.CheckRequitePhone(personInfo.sodienthoai,
                DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Phone,
                    SystemMessageConst.systemmessage.PhoneEn));
            if (result.error != null)
            {
                return result.error;
            }
            //  result = check.CheckRequiredField(personInfo.chucvu, "Chức vụ", 3, 30);
            result = check.CheckRequiredField(personInfo.chucvu,
                DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Position,
                    SystemMessageConst.systemmessage.PositionEn), 3, 50);
            if (result.error != null)
            {
                return result.error;
            }
            //  result = check.CheckRequiredField(personInfo.chucvu, "Chức vụ", 3, 30);
            result = check.CheckRequiredField(personInfo.diachi,
                DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Address,
                    SystemMessageConst.systemmessage.AddressEn), 3, 200);
            if (result.error != null)
            {
                return result.error;
            }
            //  result = check.CheckRequiredField(personInfo.cmthochieu, "Số chứng minh nhân dân / Hộ chiếu", 3, 30);
            result = check.CheckRequiredField(personInfo.cmthochieu,
                DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.PersonCardId,
                    SystemMessageConst.systemmessage.PersonCardIdEn), 3, 50);
            if (result.error != null)
            {
                return result.error;
            }
            //    result = check.CheckRequiredField(personInfo.noicap, "Nơi cấp chứng minh nhân dân / Hộ chiếu", 3, 30);
            result = check.CheckRequiredField(personInfo.noicap,
                DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.PlaceOfIssue,
                    SystemMessageConst.systemmessage.PlaceOfIssueEn), 3, 50);
            if (result.error != null)
            {
                return result.error;
            }
            //   result = check.CheckNonRequiredField(personInfo.sothenhabao, "Số thẻ nhà báo", 5, 15);
            result = check.CheckRequiredField(personInfo.sothenhabao,
                DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.CardNumber,
                    SystemMessageConst.systemmessage.CardNumberEn), 5, 50);
            if (result.error != null)
            {
                return result.error;
            }
            return null;
        }

        public SystemMess AddMember(m_thongtincanhan personInfo)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateEditMember(personInfo);
                if (check != null)
                {
                    systemMess.IsSuccess = false;
                    systemMess.Message = check;
                    return systemMess;
                }
                using (var entities = new ASS_35Entities())
                {
                    var checkExistedEmail =
                        entities.m_thongtincanhan.FirstOrDefault(ob => ob.email == personInfo.email);
                    if (checkExistedEmail != null)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(
                            SystemMessageConst.systemmessage.EmailIsExisted,
                            SystemMessageConst.systemmessage.EmailIsExistedEn);
                        return systemMess;
                    }
                    var myId = AccountUntil.GetAccount().id;
                    //thêm dữ liệu thông tin cá nhân
                    personInfo.idtaikhoan = myId;
                    personInfo.trangthai = SystemMessageConst.StatusConst.Pending;
                    entities.m_thongtincanhan.Add(personInfo);
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                    //    systemMess.Message = SystemMessageConst.systemmessage.AddSuccess;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(
                        SystemMessageConst.systemmessage.AddSuccess, SystemMessageConst.systemmessage.AddSuccessEn);
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

        public SystemMess EditMember(m_thongtincanhan personInfo)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateEditMember(personInfo);
                if (check != null)
                {
                    systemMess.IsSuccess = false;
                    systemMess.Message = check;
                    return systemMess;
                }
                using (var entities = new ASS_35Entities())
                {
                    var checkExisted =
                        entities.m_thongtincanhan.FirstOrDefault(ob => ob.id == personInfo.id &&
                                                                       ob.trangthai != SystemMessageConst.StatusConst
                                                                           .Active && ob.trangthai != SystemMessageConst
                                                                           .StatusConst.Delete);
                    if (checkExisted == null)
                    {
                        systemMess.IsSuccess = false;
                        // systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(
                            SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    checkExisted.email = personInfo.email;
                    checkExisted.hovaten = personInfo.hovaten;
                    checkExisted.diachi = personInfo.diachi;
                    checkExisted.ngaysinh = personInfo.ngaysinh;
                    checkExisted.cmthochieu = personInfo.cmthochieu;
                    checkExisted.ngaycap = personInfo.ngaycap;
                    checkExisted.ngayhethan = personInfo.ngayhethan;
                    checkExisted.sothenhabao = personInfo.sothenhabao;
                    checkExisted.noicap = personInfo.noicap;
                    checkExisted.sodienthoai = personInfo.sodienthoai;
                    checkExisted.dangkyphong = personInfo.dangkyphong;
                    checkExisted.dangkyxe = personInfo.dangkyxe;

                    checkExisted.trangthai = SystemMessageConst.StatusConst.Pending;
                    if (!string.IsNullOrEmpty(personInfo.anhcmtmattruoc))
                    {
                        if (System.IO.File.Exists(HttpContext.Current.Request.MapPath(checkExisted.anhcmtmattruoc)))
                        {
                            System.IO.File.Delete(HttpContext.Current.Request.MapPath(checkExisted.anhcmtmattruoc));
                        }
                        checkExisted.anhcmtmattruoc = personInfo.anhcmtmattruoc;
                    }
                    if (!string.IsNullOrEmpty(personInfo.anhcmtmatsau))
                    {
                        if (System.IO.File.Exists(HttpContext.Current.Request.MapPath(checkExisted.anhcmtmatsau)))
                        {
                            System.IO.File.Delete(HttpContext.Current.Request.MapPath(checkExisted.anhcmtmatsau));
                        }
                        checkExisted.anhcmtmatsau = personInfo.anhcmtmatsau;
                    }
                    if (!string.IsNullOrEmpty(personInfo.fileanhthe))
                    {
                        if (System.IO.File.Exists(HttpContext.Current.Request.MapPath(checkExisted.fileanhthe)))
                        {
                            System.IO.File.Delete(HttpContext.Current.Request.MapPath(checkExisted.fileanhthe));
                        }
                        checkExisted.fileanhthe = personInfo.fileanhthe;
                    }
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                    //  systemMess.Message = SystemMessageConst.systemmessage.EditSuccess;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(
                        SystemMessageConst.systemmessage.EditSuccess, SystemMessageConst.systemmessage.EditSuccessEn);
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

        public m_thongtincanhan GetAccountInfoById()
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myId = AccountUntil.GetAccount().id;
                    var result = entities.m_thongtincanhan.FirstOrDefault(ob => ob.idtaikhoan == myId);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public m_thongtincoquan GetCompanyInfoById(int? id)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_thongtincoquan.FirstOrDefault(ob => ob.id == id);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public m_taikhoan GetAccountById()
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myId = AccountUntil.GetAccount().id;
                    var result = entities.m_taikhoan.FirstOrDefault(ob => ob.id == myId);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool CheckRole(int roleId)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myId = AccountUntil.GetAccount().id;
                    var result =
                        entities.m_quyentaikhoan.FirstOrDefault(ob => ob.idtaikhoan == myId && ob.idquyen == roleId &&
                                                                      ob.trangthai == SystemMessageConst.StatusConst
                                                                          .Active);
                    if (result != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<m_thongtincanhan> GetListMemberInGroup()
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myId = AccountUntil.GetAccount().id;
                    var result = entities.m_thongtincanhan
                        .Where(ob => ob.idtaikhoan == myId && ob.trangthai != SystemMessageConst.StatusConst.Delete)
                        .ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string ValidateEditInfomation(m_thongtincanhan personInfo)
        {
            var result = new ValidateForm.dataInput();
            var check = new ValidateForm();
            //  result = check.CheckRequiredField(personInfo.hovaten, "Họ và tên", 5, 25);
            result = check.CheckRequiredField(personInfo.hovaten,
                DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Fullname,
                    SystemMessageConst.systemmessage.FullnameEn), 5, 25);
            if (result.error != null)
            {
                return result.error;
            }
            //   result = check.CheckRequitePhone(personInfo.sodienthoai, "Số điện thoại cá nhân");
            result = check.CheckRequitePhone(personInfo.sodienthoai,
                DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Phone,
                    SystemMessageConst.systemmessage.PhoneEn));
            if (result.error != null)
            {
                return result.error;
            }
            if (result.error != null)
            {
                return result.error;
            }
            //   result = check.CheckRequiredField(personInfo.chucvu, "Chức vụ", 3, 30);
            result = check.CheckRequiredField(personInfo.chucvu,
                DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Position,
                    SystemMessageConst.systemmessage.PositionEn), 3, 30);
            if (result.error != null)
            {
                return result.error;
            }
            //   result = check.CheckRequiredField(personInfo.diachi, "Địa chỉ", 3, 50);
            result = check.CheckRequiredField(personInfo.diachi,
                DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Address,
                    SystemMessageConst.systemmessage.AddressEn), 3, 250);
            if (result.error != null)
            {
                return result.error;
            }
            return null;
        }

        public SystemMess EditInfomation(m_thongtincanhan person)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateEditInfomation(person);
                if (check != null)
                {
                    systemMess.IsSuccess = false;
                    systemMess.Message = check;
                    return systemMess;
                }
                using (var entities = new ASS_35Entities())
                {
                    var myId = AccountUntil.GetAccount().id;
                    var checkAu =
                        entities.m_quyentaikhoan.Where(
                            ob => ob.idtaikhoan == myId && ob.trangthai == SystemMessageConst.StatusConst.Active &&
                                  (ob.idquyen == SystemMessageConst.RoleConst.Admin ||
                                   ob.idquyen == SystemMessageConst.RoleConst.Banbientap ||
                                   ob.idquyen == SystemMessageConst.RoleConst.Banthuky)).ToList();
                    if (checkAu.Count == 0)
                    {
                        systemMess.IsSuccess = false;
                        //    systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(
                            SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    var result =
                        entities.m_thongtincanhan.FirstOrDefault(ob => ob.idtaikhoan == myId &&
                                                                       ob.trangthai == SystemMessageConst.StatusConst
                                                                           .Active);
                    if (result == null)
                    {
                        person.idtaikhoan = myId;
                        person.trangthai = SystemMessageConst.StatusConst.Active;
                        entities.m_thongtincanhan.Add(person);
                        entities.SaveChanges();
                        systemMess.IsSuccess = true;
                        //  systemMess.Message = SystemMessageConst.systemmessage.EditSuccess;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(
                            SystemMessageConst.systemmessage.EditSuccess,
                            SystemMessageConst.systemmessage.EditSuccessEn);
                    }
                    result.hovaten = person.hovaten;
                    result.diachi = person.diachi;
                    result.chucvu = person.chucvu;
                    result.sodienthoai = person.sodienthoai;
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(
                        SystemMessageConst.systemmessage.EditSuccess, SystemMessageConst.systemmessage.EditSuccessEn);
                    //  systemMess.Message = SystemMessageConst.systemmessage.EditSuccess;
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

        public m_thongtincanhan GetMemberbyId(int id)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myAccId = AccountUntil.GetAccount().id;
                    var member =
                        entities.m_thongtincanhan.FirstOrDefault(
                            ob => ob.id == id && ob.trangthai == SystemMessageConst.StatusConst.Pending &&
                                  ob.idtaikhoan == myAccId);
                    return member;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SystemMess DeleteMember(int id)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myAccId = AccountUntil.GetAccount().id;
                    var member =
                        entities.m_thongtincanhan.FirstOrDefault(
                            ob => ob.id == id && ob.trangthai == SystemMessageConst.StatusConst.Pending &&
                                  ob.idtaikhoan == myAccId);
                    if (member == null)
                    {
                        systemMess.IsSuccess = false;
                        // systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(
                            SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    member.trangthai = SystemMessageConst.StatusConst.Delete;
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                    //  systemMess.Message = SystemMessageConst.systemmessage.DeleteSuccess;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(
                        SystemMessageConst.systemmessage.DeleteSuccess,
                        SystemMessageConst.systemmessage.DeleteSuccessEn);
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

        public string ValidateChangePassword(string newPass)
        {
            var result = new ValidateForm.dataInput();
            var check = new ValidateForm();
            // result = check.CheckRequiredField(newPass, "Mật khẩu mới", 5, 15);
            result = check.CheckRequiredField(newPass,
                DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NewPassword,
                    SystemMessageConst.systemmessage.NewPasswordEn), 5, 15);
            if (result.error != null)
            {
                return result.error;
            }
            return null;
        }

        public SystemMess ChangePassword(string oldpass, string newpass)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateChangePassword(newpass);
                if (check != null)
                {
                    systemMess.IsSuccess = false;
                    systemMess.Message = check;
                    return systemMess;
                }
                using (var entities = new ASS_35Entities())
                {
                    var myAccId = AccountUntil.GetAccount().id;
                    var oldPassMd5 = MD5.GeneratePasswordHash(oldpass);
                    var checkOldPass =
                        entities.m_taikhoan.FirstOrDefault(
                            ob => ob.id == myAccId && ob.trangthai == SystemMessageConst.StatusConst.Active &&
                                  ob.matkhau == oldPassMd5);
                    if (checkOldPass == null)
                    {
                        systemMess.IsSuccess = false;
                        //  systemMess.Message = SystemMessageConst.systemmessage.OldPasswordNotCorrect;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(
                            SystemMessageConst.systemmessage.OldPasswordNotCorrect,
                            SystemMessageConst.systemmessage.OldPasswordNotCorrectEn);
                        return systemMess;
                    }
                    checkOldPass.matkhau = MD5.GeneratePasswordHash(newpass);
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                    //systemMess.Message = SystemMessageConst.systemmessage.EditSuccess;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(
                        SystemMessageConst.systemmessage.EditSuccess, SystemMessageConst.systemmessage.EditSuccessEn);
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

        public m_registration_info GetRegisById(int id)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var regis = entities.m_registration_info.Where(a => a.idtaikhoan == id).SingleOrDefault();
                    return regis;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public m_room_order RoomOrder(int idOrder)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_room_order.Where(a => a.id == idOrder).SingleOrDefault();
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        // get Activity
        public List<m_regisActivity> RegisActivity(int idregis)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    List<m_regisActivity> result = entities.m_regisActivity.Where(a => a.idRegis == idregis).ToList();
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
       
       

    }

}