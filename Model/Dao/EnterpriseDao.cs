using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class EnterpriseDao
    {
        CareerWeb db = null;
        public EnterpriseDao()
        {
            db = new CareerWeb();
        }
        public bool Insert(Enterprise ins)
        {
            try
            {
                db.Enterprises.Add(ins);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<Enterprise> ReturnList()
        {
            return db.Enterprises.ToList();
        }
    }
}
