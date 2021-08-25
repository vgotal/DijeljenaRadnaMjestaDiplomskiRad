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
    public class LokacijaController : Controller
    {
        private LokacijaRepository lokacijaRepository;
        const string SessionOrgJedinica = "_OrgJedinicaID";
        const string SessionOrgJedinicaNaziv = "_OrgJedinicaNaziv";
        const string SessionStaraOrgJedinicaID = "_StaraOrgJedinicaID";


        public LokacijaController(AppDbContext db)
        {
            this.lokacijaRepository = new LokacijaRepository(db);
        }
        [HttpGet]
        public IActionResult Index(int? orgJedID, string nazivOrgJed)
        {

            if (orgJedID != 0 && orgJedID != null)
            {
                HttpContext.Session.SetInt32(SessionOrgJedinica, (int)orgJedID);
                TempData["OrgJedID"] = orgJedID;
                TempData["NazivOrgJed"] = lokacijaRepository.DohvatiNazivOrgJedinice((int)orgJedID);
                HttpContext.Session.SetString(SessionOrgJedinicaNaziv, lokacijaRepository.DohvatiNazivOrgJedinice((int)orgJedID));
                return View(lokacijaRepository.DohvatiListuLokacija((int)orgJedID));
            }
            else
            {
                TempData["OrgJedID"] = null;
                var lok = new LokacijaFilter();
                lok = lokacijaRepository.DohvatiListuLokacija();
                foreach (var org in lok.ListaLokacija)
                {
                    var lista= lokacijaRepository.DohvatiOrgJedNaLokaciji(org.Id);
                    foreach (var l in lista)
                    {
                        org.ListaOrganizacijskihJedinica += "-";
                        org.ListaOrganizacijskihJedinica +=( l + "\r\n");
                        //org.ListaOrganizacijskihJedinica += System.Environment.NewLine;
                    }

                    //org.ListaOrganizacijskihJedinica.Remove(org.ListaOrganizacijskihJedinica.Length - 2);
                }
                return View(lok);
            }
                
        }
        [HttpPost]
        public IActionResult Index(int? orgJedID, int? GradID)
        {

            if (orgJedID != 0 && GradID == null)
            {
                var nazivOrgJed = HttpContext.Session.GetString(SessionOrgJedinicaNaziv);
                HttpContext.Session.SetInt32(SessionOrgJedinica, (int)orgJedID);
                TempData["OrgJedID"] = orgJedID;
                TempData["NazivOrgJed"] = nazivOrgJed;
                return View(lokacijaRepository.DohvatiListuLokacija((int)orgJedID));
            }
            else if (orgJedID == 0 && GradID != null )
            {
                return View(lokacijaRepository.DohvatiListuLokacijaFiltriranuPoGradu((int)GradID));
            }
            else if (orgJedID != 0 && GradID != null )
            {
                var nazivOrgJed=HttpContext.Session.GetString(SessionOrgJedinicaNaziv);
                TempData["OrgJedID"] = orgJedID;
                TempData["NazivOrgJed"] = nazivOrgJed;
                return View(lokacijaRepository.DohvatiListuLokacija((int)orgJedID, (int)GradID));
            }
            else
                return View(lokacijaRepository.DohvatiListuLokacija());
        }

        //GET-CREATE
        public IActionResult Create(int? orgJedID)
        {
            TempData["OrgJedID"] = HttpContext.Session.GetInt32(SessionOrgJedinica);

            var lista = new LokacijaVM();
            lokacijaRepository.DohvatiGradove(lista);
            if (orgJedID != 0)
            {
                lokacijaRepository.DohvatiOrgJedinice(lista,(int)orgJedID);
            }
            else
            {
                lokacijaRepository.DohvatiOrgJedinice(lista);
            }
            return View(lista);
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LokacijaVM lokacija)
        {
            if (lokacijaRepository.DodajNovuLokaciju(lokacija))
            {
                TempData["OrgJedID"] = HttpContext.Session.GetInt32(SessionOrgJedinica);
                TempData["Uspješno"] = "Uspješno dodana nova lokacija!";
            }
            else
            {
                TempData["Neuspješno"] = "Lokacija već postoji!";
            }
            ModelState.Clear();
            var lista = new LokacijaVM();
            lista.Gradovi = lokacijaRepository.DohvatiGradove();
            if (TempData["OrgJedID"] != null)
            {
                lokacijaRepository.DohvatiOrgJedinice(lista, (int)TempData["OrgJedID"]);
            }
            else
                lokacijaRepository.DohvatiOrgJedinice(lista);
            return View(lista);
        }

        //GET-DELETE
        public IActionResult Delete(int? id, int? orgJedID)
        {
            TempData["OrgJedID"] = HttpContext.Session.GetInt32(SessionOrgJedinica);
            var nazivOrgJed = HttpContext.Session.GetString(SessionOrgJedinicaNaziv);
            if (ModelState.IsValid)
            {
                if (orgJedID != null)
                {
                    if (lokacijaRepository.IzbrisiLokacijuUnutarOrgJedinice((int)id, (int)orgJedID))
                    {
                        TempData["Uspješno"] = $"Uspješno izbrisana lokacija iz organizacijske jedinice: {nazivOrgJed}!";
                    }
                    else
                    {
                        TempData["Neuspješno"] = $"Neuspješno brisanje lokacije iz organizacijske jedinice:{nazivOrgJed} !";
                    }
                }
                else
                {
                    if (lokacijaRepository.IzbrisiLokaciju((int)id))
                    {
                        TempData["Uspješno"] = "Uspješno izbrisana lokacija!";
                    }
                    else
                    {
                        TempData["Neuspješno"] = "Neuspješno brisanje lokacije, pogledajte postoje li radna mjesta na toj lokaciji prije brisanja !";
                    }
                }
            }
            ModelState.Clear();
            if (orgJedID != 0)
            {
                return RedirectToAction("Index", new
                {
                    orgJedID = orgJedID
                });
            }
            return RedirectToAction("Index");
        }

        //public IActionResult DohvatiOrgJed(int? id, int? orgJedID)
        //{
        //    TempData["OrgJedID"] = HttpContext.Session.GetInt32(SessionOrgJedinica);
        //    var nazivOrgJed = HttpContext.Session.GetString(SessionOrgJedinicaNaziv);

        //    var lista=lokacijaRepository.DohvatiOrgJedNaLokaciji((int)id);
        //    var orgJedinice = new LokacijaFilter();
        //    orgJedinice.ListaOrganizacijskihJedinica = lista;
        //    if (orgJedID != 0)
        //    {
        //        return RedirectToAction("Index", new
        //        {
        //            orgJedID = orgJedID
        //        });
        //    }
        //    return RedirectToAction("Index");
        //}

        //GET-EDIT
        public IActionResult Edit(int? id, int? orgJedID)
        {
            var lokacija = lokacijaRepository.DohvatiLokacijuPoId(id); //lokacija iz baze
            var gradovi = lokacijaRepository.DohvatiGradove();
            if (id != null)
            {
                if (orgJedID != 0 && orgJedID!=null)
                {
                    HttpContext.Session.SetInt32(SessionStaraOrgJedinicaID, (int)orgJedID);
                    var OrgJed = lokacijaRepository.DohvatiOrgJedinice((int)orgJedID);
                    TempData["OrgJedID"] = HttpContext.Session.GetInt32(SessionOrgJedinica);
                    return View(new LokacijaVM { Id = lokacija.Id, Adresa = lokacija.Adresa, Gradovi = gradovi, Grad = lokacijaRepository.DohvatiNazivGrada(lokacija), OrgJedinica = lokacijaRepository.DohvatiNazivOrgJedinice((int)orgJedID), OrgJedinice = OrgJed, });
                }
                else
                {
                    return View(new LokacijaVM { Id = lokacija.Id, Adresa = lokacija.Adresa, Gradovi = gradovi, Grad = lokacijaRepository.DohvatiNazivGrada(lokacija), OrgJedinica = lokacijaRepository.DohvatiNazivOrgJedinicePoID(lokacija.Id), OrgJedinice= lokacijaRepository.DohvatiOrgJedinice() });
                }
            }
            return NotFound();
        }



        //POST-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LokacijaVM lokacija)
        {
            TempData["OrgJedID"] = HttpContext.Session.GetInt32(SessionOrgJedinica);
            var orgJedID = TempData["OrgJedID"];
            if (lokacijaRepository.EditLokacija(lokacija))
                {
                    TempData["Uspješno"] = "Uspješno promijenjene informacije o lokaciji!";
                }
            else
                {
                    TempData["Neuspješno"] = "Neuspješna promjena informacija o lokaciji, ta lokacija već postoji";
                }
            ModelState.Clear();
            lokacija.Gradovi = lokacijaRepository.DohvatiGradove();
            if (TempData["OrgJedID"]!=null )
            {
                lokacija.OrgJedinice = lokacijaRepository.DohvatiOrgJedinice((int)orgJedID);
            }
            else
            {
                lokacija.OrgJedinice = lokacijaRepository.DohvatiOrgJedinice();
            }
            return View(lokacija);
        }


    }
}

