using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using System.IO;

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
        public ActionResult CreatUniversity(University university, HttpPostedFileBase logo)
        {
            if(logo != null && logo.ContentLength > 0)
            {
                logo.InputStream.Read(new byte[logo.ContentLength], 0, logo.ContentLength);
                string fileName = System.IO.Path.GetFileName(logo.FileName);
                string urlImage = Server.MapPath("~/Assets/Admin/Img/University/" + logo.FileName);
                logo.SaveAs(urlImage);
                university.UniversityLogo = "Assets/Admin/Img/University/" + logo.FileName;
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