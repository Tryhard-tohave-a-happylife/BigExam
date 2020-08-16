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
            return db.OfferJobs.Where(x => x.EmployeeID == eplID).OrderBy(x => x.OfferCreateDate).ToList();
        }
        public OfferJob FindByID(Guid offerID)
        {
            try
            {
                return db.OfferJobs.Find(offerID);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Insert(OfferJob of)
        {
            try
            {
                db.OfferJobs.Add(of);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool Edit(OfferJob of)
        {
            try
            {
                var modifyJob = db.OfferJobs.Find(of.OfferID);
                modifyJob.OfferName = of.OfferName;
                if (of.OfferDescription != null)
                {
                    modifyJob.OfferDescription = of.OfferDescription;
                }
                modifyJob.OfferLimitDate = of.OfferLimitDate;
                if (of.OfferImage != null)
                {
                    modifyJob.OfferImage = of.OfferImage;
                }
                modifyJob.OfferMajor = of.OfferMajor;
                modifyJob.OfferPosition = of.OfferPosition;
                modifyJob.OfferSalary = of.OfferSalary;
                modifyJob.Sex = of.Sex;
                modifyJob.LearningLevelRequest = of.LearningLevelRequest;
                modifyJob.ExperienceRequest = of.ExperienceRequest;
                modifyJob.Amount = of.Amount;
                modifyJob.Area = of.Area;
                modifyJob.JobAddress = of.JobAddress;
                modifyJob.ContactEmail = of.ContactEmail;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteOffer(Guid offerID)
        {
            try
            {
                var delOffer = db.OfferJobs.Find(offerID);
                db.OfferJobs.Remove(delOffer);
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
