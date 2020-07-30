using Model.EF;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CreateAccountInfor(Guid EnterpriseID, string EmployeeName, int Position,string Sex, string BirthDay, string Email, string Mobile, string Code)
        {
            var employeeInfor = new Employee();
            employeeInfor.EnterpriseID = EnterpriseID;
            employeeInfor.EmployeeName = EmployeeName;
            employeeInfor.Position = Position;
            employeeInfor.Sex = Sex;
            employeeInfor.Email = Email;
            employeeInfor.Mobile = Mobile;
            string[] splitDate = BirthDay.Split('-');
            employeeInfor.BirthDay = new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]));
            var ent = new EnterpriseDao().FindById(EnterpriseID);
            if(ent.Code == Code)
            {
               var checkInsertEmployee = new EmployeeDao().InsertEmployee(employeeInfor);
                if (checkInsertEmployee)
                {
                    return Json(new
                    {
                        status = true,
                        codeInput = true
                    });
                }
                return Json(new
                {
                    status = false,
                    codeInput = true
                });
            }
            return Json(new
            {
                codeInput = false
            });
        }
    }
}