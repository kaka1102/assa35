using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Until;

namespace DataContext.AdminBusiness
{
    public class MeetingBusiness
    {
        public string ValidateAddMeet(m_cuochop venue)
        {
            var result = new ValidateForm.dataInput();
            var check = new ValidateForm();
            result = check.CheckRequiredField(venue.tencuochop, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NameMeeting, SystemMessageConst.systemmessage.NameMeetingEn), 3, 500);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckRequiredField(venue.noidung, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Content, SystemMessageConst.systemmessage.ContentEn), 5);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckRequiredField(venue.diadiemtochuc, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Place, SystemMessageConst.systemmessage.PlaceEn), 5,350);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(venue.ghichu, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Note, SystemMessageConst.systemmessage.NoteEn), 0, 1000);
            if (result.error != null)
            {
                return result.error;
            }
            result = check.CheckNonRequiredField(venue.lydotuchoi, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Reason, SystemMessageConst.systemmessage.Reason),0, 1000);
            if (result.error != null)
            {
                return result.error;
            }
            return null;
        }

        public SystemMess AddMeet(m_cuochop meeting,string startDate,string startTime,string endTime)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateAddMeet(meeting);
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
                    //check ngày họp
                    if (ValidateForm.IsValidDateFormat(startDate))
                    {
                        meeting.ngaydienra = DateTime.Parse(startDate);
                        if (meeting.ngaydienra.Date < DateTime.Now.Date)
                        {
                            systemMess.IsSuccess = false;
                            systemMess.Message = String.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotValid, SystemMessageConst.ValidateConst.DateNotValidEn), DisplayUntils.ReturnMessageBylanguage("Ngày họp", "Start date"));
                            return systemMess;
                        }
                    }
                    else
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = String.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage("Ngày họp", "Start date"));
                        return systemMess;
                    }
                    //check thời gian bắt đầu
                    if (ValidateForm.IsValidTimeFormat(startTime))
                    {
                        meeting.thoigianbatdau = DateTime.ParseExact(
                            startTime,
                            "h:mm tt",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None).TimeOfDay;
                    }
                    else
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = String.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.TimeNotCorrectFormat, SystemMessageConst.ValidateConst.TimeNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage("Thời gian bắt đầu ", "Time start "));
                        return systemMess;
                    }
                    //check thời gian kết thúc
                    if (ValidateForm.IsValidTimeFormat(endTime))
                    {
                        meeting.thoigianketthuc = DateTime.ParseExact(
                            endTime,
                            "h:mm tt",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None).TimeOfDay;
                        if (meeting.thoigianketthuc <= meeting.thoigianbatdau)
                        {
                            systemMess.IsSuccess = false;
                            systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.EndDateNotCorrect, SystemMessageConst.ValidateConst.EndDateNotCorrectEn);
                            return systemMess;
                        }
                    }
                    else
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = String.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.TimeNotCorrectFormat, SystemMessageConst.ValidateConst.TimeNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage("Thời gian kết thúc ", "Time end "));
                        return systemMess;
                    }
                    var checkTime = ValidateForm.CheckDateTimeMeeting(meeting.ngaydienra, meeting.thoigianbatdau,
                        meeting.thoigianketthuc);
                    if (!checkTime)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateTimeNotValid, SystemMessageConst.ValidateConst.DateTimeNotValidEn);
                        return systemMess;
                    }
                    meeting.idnguoitao = myId;
                    meeting.thoigiantao = DateTime.Now;
                    if (myRoleId != SystemMessageConst.RoleConst.Admin &&
                        myRoleId != SystemMessageConst.RoleConst.Banthuky)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    if (meeting.trangthai == SystemMessageConst.StatusConst.Active)
                    {
                        meeting.idnguoiduyet = myId;
                        meeting.thoigianduyet = DateTime.Now;
                    }
                    entities.m_cuochop.Add(meeting);
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                 //   systemMess.Message = SystemMessageConst.systemmessage.AddSuccess;
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

        public List<MeetingInfomation_Result> GetListMeeting(int status)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    int myId = AccountUntil.GetAccount().id;
                    int roleId = AccountUntil.GetAccount().idquyentaikhoan;
                    var result = entities.MeetingInfomation(roleId, myId, status).OrderByDescending(ob => ob.thoigiantao).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SystemMess DeleteMeeting(int id)
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
                       // systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    var result = entities.m_cuochop.FirstOrDefault(ob => ob.id == id);
                    if (result == null)
                    {
                        systemMess.IsSuccess = false;
                     //   systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    if (myRoleId == SystemMessageConst.RoleConst.Admin)
                    {
                        result.trangthai = SystemMessageConst.StatusConst.Delete;
                        entities.SaveChanges();
                       // if (result.idnguoitao != myId)
                        //{
                            var director = entities.m_taikhoan.SingleOrDefault(ob => ob.id == result.idnguoitao);
                           // SendEmailUntil.SendMailRemovePost(director.email, "Cuộc họp : " + result.tencuochop);
                            SendEmailUntil._SendMailRemovePost(director.email,  result.tencuochop);
                        //}
                    }
                    if (myRoleId == SystemMessageConst.RoleConst.Banthuky)
                    {
                        if (result.trangthai != SystemMessageConst.StatusConst.Active && result.idnguoitao == myId)
                        {
                            result.trangthai = SystemMessageConst.StatusConst.Delete;
                            entities.SaveChanges();
                            var director = entities.m_taikhoan.SingleOrDefault(ob => ob.id == result.idnguoitao);
                            SendEmailUntil._SendMailRemovePost(director.email, result.tencuochop);
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

        public m_cuochop GetMeetingById(int id)
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
                        entities.m_cuochop.FirstOrDefault(
                            ob => ob.id == id && ob.trangthai != SystemMessageConst.StatusConst.Delete);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SystemMess EditStatusMeeting(int? status, int id,string feedback)
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
                    var result = entities.m_cuochop.FirstOrDefault(ob => ob.id == id && ob.trangthai != SystemMessageConst.StatusConst.Delete);

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
        public SystemMess EditMeet(m_cuochop meeting, string startDate,string startTime,string endTime)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateAddMeet(meeting);
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
                        entities.m_cuochop.FirstOrDefault(ob => ob.id == meeting.id &&
                                                                      ob.trangthai != SystemMessageConst.StatusConst
                                                                          .Delete);

                    if (result == null)
                    {
                        systemMess.IsSuccess = false;
                      //  systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    if (result.idnguoitao == myId && result.trangthai != SystemMessageConst.StatusConst.Active && result.trangthai != SystemMessageConst.StatusConst.Pending)
                    {
                        //check ngày họp
                        DateTime _starDate;
                        if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                            out _starDate))
                        {
                            result.ngaydienra = _starDate;
                        }
                        else
                        {
                            systemMess.IsSuccess = false;
                            systemMess.Message = String.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage("Ngày họp", "Start date"));
                            return systemMess;
                        }
                        //check thời gian bắt đầu
                        if (ValidateForm.IsValidTimeFormat(startTime))
                        {
                            meeting.thoigianbatdau = DateTime.ParseExact(
                                startTime,
                                "h:mm tt",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None).TimeOfDay;
                        }
                        else
                        {
                            systemMess.IsSuccess = false;
                            systemMess.Message = String.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.TimeNotCorrectFormat, SystemMessageConst.ValidateConst.TimeNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage("Thời gian bắt đầu ", "Time start "));
                            return systemMess;
                        }
                        //check thời gian kết thúc
                        if (ValidateForm.IsValidTimeFormat(endTime))
                        {
                            meeting.thoigianketthuc = DateTime.ParseExact(
                                endTime,
                                "h:mm tt",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None).TimeOfDay;
                            if (meeting.thoigianketthuc <= meeting.thoigianbatdau)
                            {
                                systemMess.IsSuccess = false;
                                systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.EndDateNotCorrect, SystemMessageConst.ValidateConst.EndDateNotCorrectEn);
                                return systemMess;
                            }
                        }
                        else
                        {
                            systemMess.IsSuccess = false;
                            systemMess.Message = String.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.TimeNotCorrectFormat, SystemMessageConst.ValidateConst.TimeNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage("Thời gian kết thúc ", "Time end "));
                            return systemMess;
                        }
                        var checkTime = ValidateForm.CheckDateTimeMeeting(meeting.ngaydienra, meeting.thoigianbatdau,
                            meeting.thoigianketthuc,result.id);
                        if (!checkTime)
                        {
                            systemMess.IsSuccess = false;
                            systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateTimeNotValid, SystemMessageConst.ValidateConst.DateTimeNotValidEn);
                            return systemMess;
                        }
                        result.noidung = meeting.noidung;
                        result.ghichu = meeting.ghichu;
                        result.tencuochop = meeting.tencuochop;
                        result.diadiemtochuc = meeting.diadiemtochuc;
                        result.ngaydienra = meeting.ngaydienra;
                        result.thoigianbatdau = meeting.thoigianbatdau;
                        result.thoigianketthuc = meeting.thoigianketthuc;
                        if (meeting.avatar != null)
                        {
                            if (File.Exists(result.avatar))
                            {
                                File.Delete(result.avatar);
                            }
                            result.avatar = meeting.avatar;
                        }
                    }
                    if (AccountUntil.GetAccount().idquyentaikhoan == SystemMessageConst.RoleConst.Admin)
                    {
                         result.lydotuchoi = meeting.lydotuchoi;
                    }
                    result.trangthai = meeting.trangthai;
                    if (meeting.trangthai == SystemMessageConst.StatusConst.Active)
                    {
                        result.idnguoiduyet = myId;
                        result.thoigianduyet = DateTime.Now;
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
    }
}