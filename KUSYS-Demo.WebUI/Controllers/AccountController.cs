using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KUSYS_Demo.Data.Abstract;
using KUSYS_Demo.Identity;
using KUSYS_Demo.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KUSYS_Demo.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private IUnitOfWork unitOfWork;

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, IUnitOfWork _unitOfWork)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //throw new Exception("Yapılan bakım çalışması nedeniyle. Geçici olarak hizmet verilemiyor.");
            if (ModelState.IsValid)
            {
                /*string[] k = new string[] { "34000991", "34001479", "34000744", "34000783","3400tyilmaz", "alikemal", "ahmetaktan", "muratdikmen", "ozcangobekli", "eraygokdemir" };
                if (!k.Contains(model.UserName))
                {
                    throw new Exception("Yapılan bakım çalışması nedeniyle. Geçici olarak hizmet verilemiyor.");
                }
                if (model.UserName.StartsWith("3400"))
                {
                    //  test  firmaları
                    string[] firmalar = new string[] { "34000991", "34001479", "34000744", "34000783" };
                    if (!firmalar.Contains(model.UserName))
                    {
                        throw new Exception("Yapılan bakım çalışması nedeniyle. Geçici olarak hizmet verilemiyor.");
                    }
                }
                if (model.UserName.StartsWith("3400"))
                {
                    //  test  firmaları
                    string[] firmalar = new string[] {"3400ysaygili", "34000351", "34000000", "34000991", "34001479", "34000744", "34000783", "3400tyilmaz", "3400ioguzturk","3400agunes", "3400syildiz" };
                    if (!firmalar.Contains(model.UserName))
                    {
                        throw new Exception("Yapılan bakım çalışması nedeniyle. Geçici olarak hizmet verilemiyor.");
                    }
                }
               */
                var user = await userManager.FindByNameAsync(model.UserName);

                if (user != null && user.IsActive)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        try
                        {
                            return Redirect(returnUrl ?? "/");

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                if (user != null && user.IsActive == false)
                {
                    ModelState.TryAddModelError(nameof(model.UserName), "Kullanıcı aktif değil.");
                }
                else
                {
                    ModelState.TryAddModelError(nameof(model.UserName), "Hatalı kullanıcı adı ya da parola.");
                }
            }
            return Redirect(returnUrl ?? "/");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}

