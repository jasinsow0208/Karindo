using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kramatdjati.Models
{
    public class MasterRoleDetailView
    {
        public int MasterRoleID { get; set; }
        public string MasterRole { get; set; }
        public int MasterFormID { get; set; }
        public int? MasterRoleDetailID { get; set; }
    }

    public class UserRoleView
    {
        public int UserRoleID { get; set; }
        public int MasterRoleID { get; set; }
        public Guid UserID { get; set; }
        public string Nama { get; set; }

        public virtual MasterRole MasterRole { get; set; }
    }

    public class AppRoleFormsView
    {
        public int? AppRoleFormsID { get; set; }
        public string MasterRole { get; set; }
        public int MasterFormID { get; set; }
        public string AppRoleID { get; set; }
    }
}