using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
