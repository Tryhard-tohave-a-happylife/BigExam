using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AccountDao
    {
        CareerWeb db = null;
        public AccountDao()
        {
            db = new CareerWeb();
        }
        public bool checkEmailExsit(string email)
        {
            var count = db.Accounts.Count(x => x.AccountName == email);
            if(count > 0)
            {
                return true;
            }
            return false;
        }
        public Guid createAccount(string email, string pass, string typeOfAccount)
        {
            var newAccount = new Account();
            newAccount.AccountName = email;
            newAccount.AccountPassword = pass;
            newAccount.CreateDate = DateTime.Now;
            newAccount.UserId = Guid.NewGuid();
            newAccount.VisitFirstTime = true;
            newAccount.TypeOfAccount = typeOfAccount;
            if(typeOfAccount == "User" || typeOfAccount == "Employee")
            {
                newAccount.Status = "Complete";
            }
            else
            {
                newAccount.Status = "Request";
            }
            db.Accounts.Add(newAccount);
            db.SaveChanges();
            return newAccount.UserId;
        }
        public Account FindAccountByUserId(Guid userId)
        {
            try
            {
                return db.Accounts.SingleOrDefault(x => x.UserId == userId);
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public Account FindAccountById(int accountId)
        {
            try
            {
                return db.Accounts.Find(accountId);
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public bool RemoveAccountByUserID(Guid id)
        {
            try
            {
                var rmAcc = db.Accounts.SingleOrDefault(x => x.UserId == id);
                db.Accounts.Remove(rmAcc);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool ChangeStatus(Guid id)
        {
            try{
                var acc = db.Accounts.SingleOrDefault(x => x.UserId == id);
                acc.Status = "Complete";
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public int CheckLogin(string userName, string passWord, out int userId)
        {
            var acc = db.Accounts.SingleOrDefault(x => x.AccountName == userName);
            userId = -1;
            if (acc == null)
            {
                return -1;
            }
            if (acc.AccountPassword != passWord)
            {
                return -2;
            }
            userId = acc.AccountId;
            if (acc.TypeOfAccount == "User") return 1;
            if (acc.TypeOfAccount == "Enterprise") return 2;
            if (acc.TypeOfAccount == "Employee") return 3;
            return 4;
        }
        public string GetName(int accountId)
        {
            var acc = db.Accounts.Find(accountId);
            if (acc.TypeOfAccount == "User")
            {
                return db.Users.SingleOrDefault(x => x.UserId == acc.UserId).UserName;
            }
            if(acc.TypeOfAccount == "Employee")
            {
                return db.Employees.SingleOrDefault(x => x.EmployeeID == acc.UserId).EmployeeName;
            }
            return "";
        }
        public List<UserChat> GetUsersChatProject(int projectID, int accID)
        {
            var listUserInProject = new ProjectMemberDao().ListByProjectActive(projectID).Select(x => x.MemberID);
            var listConnect = db.UserConnections.Select(x => x.UserID).ToList();
            var result = (from a in db.Accounts
                          join b in db.Users
                          on a.UserId equals b.UserId
                          where (a.AccountId != accID && listUserInProject.Contains(b.UserId)) 
                          select new
                          {
                              accountID = a.AccountId,
                              userName = b.UserName,
                              Image = b.UserImage,
                              isOnline = listConnect.Contains(a.AccountId) == true
                          }).AsEnumerable().Select(x => new UserChat(){
                              accountID = x.accountID,
                              userName = x.userName,
                              Image = x.Image,
                              isOnline = x.isOnline
                          });
            return result.ToList();
        }
        public int AddUserConnection(Guid guid, int accID)
        {
            var userConnect = new UserConnection();
            userConnect.ConnectionID = guid;
            userConnect.UserID = accID;
            db.UserConnections.Add(userConnect);
            db.SaveChanges();
            return accID;
        }
        public int RemoveUserConnection(Guid guid)
        {
            var current = db.UserConnections.SingleOrDefault(x => x.ConnectionID == guid);
            var us = current.UserID;
            if(current != null)
            {
                db.UserConnections.Remove(current);
                db.SaveChanges();
                return us;
            }
            return 0;
        }
        public void RemoveAllUsersConnections(int accID)
        {
            var cr = db.UserConnections.Where(x => x.UserID == accID);
            db.UserConnections.RemoveRange(cr);
            db.SaveChanges();
        }
        public IList<string> GetUserConnection(int userID)
        {
            return db.UserConnections.Where(x => x.UserID == userID).Select(x => x.ConnectionID.ToString()).ToList();
        }
        public List<Message> GetChatBox(int toProjectID)
        {
            return db.Messages.Where(x => x.ToProject == toProjectID).ToList();
        }
        public List<int> UserRecieveMessage(int accID, int projectID)
        {
            var userID = db.Accounts.Find(accID);
            return db.ProjectMembers.Where(x => x.ProjectID == projectID && x.MemberID != userID.UserId).
                                     Select(x => db.Accounts.FirstOrDefault(y => y.UserId == x.MemberID).AccountId).ToList();
        }
        public List<UserChat> GetByListMes(List<Message> list)
        {
            var listRe = new List<UserChat>();
            foreach(var item in list)
            {
                var account = db.Accounts.Find(item.FromUser);
                var user = db.Users.Find(account.UserId);
                var ne = new UserChat();
                ne.accountID = item.FromUser;
                ne.userName = user.UserName;
                ne.Image = user.UserImage;
                listRe.Add(ne);
            }
            return listRe;
        }
        public UserChat FindUserChat(int accID)
        {
            var account = db.Accounts.Find(accID);
            var user = db.Users.Find(account.UserId);
            var ne = new UserChat();
            ne.accountID = accID;
            ne.userName = user.UserName;
            ne.Image = user.UserImage;
            return ne;
        }
    }
}
