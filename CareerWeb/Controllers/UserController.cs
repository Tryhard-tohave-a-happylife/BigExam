using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult JobListInCompany()
        {
            return View();
        }
        public ActionResult SearchJobForUser()
        {
            return View();
        }
        public ActionResult SearchCompanyForUser()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CreateAccountInfor(Guid userID, string email, string name, string mobile, string dateBirth, string sex, string atSchool, int area, List<int> listJob)
        {
            var userInfor = new User();
            userInfor.UserId = userID;
            userInfor.UserName = name;
            userInfor.UserEmail = email;
            userInfor.UserMobile = mobile;
            userInfor.AtSchool = (atSchool == "in") ? true : false;
            userInfor.Sex = sex;
            userInfor.UserArea = area;
            string[] splitDate = dateBirth.Split('-');
            userInfor.UserBirthDay = new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]));
            userInfor.CompleteProfile = 1;
            var checkInsertUser = new UserDao().InsertUser(userInfor);
            var checkInsertUserMajor = true;
            foreach (int x in listJob)
            {
                var userMajor = new UserMajor();
                userMajor.UserID = userID;
                userMajor.MajorID = x;
                var ck = new UserMajorDao().InsertUserMajor(userMajor);
                if (!ck) checkInsertUserMajor = ck;
            }
            if (checkInsertUser && checkInsertUserMajor)
            {
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
    }
}