using Model.EF;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerWeb.Models
{
    public class ModelProject
    {
        public List<FullProjectInfor> ListFull { set; get; }
        public List<Project> ListUserJoin { set; get; }
        public List<Project> ListUserCreate { set; get; }
    }
}