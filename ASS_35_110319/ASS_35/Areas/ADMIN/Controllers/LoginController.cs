using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContext;
using DataContext.AdminBusiness;
using System.Data;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Socket;
using Newtonsoft.Json.Linq;
using DataContext.Until;
using reCAPTCHA.MVC;
using System.Net;

namespace ASS_35.Areas.ADMIN.Controllers
{

    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckSession()
        {
            return PartialView();
        }

        [HttpPost]
       
        public JsonResult LoginAdmin()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            
            string response;
            DisplayUntils.GetJson<string>("res", JObj, out response);
            //var response = Request["g-recaptcha-response"];
            //string secretKey = "6LeUZi0UAAAAACA3usg0GC2we53URTGpSG9KI-Fd";
            string secretKey = "6LeQZi0UAAAAAErEzePg4cZmbIigD5pDfn_enSj2";
            var client = new WebClient();
            string urlReQuest = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response);
            var result = client.DownloadString(urlReQuest);
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            ViewBag.Message = status ? "Google reCaptcha validation success" : "Google reCaptcha validation failed";           
            SystemMess Resulf = new SystemMess();

            status = true;
            if (status == false)
            {
                Resulf.IsSuccess = false;
                Resulf.Message = ViewBag.Message;
                return Json(new { Resulf = Resulf }, JsonRequestBehavior.AllowGet);
            }else
            {
                string email = "", passWord = "";
                DisplayUntils.GetJson<string>("Email", JObj, out email);
                DisplayUntils.GetJson<string>("PassWord", JObj, out passWord);
               

                var context = new LoginBusiness();
                Resulf = context.CheckLoginAccount(email, passWord);
                if (Resulf.IsSuccess == true)
                {
                    var myId = AccountUntil.GetAccount().id;
                    var checkLogin = SocketUtils.listSocket.FirstOrDefault(s =>
                        s._mysession == myId.ToString()
                    );
                    if (checkLogin != null)
                    {


                        Session["uSession"] = null;
                        SocketProcessRequest.SocketRequest("!!!", myId.ToString(), 3);
                        SystemMess systemMess = new SystemMess();
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AccountLoggedAlert, SystemMessageConst.systemmessage.AccountLoggedAlertEn);
                        return Json(new { Resulf = systemMess }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
           
            return Json(new { Resulf }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckSelectRollAdmin()
        {

            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            JObject JObj2 = JObject.Parse(System.Web.HttpContext.Current.Request["objS"]);
            string email = "", ComputerName = "", ip = "", sessionid = "";
            int id = 0, idquyentk = 0;

            DisplayUntils.GetJson<string>("email", JObj, out email);
            DisplayUntils.GetJson<string>("ComputerName", JObj, out ComputerName);
            DisplayUntils.GetJson<string>("ip", JObj, out ip);
            DisplayUntils.GetJson<string>("sessionid", JObj, out sessionid);
            DisplayUntils.GetJson<int>("id", JObj, out id);
            DisplayUntils.GetJson<int>("idquyentk", JObj2, out idquyentk);


            var context = new LoginBusiness();
            var Resulf = context.CheckRollAdmin(email, ComputerName, ip, sessionid, id, idquyentk);

            return Json(new { Resulf = Resulf }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ResetPassWord()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string emailReset = "";
            DisplayUntils.GetJson<string>("emailReset", JObj, out emailReset);

            var context = new LoginBusiness();
            var Result = context.ResetPassWordAccount(emailReset);

            return Json(new { Result = Result }, JsonRequestBehavior.AllowGet);

        }
    }
}