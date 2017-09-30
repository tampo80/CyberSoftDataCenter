using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CyberSoftDataCenter.Models;
using CyberSoftDataCenter.Models.AccountViewModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

using ReflectionIT.Mvc.Paging;
using CyberSoftDataCenter.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace CyberSoftDataCenter.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<DCUsers> userManager;
        private readonly RoleManager<DCRoles> roleManager;
        private IPageHelper<UserListModelView> _pageHelper;
        public UserController(UserManager<DCUsers> userManager, RoleManager<DCRoles> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;

        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            //List<UserListModelView> model = new List<UserListModelView>();
            var model = userManager.Users.Select(u => new UserListModelView
            {
                Id = u.Id,
                FuleName = u.FulleName,
                Email = u.Email,
                UserName = u.UserName,
                Tel = u.Tel

            });

            await model.ToListAsync();
            //  var result = _pageHelper.GetPage(model, pageNumber);

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            EditUserViewModel model = new EditUserViewModel();
            model.ApplicationRoles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            if (!String.IsNullOrEmpty(id))
            {
                DCUsers user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    model.Tel = user.Tel;
                    model.UserName = user.UserName;
                    model.Email = user.Email;
                    model.FulleName = user.FulleName;
                    model.ApplicationRoleId = roleManager.Roles.Single(r => r.Name == userManager.GetRolesAsync(user).Result.Single()).Id;
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                DCUsers user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.FulleName = model.FulleName;
                    user.Tel = model.Tel;
                    string existingRole = userManager.GetRolesAsync(user).Result.Single();
                    string existingRoleId = roleManager.Roles.Single(r => r.Name == existingRole).Id;
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (existingRoleId != model.ApplicationRoleId)
                        {
                            IdentityResult roleResult = await userManager.RemoveFromRoleAsync(user, existingRole);
                            if (roleResult.Succeeded)
                            {
                                DCRoles applicationRole = await roleManager.FindByIdAsync(model.ApplicationRoleId);
                                if (applicationRole != null)
                                {
                                    IdentityResult newRoleResult = await userManager.AddToRoleAsync(user, applicationRole.Name);
                                    if (newRoleResult.Succeeded)
                                    {
                                        return RedirectToAction("Index");
                                    }
                                }
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index");

                        }
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        //public async Task<IActionResult> DeleteUser(string id)
        //{
        //    string name = string.Empty;
        //    if (!String.IsNullOrEmpty(id))
        //    {
        //        DCUsers applicationUser = await userManager.FindByIdAsync(id);
        //        if (applicationUser != null)
        //        {
        //            name = applicationUser.UserName;
        //        }
        //    }
        //    return View(name);
        //}

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                DCUsers applicationUser = await userManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    IdentityResult result = await userManager.DeleteAsync(applicationUser);
                    if (result.Succeeded)
                    {
                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
            }
            return View();
        }
    }
}
