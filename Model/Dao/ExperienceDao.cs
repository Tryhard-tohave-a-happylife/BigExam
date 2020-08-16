using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ExperienceDao
    {
        CareerWeb db = null;
        public ExperienceDao()
        {
            db = new CareerWeb();
        }
<<<<<<< HEAD
        public List<Experience> ListExperiences()
=======
        public List<Experience> ListExperience()
>>>>>>> 54301d1ff8356913c851621b294ef6681bd102d5
        {
            return db.Experiences.ToList();
        }
    }
}
