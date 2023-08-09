using System;
using System.Security.Claims;
using KUSYS_Demo.Data.Abstract;
using KUSYS_Demo.Entity;
using KUSYS_Demo.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using static KUSYS_Demo.Identity.ApplicationUser;

namespace KUSYS_Demo.Data.Utilities
{
	public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;
        private IUnitOfWork unitOfWork;
        public MyUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> _userManager,
            RoleManager<ApplicationRole> _roleManager,
            IUnitOfWork _unitOfWork,
            IOptions<IdentityOptions> optionsAccessor)
            : base(_userManager, _roleManager, optionsAccessor)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            unitOfWork = _unitOfWork;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {

            IEnumerable<Claim> userClaims = await userManager.GetClaimsAsync(user);
            //await userManager.RemoveClaimsAsync(user, userClaims);

            if (userClaims.Where(i => i.Type == "IdentityId").FirstOrDefault() != null)
            {
                var claim = userClaims.FirstOrDefault(i => i.Type == "IdentityId");
                await UserManager.RemoveClaimAsync(user, claim);
            }


            if (userClaims.Where(i => i.Type == "Role").FirstOrDefault() != null)
            {
                var claim = userClaims.FirstOrDefault(i => i.Type == "Role");
                await UserManager.RemoveClaimAsync(user, claim);
            }
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("IdentityId", user.IdentityId.ToString()));

            var role = await userManager.GetRolesAsync(user);

            if (role.FirstOrDefault() == RoleType.User.Description())
            {
                //var user1 = unitOfWork.Users.GetAll()
                //    .Select(s => new Users()
                //    {
                //        IdentityId = s.IdentityId,
                //        FirstName = s.FirstName,
                //        LastName = s.LastName,
                //        UserId = s.UserId,
                //        UserName = s.UserName,
                //        EMail = s.EMail
                //    }).FirstOrDefault(i => i.IdentityId == user.IdentityId);
                
                identity.AddClaim(new Claim("FirstLastName", user.Name));

                identity.AddClaim(new Claim("EMail", user.Email));

                identity.AddClaim(new Claim("Username", user.UserName));
            }
            else
            {
                identity.AddClaim(new Claim("Username", user.UserName.Split('@')[0]));
            }
            
            return identity;
        }
    }
}

