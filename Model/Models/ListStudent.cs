using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ListStudent
    {
        public string UserName { set; get; }
        public DateTime UserBirthDay { get; set; }
        public string UserEmail { set; get; }
        public string UserMobile { set; get; }
        public string JobName { set; get; }
        public string NameArea { set; get; }
        public string LanguageLevel { set; get; }
        public List<string> listJob { set; get; }
    }
}
