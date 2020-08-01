using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class JobMajorDao
    {
        CareerWeb db = null;
        public JobMajorDao()
        {
            db = new CareerWeb();
        }
        public List<JobMajor> ListJobMain()
        {
            return db.JobMajors.Where(x => x.JobIDParent == null).ToList();
        }

        public List<JobMajor> ListJobSub()
        {
            return db.JobMajors.Where(x => x.JobIDParent != null).ToList();
        }

        public List<JobMajor> ListJob()
        {
            return db.JobMajors.ToList();
        }

        public int Insert(JobMajor ins)
        {
            try
            {
                db.JobMajors.Add(ins);
                db.SaveChanges();
                return ins.JobID;
            }
            catch(Exception e)
            {
                return -1;
            }
        }
        public bool ConfirmJob(int jobID)
        {
            try
            {
                var job = db.JobMajors.Find(jobID);
                job.Status = true;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool DeleteJob(int jobID)
        {
            try
            {
                var job = db.JobMajors.Find(jobID);
                db.JobMajors.Remove(job);
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
