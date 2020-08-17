using Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ArticleDao
    {
        CareerWeb db = null;
        public ArticleDao()
        {
            db = new CareerWeb();
        }
        public List<Article> ListByEmployee(Guid emplID)
        {
            return db.Articles.Where(x => x.CreateID == emplID).ToList();
        }
        public bool Insert(Article ac)
        {
            try
            {
                db.Articles.Add(ac);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public Article FindByID(int articleID)
        {
            try
            {
                return db.Articles.Find(articleID);
            }
            catch
            {
                return null;
            }
        }
        public bool Edit(Article modify)
        {
            try
            {
                var ac = db.Articles.Find(modify.ID);
                if(modify.Image != null)
                {
                    ac.Image = modify.Image;
                }
                ac.Title = modify.Title;
                ac.ModifyDate = modify.ModifyDate;
                ac.Description = modify.Description;
                ac.CategoryParent = modify.CategoryParent;
                ac.CategoryID = modify.CategoryID;
                ac.ContentArticle = modify.ContentArticle;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool Delete(int articleID)
        {
            try
            {
                var remove = db.Articles.Find(articleID);
                //if(remove.Status != "Request")
                //{
                    var cateBig = db.CategoryArticles.Find(remove.CategoryParent.Value);
                    if(cateBig.Amount != 0) cateBig.Amount -= 1;
                    var cateId = db.CategoryArticles.Find(remove.CategoryID);
                    if (cateId.Amount != 0) cateId.Amount -= 1;
                //}
                db.Articles.Remove(remove);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<Article> ListByDate()
        {
            return db.Articles.OrderByDescending(x => x.CreateDate).Take(5).ToList();
        }
        public List<Article> ListByViews()
        {
            return db.Articles.OrderByDescending(x => x.Views).Take(4).ToList();
        }
        public List<Article> ListByCateBig(int CateParent)
        {
            return db.Articles.Where(x => x.CategoryParent == CateParent).ToList();
        }
        public List<Article> ListByCateID(int CateID)
        {
            return db.Articles.Where(x => x.CategoryID == CateID).ToList();
        }
        public bool IncreaseView(int articleID)
        {
            try
            {
                var article = db.Articles.Find(articleID);
                article.Views += 1;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
