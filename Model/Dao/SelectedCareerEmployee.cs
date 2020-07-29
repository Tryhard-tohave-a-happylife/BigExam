using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SelectedCareerDao
    {
        CareerWeb db = null;
        public SelectedCareerDao()
        {
            db = new CareerWeb();
        }
        public List<> ReturnList()
        {
        return db.SelectedCareers.ToList();
    }
}
}
