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
   
    public class AccountBusiness
    {
        public string ValidateRegisterform(m_taikhoan account, m_thongtincanhan personInfo, m_thongtincoquan companyInfo, m_quyentaikhoan accAu)
        {
            var result = new ValidateForm.dataInput();
            var check = new ValidateForm();
            //  result = check.CheckRequiteEmail(account.email, "Email Xác nhận tài khoản");
            result = check.CheckRequiteEmail(account.email, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EmailConfirm, SystemMessageConst.systemmessage.EmailConfirmEn));

            if (result.error != null)
            {
                return result.error;
            }
            // result = check.CheckRequiredField(companyInfo.tencoquan, "Tên cơ quan", 5, 50);
            result = check.CheckRequiredField(companyInfo.tencoquan, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.CompanyName, SystemMessageConst.systemmessage.CompanyNameEn), 5, 200);
            if (result.error != null)
            {
                return result.error;
            }
            //  result = check.CheckRequiredField(companyInfo.diachicoquan, "Địa chỉ cơ quan", 5, 50);
            result = check.CheckRequiredField(companyInfo.diachicoquan, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.CompanyAddress, SystemMessageConst.systemmessage.CompanyAddressEn), 5, 200);
            if (result.error != null)
            {
                return result.error;
            }
            // result = check.CheckRequiteEmail(companyInfo.emailcoquan, "Email cơ quan");
            result = check.CheckRequiteEmail(companyInfo.emailcoquan, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.CompanyEmail, SystemMessageConst.systemmessage.CompanyEmailEn));
            if (result.error != null)
            {
                return result.error;
            }
            //  result = check.CheckRequiredField(companyInfo.diachicoquan, "Địa chỉ cơ quan", 5, 200);
            result = check.CheckRequiredField(companyInfo.diachicoquan, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.CompanyAddress, SystemMessageConst.systemmessage.CompanyAddressEn), 1, 200);
            if (result.error != null)
            {
                return result.error;
            }
            //  result = check.CheckRequitePhone(companyInfo.sodienthoai, "Số điện thoại cơ quan");
            result = check.CheckRequitePhone(companyInfo.sodienthoai, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.CompanyPhone, SystemMessageConst.systemmessage.CompanyPhoneEn));
            if (result.error != null)
            {
                return result.error;
            }
            //  result = check.CheckNonRequiteFAX(companyInfo.sofax, "Số fax");
            result = check.CheckNonRequiteFAX(companyInfo.sofax, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.CompanyFax, SystemMessageConst.systemmessage.CompanyFaxEn));
            if (result.error != null)
            {
                return result.error;
            }
            if (string.IsNullOrEmpty(companyInfo.filecongvan))
            {
                // return "Bạn phải có ảnh công văn !";
                return DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.FileDocumentary, SystemMessageConst.systemmessage.FileDocumentaryEn);
            }

            if (accAu.idquyen != 4 && accAu.idquyen != 5)
            {
                //    return SystemMessageConst.systemmessage.NoData;
                return DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
            }
            if (account.idloaitaikhoan == SystemMessageConst.AccountType.Personal)
            {
                // result = check.CheckRequiredField(personInfo.hovaten, "Họ và tên", 5, 25);
                result = check.CheckRequiredField(personInfo.hovaten, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Fullname, SystemMessageConst.systemmessage.FullnameEn), 5, 200);
                if (result.error != null)
                {
                    return result.error;
                }
                // result = check.CheckRequiteEmail(personInfo.email, "Email cá nhân");
                result = check.CheckRequiteEmail(personInfo.email, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Email, SystemMessageConst.systemmessage.EmailEn));
                if (result.error != null)
                {
                    return result.error;
                }
                // result = check.CheckRequitePhone(personInfo.sodienthoai, "Số điện thoại cá nhân");
                result = check.CheckRequitePhone(personInfo.sodienthoai, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Phone, SystemMessageConst.systemmessage.PhoneEn));
                if (result.error != null)
                {
                    return result.error;
                }
                //   result = check.CheckRequiredField(personInfo.chucvu, "Chức vụ", 3, 30);
                result = check.CheckRequiredField(personInfo.chucvu, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Position, SystemMessageConst.systemmessage.PositionEn), 3, 200);
                if (result.error != null)
                {
                    return result.error;
                }
                //  result = check.CheckRequiredField(personInfo.cmthochieu, "Số chứng minh nhân dân / Hộ chiếu", 3, 30);
                result = check.CheckRequiredField(personInfo.cmthochieu, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.PersonCardId, SystemMessageConst.systemmessage.PersonCardIdEn), 3, 25);
                if (result.error != null)
                {
                    return result.error;
                }
                // result = check.CheckRequiredField(personInfo.noicap, "Nơi cấp chứng minh nhân dân / Hộ chiếu", 3, 30);
                result = check.CheckRequiredField(personInfo.noicap, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.PlaceOfIssue, SystemMessageConst.systemmessage.PlaceOfIssueEn), 3, 200);
                if (result.error != null)
                {
                    return result.error;
                }
                //  result = check.CheckNonRequiredField(personInfo.sothenhabao, "Số thẻ nhà báo", 5, 15);
                result = check.CheckNonRequiredField(personInfo.sothenhabao, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.CardNumber, SystemMessageConst.systemmessage.CardNumberEn), 5, 15);
                if (result.error != null)
                {
                    return result.error;
                }
                if (string.IsNullOrEmpty(personInfo.anhcmtmattruoc))
                {
                    // return "Bạn phải có ảnh chứng minh thư/ hộ chiếu mặt trước !";
                    return DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_Backside, SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_BacksideEn);
                }
                if (string.IsNullOrEmpty(personInfo.anhcmtmatsau))
                {
                    // return "Bạn phải có ảnh chứng minh thư/ hộ chiếu mặt sau !";
                    return DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_Front, SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_FrontEn);
                }
            }
            return null;
        }

        public SystemMess RegisterAccount(m_taikhoan account, m_thongtincanhan personInfo, m_thongtincoquan companyInfo,
            m_quyentaikhoan accAu)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateRegisterform(account, personInfo, companyInfo, accAu);
                if (check != null)
                {
                    systemMess.IsSuccess = false;
                    systemMess.Message = check;
                    return systemMess;
                }
                using (var entities = new ASS_35Entities())
                {
                    var checkExistedEmail = entities.m_taikhoan.FirstOrDefault(ob => ob.email == account.email);
                    if (checkExistedEmail != null)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EmailIsExisted, SystemMessageConst.systemmessage.EmailIsExistedEn);
                        return systemMess;
                    }
                    //Thêm dữ liệu cơ quan
                    entities.m_thongtincoquan.Add(companyInfo);
                    entities.SaveChanges();
                    //thêm dữ liệu tài khoản
                    account.trangthai = SystemMessageConst.StatusConst.Pending;
                    account.isConfirmEmail = false;
                    account.idcoquan = companyInfo.id;
                    entities.m_taikhoan.Add(account);
                    entities.SaveChanges();
                    //thêm dữ liệu thông tin cá nhân
                    if (account.idloaitaikhoan == SystemMessageConst.AccountType.Personal)
                    {
                        personInfo.idtaikhoan = account.id;
                        personInfo.trangthai = SystemMessageConst.StatusConst.Active;
                        entities.m_thongtincanhan.Add(personInfo);
                        entities.SaveChanges();
                    }

                    //thêm dữ liệu quyền tài khoản
                    accAu.idtaikhoan = account.id;
                    accAu.ngaytao = DateTime.Now;
                    accAu.trangthai = SystemMessageConst.StatusConst.Active;
                    entities.m_quyentaikhoan.Add(accAu);
                    entities.SaveChanges();
                    //                    SendEmailUntil.SendMailNotificalRegiterAccount(account.email);
                    SendEmailActive(account, personInfo, companyInfo);
                    systemMess.IsSuccess = true;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.RegisterSuccessfull, SystemMessageConst.systemmessage.RegisterSuccessfullEn);
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
        public SystemMess GetConfirmEmail(string id)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    string keyConfirm = DisplayUntils.Decrypt(id, "QTWHN");
                    string[] key = keyConfirm.Split(',');
                    int userId = 0;
                    Int32.TryParse(key[0], out userId);
                    DateTime dataSend = new DateTime(2000, 1, 1);
                    DateTime.TryParse(key[1], out dataSend);
                    var result = entities.m_taikhoan.FirstOrDefault(ob => ob.id == userId);
                    if (result == null)
                    {
                        systemMess.IsSuccess = false;
                        //  systemMess.Message = SystemMessageConst.systemmessage.IsNotExist;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsNotExist, SystemMessageConst.systemmessage.IsNotExistEn);
                        return systemMess;
                    }
//                    if (dataSend.AddMinutes(10000) >= DateTime.Now)
//                    {
                        result.isConfirmEmail = true;
                        entities.SaveChanges();
                        //gui socket
                       
                            //kiem tra danh sach id can gui
                        var lstAdmin = entities.m_taikhoan
                            .Where(ob => ob.m_quyentaikhoan.Any(x => x.idquyen == SystemMessageConst.RoleConst.Admin &&
                                                                     x.idtaikhoan == ob.id) && ob.trangthai ==
                                         SystemMessageConst.StatusConst.Active).ToList();
                            foreach (var item in lstAdmin)
                            {
                                SocketProcessRequest.SocketRequest("10", item.id.ToString(), 10);
                            }
                        systemMess.IsSuccess = true;
                        //      systemMess.Message = SystemMessageConst.systemmessage.EmailConfirmSuccessFull;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EmailConfirmSuccessFull, SystemMessageConst.systemmessage.EmailConfirmSuccessFullEn);
                        return systemMess;
                    }
//                    systemMess.IsSuccess = false;
//                    //   systemMess.Message = SystemMessageConst.systemmessage.EmailConfirmUnSuccessFull;
//                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EmailConfirmUnSuccessFull, SystemMessageConst.systemmessage.EmailConfirmUnSuccessFullEn);
//                    return systemMess;
//                }
            }
            catch (Exception e)
            {
               
                systemMess.IsSuccess = false;
                systemMess.Message = SystemMessageConst.systemmessage.EmailConfirmIsNotCorrectEn;
                return systemMess;
            }
        }
        public SystemMess SendEmailActive(m_taikhoan user, m_thongtincanhan myInfo, m_thongtincoquan companyInfo)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_taikhoan.FirstOrDefault(ob => ob.id == user.id && ob.trangthai != SystemMessageConst.StatusConst.Delete);
                    if (result == null)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AccountNotExist, SystemMessageConst.systemmessage.AccountNotExistEn);
                        return systemMess;
                    }
                    var key = user.id + "," + DateTime.Now;
                    string keyConfirm = HttpContext.Current.Server.UrlEncode(DisplayUntils.Encrypt(key, "QTWHN"));
                    //send mail confirm 
                    string name = "";
                    if (result.idloaitaikhoan == SystemMessageConst.AccountType.Personal)
                    {
                        name = myInfo.hovaten;
                    }
                    else
                    {
                        name = companyInfo.tencoquan;
                    }
                    SendEmailUntil.SendMailConfirmAccount(user.email, "https://" + HttpContext.Current.Request.Url.Host + "/ConfirmEmail?id=" + keyConfirm, name);
                    systemMess.IsSuccess = true;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.SendSuccess, SystemMessageConst.systemmessage.SendSuccessEn);
                    return systemMess;
                }
            }
            catch (Exception e)
            {
                systemMess.IsSuccess = false;
                systemMess.Message = e.ToString();
                return systemMess;
            }
        }
    }
}