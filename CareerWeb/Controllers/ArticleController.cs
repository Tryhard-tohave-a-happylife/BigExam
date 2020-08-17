using CareerWeb.Models;
using iTextSharp.text.pdf.qrcode;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CareerWeb.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ListCategorySmallArticle(int parentID)
        {
            var listCategory = new CategoryArticleDao().ListByParentID(parentID);
            if(listCategory == null)
            {
                return Json(new
                {
                    status = false
                });
            }
            return Json(new
            {
                list = listCategory,
                status = true
            });
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAndEditArticle(FormArticle model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            var accID = int.Parse(User.Identity.Name);
            var acc = new AccountDao().FindAccountById(accID);
            var emp = new EmployeeDao().FindById(acc.UserId);
            var article = new Article();
            if(model.typeAction == "edit")
            {
                article.ID = model.saveID;
                article.ModifyDate = DateTime.Now;
            }
            else
            {
                article.Status = "Request";
                article.CreateDate = DateTime.Now;
                article.CreateID = emp.EmployeeID;
                article.Views = 0;
            }
            article.Description = model.Description;
            article.ContentArticle = model.ContentArticle;
            article.CategoryID = model.CategorySmall;
            article.CategoryParent = model.CategoryBig;
            article.Title = model.Title;
            var file = model.Image;
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath("/Assets/Client/Img/Article/" + fileName));
                var srcImage = "/Assets/Client/Img/Article/" + fileName;
                article.Image = srcImage;
            }
            if(model.typeAction == "add")
            {
                var check = new ArticleDao().Insert(article);
                var checkSecond = new CategoryArticleDao().IncreaseAmount(article.CategoryID);
                var checkThird = new CategoryArticleDao().IncreaseAmount(article.CategoryParent.Value);
            }
            else
            {
                var check = new ArticleDao().Edit(article);
            }
            return RedirectToAction("ListArticleEmployee", "Employee");
        }
        [HttpPost]
        public JsonResult ReturnArticle(int articleID)
        {
            var article = new ArticleDao().FindByID(articleID);
            if(article == null)
            {
                return Json(new
                {
                    status = false
                });
            }
            var listCate = new CategoryArticleDao().ListByParentID(article.CategoryParent.Value);
            return Json(new
            {
                status = true,
                list = listCate,
                article = article
            });
        }
        [HttpPost]
        public JsonResult DeleteArticle(int articleID)
        {
            var check = new ArticleDao().Delete(articleID);
            return Json(new
            {
                status = check
            });
        }
        public ActionResult ArticleDetail(int articleID, int lout = 1)
        {
            ViewBag.LayoutID = lout;
            var listCateBig = new CategoryArticleDao().ListParent();
            var listCate = new List<List<CategoryArticle>>();
            foreach (var item in listCateBig)
            {
                var eachList = new CategoryArticleDao().ListByParentID(item.CategoryID);
                listCate.Add(eachList);
            }
            ViewBag.ListCateBig = listCateBig;
            ViewBag.ListCate = listCate;
            var model = new ArticleDao().FindByID(articleID);
            var check = new ArticleDao().IncreaseView(articleID);
            return View(model);
        }
    }
}