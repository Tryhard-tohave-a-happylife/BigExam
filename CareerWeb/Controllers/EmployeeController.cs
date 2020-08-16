using Model.Dao;
using Model.EF;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace CareerWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private object db;

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchCandidate()
        {
            ViewBag.JobListMain = new JobMajorDao().ListJobMain();
            ViewBag.JobListSub = new JobMajorDao().ListJobSub();
            ViewBag.AreaList = new AreaDao().ListArea();
            return View();
        
        }

        public ActionResult SearchCandidateResult(int? page, String Name = "0", String AreaID = "0", String JobID = "0")
        {
            ViewBag.JobListMain = new JobMajorDao().ListJobMain();
            ViewBag.AreaList = new AreaDao().ListArea();
            int areaId = int.Parse(AreaID); 
            int jobId = int.Parse(JobID);


            // var Model = new UserDao().ListUserFit(Name, areaId, jobId).ToPagedList(page ?? 1, 2);
            // return View(Model);
            return View(); //để tạm v cho đỡ lỗi đã, bài t cũng bị

        }

        public ActionResult JobApplicationsUser()
        {
            /*  var accID = int.Parse(User.Identity.Name);
                var acc = new AccountDao().FindAccountById(accID);
                var employee = new EmployeeDao().FindById(acc.UserId);
                var enterpriseId = employee.EnterpriseID;   */

            Guid enterpriseId = new Guid("ed4a47ea-f261-491e-aff4-6e29d36ece42");

            ViewBag.AppliedCandidate = new AppliedCandidateDao().AppliedCandidateList(enterpriseId);
            ViewBag.SavedCandidate = new SavedCandidateDao().ListCandidateSave(enterpriseId);
            


            return View();
        }
        public ActionResult ShowDetailCandidate()
        {
            return View(); 
        }

        public ActionResult Interview()
        {
            Guid userid = new Guid("ed4171f6-485b-4279-9a89-ab74678833f1");
            Guid offerid = new Guid("251e41e2-3a8b-499b-8550-84db430d8bad");
            
            ViewBag.CandidateDetail = new UserDao().FindById(userid);
            ViewBag.OfferDetail = new OfferJobDao().findById(offerid);
            ViewBag.CandidateApplied = new AppliedCandidateDao().findCandidate(userid, offerid);
            ViewBag.WorkInvitation = new WorkInvitationDao().findWorkInvitation(userid, offerid);
            return View();
        }

        [HttpPost]
        public JsonResult SaveCandidate(Guid userId)
        {
            /*  var accID = int.Parse(User.Identity.Name);
                var acc = new AccountDao().FindAccountById(accID);
                var employee = new EmployeeDao().FindById(acc.UserId);
                var enterpriseId = employee.EnterpriseID;   */

            var enterpriseId = new Guid("ed4a47ea-f261-491e-aff4-6e29d36ece42");

            var savedCandidate = new SavedCandidate();
            DateTime date = DateTime.Today;
            savedCandidate.UserID = userId;
            savedCandidate.EnterpriseID = enterpriseId;
            savedCandidate.CreateDate = date.ToString("dd/MM/yyyy");
            var checkInsertCandidate = new SavedCandidateDao().InsertCandidate(savedCandidate);
            if (checkInsertCandidate == false)
            {
                return Json(new
                {
                    status = false
                });
            }
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult checkSavedCandidate(List<Guid> userId)
        {
            /*  var accID = int.Parse(User.Identity.Name);
                            var acc = new AccountDao().FindAccountById(accID);
                            var employee = new EmployeeDao().FindById(acc.UserId);
                            var enterpriseId = employee.EnterpriseID;   */

            var enterpriseId = new Guid("ed4a47ea-f261-491e-aff4-6e29d36ece42");

            List<Boolean> save = new List<bool>();
            foreach (var item in userId)
            {
                var user = new UserDao().FindById(item);
                if (new SavedCandidateDao().findCandidate(user.UserId,enterpriseId) != null)
                {
                    var check = true;
                    save.Add(check);
                }
                else
                {
                    var check = false;
                    save.Add(check);
                }
            }
            return Json(new
            {
                savedList = save
            });
        }

        [HttpPost] 
        public JsonResult DeleteSavedCandidate(Guid userId)
        {
            /*  var accID = int.Parse(User.Identity.Name);
                var acc = new AccountDao().FindAccountById(accID);
                var employee = new EmployeeDao().FindById(acc.UserId);
                var enterpriseId = employee.EnterpriseID;   */

            var enterpriseId = new Guid("ed4a47ea-f261-491e-aff4-6e29d36ece42");

            var savedCandidate = new SavedCandidateDao().findCandidate(userId, enterpriseId);
            var checkDeleteCandidate = new SavedCandidateDao().DeleteCandidate(savedCandidate);
            if (checkDeleteCandidate == false)
            {
                return Json(new
                {
                    status = false
                });
            }
            return Json(new
            {
                status = true
            });
        }



        [HttpPost]
       public JsonResult InterviewData(Guid userId, Guid offerId, Guid employeeId, String date, String time, String address, String note)
        {
            var interview = new Interview();
            interview.Date = date;
            interview.Time = time;
            interview.Address = address;
            interview.Note = note;
            interview.UserID = userId;
            interview.OfferID = offerId;
            interview.EmployeeID = employeeId;
            interview.Status = "waiting";
            var checkInsertInterview = new InterviewDao().InsertInterview(interview);
            if (checkInsertInterview == false)
            {
                return Json(new
                {
                    status = false
                }) ; 
            }
            var checkUpdate = new AppliedCandidateDao().UpdateStatus(userId, offerId, "Phỏng vấn");
            return Json(new
            {
                status = true
            });
        }




        [HttpPost]
        public JsonResult checkStatusInterview(Guid userId, Guid offerId)
        {
                var interview = new InterviewDao().findInterview(userId, offerId);
            if (interview != null) { 
                var user = new UserDao().FindById(userId);
                var employee = new EmployeeDao().FindById(interview.EmployeeID);
                var offer = new OfferJobDao().findById(offerId);
                return Json(new
                {
                    having = true,
                    User = user.UserName,
                    InterviewID = interview.InterviewID,
                    Offer = offer.OfferName,
                    Time = interview.Time,
                    Date = interview.Date,
                    Status = interview.Status,
                    Employee = employee.EmployeeName,
                    Address = interview.Address,
                    Note = interview.Note
                }); ;
            }
                return Json(new
                {
                    having = false
                });
            
        }

        [HttpPost]
        public JsonResult WorkInvitationData(Guid userId, Guid offerId, String date, String salary, String address, String note)
        {
            var workinvitation = new WorkInvitation();
            workinvitation.UserID = userId;
            workinvitation.OfferID = offerId;
            workinvitation.StartDay = date;
            workinvitation.Salary = salary;
            workinvitation.Address = address;
            workinvitation.Note = note;
            workinvitation.Status = "waiting";
            var checkInsertWorkInvitation = new WorkInvitationDao().InsertWorkInvitation(workinvitation);
            if (checkInsertWorkInvitation == false)
            {
                return Json(new
                {
                    status = false
                });
            }
            var checkUpdate = new AppliedCandidateDao().UpdateStatus(userId, offerId, "Mời làm");
            var checkInterviewUpdate = new InterviewDao().UpdateStatus(userId, offerId, "done");
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult checkStatusWorkInvitation(Guid userId, Guid offerId)
        {
            var workinvitation = new WorkInvitationDao().findWorkInvitation(userId, offerId);
            if (workinvitation != null)
            {
                var user = new UserDao().FindById(userId);
                var offer = new OfferJobDao().findById(offerId);
                return Json(new
                {
                    having = true,
                    User = user.UserName,
                    WorkInvitation = workinvitation.WorkInvitationID,
                    Offer = offer.OfferName,
                    StartDay = workinvitation.StartDay,
                    Salary = workinvitation.Salary,
                    Address = workinvitation.Address,
                    Note = workinvitation.Note,
                    Status = workinvitation.Status
                }); ;
            }
            return Json(new
            {
                having = false
            });

        }


        [HttpPost]
        public JsonResult CreateAccountInfor(Guid EnterpriseID, string EmployeeName, int Position,string Sex, string BirthDay, string Email, string Mobile, string Code)
        {
            var employeeInfor = new Employee();
            employeeInfor.EnterpriseID = EnterpriseID;
            employeeInfor.EmployeeName = EmployeeName;
            employeeInfor.Position = Position;
            //employeeInfor.Sex = Sex;
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