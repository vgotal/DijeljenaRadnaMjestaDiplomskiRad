using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using AplikacijaDijeljenihRadnihMjesta.Repository;
using Microsoft.AspNetCore.Http;

namespace AplikacijaDijeljenihRadnihMjesta.Controllers
{
    public class AutentifikacijaController : Controller
    {
       
        private KorisnickiRacunRepository korisnickiRacunRepository;
    
        public AutentifikacijaController(AppDbContext db)
        {
            this.korisnickiRacunRepository = new KorisnickiRacunRepository(db);
        }
       
        public IActionResult Pocetna(KorisnickiRacunVM racun)
        {
            ModelState.Clear();
            
            var djelatnikID = HttpContext.Session.GetInt32("DjelatnikID");
            racun.BrojDjelatnika = korisnickiRacunRepository.DohvatiBrojDjelatnika();
            racun.BrojLaptopa = korisnickiRacunRepository.DohvatiBrojTipovaLaptopa();
            racun.BrojRadnihMjesta = korisnickiRacunRepository.DohvatiBrojRadnihMjesta();
            racun.OrganizacijskeJedinice = korisnickiRacunRepository.DohvatiBrojOrganizacijskihJedinica();
            racun.Lokacije = korisnickiRacunRepository.DohvatiBrojLokacija();
            racun.Gradovi = korisnickiRacunRepository.DohvatiBrojGradova();
            var djelatnikUloga = HttpContext.Session.GetString("Uloga");
            racun.Uloga = djelatnikUloga;
            if (djelatnikUloga == "Administrator")
            {
                HttpContext.Session.SetString("potvrda", "true");
                TempData["index"] = true;
            }
            else
            {
                TempData["index"] = false;
                HttpContext.Session.SetString("potvrda", "false");
            }
               
            return View(racun);
        }

        public IActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
            public IActionResult Prijava(KorisnickiRacunVM racun)
        {
            ModelState.Clear();
            var id = korisnickiRacunRepository.ProvjeraDjelatnikaPriPrijavi(racun.KorisnickoIme, racun.Lozinka);
            if ( id != 0)
            {
               
                HttpContext.Session.SetInt32("DjelatnikID", id);
                if (korisnickiRacunRepository.DohvatiUloguDjelatnika(id) == "Administrator")
                {
                    racun.DjelatnikId = id;
                    racun.Uloga = "Administrator";
                    HttpContext.Session.SetString("Uloga", racun.Uloga);
                    return RedirectToAction("Pocetna", racun); 
                }
                else
                {
                    racun.DjelatnikId = id;
                    racun.Uloga = "Djelatnik";
                    HttpContext.Session.SetString("Uloga", racun.Uloga);
                    TempData["index"] = false;
                    HttpContext.Session.SetString("potvrda", "false");
                    return RedirectToAction("Index","Zahtjevi");
                }
            }
            else
            {
                ViewBag.error = "Netočno korisničko ime ili lozinka!";
                return View();
            }
            
        }
    }
}
