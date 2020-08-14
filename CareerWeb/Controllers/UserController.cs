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
        public ActionResult UserHome(string OfferName = "", int Area = 0, int OfferMajor = 0, int OfferSalary = 0, int PositionJobID = 0, string Sex = "0", int ExperienceRequest = 0, int LearningLevelRequest = 0, string OfferCreateDate = "")
        {
            ViewBag.ListJobMain = new JobMajorDao().ListJobMain();
            ViewBag.ListArea = new AreaDao().ListArea();
            ViewBag.ListExperience = new ExperienceDao().ListExperience();
            ViewBag.ListSalary = new SalaryDao().ListSalary();
            ViewBag.ListPositionEmployee = new PositionEmployeeDao().ReturnList();
            ViewBag.ListLevelLearning = new LevelLearningDao().ReturnList();

            var ListJobContainer = new OfferJobDao().ReturnFilterList(OfferName, Area, OfferMajor, OfferSalary, PositionJobID, Sex, ExperienceRequest, LearningLevelRequest);
            return View(ListJobContainer);
        }
        public ActionResult ResultForSearchCompany()
        {
            return View();
        }
         public ActionResult ResultForSearchJob(Guid OfferID)
        {
            ViewBag.ListEnterpriseName = new EnterpriseDao().ReturnList(); 
            ViewBag.ListJobMain = new EnterpriseJobDao().ListEnterpriseJob();
            ViewBag.ListArea = new AreaDao().ListArea();

            var ShowDetail = new OfferJobDao().ShowDetail(OfferID);
            return View(ShowDetail);
        }
        
        public ActionResult MoreNewsFromHandbook()
        {
            return View();
        }
        public ActionResult HandbookForUser()
        {
            return View();
        }
        public ActionResult NewsOfHandBook_3()
        {
            return View();
        }
        public ActionResult SearchJobForUser(string OfferName = "", int Area = 0, int OfferMajor = 0, int OfferSalary = 0, int PositionJobID = 0, string Sex = "0",  int ExperienceRequest = 0, int LearningLevelRequest = 0, string OfferCreateDate = "")
        {
            ViewBag.ListJobMain = new JobMajorDao().ListJobMain();
            ViewBag.ListArea = new AreaDao().ListArea();
            ViewBag.ListExperience = new ExperienceDao().ListExperience();
            ViewBag.ListSalary = new SalaryDao().ListSalary();
            ViewBag.ListPositionEmployee = new PositionEmployeeDao().ReturnList();
            ViewBag.ListLevelLearning = new LevelLearningDao().ReturnList();

            var ListJobContainer = new OfferJobDao().ReturnFilterList(OfferName, Area, OfferMajor, OfferSalary, PositionJobID, Sex, ExperienceRequest, LearningLevelRequest);
            return View(ListJobContainer);
        }
        public ActionResult SearchCompanyForUser(string EName = "", int EArea = 0, int ECareer = 0, int ESize = 0)
        {
            var jobMajorDao = new JobMajorDao();
            ViewBag.ListEnterpriseSize = new EnterpriseSizeDao().ReturnList();
            ViewBag.ListJobMain = jobMajorDao.ListJobMain();
            ViewBag.ListArea = new AreaDao().ListArea();

            var ListEnterpriseContainer = new EnterpriseDao().ReturnFilterList(EName, EArea, ECareer, ESize);
            var nameJob = new List<string>();
            foreach(var item in ListEnterpriseContainer)
            {
                var saveName = "";
                for (var i = 0; i < item.listJobId.Count; i++)
                {
                    if (i == 0)
                    {
                        saveName += jobMajorDao.JobName(item.listJobId[i]);
                        continue;
                    }
                    saveName += ", " + jobMajorDao.JobName(item.listJobId[i]);
                }
                nameJob.Add(saveName);
            }
            ViewBag.ListFullJobName = nameJob;
            return View(ListEnterpriseContainer);
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