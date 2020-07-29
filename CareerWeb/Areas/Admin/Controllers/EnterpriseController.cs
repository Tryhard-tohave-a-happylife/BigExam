using CareerWeb.Models;
using Common;
using Model.Dao;
using Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Areas.Admin.Controllers
{
    public class EnterpriseController : Controller
    {
        // GET: Admin/Enterprise
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ResponseEnterprise()
        {
            var model = new EnterpriseDao().ListResponse();
            return View(model);
        }
        [HttpPost]
        public ActionResult GetInfor(Guid id)
        {
            var ent = new EnterpriseDao().FindById(id);
            var accCreate = new AccountDao().FindAccountByUserId(id);
            var listComplete = new EnterpriseJobDao().ListJob(id);
            var listNew = new EnterpriseJobDao().ListJobNew(id);
            var listArea = new EnterpriseAreaDao().ReturnList(id);
            var importantCom = new List<EnterpriseJobFull>();
            var normalCom = new List<EnterpriseJobFull>();
            var importantNew = new List<EnterpriseJobFull>();
            var normalNew = new List<EnterpriseJobFull>();
            if (ent == null || accCreate == null || (listComplete == null && listNew == null))
            {
                return Json(new
                {
                    status = false
                });
            }
            if (listComplete != null)
            {
                foreach (var item in listComplete)
                {
                    if (item.Important) importantCom.Add(item);
                    else normalCom.Add(item);
                }
            }
            if (listNew != null)
            {
                foreach (var item in listNew)
                {
                    if (item.Important) importantNew.Add(item);
                    else normalNew.Add(item);
                }
            }
            return Json(new
            {
                ent = ent,
                createDate = accCreate.CreateDate.ToString(),
                status = true,
                importantCom = importantCom,
                normalCom = normalCom,
                importantNew = importantNew,
                normalNew = normalNew,
                listArea = listArea
            });
        }
        private string RandomCode()
        {
            Random rd = new Random();
            string code = "";     
            for (int i = 0; i < 6; i++)
            {
                int mod = rd.Next(0, 100);
                code += ((rd.Next(0, 9) + mod) % 10) + "";
            }
            return code;
        }
        [HttpPost]
        public JsonResult AccpetRequest(Guid id)
        {
            var entChange = new EnterpriseDao().ChangeStatus(id);
            var accChange = new AccountDao().ChangeStatus(id);
            if (!entChange && !accChange)
            {
                return Json(new
                {
                    status = false
                });
            }
            try
            {
                var code = RandomCode();
                var ent = new EnterpriseDao().AddCode(id, code);
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/Template/SendAcceptEnterprise.html"));
                content = content.Replace("{{NameEnterprise}}", ent.EnterpriseName);
                content = content.Replace("{{Code}}", code);
                new MailHelper().SendMail(ent.Email, "Đơn hàng mới từ OnlineShop", content);
                return Json(new
                {
                    status = true,
                    checkEmail = true
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false,
                    checkEmail = true
                });
            }
        }
        [HttpPost]
        public JsonResult RemoveRequest(Guid id)
        {
            var rmEn = new EnterpriseDao().RemoveEnterprise(id);
            var rmEnJob = new EnterpriseJobDao().RemoveEntJob(id);
            var rmEnArea = new EnterpriseAreaDao().RemoveEntArea(id);
            var rmAcc = new AccountDao().RemoveAccountByUserID(id);
            if(!rmEn || !rmEnJob || !rmEnArea || !rmAcc)
            {
                return Json(new
                {
                    status = false
                });
            }
            return Json(new
            {
                status = true
            });
        }
    }
}