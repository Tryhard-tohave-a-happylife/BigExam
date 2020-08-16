using Model.Models;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;

namespace Model.Dao
{
    public class EnterpriseDao
    {
        CareerWeb db = null;
        public EnterpriseDao()
        {
            db = new CareerWeb();
        }
        public List<Enterprise> ListEnterprises()
        {
            try
            {
                return db.Enterprises.ToList();
            }
            catch
            {
                return null;
            }
        }
        public bool Insert(Enterprise ins)
        {
            try
            {
                db.Enterprises.Add(ins);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public Enterprise FindById(Guid id)
        {
            try
            {
                return db.Enterprises.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Enterprise AddCode(Guid id, string code)
        {
            try
            {
                var ent = db.Enterprises.Find(id);
                ent.Code = code;
                db.SaveChanges();
                return ent;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Enterprise> ListResponse()
        {
            try
            {
                return db.Enterprises.Where(x => x.Status == false).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool RemoveEnterprise(Guid id)
        {
            try
            {
                var rm = db.Enterprises.Find(id);
                db.Enterprises.Remove(rm);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool ChangeStatus(Guid id)
        {
            try
            {
                var cg = db.Enterprises.Find(id);
                cg.Status = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<Enterprise> ReturnList()
        {
            try
            {
                return db.Enterprises.ToList();
            }
            catch
            {
                return null;
            }
        }
        public string EnterpriseName(Guid id)
        {
            return db.Enterprises.Find(id).EnterpriseName;
        }

        public List<FormEnterpriseFull> ReturnFilterList(string EName = "0", int EArea = 0, int ECareer = 0, int ESize = 0)
        {
            try
            {
                var listEnterprise = new EnterpriseDao().ReturnList();
                var listArea = new EnterpriseAreaDao().ListEnterpriseArea();
                var listJob = new EnterpriseJobDao().ListEnterpriseJob();
                var result = (from listE in listEnterprise
                              join listA in listArea on listE.EnterpriseID equals listA.EnterpriseId
                              join listJ in listJob on listE.EnterpriseID equals listJ.EnterpriseID
                              where (EArea == 0 || listA.AreaID == EArea)
                              && (ECareer == 0 || listJ.JobId == ECareer)
                              && (ESize == 0 || listE.EnterpriseSize == ESize)
                              && (EName == "0" || listE.EnterpriseName.Contains(EName))
                              && listE.Status == true

                              select new
                              {
                                  EnterpriseID = listE.EnterpriseID,
                                  EnterpriseName = listE.EnterpriseName,
                                  ImageLogo = listE.ImageLogo,
                                  NameArea = db.Areas.Find(listA.AreaID).NameArea,
                                  listJobId = db.EnterpriseJobs.Where(x => x.EnterpriseID == listE.EnterpriseID && x.JobIdParent == null).Select(x => x.JobId).ToList(),

                              }).AsEnumerable().Select(x => new FormEnterpriseFull()
                              {
                                  EnterpriseID = x.EnterpriseID,
                                  EnterpriseName = x.EnterpriseName,
                                  ImageLogo = x.ImageLogo,
                                  NameArea = x.NameArea,
                                  listJobId = x.listJobId
                              });

                var finalResult = result.ToList();
                int n = finalResult.Count;
                if (n == 0 || n == 1) return finalResult;

                var finalResult2 = new List<FormEnterpriseFull>();

                for (int i = 0; i < n; i++)
                {
                    bool check = true;
                    for (int j = 0; j < finalResult2.Count; j++)
                    {
                        if (finalResult[i].EnterpriseID == finalResult2[j].EnterpriseID)
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

        public List<ShowFullEnterprise> ShowEnterprise(Guid EnterpriseID)
        {
            try
            {
                var listEnterprise = new EnterpriseDao().ReturnList();
                var listArea = new EnterpriseAreaDao().ListEnterpriseArea();
                var result = (from enpr in listEnterprise
                              join listA in listArea on enpr.EnterpriseID equals listA.EnterpriseId
                              where enpr.EnterpriseID == EnterpriseID
                              select new
                              {
                                  EnterpriseID = enpr.EnterpriseID,
                                  EnterpriseName = enpr.EnterpriseName,
                                  ImageLogo = enpr.ImageLogo,
                                  AmountSize = db.EnterpriseSizes.Find(enpr.EnterpriseSize).AmountSize,
                                  NameOfEnterprise = db.TypeOfEnterprises.Find(enpr.TypeOfEnterprise).NameOfEnterprise,
                                  EstablishYear = enpr.EstablishYear,
                                  NameArea = db.Areas.Find(listA.AreaID).NameArea,
                                  listJobId = db.EnterpriseJobs.Where(x => x.EnterpriseID == enpr.EnterpriseID && x.JobIdParent == null).Select(x => x.JobId).ToList(),
                                  Description = enpr.Description,
                              }).AsEnumerable().Select(x => new ShowFullEnterprise()
                              {
                                  EnterpriseID = x.EnterpriseID,
                                  EnterpriseName = x.EnterpriseName,
                                  ImageLogo = x.ImageLogo,
                                  AmountSize = x.AmountSize,
                                  NameArea = x.NameArea,
                                  NameOfEnterprise = x.NameOfEnterprise,
                                  EstablishYear = x.EstablishYear,
                                  listJobId = x.listJobId,
                                  Description = x.Description
                              });
                var finalResult = result.ToList();
                int n = finalResult.Count;
                if (n == 0 || n == 1) return finalResult;

                var finalResult2 = new List<ShowFullEnterprise>();

                for (int i = 0; i < n; i++)
                {
                    bool check = true;
                    for (int j = 0; j < finalResult2.Count; j++)
                    {
                        if (finalResult[i].EnterpriseID == finalResult2[j].EnterpriseID)
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
        public List<InfoEnterprise> ShowFullInfo(Guid id)
        {
            var listEnterprise = new EnterpriseDao().ReturnList();
            var listArea = new EnterpriseAreaDao().ListEnterpriseArea();
            var result = (from enpr in listEnterprise
                          join listA in listArea on enpr.EnterpriseID equals listA.EnterpriseId
                          where enpr.EnterpriseID == id
                          select new
                          {
                              EnterpriseID = enpr.EnterpriseID,
                              EnterpriseName = enpr.EnterpriseName,
                              ImageLogo = enpr.ImageLogo,
                              AmountSize = db.EnterpriseSizes.Find(enpr.EnterpriseSize).AmountSize,
                              NameOfEnterprise = db.TypeOfEnterprises.Find(enpr.TypeOfEnterprise).NameOfEnterprise,
                              EstablishYear = enpr.EstablishYear,
                              Description = enpr.Description,
                              Email = enpr.Email,
                              Mobile = enpr.Mobile,
                          }).AsEnumerable().Select(x => new InfoEnterprise()
                          {
                              EnterpriseID = x.EnterpriseID,
                              EnterpriseName = x.EnterpriseName,
                              ImageLogo = x.ImageLogo,
                              AmountSize = x.AmountSize,
                              NameOfEnterprise = x.NameOfEnterprise,
                              EstablishYear = x.EstablishYear,
                              Description = x.Description,
                              Email = x.Email,
                              Mobile = x.Mobile,
                          });
            var finalResult = result.ToList();
            int n = finalResult.Count;
            if (n == 0 || n == 1) return finalResult;

            var finalResult2 = new List<InfoEnterprise>();

            for (int i = 0; i < n; i++)
            {
                bool check = true;
                for (int j = 0; j < finalResult2.Count; j++)
                {
                    if (finalResult[i].EnterpriseID == finalResult2[j].EnterpriseID)
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
        public bool FuncChangeContactInfo(Guid id, string email, string mobile)
        {
            try
            {
                var ent = db.Enterprises.Find(id);
                ent.Email = email;
                ent.Mobile = mobile;
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool FuncChangeCompanyInfo(Guid id, int type, int size , int establishYear, string description)
        {
            try
            {
                var ent = db.Enterprises.Find(id);
                ent.TypeOfEnterprise = type;
                ent.EnterpriseSize = size;
                ent.EstablishYear = establishYear;
                ent.Description = description;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
            public bool FuncChangeCompanyLogo(Guid id, string srcImage)
        {
            try
            {
                var ent = db.Enterprises.Find(id);
                ent.ImageLogo = srcImage;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
