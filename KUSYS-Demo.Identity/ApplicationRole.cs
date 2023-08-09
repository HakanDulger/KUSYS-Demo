using System;
using Microsoft.AspNetCore.Identity;

namespace KUSYS_Demo.Identity
{
	public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}

