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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Globalization;

namespace CyberSoftDataCenter.Controllers
{
    public class PubsController : Controller
    {
        private readonly CdataCenterDbContext _context;
        private IHostingEnvironment _environment;
        private String[] extens = { ".jpg", ".jpeg", ".png", ".gif" };

        public PubsController(CdataCenterDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Pubs
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Pubs.ToListAsync());
        //}
        public async Task<IActionResult> Index(string filter, int page = 1,
                                    string sortExpression = "Client")
        {

            var qry = _context.Pubs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.Client.Contains(filter));
            }

            var model = await PagingList<Pubs>.CreateAsync(
                                         qry, 12, page, sortExpression, "Client");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }

        // GET: Pubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pubs = await _context.Pubs
                .SingleOrDefaultAsync(m => m.PubsId == id);
            if (pubs == null)
            {
                return NotFound();
            }

            return View(pubs);
        }

        // GET: Pubs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pubs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PubsId,Client,Format,DateCreaton,FinContrat,Url,Statut")] Pubs pubs, ICollection<IFormFile> Fichier)
        {
           // _environment=new 
            var uploads = Path.Combine(_environment.WebRootPath, "uploads/"+pubs.Format.ToString());
            string fileName = string.Empty;
            if (ModelState.IsValid)
            {
                if (!Directory.Exists(uploads))
                {
                    try
                    {
                        Directory.CreateDirectory(uploads);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                foreach (var file in Fichier)
                {
                    if (file.Length > 0)
                    {

                        fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                        string currentExt = fileName.Split('.').Last();
                        if(Array.Exists(extens, element => element == currentExt))
                        { 
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                        }
                    }
                }
                pubs.Url = fileName;
                _context.Add(pubs);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(pubs);
        }

        // GET: Pubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pubs = await _context.Pubs.SingleOrDefaultAsync(m => m.PubsId == id);
            if (pubs == null)
            {
                return NotFound();
            }
            CultureInfo Ci = new CultureInfo("fr-FR");
            ViewBag.DateDebut = pubs.DateCreaton.Date.ToString("d", Ci);
            ViewBag.DateFin = pubs.FinContrat.Date.ToString("dd/MM/yyyy");
            return View(pubs);
        }

        // POST: Pubs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PubsId,Client,Format,DateCreaton,FinContrat,Url,Statut")] Pubs pubs, ICollection<IFormFile> Fichier)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads/" + pubs.Format.ToString());
            string fileName = string.Empty;
            if (id != pubs.PubsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var file in Fichier)
                    {
                        if (file.Length > 0)
                        {
                            fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                            string currentExt = fileName.Split('.').Last();
                            if (Array.Exists(extens, element => element == currentExt))
                            {
                                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                }
                            }
                        }
                    }
                    pubs.Url = fileName==string.Empty?pubs.Url: fileName;
                    _context.Update(pubs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PubsExists(pubs.PubsId))
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
            CultureInfo Ci = new CultureInfo("fr-FR");
            ViewBag.DateDebut = pubs.DateCreaton.Date.ToString("d",Ci);
            ViewBag.DateFin = pubs.FinContrat.Date.ToString("dd/MM/yyyy");
            return View(pubs);
        }

        // GET: Pubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pubs = await _context.Pubs
                .SingleOrDefaultAsync(m => m.PubsId == id);
            if (pubs == null)
            {
                return NotFound();
            }

            return View(pubs);
        }

        // POST: Pubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pubs = await _context.Pubs.SingleOrDefaultAsync(m => m.PubsId == id);
            _context.Pubs.Remove(pubs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PubsExists(int id)
        {
            return _context.Pubs.Any(e => e.PubsId == id);
        }

        [HttpGet]
        public async Task<IActionResult> DeletePub(int id)
        {
            try
            {
                var pubs = await _context.Pubs.SingleOrDefaultAsync(m => m.PubsId == id);
                _context.Pubs.Remove(pubs);
                await _context.SaveChangesAsync();
                return Json(true);
            }
            catch (Exception)
            {

                return Json(false);
            }
          
           
        }
    }
}
