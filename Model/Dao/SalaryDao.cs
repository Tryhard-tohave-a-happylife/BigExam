using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SalaryDao
    {
        CareerWeb db = null;
        public SalaryDao()
        {
            db = new CareerWeb();
        }
        public List<Salary> ListSalary()
        {
            return db.Salaries.ToList();
        }
        public string NameSalary(int id)
        {
            return db.Salaries.Find(id).Amount;
        }
    }
}
