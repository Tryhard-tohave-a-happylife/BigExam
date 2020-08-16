using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class FullProjectInfor
    {
        public int ProjectID { set; get; }
        public int ProjectMajor { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public int Amount { set; get; }
        public int Apply { set; get;}
        public DateTime CreateDate { set; get; }
        public List<int> ListSkill { set; get; }
        public Guid MasterID { set; get; }
        public override bool Equals(object obj)
        {
            return ((FullProjectInfor)obj).ProjectID == ProjectID;
        }
        public override int GetHashCode()
        {
            return ProjectID.GetHashCode();
        }
    }
}
