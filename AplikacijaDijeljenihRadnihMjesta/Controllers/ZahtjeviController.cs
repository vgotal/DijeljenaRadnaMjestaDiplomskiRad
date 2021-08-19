using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using AplikacijaDijeljenihRadnihMjesta.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using AplikacijaDijeljenihRadnihMjesta.Models.Paginacija;

namespace AplikacijaDijeljenihRadnihMjesta.Controllers
{
    public class ZahtjeviController : Controller
    {
        private PregledZahtjevaRepository pregledZahtjevaRepository;
        const string SessionTipZahtjeva = "_TipZahtjeva";
        const string SessionPocetniDatum = "_PocetniDatum";
        const string SessionKrajnjiDatum = "_KrajnjiDatum";
        public ZahtjeviController(AppDbContext db)
        {
            this.pregledZahtjevaRepository = new PregledZahtjevaRepository(db);
        }

        //[HttpGet]
        public IActionResult Index(int pageNumber = 1, int pageSize = 4)
        {

            var djelatnikID = HttpContext.Session.GetInt32("DjelatnikID");
            var djelatnikUloga = pregledZahtjevaRepository.DohvatiDjelatnikovuUlogu((int)djelatnikID);
            if (djelatnikUloga == "Administrator")
            {
                TempData["uloga"] = "ADMIN";
            }
            else
                TempData["uloga"] = "DJELATNIK";

            var zahtjeviSPaginacijom = new PaginacijaZahtjev();
            zahtjeviSPaginacijom.pregledZahtjeva = new PregledZahtjevaVM();

            if (TempData["Resetiranje"] != null)
            {
                HttpContext.Session.SetString(SessionPocetniDatum, null);
                HttpContext.Session.SetString(SessionKrajnjiDatum, null);
            }
            if ( (pageNumber != 1 && HttpContext.Session.GetInt32(SessionTipZahtjeva) != 0) || (pageNumber == 1 && HttpContext.Session.GetInt32(SessionTipZahtjeva) != 0) && (HttpContext.Session.GetString(SessionPocetniDatum)==null || HttpContext.Session.GetString(SessionPocetniDatum) == "") && (HttpContext.Session.GetString(SessionKrajnjiDatum) == null || HttpContext.Session.GetString(SessionKrajnjiDatum) == ""))
            {
                var tipZahtjeva = HttpContext.Session.GetInt32(SessionTipZahtjeva);
                zahtjeviSPaginacijom.pregledZahtjeva.TipZahtjeva = tipZahtjeva;
                zahtjeviSPaginacijom.pregledZahtjeva.PocetniDatum = null;
                zahtjeviSPaginacijom.pregledZahtjeva.KrajnjiDatum = null;
            }
            if ((pageNumber != 1 || (pageNumber == 1 && HttpContext.Session.GetInt32(SessionTipZahtjeva) != 0)) && HttpContext.Session.GetString(SessionPocetniDatum) != null && HttpContext.Session.GetString(SessionPocetniDatum) != "" && HttpContext.Session.GetString(SessionKrajnjiDatum) != null && HttpContext.Session.GetString(SessionKrajnjiDatum) != "")
            {
                var pocetniDatum = HttpContext.Session.GetString(SessionPocetniDatum);
                var krajnjiDatum = HttpContext.Session.GetString(SessionKrajnjiDatum);
                var tipZahtjeva = HttpContext.Session.GetInt32(SessionTipZahtjeva);
                zahtjeviSPaginacijom.pregledZahtjeva.PocetniDatum = DateTime.Parse(pocetniDatum);
                zahtjeviSPaginacijom.pregledZahtjeva.KrajnjiDatum = DateTime.Parse(krajnjiDatum);
                zahtjeviSPaginacijom.pregledZahtjeva.TipZahtjeva = tipZahtjeva;
            }
            if ((pageNumber != 1 || (pageNumber == 1 && HttpContext.Session.GetInt32(SessionTipZahtjeva) == null)) && HttpContext.Session.GetString(SessionPocetniDatum) != null && HttpContext.Session.GetString(SessionPocetniDatum) != "" && HttpContext.Session.GetString(SessionKrajnjiDatum) != null && HttpContext.Session.GetString(SessionKrajnjiDatum) != "")
            {
                var pocetniDatum = HttpContext.Session.GetString(SessionPocetniDatum);
                var krajnjiDatum = HttpContext.Session.GetString(SessionKrajnjiDatum);
                zahtjeviSPaginacijom.pregledZahtjeva.PocetniDatum =DateTime.Parse(pocetniDatum);
                zahtjeviSPaginacijom.pregledZahtjeva.KrajnjiDatum = DateTime.Parse(krajnjiDatum);
                zahtjeviSPaginacijom.pregledZahtjeva.TipZahtjeva = null;
            }
            zahtjeviSPaginacijom.pregledZahtjeva.TipoviZahtjeva = pregledZahtjevaRepository.DohvatiTipoveZahtjeva();
            zahtjeviSPaginacijom = pregledZahtjevaRepository.DohvatiZahtjeve(zahtjeviSPaginacijom.pregledZahtjeva, (int)djelatnikID, pageSize, pageNumber);
            return View(zahtjeviSPaginacijom);
        }
        [HttpPost]
        public IActionResult Index(PaginacijaZahtjev zahtjeviSPaginacijom,  int pageNumber = 1, int pageSize = 4)
        {
          

            var djelatnikID = HttpContext.Session.GetInt32("DjelatnikID");
            var djelatnikUloga = pregledZahtjevaRepository.DohvatiDjelatnikovuUlogu((int)djelatnikID);
            if (djelatnikUloga == "Administrator")
            {
                TempData["uloga"] = "ADMIN";
            }
            else
                TempData["uloga"] = "DJELATNIK";
            if (zahtjeviSPaginacijom.pregledZahtjeva == null && zahtjeviSPaginacijom.paginationModel == null)
            {
                zahtjeviSPaginacijom.pregledZahtjeva = new PregledZahtjevaVM();
            }
            
            zahtjeviSPaginacijom  = pregledZahtjevaRepository.DohvatiZahtjeve(zahtjeviSPaginacijom.pregledZahtjeva, (int)djelatnikID, pageSize, pageNumber);
            if (zahtjeviSPaginacijom.pregledZahtjeva.povratnaInfo !=null)
            {
                TempData["Info"] = zahtjeviSPaginacijom.pregledZahtjeva.povratnaInfo;
            }
            else
            {
                if (zahtjeviSPaginacijom.pregledZahtjeva.TipZahtjeva != null && zahtjeviSPaginacijom.pregledZahtjeva.PocetniDatum == null 
                    && zahtjeviSPaginacijom.pregledZahtjeva.KrajnjiDatum == null)
                {
                    HttpContext.Session.SetInt32(SessionTipZahtjeva, (int)zahtjeviSPaginacijom.pregledZahtjeva.TipZahtjeva);
                }
                if (zahtjeviSPaginacijom.pregledZahtjeva.TipZahtjeva != null && zahtjeviSPaginacijom.pregledZahtjeva.PocetniDatum != null
                    && zahtjeviSPaginacijom.pregledZahtjeva.KrajnjiDatum != null)
                {
                    HttpContext.Session.SetInt32(SessionTipZahtjeva, (int)zahtjeviSPaginacijom.pregledZahtjeva.TipZahtjeva);
                    HttpContext.Session.SetString(SessionPocetniDatum, zahtjeviSPaginacijom.pregledZahtjeva.PocetniDatum.ToString());
                    HttpContext.Session.SetString(SessionKrajnjiDatum, zahtjeviSPaginacijom.pregledZahtjeva.KrajnjiDatum.ToString());
                }
                if (zahtjeviSPaginacijom.pregledZahtjeva.TipZahtjeva == null && zahtjeviSPaginacijom.pregledZahtjeva.PocetniDatum != null
                    && zahtjeviSPaginacijom.pregledZahtjeva.KrajnjiDatum != null)
                {
                    HttpContext.Session.SetString(SessionPocetniDatum, zahtjeviSPaginacijom.pregledZahtjeva.PocetniDatum.ToString());
                    HttpContext.Session.SetString(SessionKrajnjiDatum, zahtjeviSPaginacijom.pregledZahtjeva.KrajnjiDatum.ToString());
                }
                if (zahtjeviSPaginacijom.pregledZahtjeva.TipZahtjeva == null && zahtjeviSPaginacijom.pregledZahtjeva.PocetniDatum == null 
                    && zahtjeviSPaginacijom.pregledZahtjeva.KrajnjiDatum == null)
                {
                    HttpContext.Session.SetInt32(SessionTipZahtjeva, 0);
                    HttpContext.Session.SetString(SessionPocetniDatum, "");
                    HttpContext.Session.SetString(SessionKrajnjiDatum, "");
                }
            }

            zahtjeviSPaginacijom.pregledZahtjeva.TipoviZahtjeva = pregledZahtjevaRepository.DohvatiTipoveZahtjeva();
            

            return View(zahtjeviSPaginacijom);

        }

       
       

        public IActionResult Detalji(int zahtjevID)
        {
            var djelatnikID = HttpContext.Session.GetInt32("DjelatnikID");
            var djelatnikUloga = pregledZahtjevaRepository.DohvatiDjelatnikovuUlogu((int)djelatnikID);
            if (djelatnikUloga == "Administrator")
            {
                TempData["uloga"] = "ADMIN";
            }
            else
                TempData["uloga"] = "DJELATNIK";
            var detalji=pregledZahtjevaRepository.DohvatiDetalje(zahtjevID);
            return View(detalji);
        }




       
    }
}
