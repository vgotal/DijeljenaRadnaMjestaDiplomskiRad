using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using AplikacijaDijeljenihRadnihMjesta.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Controllers
{
    public class RezervacijeController : Controller
    {
        private RezervacijeRepository rezervacijeRepository;
        private MailRequest request = new MailRequest();
        private readonly IMailService mailServices;



        public RezervacijeController(AppDbContext db, IMailService mailServices)
        {
            this.rezervacijeRepository = new RezervacijeRepository(db);
            this.mailServices = mailServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ModelState.Clear();
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            var djelatnikID = HttpContext.Session.GetInt32("DjelatnikID");
            var djelatnikUloga = rezervacijeRepository.DohvatiDjelatnikovuUlogu((int)djelatnikID);
            if (djelatnikUloga == "Administrator")
            {
                TempData["uloga"] = "ADMIN";
            }
            else
                TempData["uloga"] = "DJELATNIK";
            var rezervacije = new RezervacijaVM();
            rezervacije.Lokacije = rezervacijeRepository.DohvatiLokacije(rezervacije, (int)djelatnikID);
            return View(rezervacije);
        }

        [HttpPost]
        public IActionResult Index(RezervacijaVM rezervacija) 
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            var djelatnikID = HttpContext.Session.GetInt32("DjelatnikID");
            var djelatnik = new Djelatnik();
            if (djelatnikID != 0 && djelatnikID != null)
            {
                djelatnik = rezervacijeRepository.DohvatiDjelatnika((int)djelatnikID);
            }
            var djelatnikUloga = rezervacijeRepository.DohvatiDjelatnikovuUlogu((int)djelatnikID);
            if (djelatnikUloga == "Administrator")
            {
                TempData["uloga"] = "ADMIN";
            }
            else
            {
                TempData["uloga"] = "DJELATNIK";
            }
            rezervacija.Lokacije = rezervacijeRepository.DohvatiLokacije(rezervacija, (int)djelatnikID);
            rezervacija.Adresa = rezervacijeRepository.DohvatiOdabranuAdresu(rezervacija);
            rezervacija = rezervacijeRepository.DohvatiMoguceRezervacije(rezervacija, (int)djelatnikID, Int32.Parse(rezervacija.Lokacija));
            if (rezervacija.PovratnaInfo != null)
            {
                TempData["Info"] = rezervacija.PovratnaInfo;
            }
            if (rezervacija.PovratnaInfoUspjeh != null)
            {
                TempData["Uspješno"] = rezervacija.PovratnaInfoUspjeh;
                request.ToEmail = djelatnik.Email;
                request.Subject = "Rezervacija";
                request.Body = $" <b> <h3> Podnošenje zahtjeva za rezervaciju</h3></b> <br />   Zahtjevi su podneseni za dan/e:<b> {rezervacija.Datumi}</b> <br /> Potrebno je dobiti e-mail potvrde nakon što admin odobri Vaš/e zahtjev/e.";
                mailServices.SendEmailAsync(request);
            }
            if (rezervacija.PovratnaInfoNeuspjeh != null)
            {
                TempData["Neuspješno"] = rezervacija.PovratnaInfoNeuspjeh;
            }
            
            return View(rezervacija); 
        }

        
    }

}

      

 
