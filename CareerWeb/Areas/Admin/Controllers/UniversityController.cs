using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using System.IO;
using System.Web.Hosting;

namespace CareerWeb.Areas.Admin.Controllers
{
    public class UniversityController : Controller
    {
        // GET: Admin/University
        public ActionResult Index()
        {
            var model = new UniversityDao().ListUniversities();
            return View(model);
        }
        [HttpGet]
        public ActionResult CreatUniversity()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatUniversity(University university, HttpPostedFileBase UniversityLogo)
        {
            var file = UniversityLogo;
            if (file != null)
            {

                var fileName = Path.GetFileName(file.FileName);
                var extention = Path.GetExtension(file.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);
                file.SaveAs(Server.MapPath("/Assets/Admin/Img/University/" + fileName));
                var srcImage = "/Assets/Admin/Img/University/" + fileName;
                university.UniversityLogo = srcImage;
            }

            if (ModelState.IsValid) { 
            var check = new UniversityDao().InsertUniversity(university);
            if(check)
            {
                return RedirectToAction("Index", "University");
            }
            else
            {
                ModelState.AddModelError("", "Creat university failed!");
            }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult DeleteUniversity(Guid id)
        {
            new UniversityDao().Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult DetailUniversity(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var university = new UniversityDao().FindById(id);
            return View(university);
        }
    }
}