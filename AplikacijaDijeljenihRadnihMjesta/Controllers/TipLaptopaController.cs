using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models;
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
    public class TipLaptopaController : Controller
    {
        private TipLaptopaRepository tipLaptopaRepository;

        public TipLaptopaController(AppDbContext db)
        {
            this.tipLaptopaRepository = new TipLaptopaRepository(db);
        }
        [HttpGet]
        public IActionResult Index()
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            return View(tipLaptopaRepository.DohvatiListuTipovaLaptopa());
        }

        //GET-CREATE
        public IActionResult Create()
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            return View();
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TipLaptopaVM tipLaptopa)
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            if (ModelState.IsValid)
            {
                if (tipLaptopaRepository.DodajNoviTipLaptopa(tipLaptopa))
                {
                    TempData["Uspješno"] = "Uspješno dodan novi model laptopa!";
                }
                else
                {
                    TempData["Neuspješno"] = "Željeni model već postoji!";
                }
            }
            ModelState.Clear();
            var noviLaptop = new TipLaptopaVM();
            return View(noviLaptop);
        }


        //GET-EDIT
        public IActionResult Edit(int? id)
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            if (id != null)
            {
                var tipLaptopa = tipLaptopaRepository.DohvatiTipLaptopaPoId(id);

                return View(new TipLaptopaVM { Id = tipLaptopa.Id, Model = tipLaptopa.Model });
            }
            return NotFound();
        }

        //POST-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TipLaptopaVM tipLaptopa)
        {

            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            if (ModelState.IsValid)
            {
                if (tipLaptopaRepository.EditTipLaptopa(tipLaptopa))
                {
                    TempData["Uspješno"] = "Uspješno promijenjen naziv modela laptopa!";
                }
                else
                {
                    TempData["Neuspješno"] = "Neuspješna promjena naziva modela! Pogledajte postoji li takav model laptopa.";
                }
            }
            ModelState.Clear();
            return View(tipLaptopa);
        }

        //GET-DELETE
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            if (id == null)
            { 
                return NotFound();
            }
            else
            {

                if (tipLaptopaRepository.IzbrisiTipLaptopa((int)id))
                {
                    TempData["Uspješno"] = "Uspješno brisanje modela laptopa!";
                }
                else {
                    TempData["Neuspješno"] = "Neuspješna brisanje modela jer postoje djelatnici koji ga koriste, dodijelite im drugi model laptopa pa onda obrišite!";
                    
                }
                //ModelState.Clear();
                return RedirectToAction("Index");
            }
        }


    }
}
