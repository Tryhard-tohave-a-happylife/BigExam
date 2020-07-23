using CareerWeb.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Controllers
{
    public class EnterpriseController : Controller
    {
        // GET: Enterprise
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CreateAccountInfor(InsertEnterpriseForm model)
        {
            var enterprise = new Enterprise();
            enterprise.EnterpriseID = model.Id;
            enterprise.EnterpriseName = model.Name;
            enterprise.Email = model.Email;
            enterprise.Mobile = model.Mobile;
            enterprise.EstablishYear = model.EstablishYear;
            enterprise.ImageLogo = model.Logo;
            enterprise.TypeOfEnterprise = model.Type;
            enterprise.Status = false;
            enterprise.EnterpriseSize = model.Size;
            var checkInsEnterprise = new EnterpriseDao().Insert(enterprise);
            var ret = true;
            foreach(var item in model.ListArea)
            {
                if (!ret) break;
                var enterpriseArea = new EnterpriseArea();
                enterpriseArea.AreaID = item;
                enterpriseArea.EnterpriseId = model.Id;
                var check = new EnterpriseAreaDao().Insert(enterpriseArea);
                if (!check) ret = false;
            }
            foreach(var item in model.ListJobImp)
            {
                if (!ret) break;
                var enterpriseJob = new EnterpriseJob();
                enterpriseJob.EnterpriseID = model.Id;
                enterpriseJob.JobId = item;
                enterpriseJob.JobIdParent = null;
                enterpriseJob.Important = true;
                var check = new EnterpriseJobDao().Insert(enterpriseJob);
                if (!check) ret = false;
            }
            foreach (var item in model.ListJobSub)
            {
                if (!ret) break;
                var enterpriseJob = new EnterpriseJob();
                enterpriseJob.EnterpriseID = model.Id;
                enterpriseJob.JobId = item;
                enterpriseJob.JobIdParent = null;
                enterpriseJob.Important = false;
                var check = new EnterpriseJobDao().Insert(enterpriseJob);
                if (!check) ret = false;
            }
            foreach(var item in model.NewJobImp)
            {
                if (!ret) break;
                var newJob = new JobMajor();
                newJob.JobName = item;
                newJob.Status = false;
                var checkJob = new JobMajorDao().Insert(newJob);
                if(checkJob == -1)
                {
                    ret = false;
                    continue;
                }
                var enterpriseJob = new EnterpriseJob();
                enterpriseJob.EnterpriseID = model.Id;
                enterpriseJob.JobId = checkJob;
                enterpriseJob.JobIdParent = null;
                enterpriseJob.Important = true;
                var check = new EnterpriseJobDao().Insert(enterpriseJob);
                if (!check) ret = false;
            }
            foreach (var item in model.NewJobSub)
            {
                if (!ret) break;
                var newJob = new JobMajor();
                newJob.JobName = item;
                newJob.Status = false;
                var checkJob = new JobMajorDao().Insert(newJob);
                if (checkJob == -1)
                {
                    ret = false;
                    continue;
                }
                var enterpriseJob = new EnterpriseJob();
                enterpriseJob.EnterpriseID = model.Id;
                enterpriseJob.JobId = checkJob;
                enterpriseJob.JobIdParent = null;
                enterpriseJob.Important = false;
                var check = new EnterpriseJobDao().Insert(enterpriseJob);
                if (!check) ret = false;
            }
            return Json(new
            {
                status = true
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
                file.SaveAs(Server.MapPath("/Assets/Client/Img/Enterprise/LogoEnterprise/" + fileName));
                return Json(new
                {
                    status = true,
                    srcImage = "/Assets/Client/Img/Enterprise/LogoEnterprise/" + fileName
                });
            }
            return Json(new
            {
                status = false
            });
        }
    }
}