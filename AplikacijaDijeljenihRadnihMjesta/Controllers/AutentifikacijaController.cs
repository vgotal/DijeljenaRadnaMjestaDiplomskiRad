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
                TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            }
            else
            {
                TempData["index"] = false;
                TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
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
            //ModelState.Clear();
            if (ModelState.IsValid)
            {

                var id = korisnickiRacunRepository.ProvjeraDjelatnikaPriPrijavi(racun.KorisnickoIme, racun.Lozinka);
                if (id != 0)
                {

                    HttpContext.Session.SetInt32("DjelatnikID", id);
                    if (korisnickiRacunRepository.DohvatiUloguDjelatnika(id) == "Administrator")
                    {
                        racun.DjelatnikId = id;
                        racun.Uloga = "Administrator";
                        racun.ImePrezime = korisnickiRacunRepository.DohvatiImeIPrezime(id);
                        HttpContext.Session.SetString("Uloga", racun.Uloga);
                        HttpContext.Session.SetString("ImePrezime", racun.ImePrezime);
                        return RedirectToAction("Pocetna", racun);
                    }
                    else
                    {
                        racun.DjelatnikId = id;
                        racun.Uloga = "Djelatnik";
                        racun.ImePrezime = korisnickiRacunRepository.DohvatiImeIPrezime(id);
                        HttpContext.Session.SetString("Uloga", racun.Uloga);
                        TempData["index"] = false;
                        HttpContext.Session.SetString("potvrda", "false");
                        HttpContext.Session.SetString("ImePrezime", racun.ImePrezime);
                        return RedirectToAction("Index", "Zahtjevi");
                    }


                }
                else
                {
                    TempData["Neuspješno"] = "Netočno korisničko ime i/ili lozinka! Provjerite svoje podatke";
                    return View(racun);
                }

            }
            else { return View(racun); }
            
        }
    }
}
