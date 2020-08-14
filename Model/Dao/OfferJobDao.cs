using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OfferJobDao
    {
        CareerWeb db = null;
        public OfferJobDao()
        {
            db = new CareerWeb();
        }
        public List<OfferJob> ListByEmployee(Guid eplID)
        {
            return db.OfferJobs.Where(x => x.EmployeeID == eplID).ToList();
        }

        public OfferJob findById(Guid offerId)
        {
            return db.OfferJobs.Find(offerId);
        }
    }
}
