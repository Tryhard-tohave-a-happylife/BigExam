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
        public List<OfferJob> ListOfferJob()
        {
            return db.OfferJobs.ToList();
        }

        public List<OfferJob> ReturnFilterList(string offerName = "0", int Area = 0,
            int OfferMajor = 0, int offerSalary = 0, int positionJobId = 0,
            string sex = "0", int experienceRequest = 0, int learningLevelRequest = 0)
        {
     
            try
            {
                var listOfferJob = new OfferJobDao().ListOfferJob();
                var listOfferJobMajor = new OfferJobSkillDao().ReturnList();
                var listEnterprise = new EnterpriseDao().ReturnList();
                var result = (from job in listOfferJob
                              join jobMajor in listOfferJobMajor on job.OfferID equals jobMajor.OfferID
                              join enterprise in listEnterprise on job.EnterpriseID equals enterprise.EnterpriseID
                              where (Area == 0 || job.Area == Area)
                              && (OfferMajor == 0 || job.OfferMajor == OfferMajor)
                              && (offerSalary == 0 || job.OfferSalary == offerSalary)
                              && (positionJobId == 0 || job.OfferPosition == positionJobId)
                              && (sex == "0" || job.Sex == sex)
                              && (experienceRequest == 0 || job.ExperienceRequest == experienceRequest)
                              && (learningLevelRequest == 0 || job.LearningLevelRequest == learningLevelRequest)
                              && (offerName == "0" || job.OfferName.Contains(offerName) || enterprise.EnterpriseName.Contains(offerName))


                              select new
                              {
                                  OfferID = job.OfferID,
                                  OfferName = job.OfferName,
                                  EnterpriseID = job.EnterpriseID,
                                  EnterpriseName = db.Enterprises.Find(job.EnterpriseID).EnterpriseName,
                                  JobAddress = job.JobAddress,
                                  OfferSalary = job.OfferSalary,
                                  Amount = db.Salaries.Find(job.OfferSalary).Amount,
                                  OfferLimitDate = job.OfferLimitDate,
                                  Bonus = job.Bonus,
                                  OfferImage = job.OfferImage,

                              }).AsEnumerable().Select(x => new OfferJob()
                              {
                                  OfferID = x.OfferID,
                                  OfferName = x.OfferName,
                                  EnterpriseID = x.EnterpriseID,
                                  JobAddress = x.JobAddress,
                                  OfferSalary = x.OfferSalary,
                                  OfferLimitDate = x.OfferLimitDate,
                                  Bonus = x.Bonus,
                                  OfferImage = x.OfferImage
                              })   ;

                List<OfferJob> finalResult = result.ToList();
                int n = finalResult.Count;
                if (n == 0 || n == 1) return finalResult;

                List<OfferJob> finalResult2 = new List<OfferJob>();

                for (int i = 0; i < n; i++)
                {
                    bool check = true;
                    for (int j = 0; j < finalResult2.Count; j++)
                    {
                        if (finalResult[i].OfferID == finalResult2[j].OfferID)
                        {
                            check = false;
                            break;
                        }
                    }
                    if (check == true)
                    {
                        finalResult2.Add(finalResult[i]);
                    }


                }
                return finalResult2;


            }
            catch (Exception e)
            {
                return null;
            }

        }

    }
}
