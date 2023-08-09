using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace KUSYS_Demo.Identity
{
	public class ApplicationUser : IdentityUser
    {
        public Guid IdentityId { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }//User First-Last Name
        public enum RoleType : short
        {

            [Description("Admin")]
            [Display(Name = "Admin")]
            Admin = 0,
            [Description("User")]
            [Display(Name = "User")]
            User = 1,
            
        }
    }
}

