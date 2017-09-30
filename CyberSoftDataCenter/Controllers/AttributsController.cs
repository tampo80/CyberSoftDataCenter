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
    public class AttributsController : Controller
    {
        private readonly CdataCenterDbContext _context;

        public AttributsController(CdataCenterDbContext context)
        {
            _context = context;    
        }

        // GET: Attributs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Attributs.ToListAsync());
        }

        // GET: Attributs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributs = await _context.Attributs
                .SingleOrDefaultAsync(m => m.AttributsID == id);
            if (attributs == null)
            {
                return NotFound();
            }

            return View(attributs);
        }

        // GET: Attributs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attributs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttributsID,AttributName,Description")] Attributs attributs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attributs);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(attributs);
        }

        // GET: Attributs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributs = await _context.Attributs.SingleOrDefaultAsync(m => m.AttributsID == id);
            if (attributs == null)
            {
                return NotFound();
            }
            return View(attributs);
        }

        // POST: Attributs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttributsID,AttributName,Description")] Attributs attributs)
        {
            if (id != attributs.AttributsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attributs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributsExists(attributs.AttributsID))
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
            return View(attributs);
        }

        // GET: Attributs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributs = await _context.Attributs
                .SingleOrDefaultAsync(m => m.AttributsID == id);
            if (attributs == null)
            {
                return NotFound();
            }

            return View(attributs);
        }

        // POST: Attributs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attributs = await _context.Attributs.SingleOrDefaultAsync(m => m.AttributsID == id);
            _context.Attributs.Remove(attributs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AttributsExists(int id)
        {
            return _context.Attributs.Any(e => e.AttributsID == id);
        }
    }
}
