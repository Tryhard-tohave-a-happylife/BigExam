using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWeb.Models
{
    public class FormProject
    {
        public int saveID { set; get; }
        public string typeAction { set; get; }
        public string title { set; get; }
        public int major { set; get; }
        public int amount { set; get; }
        public string description { set; get; }
        public List<int> skill { set; get; }
    }
}