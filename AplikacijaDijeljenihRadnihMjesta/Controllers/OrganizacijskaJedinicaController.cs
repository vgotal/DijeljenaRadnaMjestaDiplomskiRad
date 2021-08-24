using AplikacijaDijeljenihRadnihMjesta.Data;
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
    public class OrganizacijskaJedinicaController : Controller
    {
        private OrgJedinicaRepository orgJedinicaRepository;
        
        const string SessionOrgJed= "_OrgJedID";

        public OrganizacijskaJedinicaController(AppDbContext db)
        {
            this.orgJedinicaRepository = new OrgJedinicaRepository(db);
        }
        [HttpGet]
        public IActionResult Index()
        {
            //try
            //{

            //    return View(orgJedinicaRepository.DohvatiPopisOrgJedinica());
            //}
            //catch (Exception ex)
            //{
            //    Console.Write("Error info:" + ex.Message);
            //    return View()
            //}
            return View();
        }

        //GET-CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrgJedinicaVM orgJedinica)
        {
            if (orgJedinicaRepository.DodajNovuOrgJedinicu(orgJedinica))
            {
                TempData["Uspješno"] = "Uspješno dodana nova lokacija!";
            }
            else
            {
                TempData["Neuspješno"] = "Lokacija već postoji!";
            }
            ModelState.Clear();
            return View();
        }

        //GET-EDIT
        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                var orgJedinica = orgJedinicaRepository.DohvatiOrgJedinicuPoID(id);
                HttpContext.Session.SetInt32(SessionOrgJed, orgJedinica.Id);
                return View(new OrgJedinicaVM { Naziv = orgJedinica.Naziv });
            }
            return NotFound();
        }

        //POST-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OrgJedinicaVM orgJed)
        {
            if (ModelState.IsValid)
            {
                var orgJedID = HttpContext.Session.GetInt32(SessionOrgJed);
                if (orgJedinicaRepository.EditOrgJedinicu(orgJed, (int)orgJedID))
                {
                    TempData["Uspješno"] = "Uspješno promijenjen naziv organizacijske jedinice!";
                }
                else
                {
                    TempData["Neuspješno"] = "Neuspješna promjena naziva organizacijske jedinice!";
                }
            }
            ModelState.Clear();
            return View(orgJed);
        }

        //GET-DELETE
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                if (orgJedinicaRepository.IzbrisiOrgJedinicu(id))
                {
                    TempData["Uspješno"] = "Uspješno izbrisana organizacijska jedinica!";
                }
                else
                {
                    TempData["Neuspješno"] = "Neuspješno brisanje organizacijske jedinice jer u njoj postoje zaposlenici!";
                }
            }
            ModelState.Clear();
            return RedirectToAction("Index");
        }

        //GET-DODAJLOKACIJE
        public IActionResult DodajLokacije(int orgJedID)
        {
            TempData["OrgJedID"] = orgJedID;
            var lista = orgJedinicaRepository.DohvatiListuLokacija(orgJedID);
            HttpContext.Session.SetInt32(SessionOrgJed, orgJedID);
            lista.OrgJedID = orgJedID;
            return View(lista);
        }

        //POST-DODAJLOKACIJE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DodajLokacije(LokacijaORgJedinicaVM orgJedinica)
        {
            var orgJedID = HttpContext.Session.GetInt32(SessionOrgJed);
            TempData["OrgJedID"] = orgJedID;
            if (orgJedinicaRepository.DodajNovuLokacijuUOrgJedinicu(orgJedinica, (int)orgJedID))
            {
                TempData["Uspješno"] = "Uspješno dodana nova lokacija u organizacijsku jedinicu!";
            }
            else
            {
                TempData["Neuspješno"] = "Lokacija je već u sklopu organizacijske jedinice!";
            }
            ModelState.Clear();
            var lista = orgJedinicaRepository.DohvatiListuLokacija((int)orgJedID);
            return View(lista);
        }

       

    }
}
