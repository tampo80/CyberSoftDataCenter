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
    public class BkpDataBasesController : Controller
    {
        private readonly CdataCenterDbContext _context;

        public BkpDataBasesController(CdataCenterDbContext context)
        {
            _context = context;    
        }

        // GET: BkpDataBases
        public async Task<IActionResult> Index()
        {
            return View(await _context.BkpDataBases.ToListAsync());
        }

        // GET: BkpDataBases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bkpDataBases = await _context.BkpDataBases
                .SingleOrDefaultAsync(m => m.BkpDataBasesID == id);
            if (bkpDataBases == null)
            {
                return NotFound();
            }

            return View(bkpDataBases);
        }

        // GET: BkpDataBases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BkpDataBases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BkpDataBasesID,CyberCenterID,DateBKP")] BkpDataBases bkpDataBases)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bkpDataBases);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bkpDataBases);
        }

        // GET: BkpDataBases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bkpDataBases = await _context.BkpDataBases.SingleOrDefaultAsync(m => m.BkpDataBasesID == id);
            if (bkpDataBases == null)
            {
                return NotFound();
            }
            return View(bkpDataBases);
        }

        // POST: BkpDataBases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BkpDataBasesID,CyberCenterID,DateBKP")] BkpDataBases bkpDataBases)
        {
            if (id != bkpDataBases.BkpDataBasesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bkpDataBases);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BkpDataBasesExists(bkpDataBases.BkpDataBasesID))
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
            return View(bkpDataBases);
        }

        // GET: BkpDataBases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bkpDataBases = await _context.BkpDataBases
                .SingleOrDefaultAsync(m => m.BkpDataBasesID == id);
            if (bkpDataBases == null)
            {
                return NotFound();
            }

            return View(bkpDataBases);
        }

        // POST: BkpDataBases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bkpDataBases = await _context.BkpDataBases.SingleOrDefaultAsync(m => m.BkpDataBasesID == id);
            _context.BkpDataBases.Remove(bkpDataBases);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BkpDataBasesExists(int id)
        {
            return _context.BkpDataBases.Any(e => e.BkpDataBasesID == id);
        }
    }
}
