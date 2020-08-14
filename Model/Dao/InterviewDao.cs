using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class InterviewDao
    {
        CareerWeb db = null;
        public InterviewDao()
        {
            db = new CareerWeb();
        }

        public Interview findInterview(Guid userId,Guid offerId)
        {
            return db.Interviews.SingleOrDefault(x => x.UserID == userId && x.OfferID == offerId);

        }

        public bool InsertInterview(Interview interview)
        {
            try
            {
                db.Interviews.Add(interview);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateStatus(Guid userId, Guid offerId, String status)
        {
            try
            {
                var interview = findInterview(userId, offerId);
                interview.Status = status;
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
