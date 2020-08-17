using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class InfoEnterprise
    {
        public Guid EnterpriseID { get; set; }
        public string EnterpriseName { get; set; }
        public string ImageLogo { get; set; }
        public string AmountSize { get; set; }
        public string NameOfEnterprise { get; set; }
        public int EstablishYear { get; set; }
        public string Description { set; get; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
