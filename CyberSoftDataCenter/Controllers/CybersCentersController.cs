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
    public class CybersCentersController : Controller
    {
        private readonly CdataCenterDbContext _context;

        public CybersCentersController(CdataCenterDbContext context)
        {
            _context = context;    
        }

        // GET: CybersCenters
        //public async Task<IActionResult> Index()
        //{
        //    var cdataCenterDbContext = _context.CybersCenters.Include(c => c.Villes);
        //    return View(await cdataCenterDbContext.ToListAsync());
        //}
        public async Task<IActionResult> Index(string filter, int page = 1,
                                     string sortExpression = "Nom")
        {

            var qry = _context.CybersCenters.Include(e => e.Villes).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.Nom.Contains(filter));
            }

            var model = await PagingList<CybersCenters>.CreateAsync(
                                         qry, 12, page, sortExpression, "Nom");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }

        // GET: CybersCenters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cybersCenters = await _context.CybersCenters
                .Include(c => c.Villes)
                .SingleOrDefaultAsync(m => m.CybersCentersID == id);
            if (cybersCenters == null)
            {
                return NotFound();
            }

            return View(cybersCenters);
        }

        // GET: CybersCenters/Create
        public IActionResult Create()
        {
            ViewData["VillesID"] = new SelectList(_context.Villes, "VillesID", "Nom");
            return View();
        }

        // POST: CybersCenters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CybersCentersID,Nom,Tel,VillesID")] CybersCenters cybersCenters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cybersCenters);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["VillesID"] = new SelectList(_context.Villes, "VillesID", "Nom", cybersCenters.VillesID);
            return View(cybersCenters);
        }

        // GET: CybersCenters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cybersCenters = await _context.CybersCenters.SingleOrDefaultAsync(m => m.CybersCentersID == id);
            if (cybersCenters == null)
            {
                return NotFound();
            }
            ViewData["VillesID"] = new SelectList(_context.Villes, "VillesID", "Nom", cybersCenters.VillesID);
            return View(cybersCenters);
        }

        // POST: CybersCenters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CybersCentersID,Nom,Tel,VillesID")] CybersCenters cybersCenters)
        {
            if (id != cybersCenters.CybersCentersID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cybersCenters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CybersCentersExists(cybersCenters.CybersCentersID))
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
            ViewData["VillesID"] = new SelectList(_context.Villes, "VillesID", "Nom", cybersCenters.VillesID);
            return View(cybersCenters);
        }

        // GET: CybersCenters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cybersCenters = await _context.CybersCenters
                .Include(c => c.Villes)
                .SingleOrDefaultAsync(m => m.CybersCentersID == id);
            if (cybersCenters == null)
            {
                return NotFound();
            }

            return View(cybersCenters);
        }

        // POST: CybersCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cybersCenters = await _context.CybersCenters.SingleOrDefaultAsync(m => m.CybersCentersID == id);
            _context.CybersCenters.Remove(cybersCenters);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CybersCentersExists(int id)
        {
            return _context.CybersCenters.Any(e => e.CybersCentersID == id);
        }
    }
}
