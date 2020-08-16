using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model.Dao
{
    public class UserMajorDao
    {
        CareerWeb db = null;
        public UserMajorDao()
        {
            db = new CareerWeb();
        }
        public bool InsertUserMajor(UserMajor userMajor)
        {
            try
            {
                db.UserMajors.Add(userMajor);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool DeleteUserJob(Guid userId, int JobID)
        {
            try
            {
                var removeJob = db.UserMajors.SingleOrDefault(x => x.UserID == userId && x.MajorID == JobID);
                db.UserMajors.Remove(removeJob);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<UserMajor> ListUserMajor()
        {
            return db.UserMajors.ToList();
        }
        public List<UserMajor> ListUserMajor(Guid userId)
        {
            return db.UserMajors.Where(x => x.MajorParent == null && x.UserID == userId).ToList();
        }
        public List<UserMajor> ListSkillByUserId(Guid id)
        {
            return db.UserMajors.Where(x => x.UserID == id && x.MajorParent != null).ToList();
        }
        public List<UserMajor> ListSkillByUserAndMajor(Guid id, int parent)
        {
            return db.UserMajors.Where(x => x.UserID == id && x.MajorParent.Value == parent).ToList();
        }
    }
}
