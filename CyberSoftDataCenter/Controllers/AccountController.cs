using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CyberSoftDataCenter.Models;
using CyberSoftDataCenter.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;

namespace CyberSoftDataCenter.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<DCUsers> userManager;
        private readonly SignInManager<DCUsers> loginManager;
        private readonly RoleManager<DCRoles> roleManager;


        public AccountController(UserManager<DCUsers> userManager,
           SignInManager<DCUsers> loginManager,
           RoleManager<DCRoles> roleManager)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
            this.roleManager = roleManager;
        }
     ///   [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel obj)
            {
            if (ModelState.IsValid)
            {
                DCUsers user = new DCUsers();
                user.UserName = obj.UserName;
                user.Email = obj.Email;
                user.FulleName = obj.FulleName;
                user.Tel = obj.Tel;

                

                IdentityResult result = userManager.CreateAsync
                (user, obj.Password).Result;

                if (result.Succeeded)
                {
                    if (!roleManager.RoleExistsAsync("User").Result)
                    {
                        DCRoles role = new DCRoles();
                        role.Name = "User";
                        role.Description = "Utilisateurs.";
                        IdentityResult roleResult = roleManager.
                        CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("",
                             "Error while creating role!");
                            return View(obj);
                        }
                    }

                    userManager.AddToRoleAsync(user,
                                 "User").Wait();
                    return RedirectToAction("Index", "User");
                }
            }
            return View(obj);
        }



        [HttpPost]
       
        public async Task<IActionResult> Reinit(string Id,string password)
        {
            DCUsers user = userManager.Users.FirstOrDefault(e => e.Id == Id);
            if (user!=null)
            {
                string token =await userManager.GeneratePasswordResetTokenAsync(user);
                try
                {
                    IdentityResult Result = await userManager.ResetPasswordAsync(user, token, password);
                    if (Result.Succeeded)
                    {
                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                catch (Exception ex)
                {

                    return Json(ex.Message);
                }
               
            }


            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var result = loginManager.PasswordSignInAsync
                (obj.UserName, obj.Password,
                  obj.RememberMe, false).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login!");
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOff()
        {
            loginManager.SignOutAsync().Wait();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}