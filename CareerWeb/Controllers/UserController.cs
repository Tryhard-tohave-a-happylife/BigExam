using CareerWeb.Models;
using Model.Dao;
using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace CareerWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserHome()
        {
            ViewBag.ListEnterpriseName = new EnterpriseDao().ReturnList();
            ViewBag.ListJobMain = new EnterpriseJobDao().ListEnterpriseJob();
            ViewBag.ListArea = new AreaDao().ListArea();
            ViewBag.ListOffer = new OfferJobDao().ListOfferJob();
            return View();
        }
        public ActionResult ResultForSearchCompany()
        {
            return View();
        }
        public ActionResult SearchCompanyForUser()
        {
            return View();
        }
        public ActionResult ResultForSearchJob()
        {

            
            ViewBag.ListEnterpriseName = new EnterpriseDao().ReturnList(); 
            ViewBag.ListJobMain = new EnterpriseJobDao().ListEnterpriseJob();
            ViewBag.ListArea = new AreaDao().ListArea();
            return View();
        }
        public ActionResult MoreNewsFromHandbook()
        {
            return View();
        }
        public ActionResult HandbookForUser()
        {
            return View();
        }
        public ActionResult SearchJobForUser(String OfferName = "", int Area = 0, String JobAddress = "0", int OfferSalary = 0, int PositionJobID = 0, String Sex = "0",  int ExperienceRequest = 0, int LearningLevelRequest = 0, DateTime OfferCreateDate = new DateTime())
        {
            ViewBag.ListJobMain = new JobMajorDao().ListJobMain();
            ViewBag.ListArea = new AreaDao().ListArea();
            ViewBag.ListExperience = new ExperienceDao().ListExperience();
            ViewBag.ListSalary = new SalaryDao().ListSalary();
            ViewBag.ListPositionEmployee = new PositionEmployeeDao().ReturnList();
            ViewBag.ListLevelLearning = new LevelLearningDao().ReturnLevelLearning();
            ViewBag.ListJobContainer = new OfferJobDao().ReturnFilterList(OfferName, Area, JobAddress, OfferSalary, PositionJobID, Sex, ExperienceRequest, LearningLevelRequest, OfferCreateDate);
            return View();
        }
       
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var user = new UserDao().FindById(acc.UserId);
            ViewBag.ListUserJobParent = new JobMajorDao().ReturnParentListByUser(user.UserId);
          
            ViewBag.ListPostion = new PositionEmployeeDao().ReturnList();
            ViewBag.ListLanguage = new LanguageDao().ReturnList();
            ViewBag.ListJobSub = new JobMajorDao().ListJobSubByUser(user.UserId);
            ViewBag.ListEnterprise = new EnterpriseDao().ReturnList();
            ViewBag.ListArea = new AreaDao().ListArea();
            ViewBag.ListUserExperience = new UserExperienceDao().ListByUser(acc.UserId);
            ViewBag.ListUserForeignLanguage = new UserForeignLanguageDao().ListByUser(acc.UserId);
            ViewBag.ListUserCertificate = new UserCertificateDao().ListByUser(acc.UserId);
            return View(user);
        }
        [HttpPost]
        public JsonResult CreateAccountInfor(Guid userID, string email, string name, string mobile, string dateBirth, string sex, string atSchool, int area, List<int> listJob)
        {
            var userInfor = new User();
            userInfor.UserId = userID;
            userInfor.UserName = name;
            userInfor.UserEmail = email;
            userInfor.UserMobile = mobile;
            userInfor.AtSchool = (atSchool == "in") ? true : false;
            userInfor.Sex = sex;
            userInfor.UserArea = area;
            string[] splitDate = dateBirth.Split('-');
            userInfor.UserBirthDay = new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]));
            userInfor.CompleteProfile = 1;
            var checkInsertUser = new UserDao().InsertUser(userInfor);
            var checkInsertUserMajor = true;
            foreach (int x in listJob)
            {
                var userMajor = new UserMajor();
                userMajor.UserID = userID;
                userMajor.MajorID = x;
                var ck = new UserMajorDao().InsertUserMajor(userMajor);
                if (!ck) checkInsertUserMajor = ck;
            }
            if (checkInsertUser && checkInsertUserMajor)
            {
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
        [HttpPost]
        public JsonResult ModifyUser(ModifyUserForm user)
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var check = new UserDao().ModifyUser(acc.UserId, user);
            if (check)
            {
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
        [HttpPost]
        public JsonResult ImageUpload(FileUploadModel model)
        {
            var file = model.ImageFile;
            if (file != null)
            {

                var fileName = Path.GetFileName(file.FileName);
                var extention = Path.GetExtension(file.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);
                file.SaveAs(Server.MapPath("/Assets/Client/Img/User/ImageProfile/" + fileName));
                var srcImage = "/Assets/Client/Img/User/ImageProfile/" + fileName;
                var accID = int.Parse(User.Identity.Name);
                var acc = new AccountDao().FindAccountById(accID);
                var check = new UserDao().UploadImage(acc.UserId, srcImage);
                if (check)
                {
                    return Json(new
                    {
                        status = true,
                    });
                }
            }
            return Json(new
            {
                status = false
            });
        }
    }
}