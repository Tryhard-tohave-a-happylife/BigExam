using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class EnterpriseJobDao
    {
        CareerWeb db = null;
        public EnterpriseJobDao()
        {
            db = new CareerWeb();
        }
        public bool Insert(EnterpriseJob ins)
        {
            try
            {
                db.EnterpriseJobs.Add(ins);
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
