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
    public class VillesController : Controller
    {
        private readonly CdataCenterDbContext _context;

        public VillesController(CdataCenterDbContext context)
        {
            _context = context;    
        }

        // GET: Villes
        public async Task<IActionResult> Index(string filter, int page = 1,
                                  string sortExpression = "Nom")
        {

            var qry = _context.Villes.Include(e=>e.Pays).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.Nom.Contains(filter));
            }

            var model = await PagingList<Villes>.CreateAsync(
                                         qry, 12, page, sortExpression, "Nom");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }
        //public async Task<IActionResult> Index()
        //{
        //    var cdataCenterDbContext = _context.Villes.Include(v => v.Pays);
        //    return View(await cdataCenterDbContext.ToListAsync());
        //}

        // GET: Villes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var villes = await _context.Villes
                .Include(v => v.Pays)
                .SingleOrDefaultAsync(m => m.VillesID == id);
            if (villes == null)
            {
                return NotFound();
            }

            return View(villes);
        }

        // GET: Villes/Create
        public IActionResult Create()
        {
            ViewData["PaysID"] = new SelectList(_context.Pays, "PaysID", "Nom");
            return View();
        }

        // POST: Villes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VillesID,Nom,PaysID")] Villes villes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(villes);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PaysID"] = new SelectList(_context.Pays, "PaysID", "Nom", villes.PaysID);
            return View(villes);
        }

        // GET: Villes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var villes = await _context.Villes.SingleOrDefaultAsync(m => m.VillesID == id);
            if (villes == null)
            {
                return NotFound();
            }
            ViewData["PaysID"] = new SelectList(_context.Pays, "PaysID", "Nom", villes.PaysID);
            return View(villes);
        }

        // POST: Villes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VillesID,Nom,PaysID")] Villes villes)
        {
            if (id != villes.VillesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(villes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VillesExists(villes.VillesID))
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
            ViewData["PaysID"] = new SelectList(_context.Pays, "PaysID", "Nom", villes.PaysID);
            return View(villes);
        }

        // GET: Villes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var villes = await _context.Villes
                .Include(v => v.Pays)
                .SingleOrDefaultAsync(m => m.VillesID == id);
            if (villes == null)
            {
                return NotFound();
            }

            return View(villes);
        }

        // POST: Villes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var villes = await _context.Villes.SingleOrDefaultAsync(m => m.VillesID == id);
            _context.Villes.Remove(villes);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool VillesExists(int id)
        {
            return _context.Villes.Any(e => e.VillesID == id);
        }
    }
}
