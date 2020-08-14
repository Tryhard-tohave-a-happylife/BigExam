using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SavedCandidateDao
    {
        CareerWeb db = null;
        public SavedCandidateDao()
        {
            db = new CareerWeb();
        }

        public List<SavedCandidate> ListSavedCandidate()
        {
            return db.SavedCandidates.ToList();
        }

        public List<SavedCandidate> ListCandidateSave(Guid enterpriseId)
        {
            var listCandidate = ListSavedCandidate();
            var result = from SavedCandidateOfEnterprise in listCandidate
                         where SavedCandidateOfEnterprise.EnterpriseID == enterpriseId
                         select SavedCandidateOfEnterprise;
            return result.ToList();
        }
    }
}
