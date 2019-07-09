using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASS_35.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            ViewBag.Title = "Regular Error";
            return View();
        }
        public ActionResult NotFound404()
        {
            ViewBag.Title = "Error 404 - File not Found";
            return View("Index");
        }
        public ActionResult Page404()
        {
            ViewBag.Title = "Error 404";
            return View("Index");
        }
    }
}