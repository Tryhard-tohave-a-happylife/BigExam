using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private object db;

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchCandidate()
        {
            ViewBag.JobListMain = new JobMajorDao().ListJobMain();
            ViewBag.JobListSub = new JobMajorDao().ListJobSub();
            ViewBag.AreaList = new AreaDao().ListArea();
            return View();
        
        }

        public ActionResult SearchCandidateResult(String Name = "0", String AreaID = "0", String JobID = "0")
        {
            ViewBag.JobListMain = new JobMajorDao().ListJobMain();
            ViewBag.AreaList = new AreaDao().ListArea();
            int areaId = int.Parse(AreaID); 
            int jobId = int.Parse(JobID);
            ViewBag.ListUser = new UserDao().ListUserFit(Name,areaId,jobId);

            return View();
        }



    }
}