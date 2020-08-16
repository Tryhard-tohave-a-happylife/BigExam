using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProjectDao
    {
        CareerWeb db = null;
        public ProjectDao()
        {
            db = new CareerWeb();
        }
        public List<FullProjectInfor> FindBySearch(int major, string title, List<int> skill, bool space)
        {
            try
            {
                var model = (from a in db.Projects
                             join b in db.ProjectSkills
                             on a.ProjectID equals b.ProjectID
                             where (a.ProjectMajor == major || major == -1)
                             && (a.Title.ToLower().Contains(title.ToLower()) || title == "")
                             && (skill.Count == 0 || skill.Contains(b.ProjectSkill1))
                             && ((a.Amount - a.Apply) != 0 || !space)
                             select new
                             {
                                 ProjectID = a.ProjectID,
                                 ProjectMajor = a.ProjectMajor,
                                 Title = a.Title,
                                 Description = a.Description,
                                 Amount = a.Amount,
                                 Apply = a.Apply.Value,
                                 CreateDate = a.CreateDate.Value,
                                 MasterID = a.MasterID,
                                 ListSkill = db.ProjectSkills.Where(x => x.ProjectID == a.ProjectID).Select(x => x.ProjectSkill1).ToList()
                             }).AsEnumerable().Select(x => new FullProjectInfor()
                             {
                                 ProjectID = x.ProjectID,
                                 ProjectMajor = x.ProjectMajor,
                                 Title = x.Title,
                                 Description = x.Description,
                                 Amount = x.Amount,
                                 Apply = x.Apply,
                                 CreateDate = x.CreateDate,
                                 MasterID = x.MasterID,
                                 ListSkill = x.ListSkill
                             });
                return model.Distinct().ToList();
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public List<Project> ListByUserCreate(Guid userID)
        {
            return db.Projects.Where(x => x.MasterID == userID).OrderByDescending(x => x.CreateDate).ToList();
        }
        public List<Project> ListByUserJoin(Guid userID)
        {
            try
            {
                var model = (from a in db.Projects
                             join b in db.ProjectMembers
                             on a.ProjectID equals b.ProjectID
                             where (b.MemberID == userID && b.Status != "request")
                             select new
                             {
                                 ProjectID = a.ProjectID,
                                 ProjectMajor = a.ProjectMajor,
                                 Title = a.Title,
                                 Description = a.Description,
                                 Amount = a.Amount,
                                 Apply = a.Apply.Value,
                                 CreateDate = a.CreateDate.Value,

                             }).AsEnumerable().Select(x => new Project()
                             {
                                 ProjectID = x.ProjectID,
                                 ProjectMajor = x.ProjectMajor,
                                 Title = x.Title,
                                 Description = x.Description,
                                 Amount = x.Amount,
                                 Apply = x.Apply,
                                 CreateDate = x.CreateDate
                             });
                return model.OrderByDescending(x => x.CreateDate).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int Insert(Project pr)
        {
            try
            {
                db.Projects.Add(pr);
                db.SaveChanges();
                return pr.ProjectID;
            }
            catch(Exception e)
            {
                return -1;
            }
        }
        public bool Edit(Project pr)
        {
            try
            {
                var edit = db.Projects.Find(pr.ProjectID);
                edit.ProjectMajor = pr.ProjectMajor;
                edit.Title = pr.Title;
                edit.Description = pr.Description;
                edit.Amount = pr.Amount;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool Delete(int projectID)
        {
            try
            {
                var remove = db.Projects.Find(projectID);
                var checkFirst = new ProjectMemberDao().DeleteByProject(projectID);
                var checkSecond = new ProjectSkillDao().DeleteByProject(projectID);
                db.Projects.Remove(remove);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public Project FindByID(int projectID)
        {
            try
            {
                return db.Projects.Find(projectID);
            }
            catch
            {
                return null;
            }
        }
    }
}
