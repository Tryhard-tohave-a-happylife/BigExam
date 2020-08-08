using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OfferJobMajorDao
    {
        CareerWeb db = null;
        public OfferJobMajorDao()
        {
            db = new CareerWeb();
        }

        public List<OfferJobMajor> ReturnList()
        {
            return db.OfferJobMajors.ToList();
        }

        public bool InsertJobMajor(OfferJobMajor JobMajor)
        {
            try
            {
                db.OfferJobMajors.Add(JobMajor);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
