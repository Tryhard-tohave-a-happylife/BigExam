using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

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
    }
}
