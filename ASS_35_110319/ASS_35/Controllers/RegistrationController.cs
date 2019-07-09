using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContext.DAL;
using DataContext.ClientBusiness;
using Newtonsoft.Json.Linq;
using DataContext.Until;
using System.Globalization;
using System.IO;
using reCAPTCHA.MVC;
using System.Net;
using System.Net.Mail;
using DataContext.AdminBusiness;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ASS_35.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult ConfirmEmail(string id)
        {
            var db = new AccountBusiness();
            var result = db.GetConfirmEmail(id);
            return PartialView(result);
        }
        [HttpPost]
        
        public ActionResult AddNew()
        {
            try
            {
                string response;
                response = (string)System.Web.HttpContext.Current.Request["response"];
                //var response = Request["g-recaptcha-response"];
                //string secretKey = "6LeUZi0UAAAAACA3usg0GC2we53URTGpSG9KI-Fd";
                string secretKey = "6LeQZi0UAAAAAErEzePg4cZmbIigD5pDfn_enSj2";
                var client = new WebClient();
                string urlReQuest = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response);
                var resu = client.DownloadString(urlReQuest);
                var obj = JObject.Parse(resu);
                var status = (bool)obj.SelectToken("success");
                ViewBag.Message = status ? "Google reCaptcha validation success" : "Google reCaptcha validation failed";
                SystemMess systemMess = new SystemMess();

                //status = true;
                if (status == false)
                {
                    systemMess.IsSuccess = false;
                    // systemMess.Message = "Ngay sinh khong dung dinh dang";
                    systemMess.Message = response;
                    return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
                }

                JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["data"]);
                string accEmail, titleName, titleOther, lastName, firstName, middleInitial, gender, country, address, organization, department, position, telno, faxno, email, meal, meal_other_req, acc_per1, acc_per2, acc_per3, acc_spec, arr_con, conarr_airline, conarr_flight, conarr_date, conarr_time, dep_con, condep_airline, condep_flight, condep_date, condep_time;
                DisplayUntils.GetJson<string>("email", JObj, out accEmail);
                DisplayUntils.GetJson<string>("titleName", JObj, out titleName);
                DisplayUntils.GetJson<string>("titleOther", JObj, out titleOther);              
                DisplayUntils.GetJson<string>("lastName", JObj, out lastName);
                DisplayUntils.GetJson<string>("firstName", JObj, out firstName);
                DisplayUntils.GetJson<string>("middleInitial", JObj, out middleInitial);
                DisplayUntils.GetJson<string>("gender", JObj, out gender);
                DisplayUntils.GetJson<string>("country", JObj, out country);
                DisplayUntils.GetJson<string>("address", JObj, out address);
                DisplayUntils.GetJson<string>("organization", JObj, out organization);
                DisplayUntils.GetJson<string>("department", JObj, out department);
                DisplayUntils.GetJson<string>("position", JObj, out position);
                DisplayUntils.GetJson<string>("telno", JObj, out telno);
                DisplayUntils.GetJson<string>("faxno", JObj, out faxno);
                DisplayUntils.GetJson<string>("email", JObj, out email);
                DisplayUntils.GetJson<string>("meal", JObj, out meal);
                DisplayUntils.GetJson<string>("meal_other_req", JObj, out meal_other_req);

                DisplayUntils.GetJson<string>("acc_per1", JObj, out acc_per1);
                DisplayUntils.GetJson<string>("acc_per2", JObj, out acc_per2);
                DisplayUntils.GetJson<string>("acc_per1", JObj, out acc_per3);
                DisplayUntils.GetJson<string>("acc_spec", JObj, out acc_spec);


                DisplayUntils.GetJson<string>("arr_con", JObj, out arr_con);
                DisplayUntils.GetJson<string>("conarr_airline", JObj, out conarr_airline);
                DisplayUntils.GetJson<string>("conarr_flight", JObj, out conarr_flight);
                DisplayUntils.GetJson<string>("conarr_date", JObj, out conarr_date);
                DisplayUntils.GetJson<string>("conarr_time", JObj, out conarr_time);
                DisplayUntils.GetJson<string>("dep_con", JObj, out dep_con);
                DisplayUntils.GetJson<string>("condep_airline", JObj, out condep_airline);
                DisplayUntils.GetJson<string>("condep_flight", JObj, out condep_flight);
                DisplayUntils.GetJson<string>("condep_date", JObj, out condep_date);
                DisplayUntils.GetJson<string>("condep_time", JObj, out condep_time);

                int _gender;
                int _meal;

                Int32.TryParse(gender, out _gender);
                Int32.TryParse(meal, out _meal);

                JObject Jroom = JObject.Parse(System.Web.HttpContext.Current.Request["room"]);
                string date_checkin, date_checkout, other_req, roomtype, roomop1, roomop2;
                DisplayUntils.GetJson<string>("date_checkin", Jroom, out date_checkin);
                DisplayUntils.GetJson<string>("date_checkout", Jroom, out date_checkout);
                DisplayUntils.GetJson<string>("roomtype", Jroom, out roomtype);
                DisplayUntils.GetJson<string>("roomop1", Jroom, out roomop1);
                DisplayUntils.GetJson<string>("roomop2", Jroom, out roomop2);
                DisplayUntils.GetJson<string>("sec2_other_req", Jroom, out other_req);

                int _roomtype, _roomop1, _roomop2;
                Int32.TryParse(roomtype, out _roomtype);
                Int32.TryParse(roomop1, out _roomop1);
                Int32.TryParse(roomop2, out _roomop2);

                JObject Jact = JObject.Parse(System.Web.HttpContext.Current.Request["actData"]);
                string a1, a2, a3, a4, a5, a6, shirtsize, shirtfor;
                DisplayUntils.GetJson<string>("a1", Jact, out a1);
                DisplayUntils.GetJson<string>("a2", Jact, out a2);
                DisplayUntils.GetJson<string>("a3", Jact, out a3);
                DisplayUntils.GetJson<string>("shirtsize", Jact, out shirtsize);
                DisplayUntils.GetJson<string>("shirtfor", Jact, out shirtfor);
                DisplayUntils.GetJson<string>("a4", Jact, out a4);
                DisplayUntils.GetJson<string>("a5", Jact, out a5);
                DisplayUntils.GetJson<string>("a6", Jact, out a6);

                int _a1, _a2, _a3, _a4, _a5, _a6, _shirtsize;
                Int32.TryParse(a1, out _a1);
                Int32.TryParse(a2, out _a2);
                Int32.TryParse(a3, out _a3);
                Int32.TryParse(a4, out _a4);
                Int32.TryParse(a5, out _a5);
                Int32.TryParse(a6, out _a6);
                Int32.TryParse(shirtsize, out _shirtsize);
                List<int> listact = new List<int>();
                listact.Add(_a1);
                listact.Add(_a2);
                listact.Add(_a3);
                listact.Add(_a4);
                listact.Add(_a5);
                listact.Add(_a6);

                

                m_taikhoan acc = new m_taikhoan();
                m_registration_info regis = new m_registration_info();
                m_quyentaikhoan au = new m_quyentaikhoan();
                m_room_order room = new m_room_order();

                List<m_regisActivity> list = new List<m_regisActivity>();
                // Tao Dang ki hoat dong
                for (var i = 0; i < listact.Count; i++)
                {
                    m_regisActivity act = new m_regisActivity();
                    act.idActivity = i + 1;
                    act.value0 = listact[i];
                    if (i+1 == 3)
                    {
                        act.idActDetail = _shirtsize;
                        act.value2 = shirtfor;
                    }
                    list.Add(act);
                }
                // tao tai khoan
                acc.email = accEmail;
                acc.idloaitaikhoan = 1;
                
                acc.ngaytao = DateTime.Now;

                //Tao thong tin dang ki
                regis.titlename = titleName;
                regis.titleOther = titleOther;
                regis.lastname = lastName;
                regis.firstname = firstName;
                regis.middleinitial = middleInitial;
                regis.gender = _gender;
                regis.meal_other = meal_other_req;
                regis.country = country;
                regis.address = address;
                regis.organization = organization;
                regis.department = department;
                regis.position = position;
                regis.telephone = telno;
                regis.cellphone = faxno;
                regis.email = accEmail;
                regis.mealprefer = _meal;
                regis.acc_per1 = acc_per1;
                regis.acc_per2 = acc_per2;
                regis.acc_per3 = acc_per3;
                regis.acc_spec = acc_spec;
               
                regis.arr_con = arr_con;
                regis.conarr_airline = conarr_airline;
                regis.conarr_flight = conarr_flight;

                if (conarr_date != null && conarr_date != "")
                {
                    DateTime _conarr_date;
                    if (DateTime.TryParseExact(conarr_date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                            out _conarr_date))
                    {
                        regis.conarr_date = _conarr_date;
                    }
                    else
                    {
                        systemMess.IsSuccess = false;
                        // systemMess.Message = "Ngay sinh khong dung dinh dang";
                        systemMess.Message = string.Format("Arrival connecting fliht's date is not valid");

                        return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (conarr_time != null && conarr_time != "")
                {
                    TimeSpan _conarr_time;
                    if (TimeSpan.TryParse(conarr_time, out _conarr_time))
                    {
                        regis.conarr_time = _conarr_time;
                    }
                    else
                    {
                        systemMess.IsSuccess = false;
                        // systemMess.Message = "Ngay sinh khong dung dinh dang";
                        systemMess.Message = string.Format("Arrival connecting flight's time is not valid");

                        return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
                    }
                }
                regis.dep_con = dep_con;
                regis.condep_airline = condep_airline;
                regis.condep_flight = condep_flight;

                if (condep_date != null && condep_date != "")
                {
                    DateTime _condep_date;
                    if (DateTime.TryParseExact(condep_date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                            out _condep_date))
                    {
                        regis.condep_date = _condep_date;
                    }
                    else
                    {
                        systemMess.IsSuccess = false;
                        // systemMess.Message = "Ngay sinh khong dung dinh dang";
                        systemMess.Message = string.Format("Departure Connecting's date is not valid");

                        return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (condep_time != null && condep_time != "")
                {
                    TimeSpan _condep_time;
                    if (TimeSpan.TryParse(condep_time, out _condep_time))
                    {
                        regis.condep_time = _condep_time;
                    }
                    else
                    {
                        systemMess.IsSuccess = false;
                        // systemMess.Message = "Ngay sinh khong dung dinh dang";
                        systemMess.Message = string.Format("Departure Connecting's time is not valid");

                        return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
                    }
                }

                //Tao order room order

                if (_roomtype == 1 || _roomtype == 2 || _roomtype == 3)
                {
                    room.idRoomType = _roomtype;
                    room.idHotel = 1;
                    room.idRoomOption1 = _roomop1;
                    room.idRoomOption2 = _roomop2;
                    room.otherreq = other_req;

                    if (date_checkin != null && date_checkin != "")
                    {
                        DateTime _checkin;

                        if (DateTime.TryParseExact(date_checkin, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                            out _checkin))
                        {
                            room.checkin = _checkin;
                        }
                        else
                        {
                            systemMess.IsSuccess = false;
                            // systemMess.Message = "Ngay sinh khong dung dinh dang";
                            systemMess.Message = string.Format("Check in date is not valid");

                            return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (date_checkout != null && date_checkout != "")
                    {
                        DateTime _checkout;
                        if (DateTime.TryParseExact(date_checkout, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                            out _checkout))
                        {
                            room.checkout = _checkout;
                        }
                        else
                        {
                            systemMess.IsSuccess = false;
                            // systemMess.Message = "Ngay sinh khong dung dinh dang";
                            systemMess.Message = string.Format("Check in date is not valid");

                            return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                // tao quyen tai khoan
               
                au.ngaytao = DateTime.Now;
                au.trangthai = 3;

                var db = new Registration();
                //string url = "";
                //string files = System.Web.HttpContext.Current.Request["base64"];
                //string file = files.Replace("data:image/png;base64,", "");
                //byte[] uploadedImage = Convert.FromBase64String(file);
                //string fileName = DateTime.Now.ToString("ddMMhhmm") + ".png";
                //string path = Server.MapPath("~/Content/IMG/Registration/" + fileName);
                //url = string.Format("https://assa35.com/Content/IMG/Registration/{0}", fileName);
                //System.IO.File.WriteAllBytes(path, uploadedImage);
                var result = db.AddRegisterInfo(au, regis, acc, room, list);
                
                //HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                //HttpPostedFile file = files["dataPDF"];
                //string fileExtF1 = System.IO.Path.GetExtension(file.FileName).ToLower();
                //if (string.IsNullOrEmpty(fileExtF1))
                //    fileExtF1 = ".pdf";
                //file.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/Content/IMG/Registration/" + DateTime.Now.ToString("ddMMhhmm") + fileExtF1));



                return Json(new { result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var systemMess = new SystemMess();
                systemMess.IsSuccess = false;
                systemMess.Message = e.Message;
                return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetListAct (int? actType)
        {
            var result = new BS_LoadActivity().GetListAct(actType);
            return View(result);
        }

        public ActionResult GetListOp(int id)
        {
            var result = new BS_LoadActivity().GetListOp(id);
            return View(result);
        }

        public ActionResult GetHotel()
        {
            return View();
        }


        
    }
}