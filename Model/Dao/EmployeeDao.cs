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
        public List<Employee> ListEmployees()
        {
            try
            {
                return db.Employees.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Employee FindById(Guid id)
        {
            try
            {
                return db.Employees.Find(id);
            }
            catch
            {
                return null;
            }
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
        public bool Delete(Guid id)
        {
            try
            {
                var employee = db.Employees.Find(id);
                db.Employees.Remove(employee);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
