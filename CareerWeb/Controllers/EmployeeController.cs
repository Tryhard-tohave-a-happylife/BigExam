using Model.Dao;
﻿using Model.EF;
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
        public ActionResult ListAndCreateOffer(int page = 1)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var listOffer = new OfferJobDao().ListByEmployee(acc.UserId);
            ViewBag.ListMajor = new JobMajorDao().ListJobMain();
            ViewBag.ListSkill = new JobMajorDao().ListJobSub();
            ViewBag.ListArea = new AreaDao().ListArea();
            ViewBag.ListPosition = new PositionEmployeeDao().ReturnList();
            ViewBag.ListSalary = new SalaryDao().ListSalary();
            ViewBag.ListLearning = new LevelLearningDao().ReturnList();
            ViewBag.ListExperience = new ExperienceDao().ListExperiences();
            return View(listOffer);
        }


        [HttpPost]
        public JsonResult CreateAccountInfor(Guid UserID, Guid EnterpriseID, string EmployeeName, int Position,string Sex, string BirthDay, string Email, string Mobile, string Code)
        {
            var employeeInfor = new Employee();
            employeeInfor.EmployeeID = UserID;
            employeeInfor.EnterpriseID = EnterpriseID;
            employeeInfor.EmployeeName = EmployeeName;
            employeeInfor.Position = Position;
            employeeInfor.Sex = Sex;
            employeeInfor.Email = Email;
            employeeInfor.Mobile = Mobile;
            string[] splitDate = BirthDay.Split('-');
            employeeInfor.BirthDay = new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]));
            var ent = new EnterpriseDao().FindById(EnterpriseID);
            if(ent.Code == Code)
            {
               var checkInsertEmployee = new EmployeeDao().InsertEmployee(employeeInfor);
               return Json(new
                    {
                        status = checkInsertEmployee,
                        codeInput = true
                    });
            }
            return Json(new
            {
                codeInput = false
            });
        }
        public ActionResult ListArticleEmployee()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var ListArticle = new ArticleDao().ListByEmployee(acc.UserId);
            ViewBag.ListCategory = new CategoryArticleDao().ListParent();
            return View(ListArticle);
        }
    }
}