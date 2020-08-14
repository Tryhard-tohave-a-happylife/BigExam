using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AppliedCandidateDao
    {
        CareerWeb db = null;
        public AppliedCandidateDao()
        {
            db = new CareerWeb();
        }
    
        public List<AppliedCandidate> ListCandidate()
        {
            return db.AppliedCandidates.ToList();
        }
    
        public List<AppliedCandidate> ListCandidateApply(Guid enterpriseId)
        {
            return db.AppliedCandidates.Where(x => x.EnterpriseID == enterpriseId).ToList();
        }

        public AppliedCandidate findCandidate(Guid userId, Guid offerId)
        {
            return db.AppliedCandidates.SingleOrDefault(x => x.UserID == userId && x.OfferID == offerId);
        }

        public bool UpdateStatus(Guid userId, Guid offerId, String status)
        {
            try
            {
                var candidate = findCandidate(userId, offerId);
                candidate.Status = status;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public List<AppliedCandidateInfo> AppliedCandidateList(Guid enterpriseId)
        {
            var appliedCandidateList = ListCandidateApply(enterpriseId);
            var result = (from CandidateOfEnterprise in appliedCandidateList
                          where CandidateOfEnterprise.EnterpriseID == enterpriseId
                          select new
                          {
                              EnterpriseID = CandidateOfEnterprise.EnterpriseID,
                              CandidateID = CandidateOfEnterprise.UserID,
                              CandidateName = new UserDao().FindById(CandidateOfEnterprise.UserID).UserName,
                              OfferID = CandidateOfEnterprise.OfferID,
                              OfferName = new OfferJobDao().findById(CandidateOfEnterprise.OfferID).OfferName,
                              Status = CandidateOfEnterprise.Status,
                              CreateDate = CandidateOfEnterprise.CreateDate
                          }).AsEnumerable().Select(x => new AppliedCandidateInfo()
                          {
                              EnterpriseID = x.EnterpriseID,
                              CandidateID = x.CandidateID,
                              CandidateName = x.CandidateName,
                              OfferID = x.OfferID,
                              OfferName = x.OfferName,
                              Status = x.Status,
                              CreateDate = x.CreateDate
                          });
            return result.ToList();
        }


    }


}
