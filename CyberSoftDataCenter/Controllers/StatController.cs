using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CyberSoftDataCenter.Data;
using Microsoft.EntityFrameworkCore;
using CyberSoftDataCenter.Models.ViewModels;
using CyberSoftDataCenter.Models;

namespace CyberSoftDataCenter.Controllers
{
    public class StatController : Controller
    {
        private readonly CdataCenterDbContext _context;

        public StatController(CdataCenterDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
       // GET: CybersCenters
        public async Task<IActionResult> ParCyberCentreUnite(string DateDebut,string DateFin)
        {
            var CybersCentres = await _context.CybersCenters.ToListAsync();

            var FirstOperation = await _context.VenteUnites.OrderBy(e => e.DateVente).FirstOrDefaultAsync();

            DateTime FirtDate = FirstOperation == null ? DateTime.Now : FirstOperation.DateVente;
           
            List<ParCyberCenterVm> data = new List<ParCyberCenterVm>();
            DateTime DtDebut = DateDebut == null ? FirtDate : Convert.ToDateTime(Helper.DateConverter.ConvertDate(DateDebut));

            DateTime DtFin =DateFin==null?DateTime.Now: Convert.ToDateTime(Helper.DateConverter.ConvertDate(DateFin));;

            double SoldeTotal = _context.VenteUnites.Where(e => (e.DateVente >= DtDebut.AddDays(-1) && e.DateVente <= DtFin)).Sum(e => e.MontantAchat);
            int I = 0;
            foreach (var item in CybersCentres)
            { I++;
                data.Add(new ParCyberCenterVm
                {
                    CyberCentres = item.Nom,
                    Solde = _context.VenteUnites.Where(e => (e.DateVente >= DtDebut.AddDays(-1) && e.DateVente <= DtFin) && e.CybersCenters.Nom == item.Nom).Sum(e => e.MontantAchat),
                    Id = I,
                    Taux = SoldeTotal == 0 ? 0 : Math.Round((_context.VenteUnites.Where(e => (e.DateVente >= DtDebut.AddDays(-1) && e.DateVente <= DtFin) && e.CybersCenters.Nom == item.Nom).Sum(e => e.MontantAchat)/ SoldeTotal)*100,2)

                });
            }
            ViewBag.DateDebut = DtDebut.ToString("dd/MM/yyyy");
            ViewBag.DateFin = DtFin.ToString("dd/MM/yyyy");
            // var cdataCenterDbContext = _context.CybersCenters.Include(c => c.Villes);
            return View( data);
        }

        public async Task<IActionResult> ParVillesUnite(string DateDebut, string DateFin)
        {
            var Villes = await _context.Villes.ToListAsync();
            var FirstOperation = await _context.VenteUnites.OrderBy(e => e.DateVente).FirstOrDefaultAsync();

            DateTime FirtDate = FirstOperation == null ? DateTime.Now : FirstOperation.DateVente;

            List<ParVillesVm> data = new List<ParVillesVm>();
            DateTime DtDebut = DateDebut == null ?FirtDate : Convert.ToDateTime(Helper.DateConverter.ConvertDate(DateDebut));

            DateTime DtFin = DateFin == null ? DateTime.Now : Convert.ToDateTime(Helper.DateConverter.ConvertDate(DateFin));;

            double SoldeTotal = _context.VenteUnites.Where(e => (e.DateVente >= DtDebut.AddDays(-1) && e.DateVente <= DtFin)).Sum(e => e.MontantAchat);
            int I = 0;
            foreach (var item in Villes)
            {
                I++;
                data.Add(new ParVillesVm
                {
                    Ville = item.Nom,
                    Solde = _context.VenteUnites.Where(e => (e.DateVente >= DtDebut.AddDays(-1) && e.DateVente <= DtFin) && e.CybersCenters.Villes.Nom == item.Nom).Sum(e => e.MontantAchat),
                    Id = I,
                    Taux = SoldeTotal == 0 ? 0 : Math.Round((_context.VenteUnites.Where(e => (e.DateVente >= DtDebut.AddDays(-1) && e.DateVente <= DtFin) && e.CybersCenters.Villes.Nom == item.Nom).Sum(e => e.MontantAchat) / SoldeTotal) * 100, 2)

                });
            }
            ViewBag.DateDebut = DtDebut.ToString("dd/MM/yyyy");
            ViewBag.DateFin = DtFin.ToString("dd/MM/yyyy");
            // var cdataCenterDbContext = _context.CybersCenters.Include(c => c.Villes);
            return View(data);
        }
        public async Task<IActionResult> ParCyberCentreProduit(string DateDebut, string DateFin)
        {
            var CybersCentres = await _context.CybersCenters.ToListAsync();
            var FirstOperation = await _context.VenteProduits.OrderBy(e => e.DateOperation).FirstOrDefaultAsync();

            DateTime FirtDate = FirstOperation == null ? DateTime.Now : FirstOperation.DateOperation;

            List<ParCyberCenterVm> data = new List<ParCyberCenterVm>();
            DateTime DtDebut = DateDebut == null ? FirtDate : Convert.ToDateTime(Helper.DateConverter.ConvertDate(DateDebut));

            DateTime DtFin = DateFin == null ? DateTime.Now : Convert.ToDateTime(Helper.DateConverter.ConvertDate(DateFin));

            double SoldeTotal = _context.VenteProduits.Where(e => (e.DateOperation >= DtDebut.AddDays(-1) && e.DateOperation <= DtFin)).Sum(e =>Convert.ToDouble(e.Montant));
            int I = 0;
            foreach (var item in CybersCentres)
            {
                I++;
                data.Add(new ParCyberCenterVm
                {
                    CyberCentres = item.Nom,
                    Solde = _context.VenteProduits.Where(e => (e.DateOperation >= DtDebut.AddDays(-1) && e.DateOperation <= DtFin) && e.CybersCenters.Nom == item.Nom).Sum(e => Convert.ToDouble(e.Montant)),
                    Id = I,
                    Taux = SoldeTotal == 0 ? 0 : Math.Round((_context.VenteProduits.Where(e => (e.DateOperation >= DtDebut.AddDays(-1) && e.DateOperation <= DtFin) && e.CybersCenters.Nom == item.Nom).Sum(e => Convert.ToDouble(e.Montant)) / SoldeTotal) * 100, 2)

                });
            }
            ViewBag.DateDebut = DtDebut.ToString("dd/MM/yyyy");
            ViewBag.DateFin = DtFin.ToString("dd/MM/yyyy");
            // var cdataCenterDbContext = _context.CybersCenters.Include(c => c.Villes);
            return View(data);
        }

        public async Task<IActionResult> ParVillesProduit(string DateDebut, string DateFin)
        {
            var Villes = await _context.Villes.ToListAsync();

            var FirstOperation = await _context.VenteProduits.OrderBy(e => e.DateOperation).FirstOrDefaultAsync();

            DateTime FirtDate = FirstOperation == null ? DateTime.Now : FirstOperation.DateOperation;


            List<ParVillesVm> data = new List<ParVillesVm>();
            DateTime DtDebut = DateDebut == null ? FirtDate : Convert.ToDateTime(Helper.DateConverter.ConvertDate(DateDebut));

            DateTime DtFin = DateFin == null ? DateTime.Now : Convert.ToDateTime(Helper.DateConverter.ConvertDate(DateFin));;

            double SoldeTotal = _context.VenteProduits.Where(e => (e.DateOperation >= DtDebut.AddDays(-1) && e.DateOperation <= DtFin)).Sum(e => Convert.ToDouble(e.Montant));
            int I = 0;
            foreach (var item in Villes)
            {
                I++;
                data.Add(new ParVillesVm
                {
                    Ville = item.Nom,
                    Solde = _context.VenteProduits.Where(e => (e.DateOperation >= DtDebut.AddDays(-1) && e.DateOperation <= DtFin) && e.CybersCenters.Villes.Nom == item.Nom).Sum(e => Convert.ToDouble(e.Montant)),
                    Id = I,
                    Taux = SoldeTotal == 0 ? 0 : Math.Round((_context.VenteProduits.Where(e => (e.DateOperation >= DtDebut.AddDays(-1) && e.DateOperation <= DtFin) && e.CybersCenters.Villes.Nom == item.Nom).Sum(e => Convert.ToDouble(e.Montant)) / SoldeTotal) * 100, 2)

                });
            }
            ViewBag.DateDebut = DtDebut.ToString("dd/MM/yyyy");
            ViewBag.DateFin = DtFin.ToString("dd/MM/yyyy");
            // var cdataCenterDbContext = _context.CybersCenters.Include(c => c.Villes);
            return View(data);
        }
    }
}