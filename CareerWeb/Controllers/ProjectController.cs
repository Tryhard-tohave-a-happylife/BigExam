using CareerWeb.Hubs;
using CareerWeb.Models;
using Common;
using iTextSharp.text.pdf.qrcode;
using Model.Dao;
using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CareerWeb.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult ChatProject(int projectID)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            var accID = int.Parse(User.Identity.Name);
            ViewBag.ProjectID = projectID;
            ViewBag.accID = accID;
            var chatboxModel = new AccountDao().GetChatBox(projectID);
            ViewBag.listUser = new AccountDao().GetByListMes(chatboxModel);
            return View(chatboxModel);
        }
        public ActionResult ListProject(int major = -1, string title = "", string listSkill = "", bool space = false, int page = 1)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var proDao = new ProjectDao();
            var model = new ModelProject();
            var arr = listSkill.Split('-');
            var list = new List<int>();
            if (listSkill != "")
            {
                foreach (var item in arr)
                {
                    list.Add(int.Parse(item));
                }
            }
            ViewBag.ListMajor = new JobMajorDao().ListJobMain();
            ViewBag.ListSkill = new JobMajorDao().ListJobSub();
            ViewBag.UserID = acc.UserId;
            model.ListFull = proDao.FindBySearch(major, title, list, space);
            model.ListUserCreate = proDao.ListByUserCreate(acc.UserId);
            model.ListUserJoin = proDao.ListByUserJoin(acc.UserId);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddAndEditProject(FormProject model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var project = new Project();
            if(model.typeAction == "add")
            {
                project.CreateDate = DateTime.Now;
                project.MasterID = acc.UserId;
            }
            else
            {
                project.ProjectID = model.saveID;
            }
            project.ProjectMajor = model.major;
            project.Title = model.title;
            project.Amount = model.amount;
            project.Apply = 1;
            project.Description = model.description;
            var check = -1;
            if(model.typeAction == "add")
            {
                check = new ProjectDao().Insert(project);
                var projectMember = new ProjectMember();
                projectMember.ProjectID = check;
                projectMember.MemberID = acc.UserId;
                projectMember.CreateDate = DateTime.Now;
                projectMember.Status = "master";
                var checkSecond = new ProjectMemberDao().Insert(projectMember);
            }
            else
            {
                var edit = new ProjectDao().Edit(project);
            }
            if (model.skill != null && model.skill.Count != 0)
            {
                var prSkillDao = new ProjectSkillDao();
                foreach (var item in model.skill)
                {
                    var prk = new ProjectSkill();
                    prk.ProjectSkill1 = item;
                    prk.ProjectID = check != -1 ? check : model.saveID;
                    var echInsert = prSkillDao.Insert(prk);
                }
            }
            return RedirectToAction("ListProject");
        }
        [HttpPost]
        public JsonResult ReturnProjectAndSkill(int projectID)
        {
            var project = new ProjectDao().FindByID(projectID);
            var listSkill = new ProjectSkillDao().ListByProject(projectID);
            var listName = new List<string>();
            var jmDao = new JobMajorDao();
            foreach(var item in listSkill)
            {
                listName.Add(jmDao.NameJob(item.ProjectSkill1));
            }
            if (project == null || listSkill == null)
            {
                return Json(new
                {
                    status = false
                });
            }         
            return Json(new
            {
                status = true,
                project = project,
                listSkill = listSkill,
                listName = listName
            });
        }
        [HttpPost]
        public JsonResult DeleteSkillProject(int projectID, int skillID)
        {
            var check = new ProjectSkillDao().DeleteBySkill(projectID, skillID);
            return Json(new
            {
                status = check
            });
        }
        [HttpPost]
        public JsonResult DeleteProject(int projectID)
        {
            var check = new ProjectDao().Delete(projectID);
            return Json(new
            {
                status = check
            });
        }
        [HttpPost]
        public JsonResult SendRequestApply(int projectID, string description, string email)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new
                {
                    status = 0
                });
            }
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var listMember = new ProjectMemberDao().ListByProject(projectID);
            if (listMember.Select(x => x.MemberID).Contains(acc.UserId))
            {
                return Json(new
                {
                    status = -1
                });
            }
            var projectMember = new ProjectMember();
            projectMember.ProjectID = projectID;
            projectMember.MemberID = acc.UserId;
            projectMember.CreateDate = DateTime.Now;
            projectMember.Status = "request";
            projectMember.Description = description;
            var check = new ProjectMemberDao().Insert(projectMember);
            if (!check)
            {
                return Json(new
                {
                    stauts = 0
                });
            }
            try
            {
                var user = new UserDao().FindById(acc.UserId);
                var project = new ProjectDao().FindByID(projectID);
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/Template/SendRequestApply.html"));
                content = content.Replace("{{NameUser}}", user.UserName);
                content = content.Replace("{{NameProject}}", project.Title);
                content = content.Replace("{{Description}}", description);
                new MailHelper().SendMail(email, "Request Apply Dự án", content);
                return Json(new
                {
                    status = 1,
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = 0,
                });
            }
        }
        [HttpPost]
        public JsonResult ReturnListMemberAndRequest(int projectID)
        {
            var listMember = new ProjectMemberDao().ListByProject(projectID);
            if(listMember == null)
            {
                return Json(new
                {
                    status = false
                });
            }
            var memberDetail = new List<User>();
            var memberBirthDay = new List<string>();
            var usDao = new UserDao();
            foreach(var item in listMember) {
                var us = usDao.FindById(item.MemberID);
                memberDetail.Add(us);
                memberBirthDay.Add(us.UserBirthDay.ToString("dd/MM/yyyy"));
            }
            return Json(new
            {
                status = true,
                listMember = listMember,
                memberDetail = memberDetail,
                memberBirthDay = memberBirthDay
            });
        }
        [HttpPost]
        public JsonResult AcceptRequest(int projectID, Guid userID, string email)
        {
            var check = new ProjectMemberDao().Accept(projectID, userID);
            if (!check)
            {
                return Json(new
                {
                    status = false
                });
            }
            try
            { 
                var project = new ProjectDao().FindByID(projectID);
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/Template/SendAcceptProject.html"));
                content = content.Replace("{{NameProject}}", project.Title);
                new MailHelper().SendMail(email, "Thông báo tham gia Dự án", content);
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
        [HttpPost]
        public JsonResult DeleteMember(int projectID, Guid userID)
        {
            var check = new ProjectMemberDao().DeleteByUser(projectID, userID);
            return Json(new
            {
                status = check
            });
        }
        [HttpPost]
        public JsonResult SendMessageProjectChat(string message, int toProjectID)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new
                {
                    status = 0
                });
            }
            var accID = int.Parse(User.Identity.Name);
            ChatHub.RecieveMessage(toProjectID, message);
            var us = new AccountDao().FindUserChat(accID);
            return Json(new
            {
                status = new MessageDao().SendMessage(accID, toProjectID, message),
                us = us
            });
        }
    }
}