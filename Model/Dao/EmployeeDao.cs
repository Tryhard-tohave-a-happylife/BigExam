using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class EmployeeDao
    {
        CareerWeb db = null;
        public EmployeeDao()
        {
            db = new CareerWeb();
        }
        public bool InsertEmployee(Employee employee)
        {
            try
            {
                db.Employees.Add(employee);
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
