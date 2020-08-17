using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProjectSkillDao
    {
        CareerWeb db = null;
        public ProjectSkillDao()
        {
            db = new CareerWeb();
        }
        public bool Insert(ProjectSkill ps)
        {
            try
            {
                db.ProjectSkills.Add(ps);
                db.SaveChanges();
                return true;
            }
            catch(Exception e) 
            {
                return false;
            }
        }
        public List<ProjectSkill> ListByProject(int projectID)
        {
            try
            {
                return db.ProjectSkills.Where(x => x.ProjectID == projectID).ToList();
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public bool DeleteByProject(int projectID)
        {
            try
            {
                var list = db.ProjectSkills.Where(x => x.ProjectID == projectID).ToList();
                foreach(var item in list)
                {
                    db.ProjectSkills.Remove(item);
                    db.SaveChanges();
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool DeleteBySkill(int projectID, int skill)
        {
            try
            {
                var remove = db.ProjectSkills.Find(projectID, skill);
                db.ProjectSkills.Remove(remove);
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
