using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Kramatdjati.Models
{
    public class MasterForm
    {
        public int MasterFormID { get; set; }

        [Required]
        [Display(Name="Kode")]
        public string KodeForm { get; set; }

        [Required]
        [Display(Name="Nama Form")]
        public string NamaForm { get; set; }
        public string Keterangan { get; set; }

        public virtual List<MasterRoleDetail> MasterRoleDetails { get; set; }
    }

    public class MasterRole
    {
        public int MasterRoleID { get; set; }

        [Required]
        [Display(Name="Role")]
        public string NamaRole { get; set; }

        public virtual List<MasterRoleDetail> MasterRoleDetails { get; set; }
        public virtual List<UserRole> UserRoles { get; set; }
    }

    public class MasterRoleDetail
    {
        public int MasterRoleDetailID { get; set; }

        public int MasterRoleID { get; set; }
        public int MasterFormID { get; set; }

        public virtual MasterRole MasterRole { get; set; }
        public virtual MasterForm MasterForm { get; set; }
    }

    public class UserRole
    {
        public int UserRoleID { get; set; }
        public int MasterRoleID { get; set; }
        public Guid  UserID { get; set; }

        public virtual MasterRole MasterRole { get; set; }
    }

    public class AppRoleForms
    {
        public int AppRoleFormsID { get; set; }

        public string AppRoleID { get; set; }
        public int MasterFormID { get; set; }

        public virtual AppRole  AppRole { get; set; }
        public virtual MasterForm MasterForm { get; set; }
    }
}