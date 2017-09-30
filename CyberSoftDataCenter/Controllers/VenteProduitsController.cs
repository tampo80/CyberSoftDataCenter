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
    public class VenteProduitsController : Controller
    {
        private readonly CdataCenterDbContext _context;

        public VenteProduitsController(CdataCenterDbContext context)
        {
            _context = context;    
        }

        // GET: VenteProduits
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.VenteProduits.Include(e=>e.CybersCenters).ToListAsync());
        //}

        public async Task<IActionResult> Index(string filter, int page = 1,
                                       string sortExpression = "Users")
        {

            var qry = _context.VenteProduits.Include(e => e.CybersCenters).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.Users.Contains(filter));
            }

            var model = await PagingList<VenteProduits>.CreateAsync(
                                         qry, 12, page, sortExpression, "Users");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }


        // GET: VenteProduits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venteProduits = await _context.VenteProduits
                .SingleOrDefaultAsync(m => m.VenteProduitsID == id);
            if (venteProduits == null)
            {
                return NotFound();
            }

            return View(venteProduits);
        }

        public async Task<IActionResult> DetailsProduits(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venteProduits = await _context.DetaillesVentes.Where(e=>e.VenteProduitsID==id).ToListAsync();
            if (venteProduits == null)
            {
                return NotFound();
            }

            return View(venteProduits);
        }
        // GET: VenteProduits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VenteProduits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenteProduitsID,DateOperation,Montant,Remise,Users,RefVente")] VenteProduits venteProduits)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venteProduits);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(venteProduits);
        }

        // GET: VenteProduits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venteProduits = await _context.VenteProduits.SingleOrDefaultAsync(m => m.VenteProduitsID == id);
            if (venteProduits == null)
            {
                return NotFound();
            }
            return View(venteProduits);
        }

        // POST: VenteProduits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenteProduitsID,DateOperation,Montant,Remise,Users,RefVente")] VenteProduits venteProduits)
        {
            if (id != venteProduits.VenteProduitsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venteProduits);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenteProduitsExists(venteProduits.VenteProduitsID))
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
            return View(venteProduits);
        }

        // GET: VenteProduits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venteProduits = await _context.VenteProduits
                .SingleOrDefaultAsync(m => m.VenteProduitsID == id);
            if (venteProduits == null)
            {
                return NotFound();
            }

            return View(venteProduits);
        }

        // POST: VenteProduits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venteProduits = await _context.VenteProduits.SingleOrDefaultAsync(m => m.VenteProduitsID == id);
            _context.VenteProduits.Remove(venteProduits);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool VenteProduitsExists(int id)
        {
            return _context.VenteProduits.Any(e => e.VenteProduitsID == id);
        }
    }
}
