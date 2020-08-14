using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UniversityDao { 
        CareerWeb db = null;
        public UniversityDao()
        {
            db = new CareerWeb();
        }
    
    public List<University> ListUniversities()
    {
        try
        {
            return db.Universities.ToList();
        }
        catch (Exception e)
        {
            return null;
        }
    }
        public University FindById(Guid id)
        {
            try
            {
                return db.Universities.Find(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool InsertUniversity(University university)
        {
            try
            {
                university.UniversityID = Guid.NewGuid();
                db.Universities.Add(university);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(Guid id)
        {
            try
            {
                var university = db.Universities.Find(id);
                db.Universities.Remove(university);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
