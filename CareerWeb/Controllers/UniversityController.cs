using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;

namespace CareerWeb.Controllers
{
    public class UniversityController : Controller
    {
        // GET: University
        public ActionResult UniversityHome()
        {

            return View();
        }
        public ActionResult Statistic()
        {
            ViewBag.count = new UserForeignLanguageDao().Count();
            return View();
        }
        public ActionResult ListOfStudent()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}