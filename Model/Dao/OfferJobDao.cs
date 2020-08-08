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

        public List<OfferJob> ReturnFilterList(String OfferName = "0", int Area = 0, String JobAddress = "0", int OfferSalary = 0, int PositionJobID = 0, String Sex = "0", int ExperienceRequest = 0, int LearningLevelRequest = 0, DateTime OfferCreateDate = new DateTime())
        {
            try
            {
                var listOfferJob = ListOfferJob();
                var listOfferJobMajor = new OfferJobMajorDao().ReturnList();

                var result = (from Job in listOfferJob
                              join JobMajor in listOfferJobMajor on Job.OfferID equals JobMajor.OfferID
                              where (Area == 0 || Job.Area == Area)
                              where (OfferName == "0" || Job.OfferName.Contains(OfferName))
                              where (JobAddress == "0" || Job.JobAddress == JobAddress)
                              where (OfferSalary == 0 || Job.OfferSalary == OfferSalary)
                              where (PositionJobID == 0 || Job.PositionJobID == PositionJobID)
                              where (Sex == "0" || Job.Sex == Sex)
                              where (ExperienceRequest == 0 || Job.ExperienceRequest == ExperienceRequest)
                              where (LearningLevelRequest == 0 || Job.LearningLevelRequest == LearningLevelRequest)
                              //date posted
                              select new
                              {
                                  OfferID = Job.OfferID,
                                  OfferName = Job.OfferName,
                                  EnterpriseID = Job.EnterpriseID,
                                  EnterpriseName = db.Enterprises.Find(Job.EnterpriseID).EnterpriseName,
                                  JobAddress = Job.JobAddress,
                                  Amount = db.Salaries.Find(Job.OfferSalary).Amount,
                                  OfferLimitDate = Job.OfferLimitDate,
                                  Bonus = Job.Bonus,
                                  OfferImage = Job.OfferImage,
                                  
                                
                              });

                List<CandidateInfo> finalResult = result.ToList();
                int n = finalResult.Count;
                if (n == 0 || n == 1) return finalResult;

                List<CandidateInfo> finalResult2 = new List<CandidateInfo>();

                for (int i = 0; i < n; i++)
                {
                    bool check = true;
                    for (int j = 0; j < finalResult2.Count; j++)
                    {
                        if (finalResult[i].UserId == finalResult2[j].UserId)
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
            catch (exception e)
            {
                return null;
            }

        }

    }
}
