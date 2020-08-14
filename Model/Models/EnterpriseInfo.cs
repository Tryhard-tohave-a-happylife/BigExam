using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class EnterpriseInfo
    {
        public Guid EnterpriseID { get; set; }
        public string EnterpriseName { get; set; }
        public string ImageLogo { get; set; }

        public string AreaName { get; set; }

        public string Salary { get; set; }

        public string Bonus { get; set; }

    }
}
