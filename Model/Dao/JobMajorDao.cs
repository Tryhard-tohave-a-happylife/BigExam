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
    }
}
