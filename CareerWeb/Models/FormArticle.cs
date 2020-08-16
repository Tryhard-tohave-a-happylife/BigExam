using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWeb.Models
{
    public class FormArticle
    {
        public string Title { set; get; }
        public int CategoryBig { set; get; }
        public int CategorySmall { set; get; }
        public string Description { set; get; }
        public HttpPostedFileBase Image { set; get; }
        public string ContentArticle { set; get; }
        public string typeAction { set; get; }
        public int saveID { set; get; }
    }
}