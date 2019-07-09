using DataContext.Constants;
using DataContext.Until;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace DataContext.DAL
{
    public class Registration
    {
        public string ValidateForm(m_registration_info regis_info, m_taikhoan acc,m_room_order room_Order )
        {
            var result = new ValidateForm.dataInput();
            var check = new ValidateForm();

            result = check.CheckRequiredField(regis_info.titlename, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.TitleNameVi, SystemMessageConst.systemmessage.TitleNameEn), 2, 50);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckRequiredField(regis_info.firstname, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.FirstNameVi, SystemMessageConst.systemmessage.FirstNameEn), 2, 50);
            if(result.error != null)
            {
                return result.error;
            }
            result = check.CheckRequiredField(regis_info.lastname, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.LastNameVi, SystemMessageConst.systemmessage.LastNameEn), 1, 50);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.titleOther, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.SpecifyVi, SystemMessageConst.systemmessage.SpecifyEn), 2, 50);
            if(result.error !=null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.middleinitial, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.MiddleInitialVi, SystemMessageConst.systemmessage.MiddleInitialEn), 2, 50);
            if (result.error != null)
            {
                return result.error;
            }
           
            result = check.CheckNonRequiredField(regis_info.address, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Address, SystemMessageConst.systemmessage.AddressEn), 1, 100);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.organization, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.OrganizationVi, SystemMessageConst.systemmessage.OrganizationEn), 1, 200);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.department, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DepartmentVi, SystemMessageConst.systemmessage.DepartmentEn), 1, 200);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.position, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Position, SystemMessageConst.systemmessage.PositionEn), 1, 100);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.meal_other, SystemMessageConst.systemmessage.MealOtherReqEn, 1, 100);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.telephone, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.TeleNumberVi, SystemMessageConst.systemmessage.TeleNumberEn), 1, 20);
            if (result.error != null)
            {
                return result.error;
            }           
            result = check.CheckNonRequiteFAX(regis_info.cellphone, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.FaxNumVi, SystemMessageConst.systemmessage.FaxNumEn));
            //if (result.error != null)
            //{
            //    return result.error;
            //}
            //result = check.CheckNonRequiredField(regis_info.otherNote, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.OtherNoteVi, SystemMessageConst.systemmessage.OtherNoteEn),5, 500);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckRequiteEmail(regis_info.email, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EmailAddVi, SystemMessageConst.systemmessage.EmailAddEn));
            if(result.error !=null)
            {
                return result.error;
            }
            //result = check.CheckNonRequiredField(room_Order.accFirstname, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.FirstNameofAccVi, SystemMessageConst.systemmessage.FirstNameofAccEn), 1, 50);
            //if (result.error != null)
            //{
            //    return result.error;
            //}
            //result = check.CheckNonRequiredField(room_Order.accLastname, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.LastNameofAccVi, SystemMessageConst.systemmessage.LastNameofAccEn), 1, 50);
            //if (result.error != null)
            //{
            //    return result.error;
            //}
            //result = check.CheckNonRequiredField(room_Order.accMiddleinitial, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.MiddleNameofAccVi, SystemMessageConst.systemmessage.MiddleNameofAccEn), 1, 50);
            //if (result.error != null)
            //{
            //    return result.error;
            //}

            // validate Date , Time           
            result = check.CheckNonRequiredField(regis_info.acc_per1, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.per1Vi, SystemMessageConst.systemmessage.per1En), 1, 200);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.acc_per2, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.per2Vi, SystemMessageConst.systemmessage.per2En), 1, 200);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.acc_per3, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.per3Vi, SystemMessageConst.systemmessage.per3En), 1, 200);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.acc_spec, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.specReVi, SystemMessageConst.systemmessage.specReEn), 1, 200);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.conarr_airline, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.ConAirVi, SystemMessageConst.systemmessage.ConAirEn), 1, 50);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.conarr_flight, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.ConFlightVi, SystemMessageConst.systemmessage.ConFlightEn), 1, 50);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.condep_airline, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DepConAirVi, SystemMessageConst.systemmessage.DepConAirEn), 1, 50);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(regis_info.condep_flight, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DepConFlightVi, SystemMessageConst.systemmessage.DepConFlightEn), 1, 50);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(room_Order.otherreq, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.OtherReqVi, SystemMessageConst.systemmessage.OtherReqEn), 1, 200);
            if (result.error != null)
            {
                return result.error;
            }

            return null;
        }
        public SystemMess AddRegisterInfo (m_quyentaikhoan au, m_registration_info regis_info, m_taikhoan acc,  m_room_order room_Order, List<m_regisActivity> act)
        {
            //Validate regis_info
            // validate chi tiết
            SystemMess systemMess = new SystemMess();
            try
            {
               
                var check = ValidateForm(regis_info, acc, room_Order);
                if(check !=null)
                {
                    systemMess.IsSuccess = false;
                    systemMess.Message = check;
                    return systemMess;
                }
                using (var entities = new ASS_35Entities())
                {
                    var checkExistedEmail = entities.m_registration_info.FirstOrDefault(ob => ob.email == regis_info.email);
                    if(checkExistedEmail != null)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EmailIsExisted, SystemMessageConst.systemmessage.EmailIsExistedEn);
                        return systemMess;
                    }
                    // Add dữ liệu tai khoan
                    acc.trangthai = SystemMessageConst.StatusConst.Pending;
                    acc.isConfirmEmail = false;
                    
                    entities.m_taikhoan.Add(acc);
                    entities.SaveChanges();

                    // Add du lieu room order
                   if(room_Order != null)
                    {
                        room_Order.created = DateTime.Now;

                        entities.m_room_order.Add(room_Order);
                        entities.SaveChanges();
                        regis_info.idRoomOrder = room_Order.id;
                    }
                    // Add du lieu regis info
                    
                    regis_info.idtaikhoan = acc.id;
                    regis_info.regis_date = DateTime.Now;
                    
                    entities.m_registration_info.Add(regis_info);
                    entities.SaveChanges();
                    // Add du lieu quyen tai khoan

                    au.idtaikhoan = acc.id;
                    au.idquyen = SystemMessageConst.RoleConst.Daibieu;
                    au.ngaytao = DateTime.Now;
                    au.trangthai = SystemMessageConst.StatusConst.Active;
                    entities.m_quyentaikhoan.Add(au);
                    entities.SaveChanges();

                    // Add du lieu regis act
                    foreach (var item in act)
                    {
                        item.idRegis = regis_info.id;
                        entities.m_regisActivity.Add(item);
                        entities.SaveChanges();
                    }
                    //send mail
                    var guestName = "";
                    if (regis_info.titleOther != null && regis_info.titleOther != "")
                    {
                        guestName = regis_info.titleOther + " " + regis_info.firstname + " "+ regis_info.middleinitial+ " " + regis_info.lastname;
                    }
                    else
                    {
                        guestName = regis_info.titlename + " " + regis_info.firstname + " " + regis_info.middleinitial + " " + regis_info.lastname;
                    }
                    SendEmailActiveRegis(acc, regis_info);
                    

                    // Send Mail To VSS and VC
                    var doc = new Document();
                    MemoryStream memoryStream = new MemoryStream();
                    PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                    doc.Open();
                    string mealpre = "";
                    switch (regis_info.mealprefer)
                    {
                        case 1:
                            mealpre = "Vegetarian";
                            break;
                        case 2:
                            mealpre = "Non-Vegetarian";
                            break;
                        case 3:
                            mealpre = "Halal Food";
                            break;
                        case 5:
                            mealpre = "Other";
                            break;
                        default:
                                mealpre = "Normal";
                            break;
                    }
                    string connectFlight = "";
                    switch (regis_info.arr_con)
                    {
                        case "kl":
                            connectFlight = "Kuala Lumpur";
                            break;
                        case "hcm":
                            connectFlight = "Ho Chi Minh";
                            break;
                        case "dn":
                            connectFlight = "Da Nang";
                            break;
                            default:
                                connectFlight = "Ha Noi";
                            break;
                    }
                    string connectTo = "";
                    switch (regis_info.dep_con)
                    {
                        case "kl":
                            connectTo = "Kuala Lumpur";
                            break;
                        case "hcm":
                            connectTo = "Ho Chi Minh";
                            break;
                        case "dn":
                            connectTo = "Da Nang";
                            break;
                        default:
                            connectTo = "Ha Noi";
                            break;
                    }
                    doc.Add(new Paragraph("GENERAL INFORMATION "));
                    doc.Add(new Paragraph("Guest name: " + guestName));
                    doc.Add(new Paragraph("Gender: " + (regis_info.gender ==1 ? "Male":"Female")));
                    doc.Add(new Paragraph("Country: "+ regis_info.country));
                    doc.Add(new Paragraph("Address: "+regis_info.address));
                    doc.Add(new Paragraph("Institution: " + regis_info.organization));
                    doc.Add(new Paragraph("Department: " + regis_info.department));
                    doc.Add(new Paragraph("Position: " + regis_info.position));
                    doc.Add(new Paragraph("Telephone: " + regis_info.telephone));
                    doc.Add(new Paragraph("Cellphone: " + regis_info.cellphone));
                    doc.Add(new Paragraph("Email: " + regis_info.email));
                    doc.Add(new Paragraph("Meal prefer: " + mealpre));
                    doc.Add(new Paragraph("Meal other: " + regis_info.meal_other));
                    doc.Add(new Paragraph("Accompany person 1: " + regis_info.acc_per1));
                    doc.Add(new Paragraph("Accompany person 2: " + regis_info.acc_per2));
                    doc.Add(new Paragraph("Accompany person 3: " + regis_info.acc_per3));
                    doc.Add(new Paragraph("Accompany specification: " + regis_info.acc_spec));
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph("FLIGHT DETAIL"));
                    doc.Add(new Paragraph("Connecting Flight From: " + connectFlight));
                    doc.Add(new Paragraph("Airline: " + regis_info.conarr_airline));
                    doc.Add(new Paragraph("Flight: " + regis_info.conarr_flight));
                    doc.Add(new Paragraph("Date: " + String.Format("{0:dd-MM-yyyy}", regis_info.conarr_date)));
                    doc.Add(new Paragraph("Time: " + regis_info.conarr_time));
                    doc.Add(new Paragraph("Connecting Flight To: " + connectTo));
                    doc.Add(new Paragraph("Airline: " + regis_info.condep_airline));
                    doc.Add(new Paragraph("Flight: " + regis_info.condep_flight));
                    doc.Add(new Paragraph("Date: " + String.Format("{0:dd-MM-yyyy}", regis_info.condep_date)));
                    doc.Add(new Paragraph("Time: " + regis_info.condep_time));
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph("INDICATE INFORMATION"));
                    foreach (var item in act)
                    {
                        switch (item.idActivity)
                        {
                            case 1:
                                doc.Add(new Paragraph("Opening Ceremony Of 35th ASSA Board Meeting: " + (item.value0 == 1? "Yes": "No")));
                                break;
                            case 2:
                                doc.Add(new Paragraph("Pre-ASSA Board Meeting Seminar: " + (item.value0 == 1 ? "Yes" : "No")));
                                break;
                            case 3:
                                doc.Add(new Paragraph("The 20th Anniversary of he ASSA's Establishment/Welcome Dinner: " + (item.value0 == 1 ? "Yes" : "No")));
                                break;
                            case 4:
                                doc.Add(new Paragraph("ASSA Board Meeting and related activities: " + (item.value0 == 1 ? "Yes" : "No")));
                                break;
                            case 5:
                                doc.Add(new Paragraph("Farewell Dinner: " + (item.value0 == 1 ? "Yes" : "No")));
                                break;
                            case 6:
                                doc.Add(new Paragraph("City tour/Sight-seeing(visit https://assa35.com/program for detailed program): " + (item.value0 == 1 ? "Yes" : "No")));
                                break;
                            default:
                                doc.Add(new Paragraph("Cannot get Value"));
                                break;
                        }
                    }
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph("HOTEL REGISTRATION"));
                    if (room_Order != null)
                    {
                        doc.Add(new Paragraph("Check in: " + String.Format("{0:dd-MM-yyyy}", room_Order.checkin)));
                        doc.Add(new Paragraph("Check out: " + String.Format("{0:dd-MM-yyyy}", room_Order.checkout)));
                        string roomType = "";
                        string roomType2 = "";
                        switch (room_Order.idRoomType)
                        {
                            case 1:
                                roomType = "Deluxe";
                                break;
                            case 2:
                                roomType = "Premium Deluxe Ocean View";
                                break;
                            case 3:
                                roomType = "Club Ocean View";
                                break;
                            default:
                                roomType = "Deluxe";
                                break;
                        }
                        doc.Add(new Paragraph("Room Type: " + roomType));

                        switch (room_Order.idRoomOption1)
                        {
                            case 1:
                                roomType2 = "SGL";
                                break;
                            case 2:
                                roomType2 = "DBL";
                                break;
                            case 3:
                                roomType2 = "TWIN";
                                break;
                            default:
                                roomType2 = "SGL";
                                break;
                        }
                        doc.Add(new Paragraph("Room Type: " + roomType2));
                        doc.Add(new Paragraph("Smoking: "+ (room_Order.idRoomOption2 ==1?"Smoking" :"Non-smoking")));
                        doc.Add(new Paragraph("Other requirements: " + room_Order.otherreq));
                    }
                    writer.CloseStream = false;
                    doc.Close();
                    memoryStream.Position = 0;
                    string mailVc = "mice9@vietcenter.vn";
                    //string mailVc = "canquy@gmail.com";
                    string mailVss = "vuhtqt@vss.gov.vn";
                    //string mailVss = "chquy1986@gmail.com";
                    var body = string.Format(string.Format(SystemMessageConst.SendEmailConst.SendMailInfor, guestName));
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(ListMailAddress.SystemEmailAddress.EmailAdmin);
                    message.To.Add(mailVc);
                    message.To.Add(mailVss);
                    message.Subject = "Thông tin khách " + guestName;
                    message.IsBodyHtml = true;
                    message.Body = body;
                    message.Attachments.Add(new Attachment(memoryStream, guestName + ".pdf"));
                   
                   
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        Credentials = new NetworkCredential(ListMailAddress.SystemEmailAddress.EmailAdmin, ListMailAddress.SystemEmailAddress.PassWordEmailAdmin)

                    };
                    
                    
                    smtp.Send(message);


                    // Send Mail to VSS and VC

                    //SendEmailRegisInfo(path, guestName);

                    //End
                    systemMess.IsSuccess = true;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.RegisterSuccessfull, SystemMessageConst.systemmessage.RegisterSuccessfullEn);
                    return systemMess;
                    //acc.trangthai = 2;     

                    //entities.m_registration_info.Add(regis_info);

                    //if(entities.SaveChanges()> 0)
                    //{
                    //    systemMess.IsSuccess = true;
                    //    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.RegisterSuccessfull, SystemMessageConst.systemmessage.RegisterSuccessfullEn);
                    //    return systemMess;
                    //}
                    //else
                    //{
                    //    systemMess.IsSuccess = false;
                    //    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.ErrorHappenVi, "This form is maintaning!. Please comback late.");
                    //    return systemMess;
                    //}


                }


            }
            catch (Exception ex)
            {
                systemMess.IsSuccess = false;
                systemMess.Message = ex.ToString();
                return systemMess;
            }

        }
        public SystemMess SendEmailActiveRegis(m_taikhoan user, m_registration_info regis_info)
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
                    if (regis_info.titleOther != null && regis_info.titleOther != "")
                    {
                        name = regis_info.titleOther + " " + regis_info.firstname + " " + regis_info.lastname;
                    }else
                    {
                        name = regis_info.titlename + " " + regis_info.firstname + " " + regis_info.lastname;
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
                systemMess.Message = "Sending actived email failed!";
                return systemMess;
            }
        }

        //public SystemMess SendEmailRegisInfo(string path, string name)
        //{
        //    SystemMess systemMess = new SystemMess();
        //    try
        //    {
        //        using (var entities = new ASS_35Entities())
        //        {
        //            //SendEmailUntil.SendMailInfo("mice9@vietcenter.vn", path, name);
        //            //SendEmailUntil.SendMailInfoToVSS("vuhtqt@vss.gov.vn", path, name);
        //            //systemMess.IsSuccess = true;
        //            //systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.SendSuccess, SystemMessageConst.systemmessage.SendSuccessEn);
        //            //return systemMess;
                   
                        


        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        systemMess.IsSuccess = false;
        //        systemMess.Message = "Sending infomation email failed!";
        //        return systemMess;
        //    }
        //}




    }
}