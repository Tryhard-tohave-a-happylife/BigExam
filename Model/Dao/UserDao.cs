using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        CareerWeb db = null;
        public UserDao()
        {
            db = new CareerWeb();
        }
        public List<User> ListUsers()
        {
            try
            {
                return db.Users.ToList();
            }
            catch
            {
                return null;
            }
        }
        public bool InsertUser(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public User FindById(Guid userId)
        {
            try
            {
                return db.Users.Find(userId);
            }
            catch
            {
                return null;
            }
        }

        public String findDesiredJob(Guid userId)
        {
            try
            {
                return db.Users.Find(userId).DesiredJob;
            }
            catch
            {
                return "   ";
            }
        }
        public bool ModifyUser(Guid userId, ModifyUserForm user)
        {
            try
            {
                var userModify = db.Users.Find(userId);
                userModify.UserName = user.userName;
                userModify.UserBirthDay = DateTime.ParseExact(user.userDob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                userModify.UserEmail = user.userEmail;
                userModify.UserArea = user.userArea;
                userModify.UserMobile = user.userMobile;
                userModify.Sex = user.userGender;
                if(user.userAddress != null && user.userAddress != "")
                {
                    userModify.UserAddress = user.userAddress;
                }
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool UploadImage(Guid userId, string fileName)
        {
            try
            {
                var user = db.Users.Find(userId);
                user.UserImage = fileName;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public List<User> ListUser()
        {
            return db.Users.ToList();
        }

        public List<CandidateInfo> ListUserFit(String name, int areaID, int JobId)
        {

            var listUsers = ListUser();
            var listUserMajors = new UserMajorDao().ListUserMajor();
            var listJobs = new JobMajorDao().ListJobMain();

            var result = (from User in listUsers
                          join UserMajor in listUserMajors on User.UserId equals UserMajor.UserID
                          where (areaID == 0 || User.UserArea == areaID)
                          where (JobId == 0 || UserMajor.MajorID == JobId)
                          where (name == "0" || User.UserName.Contains(name))
                          select new
                          {
                              UserId = User.UserId,
                              UserName = User.UserName,
                              //UserImage = User.UserImage,
                              //UserExperience = User.UserExperience,
                              UserSalary = (User.Salary != null)?  User.Salary : 0,
                              UserArea = db.Areas.Find(User.UserArea).NameArea,
                              UserMajorName = (from UserMajor in listUserMajors
                                               join JobMajor in listJobs on UserMajor.MajorID equals JobMajor.JobID

                                               where (UserMajor.UserID == User.UserId)

                                               select JobMajor.JobName).ToList()

                          }).AsEnumerable().Select(x =>new CandidateInfo()
                          {
                              UserId = x.UserId,
                              UserName = x.UserName,
                              //UserImage = x.UserImage,
                              //UserExperience = x.UserExperience,
                              UserSalary = new SalaryDao().AmountSalary(x.UserSalary.Value),
                              UserArea = x.UserArea,
                              UserMajorName = x.UserMajorName
                          });

            List<CandidateInfo> finalResult = result.ToList();
            int n = finalResult.Count;
            if (n == 0 || n == 1) return finalResult;

            List<CandidateInfo> finalResult2 = new List<CandidateInfo>();

            for (int i = 0; i < n; i++)
            {
                bool check = true;
                for (int j = 0; j < finalResult2.Count; j++)
                {
                    if (finalResult[i].UserId == finalResult2[j].UserId)
                    {
                        check = false;
                        break;
                    }
                }
                if (check == true)
                {
                    finalResult2.Add(finalResult[i]);
                }


            }
            return finalResult2;


        }

       
    }
}
