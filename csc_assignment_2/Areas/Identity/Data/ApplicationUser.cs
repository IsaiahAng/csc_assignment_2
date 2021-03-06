﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using csc_assignment_2.Models;
using Microsoft.AspNetCore.Identity;

namespace csc_assignment_2.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName="nvarchar(100)")]
        public string FirstName { get; set; }
        [PersonalData]
        [Column(TypeName="nvarchar(100)")]
        public string LastName { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string SubPlan { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastPaid { get; set; }
        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}

    }
}
