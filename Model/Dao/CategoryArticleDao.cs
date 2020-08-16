using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryArticleDao
    {
        CareerWeb db = null;
        public CategoryArticleDao()
        {
            db = new CareerWeb();
        }
        public List<CategoryArticle> ListParent()
        {
            return db.CategoryArticles.Where(x => x.ParentID == null).ToList();
        }
        public List<CategoryArticle> ListByParentID(int parentID)
        {
            try
            {
                return db.CategoryArticles.Where(x => x.ParentID == parentID).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool IncreaseAmount(int categoryID)
        {
            try
            {
                var cg = db.CategoryArticles.Find(categoryID);
                cg.Amount += 1;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public string NameCategoryArticle(int categoryID)
        {
            return db.CategoryArticles.Find(categoryID).NameCategory;
        }
    }
}
