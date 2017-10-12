using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CyberSoftDataCenter.Data;
using CyberSoftDataCenter.Models;

namespace CyberSoftDataCenter.Controllers
{
    public class TarifsSocialeController : Controller
    {
        private readonly CdataCenterDbContext _context;

        public TarifsSocialeController(CdataCenterDbContext context)
        {
            _context = context;    
        }

        // GET: TarifsSociale
        public async Task<IActionResult> Index()
        {
            var cdataCenterDbContext = _context.Tarifs.Include(t => t.TypeCybers);
            return View(await cdataCenterDbContext.ToListAsync());
        }

        // GET: TarifsSociale/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifs = await _context.Tarifs
                .Include(t => t.TypeCybers)
                .SingleOrDefaultAsync(m => m.TarifsId == id);
            if (tarifs == null)
            {
                return NotFound();
            }

            return View(tarifs);
        }

        // GET: TarifsSociale/Create
        public IActionResult Create()
        {
            ViewData["TypeCybersId"] = new SelectList(_context.TypeCybers, "TypeCybersId", "TypeCybersId");
            return View();
        }

        // POST: TarifsSociale/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TarifsId,TypeCybersId,EpireDate,TarificationName,Isdefault")] Tarifs tarifs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarifs);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["TypeCybersId"] = new SelectList(_context.TypeCybers, "TypeCybersId", "TypeCybersId", tarifs.TypeCybersId);
            return View(tarifs);
        }

        // GET: TarifsSociale/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifs = await _context.Tarifs.SingleOrDefaultAsync(m => m.TarifsId == id);
            if (tarifs == null)
            {
                return NotFound();
            }
            ViewData["TypeCybersId"] = new SelectList(_context.TypeCybers, "TypeCybersId", "TypeCybersId", tarifs.TypeCybersId);
            return View(tarifs);
        }

        // POST: TarifsSociale/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TarifsId,TypeCybersId,EpireDate,TarificationName,Isdefault")] Tarifs tarifs)
        {
            if (id != tarifs.TarifsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarifs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarifsExists(tarifs.TarifsId))
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
            ViewData["TypeCybersId"] = new SelectList(_context.TypeCybers, "TypeCybersId", "TypeCybersId", tarifs.TypeCybersId);
            return View(tarifs);
        }

        // GET: TarifsSociale/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifs = await _context.Tarifs
                .Include(t => t.TypeCybers)
                .SingleOrDefaultAsync(m => m.TarifsId == id);
            if (tarifs == null)
            {
                return NotFound();
            }

            return View(tarifs);
        }

        // POST: TarifsSociale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarifs = await _context.Tarifs.SingleOrDefaultAsync(m => m.TarifsId == id);
            _context.Tarifs.Remove(tarifs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TarifsExists(int id)
        {
            return _context.Tarifs.Any(e => e.TarifsId == id);
        }
    }
}
