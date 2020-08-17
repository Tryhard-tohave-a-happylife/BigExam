using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProjectMemberDao
    {
        CareerWeb db = null;
        public ProjectMemberDao()
        {
            db = new CareerWeb();
        }
        public bool Insert(ProjectMember pm)
        {
            try
            {
                db.ProjectMembers.Add(pm);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool DeleteByProject(int projectID)
        {
            try
            {
                var list = db.ProjectMembers.Where(x => x.ProjectID == projectID).ToList();
                foreach(var item in list)
                {
                    db.ProjectMembers.Remove(item);
                    db.SaveChanges();
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool DeleteByUser(int projectID, Guid userID)
        {
            try
            {
                var remove = db.ProjectMembers.Find(projectID, userID);
                if(remove.Status != "request")
                {
                    var project = db.Projects.Find(projectID);
                    project.Apply -= 1;
                }
                db.ProjectMembers.Remove(remove);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool Accept(int projectID, Guid userID)
        {
            try
            {
                var modify = db.ProjectMembers.Find(projectID, userID);
                modify.Status = "member";
                modify.CreateDate = DateTime.Now;
                var project = db.Projects.Find(projectID);
                project.Apply += 1;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<ProjectMember> ListByProject(int projectID)
        {
            try
            {
                return db.ProjectMembers.Where(x => x.ProjectID == projectID).OrderBy(x => x.CreateDate).ToList();
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public List<ProjectMember> ListByProjectActive(int projectID)
        {
            try
            {
                return db.ProjectMembers.Where(x => x.ProjectID == projectID && x.Status != "request").ToList();
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
