using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OfferJobSkillDao
    {
        CareerWeb db = null;
        public OfferJobSkillDao()
        {
            db = new CareerWeb();
        }

        public List<OfferJobSkill> ReturnList()
        {
            return db.OfferJobSkills.ToList();
        }

        public bool InsertJobMajor(OfferJobSkill JobMajor)
        {
            try
            {
                db.OfferJobSkills.Add(JobMajor);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
