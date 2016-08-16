//using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
//using System.Net;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Kramatdjati.Infrastructure;
using Kramatdjati.Models;
//using Microsoft.AspNet.Identity;
using System.Threading.Tasks;


namespace Kramatdjati.Infrastructure
{
    public static class LocationClaimsProvider
    {

        public static IEnumerable<Claim> GetClaims(ClaimsIdentity user)
        {
            List<Claim> claims = new List<Claim>();
            if (user.Name.ToLower() == "alice")
            {
                claims.Add(CreateClaim(ClaimTypes.PostalCode, "DC 20500"));
                claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "DC"));
            }
            else
            {
                claims.Add(CreateClaim(ClaimTypes.PostalCode, "NY 10036"));
                claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "NY"));
                ;
            }
            return claims;
        }

        public static IEnumerable<Claim> CustomClaims(ClaimsIdentity user)
        {
            List<Claim> claims = new List<Claim>();

            AppIdentityDbContext db = new AppIdentityDbContext();

            string idUser = db.Users.Where(x => x.UserName == user.Name).FirstOrDefault().Id;
            var appRole = db.Roles.ToList ();

            foreach (var ur in appRole)
            {
                int intProses = 0;
                foreach (var usr in ur.Users)
                {
                    if (usr.UserId == idUser)
                    {
                        intProses = 1;
                        break;
                    }
                    intProses = 0;
                }

                if (intProses == 1)
                {
                    var Role = db.AppRoleForms.Where(x => x.AppRoleID == ur.Id);
                    foreach (var mf in Role.Include(u => u.MasterForm))
                    {
                        claims.Add(CreateClaim("Form", mf.MasterForm.KodeForm));
                    }
                }
            }

            return claims;
        }

        private static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String, "LocalClaims");
        }

    }
}