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
    public class VenteUnitesController : Controller
    {
        private readonly CdataCenterDbContext _context;

        public VenteUnitesController(CdataCenterDbContext context)
        {
            _context = context;    
        }

        // GET: VenteUnites
        //public async Task<IActionResult> Index()
        //{
        //    var cdataCenterDbContext = _context.VenteUnites.Include(v => v.CybersCenters);
        //    return View(await cdataCenterDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string filter, int page = 1,
                                         string sortExpression = "Clients")
        {

            var qry = _context.VenteUnites.Include(e => e.CybersCenters).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.Clients.Contains(filter));
            }

            var model = await PagingList<VenteUnites>.CreateAsync(
                                         qry, 12, page, sortExpression, "Clients");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }

        // GET: VenteUnites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venteUnites = await _context.VenteUnites
                .Include(v => v.CybersCenters)
                .SingleOrDefaultAsync(m => m.VenteUnitesID == id);
            if (venteUnites == null)
            {
                return NotFound();
            }

            return View(venteUnites);
        }

        // GET: VenteUnites/Create
        public IActionResult Create()
        {
            ViewData["CybersCentersID"] = new SelectList(_context.CybersCenters, "CybersCentersID", "CybersCentersID");
            return View();
        }

        // POST: VenteUnites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenteUnitesID,HeureVente,DateVente,Clients,TypeTarification,Users,MontantAchat,HeuresAchete,CybersCentersID")] VenteUnites venteUnites)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venteUnites);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CybersCentersID"] = new SelectList(_context.CybersCenters, "CybersCentersID", "CybersCentersID", venteUnites.CybersCentersID);
            return View(venteUnites);
        }

        // GET: VenteUnites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venteUnites = await _context.VenteUnites.SingleOrDefaultAsync(m => m.VenteUnitesID == id);
            if (venteUnites == null)
            {
                return NotFound();
            }
            ViewData["CybersCentersID"] = new SelectList(_context.CybersCenters, "CybersCentersID", "CybersCentersID", venteUnites.CybersCentersID);
            return View(venteUnites);
        }

        // POST: VenteUnites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenteUnitesID,HeureVente,DateVente,Clients,TypeTarification,Users,MontantAchat,HeuresAchete,CybersCentersID")] VenteUnites venteUnites)
        {
            if (id != venteUnites.VenteUnitesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venteUnites);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenteUnitesExists(venteUnites.VenteUnitesID))
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
            ViewData["CybersCentersID"] = new SelectList(_context.CybersCenters, "CybersCentersID", "CybersCentersID", venteUnites.CybersCentersID);
            return View(venteUnites);
        }

        // GET: VenteUnites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venteUnites = await _context.VenteUnites
                .Include(v => v.CybersCenters)
                .SingleOrDefaultAsync(m => m.VenteUnitesID == id);
            if (venteUnites == null)
            {
                return NotFound();
            }

            return View(venteUnites);
        }

        // POST: VenteUnites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venteUnites = await _context.VenteUnites.SingleOrDefaultAsync(m => m.VenteUnitesID == id);
            _context.VenteUnites.Remove(venteUnites);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool VenteUnitesExists(int id)
        {
            return _context.VenteUnites.Any(e => e.VenteUnitesID == id);
        }
    }
}
