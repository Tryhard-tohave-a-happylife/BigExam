using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class EnterpriseDao
    {
        CareerWeb db = null;
        public EnterpriseDao()
        {
            db = new CareerWeb();
        }
        public bool Insert(Enterprise ins)
        {
            try
            {
                db.Enterprises.Add(ins);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public Enterprise FindById(Guid id)
        {
            try
            {
                return db.Enterprises.Find(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Enterprise AddCode(Guid id, string code)
        {
            try
            {
                var ent = db.Enterprises.Find(id);
                ent.Code = code;
                db.SaveChanges();
                return ent;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Enterprise> ListResponse()
        {
            try
            {
                return db.Enterprises.Where(x => x.Status == false).ToList();
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public bool RemoveEnterprise(Guid id)
        {
            try
            {
                var rm = db.Enterprises.Find(id);
                db.Enterprises.Remove(rm);
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
            try
            {
                var cg = db.Enterprises.Find(id);
                cg.Status = true;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<Enterprise> ReturnList()
        {
            try
            {
                return db.Enterprises.Where(x => x.Status == true).ToList();
            } catch
            {
                return null;
            }
        }
    }
}
