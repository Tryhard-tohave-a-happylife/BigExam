using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class WorkInvitationDao
    {
        CareerWeb db = null;
        public WorkInvitationDao()
        {
            db = new CareerWeb();
        }

        public WorkInvitation findWorkInvitation(Guid userId, Guid offerId)
        {
            return db.WorkInvitations.SingleOrDefault(x => x.UserID == userId && x.OfferID == offerId);

        }

        public bool InsertWorkInvitation(WorkInvitation workInvitation)
        {
            try
            {
                db.WorkInvitations.Add(workInvitation);
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
