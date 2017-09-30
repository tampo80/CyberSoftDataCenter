using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CyberSoftDataAPI.Models;
using System.Collections;
using Newtonsoft.Json.Linq;
using CyberSoftDataCenter.Data;
using Newtonsoft.Json;

namespace CyberSoftDataAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Ventes")]
    public class VentesController : Controller
    {
        private readonly CdataCenterDbContext _context;

        public VentesController(CdataCenterDbContext context)
        {
            _context = context;
        }


        [HttpPost("VU")]
        public void PostVente([FromBody] ArrayList Ventes)
        {
            if (Ventes.Count>0)
            {
                DCventesInternet Vente = JsonConvert.DeserializeObject<DCventesInternet>(Ventes[0].ToString());
                creatCyberCenter(Vente.Pays,Vente.ville,Vente.Cybercenter,Vente.Tel);
                foreach (var item in Ventes)

                {
                    DCventesInternet DCVente = JsonConvert.DeserializeObject<DCventesInternet>(item.ToString());
                    var v = _context.VenteUnites.FirstOrDefault(e=>e.OperationID==DCVente.id && e.CybersCenters.Nom==DCVente.Cybercenter && e.Clients==DCVente.Client && e.MontantAchat== Convert.ToDouble(String.Join("", DCVente.MontantAchat.Where(c => !char.IsWhiteSpace(c)))));
                    if (v==null)
                    {

                    
                    _context.VenteUnites.Add(

                        new CyberSoftDataCenter.Models.VenteUnites
                        {
                            Clients = DCVente.Client,
                            DateVente = DCVente.DateOperation,
                            HeuresAchete = DCVente.HeuresAchete,
                            MontantAchat = Convert.ToDouble(String.Join("", DCVente.MontantAchat.Where(c => !char.IsWhiteSpace(c)))),
                            HeureVente = DCVente.DateOperation,
                            Users = DCVente.Users,
                            CybersCenters = _context.CybersCenters.FirstOrDefault(e => e.Nom == DCVente.Cybercenter),
                            OperationID=DCVente.id

                        }
                        );
                    }
                }
            }
            else
            {
                return;
            }
            _context.SaveChanges();
        }

        [HttpPost("VP")]
        public void PostVenteProduit([FromBody] ArrayList Ventes)
        {
            if (Ventes.Count > 0)
            {
               DCVente  Vente = JsonConvert.DeserializeObject<DCVente>(Ventes[0].ToString());
                creatCyberCenter(Vente.Pays, Vente.Ville, Vente.Cybercenter,Vente.Tel);
                foreach (var item in Ventes)

                {
                    DCVente DCVente = JsonConvert.DeserializeObject<DCVente>(item.ToString());
                    var v = _context.VenteProduits.FirstOrDefault(e => e.RefVente == DCVente.RefVente && e.CybersCenters.Nom==DCVente.Cybercenter && e.Users==DCVente.Users && DCVente.Montant==DCVente.Montant);
                    if (v == null)
                    {


                        _context.VenteProduits.Add(

                            new CyberSoftDataCenter.Models.VenteProduits
                            {

                                DateOperation = DCVente.DateOperation,
                                Montant = DCVente.Montant,
                                RefVente = DCVente.RefVente,
                                Remise = DCVente.Remise,
                                Users = DCVente.Users,
                                CybersCenters = _context.CybersCenters.FirstOrDefault(e => e.Nom == DCVente.Cybercenter),
                                DetaillesVentes = DCVente.DCdetailVente.Select(u => new CyberSoftDataCenter.Models.DetaillesVentes {

                                    Montant = u.Montant,
                                    prixVente = u.prixVente,
                                    Produits = u.Produits,
                                    Quantite = u.Quantite,
                                    VenteProduitsID=u.VenteProduitsID

                                }).ToList()
                               

                            }
                            );
                    }
                }
            }
            else
            {
                return;
            }
            _context.SaveChanges();
        }

        private void creatCyberCenter(string pays,string ville,string cybercentres,string Tel)
        {
            var Cyber = _context.CybersCenters.FirstOrDefault(e=>e.Nom==cybercentres);
            if (Cyber!=null)
            {
                return;
            }
            else
            {
                var Ville = _context.Villes.FirstOrDefault(e => e.Nom == ville);
                if (Ville!=null)
                {
                    _context.CybersCenters.Add(new CyberSoftDataCenter.Models.CybersCenters {
                        Nom=cybercentres,
                        Tel=Tel,
                        Villes=Ville

                    });
                }
                else
                {
                    var Pays = _context.Pays.FirstOrDefault(e => e.Nom == pays);
                    if (Pays!=null)
                    {
                        _context.CybersCenters.Add(
                            new CyberSoftDataCenter.Models.CybersCenters
                            {
                                Nom=cybercentres,
                                Tel=Tel,
                                Villes=new CyberSoftDataCenter.Models.Villes {
                                    Nom=ville,
                                    Pays=Pays
                                }
                            }
                            );
                    }
                    else
                    {
                        _context.CybersCenters.Add(
                          new CyberSoftDataCenter.Models.CybersCenters
                          {
                              Nom = cybercentres,
                              Tel = Tel,
                              Villes = new CyberSoftDataCenter.Models.Villes
                              {
                                  Nom = ville,
                                  Pays = new CyberSoftDataCenter.Models.Pays
                                  {
                                      Nom=pays
                                  }
                              }
                          }
                          );
                    }
                }
            }
            _context.SaveChanges();
        }
    }
}