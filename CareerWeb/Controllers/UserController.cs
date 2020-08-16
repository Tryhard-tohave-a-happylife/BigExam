using CareerWeb.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
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
using System.Text;
using HtmlAgilityPack;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.html;
using System.Web.UI.WebControls;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.html.head;
using Rotativa;
using System.Data.Entity.Core.Metadata.Edm;

namespace CareerWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult ResultForSearchJob()
        {
            return View();
        }
        public ActionResult MoreNewsFromHandbook(int lout = 1, int CateBig = -1, int CateID = -1)
        {
            ViewBag.LayoutID = lout;
            var listCateBig = new CategoryArticleDao().ListParent();
            var listCate = new List<List<CategoryArticle>>();
            ViewBag.CateBig = CateBig;
            ViewBag.CateID = CateID;
            ViewBag.ListArticleByView = new ArticleDao().ListByViews();
            foreach (var item in listCateBig)
            {
                var eachList = new CategoryArticleDao().ListByParentID(item.CategoryID);
                listCate.Add(eachList);
            }
            ViewBag.ListCateBig = listCateBig;
            ViewBag.ListCate = listCate;
            if (CateID != -1)
            {
                var model = new ArticleDao().ListByCateID(CateID);
                return View(model);
            }
            else
            {
                var model = new ArticleDao().ListByCateBig(CateBig);
                return View(model);
            }
            
        }
        public ActionResult HandbookForUser(int lout = 1)
        {
            ViewBag.LayoutID = lout;
            var listCateBig = new CategoryArticleDao().ListParent();
            var listCate = new List<List<CategoryArticle>>();
            foreach(var item in listCateBig)
            {
                var eachList = new CategoryArticleDao().ListByParentID(item.CategoryID);
                listCate.Add(eachList);
            }
            ViewBag.ListCateBig = listCateBig;
            ViewBag.ListCate = listCate;
            var arDao = new ArticleDao();
            ViewBag.ListArticleNew = arDao.ListByDate();
            ViewBag.ListArticleByView = arDao.ListByViews();
            var listArticleByCateBig = new List<List<Article>>();
            foreach(var item in listCateBig)
            {
                var eachList = arDao.ListByCateBig(item.CategoryID);
                listArticleByCateBig.Add(eachList);
            }
            ViewBag.ListArticleByCateBig = listArticleByCateBig;
            return View();
        }
        public ActionResult SearchJobForUser()
        {
            ViewBag.ListJobMain = new JobMajorDao().ListJobMain();
            ViewBag.ListArea = new AreaDao().ListArea();
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
            ViewBag.ListLevelLearning = new LevelLearningDao().ReturnList();
            ViewBag.ListPostion = new PositionEmployeeDao().ReturnList();
            ViewBag.ListLanguage = new LanguageDao().ReturnList();
            ViewBag.ListJobSub = new JobMajorDao().ListJobSubByUser(user.UserId);
            ViewBag.ListEnterprise = new EnterpriseDao().ReturnList();
            ViewBag.ListArea = new AreaDao().ListArea();
            ViewBag.ListUserExperience = new UserExperienceDao().ListByUser(acc.UserId);
            ViewBag.ListUserForeignLanguage = new UserForeignLanguageDao().ListByUser(acc.UserId);
            ViewBag.ListUserCertificate = new UserCertificateDao().ListByUser(acc.UserId);
            ViewBag.ListSkill = new UserMajorDao().ListSkillByUserId(acc.UserId);
            ViewBag.ListMajorUser = new UserMajorDao().ListUserMajor(acc.UserId);
            ViewBag.ListMajorCanChoose = new JobMajorDao().ListJobMainByUser(acc.UserId);
            ViewBag.ListSalary = new SalaryDao().ListSalary();
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
            var check = new UserDao().ModifyUserBasic(acc.UserId, user);
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
        [HttpPost]
        public JsonResult ModifyInforJob(string nameJob, int salaryID, int positionId, List<int> idList)
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var userNew = new User();
            userNew.DesiredJob = nameJob;
            userNew.Salary = salaryID;
            userNew.PositionApply = positionId;
            var check = new UserDao().ModifyUserJob(acc.UserId, userNew);
            if (!check)
            {
                return Json(new
                {
                    status = false
                });
            }
            for(var i = 0; i < idList.Count; i++)
            {
                var newUserMajor = new UserMajor();
                newUserMajor.UserID = acc.UserId;
                newUserMajor.MajorID = idList[i];
                var checkStepSecond = new UserMajorDao().InsertUserMajor(newUserMajor);
                if (!checkStepSecond)
                {
                    check = false;
                    break;
                }
            }
            return Json(new
            {
                status = check
            });
        }
        public ActionResult ViewCV(string template = "1")
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            ViewBag.Template = template;
            return View();
        }
        public ActionResult Check(string template, bool isPdf = false )
        {
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var user = new UserDao().FindById(acc.UserId);
            ViewBag.ListUserExperience = new UserExperienceDao().ListByUser(acc.UserId);
            ViewBag.ListUserForeignLanguage = new UserForeignLanguageDao().ListByUser(acc.UserId);
            ViewBag.ListUserCertificate = new UserCertificateDao().ListByUser(acc.UserId);
            ViewBag.ListSkill = new UserMajorDao().ListSkillByUserId(acc.UserId);
            ViewBag.ListMajorUser = new UserMajorDao().ListUserMajor(acc.UserId);
            ViewBag.Link = Server.MapPath(user.UserImage);
            if (!isPdf)
            {
                return PartialView(template, user);
            }
            return new PartialViewAsPdf(template, user);
        }
        public ActionResult TemplateCV_1()
        {
            return PartialView();
        }
    }
}