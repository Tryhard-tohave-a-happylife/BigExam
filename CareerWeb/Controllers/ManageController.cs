using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InterviewSchedule()
        {
            return View();
        }
        public ActionResult WorkList()
        {
            return View();
        }
        public ActionResult InviteWork()
        {
            return View();
        }
        public ActionResult InterviewLetter()
        {
            return View();
        }
        public ActionResult Result()
        {
            return View();
        }
    }
}