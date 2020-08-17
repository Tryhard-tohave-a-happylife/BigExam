using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class UserChat
    {
        public int accountID { set; get; }
        public string userName { set; get; }
        public string Image { set; get; }
        public bool isOnline { set; get; }
    }
}
