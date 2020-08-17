using Model.Dao;
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
        public ActionResult UniversityHome(Guid UniversityID)
        {

            ViewBag.InfoUni = new UniversityDao().FindById(UniversityID);

            return View();
        }
        public ActionResult Statistic(Guid UniversityID)
        {

            ViewBag.count = new UserForeignLanguageDao().Count();

            ViewBag.InfoUni = new UniversityDao().FindById(UniversityID);

            return View();
        }
        public ActionResult ListOfStudent(Guid UniversityID)
        {
            ViewBag.InfoUni = new UniversityDao().FindById(UniversityID);
            ViewBag.ListStudent = new UserDao().ListFilterUni(UniversityID);
            var ListJob = new List<string> ();
            var ShowJob = new UserDao().ListFilterUni(UniversityID);
            foreach (var item in ShowJob)
            {
                var saveName = "";
                for (var i = 0; i < item.listJob.Count; i += 1)
                {
                    saveName += item.listJob[i] + ", ";
                }
                saveName.Remove(saveName.Length - 1);
                ListJob.Add(saveName);
            }
            ViewBag.ListFullJobName = ListJob;
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}