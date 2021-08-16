using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models;
using AplikacijaDijeljenihRadnihMjesta.Models.Paginacija;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using AplikacijaDijeljenihRadnihMjesta.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Controllers
{
    public class OdobravanjeZahtjevaController : Controller
    {

        private OdobravanjeZahtjevaRepository odobravanjeZahtjevaRepository;
        private MailRequest request = new MailRequest();
        private readonly IMailService mailServices;
        const string SessionStatus = "_Status";
        public OdobravanjeZahtjevaController(AppDbContext db, IMailService mailServices)
        {
            this.odobravanjeZahtjevaRepository = new OdobravanjeZahtjevaRepository(db, mailServices);
            this.mailServices = mailServices;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            ModelState.Clear();
            var djelatnikID = HttpContext.Session.GetInt32("DjelatnikID");
            var djelatnikUloga = odobravanjeZahtjevaRepository.DohvatiDjelatnikovuUlogu((int)djelatnikID);
            if (djelatnikUloga == "Administrator")
            {
                TempData["uloga"] = "ADMIN";
            }
            else
                TempData["uloga"] = "DJELATNIK";
            var zahtjeviOdobravanjeSPaginacijom = new PaginacijaOdobravanjeZahtjeva();
            zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva = new OdobravanjeZahtjeva();

            if (HttpContext.Session.GetInt32(SessionStatus) != null)
            {
                var statusi = HttpContext.Session.GetInt32(SessionStatus);
                zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva.Status = statusi;
            }
            zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva.Statusi = odobravanjeZahtjevaRepository.DohvatiStatuse();
            odobravanjeZahtjevaRepository.DohvatiZahtjeveZaOdobravanjeSPaginacijom(zahtjeviOdobravanjeSPaginacijom, pageSize, pageNumber);

            return View(zahtjeviOdobravanjeSPaginacijom);
        }

        [HttpPost]
        public IActionResult Index(PaginacijaOdobravanjeZahtjeva zahtjeviOdobravanjeSPaginacijom, int pageNumber = 1, int pageSize = 5)
        {
            var djelatnikID = HttpContext.Session.GetInt32("DjelatnikID");
            var djelatnik = new Djelatnik();
            if (djelatnikID != 0 && djelatnikID != null)
            {
                djelatnik = odobravanjeZahtjevaRepository.DohvatiDjelatnika((int)djelatnikID);
            }
            var djelatnikUloga = odobravanjeZahtjevaRepository.DohvatiDjelatnikovuUlogu((int)djelatnikID);
            if (djelatnikUloga == "Administrator")
            {
                TempData["uloga"] = "ADMIN";
            }
            else
                TempData["uloga"] = "DJELATNIK";
            if (zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva.Status == null)
            {
                var statusi = HttpContext.Session.GetInt32(SessionStatus);
                zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva.Status = statusi;
            }
            
            if (zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva == null && zahtjeviOdobravanjeSPaginacijom.paginationModel == null)
            {
                zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva = new OdobravanjeZahtjeva();
            }
            if (zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva.RezervacijeOtkazivanje != null)
            {
                odobravanjeZahtjevaRepository.PotvrdiZahtjev(zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva);

            }
            if (zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva.Status != null)
            {
                HttpContext.Session.SetInt32(SessionStatus, (int)zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva.Status);
            }
            else
            {
                
            }
            odobravanjeZahtjevaRepository.DohvatiZahtjeveZaOdobravanjeSPaginacijom(zahtjeviOdobravanjeSPaginacijom, pageSize, pageNumber);
            zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva.Statusi = odobravanjeZahtjevaRepository.DohvatiStatuse();

   
            
            var datumi = odobravanjeZahtjevaRepository.DohvatiDatumOdobravanja(zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva);
            return View(zahtjeviOdobravanjeSPaginacijom);
        }
    }
}
