using Model.EF;
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
    }
}
