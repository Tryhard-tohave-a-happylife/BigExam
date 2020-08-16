using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MessageDao
    {
        CareerWeb db = null;
        public MessageDao()
        {
            db = new CareerWeb();
        }
        public bool SendMessage(int accID, int projectID, string message)
        {
            try
            {
                var mes = new Message();
                mes.ToProject = projectID;
                mes.FromUser = accID;
                mes.Message1 = message;
                mes.Date = DateTime.Now;
                db.Messages.Add(mes);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
