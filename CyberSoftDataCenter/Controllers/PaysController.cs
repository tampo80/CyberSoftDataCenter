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
    public class PaysController : Controller
    {
        private readonly CdataCenterDbContext _context;

        public PaysController(CdataCenterDbContext context)
        {
            _context = context;    
        }

        // GET: Pays
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Pays.ToListAsync());
        //}

        public async Task<IActionResult> Index(string filter, int page = 1,
                                  string sortExpression = "Nom")
        {

            var qry = _context.Pays.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.Nom.Contains(filter));
            }

            var model = await PagingList<Pays>.CreateAsync(
                                         qry, 12, page, sortExpression, "Nom");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }
        // GET: Pays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pays = await _context.Pays
                .SingleOrDefaultAsync(m => m.PaysID == id);
            if (pays == null)
            {
                return NotFound();
            }

            return View(pays);
        }

        // GET: Pays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaysID,Nom")] Pays pays)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pays);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pays);
        }

        // GET: Pays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pays = await _context.Pays.SingleOrDefaultAsync(m => m.PaysID == id);
            if (pays == null)
            {
                return NotFound();
            }
            return View(pays);
        }

        // POST: Pays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaysID,Nom")] Pays pays)
        {
            if (id != pays.PaysID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pays);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaysExists(pays.PaysID))
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
            return View(pays);
        }

        // GET: Pays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pays = await _context.Pays
                .SingleOrDefaultAsync(m => m.PaysID == id);
            if (pays == null)
            {
                return NotFound();
            }

            return View(pays);
        }

        // POST: Pays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pays = await _context.Pays.SingleOrDefaultAsync(m => m.PaysID == id);
            _context.Pays.Remove(pays);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PaysExists(int id)
        {
            return _context.Pays.Any(e => e.PaysID == id);
        }
    }
}
