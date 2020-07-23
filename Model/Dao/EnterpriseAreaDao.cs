using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class EnterpriseAreaDao
    {
        CareerWeb db = null;
        public EnterpriseAreaDao()
        {
            db = new CareerWeb();
        }
        public bool Insert(EnterpriseArea ins)
        {
            try
            {
                db.EnterpriseAreas.Add(ins);
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
