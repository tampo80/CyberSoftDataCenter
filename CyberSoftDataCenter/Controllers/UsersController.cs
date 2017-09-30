using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CyberSoftDataCenter.Data;
using CyberSoftDataCenter.Models;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;

namespace CyberSoftDataCenter.Controllers
{
    public class UsersController : Controller
    {
        private readonly CdataCenterDbContext _context;

        public UsersController(CdataCenterDbContext context)
        {
            _context = context;    
        }

        // GET: Users
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Users.ToListAsync());

        //}
        //public async Task<IActionResult> Index(int page = 1)
        //{
        //    var qry = _context.Users.AsNoTracking().OrderBy(p => p.UserName);
        //    var model = await PagingList<Users>.CreateAsync(qry, 10, page);
        //    return View(model);
        //}

        public async Task<IActionResult> Index(string filter, int page = 1,
                                          string sortExpression = "UserName")
        {

            var qry = _context.Users.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.UserName.Contains(filter));
            }

            var model = await PagingList<Users>.CreateAsync(
                                         qry, 10, page, sortExpression, "UserName");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }

        //public async Task<IActionResult> Index(int page = 1,
        //                               string sortExpression = "UserName")
        //{

        //    var qry = _context.Users.AsNoTracking();
        //    var model = await PagingList<Users>.CreateAsync(
        //                            qry, 10, page, sortExpression, "UserName");
        //    return View(model);
        //}
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.UsersID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsersID,UserName,Password,PasswordSalt,FuleName,Tel,Isconnected,CreationDate,LastContedDate,IsActif")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.SingleOrDefaultAsync(m => m.UsersID == id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsersID,UserName,Password,PasswordSalt,FuleName,Tel,Isconnected,CreationDate,LastContedDate,IsActif")] Users users)
        {
            if (id != users.UsersID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UsersID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.UsersID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.SingleOrDefaultAsync(m => m.UsersID == id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UsersID == id);
        }
    }
}
