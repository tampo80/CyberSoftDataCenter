using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CyberSoftDataCenter.Models;
using CyberSoftDataCenter.Models.AccountViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace CyberSoftDataCenter.Controllers
{
  //  [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<DCRoles> roleManager;

        public RoleController(RoleManager<DCRoles> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<RolesListViewModel> model = new List<RolesListViewModel>();
            model = roleManager.Roles.Select(r => new RolesListViewModel
            {
                RoleName = r.Name,
                Id = r.Id,
                Description = r.Description,
                NumberOfUsers = r.Users.Count
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddEditRole(string id)
        {
            RoleViewModel model = new RoleViewModel();
            ViewBag.Titre = "Nouveau Rôle";
            ViewBag.button = "Créer le Rôle";
            if (!String.IsNullOrEmpty(id))
            {
                DCRoles applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    model.Id = applicationRole.Id;
                    model.RoleName = applicationRole.Name;
                    model.Description = applicationRole.Description;
                    ViewBag.Titre = "Editer le rôles";
                    ViewBag.button = "Editer le Rôle";
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEditRole(string id, RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isExist = !String.IsNullOrEmpty(id);
                DCRoles applicationRole = isExist ? await roleManager.FindByIdAsync(id) :
               new DCRoles
               {
                 //  CreatedDate = DateTime.UtcNow
               };
                applicationRole.Name = model.RoleName;
                applicationRole.Description = model.Description;
              //  applicationRole.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                IdentityResult roleRuslt = isExist ? await roleManager.UpdateAsync(applicationRole)
                                                    : await roleManager.CreateAsync(applicationRole);
                if (roleRuslt.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            if (!String.IsNullOrEmpty(id))
            {
                ViewBag.Titre = "Editer le rôles";
                ViewBag.button = "Editer le Rôle";
            }
            else
            {
                ViewBag.Titre = "Nouveau Rôle";
                ViewBag.button = "Créer le Rôle";
            }
            return View(model);
        }

        //[HttpGet]
        //public async Task<IActionResult> DeleteRole(string id)
        //{
        //    string name = string.Empty;
        //    if (!String.IsNullOrEmpty(id))
        //    {
        //        DCRoles applicationRole = await roleManager.FindByIdAsync(id);
        //        if (applicationRole != null)
        //        {
        //            name = applicationRole.Name;
        //        }
        //    }
        //    return PartialView("_DeleteApplicationRole", name);
        //}

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                DCRoles applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    IdentityResult roleRuslt = roleManager.DeleteAsync(applicationRole).Result;
                    if (roleRuslt.Succeeded)
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
