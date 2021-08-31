using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using AplikacijaDijeljenihRadnihMjesta.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using AplikacijaDijeljenihRadnihMjesta.Models.Paginacija;

namespace AplikacijaDijeljenihRadnihMjesta.Controllers
{
    public class DjelatnikController : Controller
    {
        private DjelatnikRepository djelatnikRepository;
        private LokacijaRepository lokacijaRepository;
        const string SessionDjelatnik = "_DjelatnikMBR";
        const string SessionOrgJedID = "_OrgJedinicaID";
        const string SessionOrgJedinicaNaziv = "_OrgJedinicaNaziv";
        const string SessionUlogaDjelatnika = "_UlogaDjelatnika";
        const string SessionStaraLozinkaDjelatnika = "_StaraLozinkaDjelatnika";

        public DjelatnikController(AppDbContext db, IMailService mailServices)
        {
            this.djelatnikRepository = new DjelatnikRepository(db, mailServices);
            this.lokacijaRepository = new LokacijaRepository(db);
        }
        [HttpGet]
        public IActionResult Index(int? orgJedID, int pageNumber = 1, int pageSize = 4)
        {
            var djelatniciSPaginacijom = new PaginacijaDjelatnik();
            djelatniciSPaginacijom.djelatnikFilter = new DjelatnikFilter();
            var uloga=0;
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            HttpContext.Session.SetInt32(SessionOrgJedID, 0);
            if (orgJedID != 0 && orgJedID != null)
            {
                if (HttpContext.Session.GetInt32(SessionUlogaDjelatnika) != null && HttpContext.Session.GetInt32(SessionUlogaDjelatnika)!=0)
                {
                    uloga = (int)HttpContext.Session.GetInt32(SessionUlogaDjelatnika);
                    djelatniciSPaginacijom.djelatnikFilter.Uloga = uloga;
                }

                HttpContext.Session.SetInt32(SessionOrgJedID, (int)orgJedID);
                TempData["OrgJedID"] = orgJedID;
                TempData["NazivOrgJed"] = lokacijaRepository.DohvatiNazivOrgJedinice((int)orgJedID);
                HttpContext.Session.SetString(SessionOrgJedinicaNaziv, lokacijaRepository.DohvatiNazivOrgJedinice((int)orgJedID));
                djelatniciSPaginacijom.djelatnikFilter.ListaUloga = djelatnikRepository.DohvatiUlogeDjelatnika();
                djelatniciSPaginacijom= djelatnikRepository.DohvatiListuDjelatnikaOrgJedID(djelatniciSPaginacijom,(int)orgJedID, pageSize, pageNumber);
                return View(djelatniciSPaginacijom);
            }
            else
            {
                if (HttpContext.Session.GetInt32(SessionUlogaDjelatnika) != null && HttpContext.Session.GetInt32(SessionUlogaDjelatnika) != 0)
                {
                    uloga = (int)HttpContext.Session.GetInt32(SessionUlogaDjelatnika);
                    djelatniciSPaginacijom.djelatnikFilter.Uloga = uloga;
                }
                TempData["OrgJedID"] = null;
               
                djelatniciSPaginacijom = djelatnikRepository.DohvatiListuDjelatnika(djelatniciSPaginacijom, pageSize, pageNumber);
                djelatniciSPaginacijom.djelatnikFilter.ListaUloga = djelatnikRepository.DohvatiUlogeDjelatnika();
                if (uloga != 0)
                {
                    djelatniciSPaginacijom.djelatnikFilter.Uloga = uloga;
                }
               
                return View(djelatniciSPaginacijom);
            }
            
        }

        [HttpPost]
        public IActionResult Index(PaginacijaDjelatnik paginacijaDjelatnik, int? orgJedID, int pageNumber = 1, int pageSize = 4)
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            if (orgJedID != 0 && paginacijaDjelatnik.djelatnikFilter.Uloga == null)
            {
                if (paginacijaDjelatnik.djelatnikFilter.Uloga != null)
                {
                    HttpContext.Session.SetInt32(SessionUlogaDjelatnika, (int)paginacijaDjelatnik.djelatnikFilter.Uloga);
                }

                var nazivOrgJed = HttpContext.Session.GetString(SessionOrgJedinicaNaziv);
                HttpContext.Session.SetInt32(SessionOrgJedID, (int)orgJedID);
                TempData["OrgJedID"] = orgJedID;
                TempData["NazivOrgJed"] = nazivOrgJed;
                paginacijaDjelatnik = djelatnikRepository.DohvatiListuDjelatnikaOrgJedID(paginacijaDjelatnik, (int)orgJedID, pageSize, pageNumber);
                return View(paginacijaDjelatnik);
            }
            else if (orgJedID == 0 && paginacijaDjelatnik.djelatnikFilter.Uloga != null)
            {
                if (paginacijaDjelatnik.djelatnikFilter.Uloga != null)
                {
                    HttpContext.Session.SetInt32(SessionUlogaDjelatnika, (int)paginacijaDjelatnik.djelatnikFilter.Uloga);
                }
                paginacijaDjelatnik = djelatnikRepository.DohvatiListuDjelatnikaFiltriranuPoUlozi(paginacijaDjelatnik, (int)paginacijaDjelatnik.djelatnikFilter.Uloga, pageSize, pageNumber);
                return View(paginacijaDjelatnik);
            }
            else if (orgJedID != 0 && paginacijaDjelatnik.djelatnikFilter.Uloga != null)
            {
                if (paginacijaDjelatnik.djelatnikFilter.Uloga != null)
                {
                    HttpContext.Session.SetInt32(SessionUlogaDjelatnika, (int)paginacijaDjelatnik.djelatnikFilter.Uloga);
                }
                var nazivOrgJed = HttpContext.Session.GetString(SessionOrgJedinicaNaziv);
                TempData["OrgJedID"] = orgJedID;
                TempData["NazivOrgJed"] = nazivOrgJed;
                paginacijaDjelatnik = djelatnikRepository.DohvatiListuDjelatnikaFiltriranuPoUloziOrgJed(paginacijaDjelatnik, (int)orgJedID, (int)paginacijaDjelatnik.djelatnikFilter.Uloga, pageSize, pageNumber);
                return View(paginacijaDjelatnik);
            }
            else
            {
                paginacijaDjelatnik = djelatnikRepository.DohvatiListuDjelatnika(paginacijaDjelatnik, pageSize, pageNumber);
                paginacijaDjelatnik.djelatnikFilter.Uloga = null;
                HttpContext.Session.SetInt32(SessionUlogaDjelatnika, 0);
                return View(paginacijaDjelatnik);
            }
                
        }
        [HttpGet]
         public IActionResult Create(int orgJedID)
         {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            TempData["OrgJedID"] = orgJedID;
            var djelatnik = new DjelatnikVM();
            djelatnik = djelatnikRepository.PopuniFiltereSPodatcima(orgJedID, djelatnik);
            return View(djelatnik);
         }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DjelatnikVM djelatnik)
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            var orgJedID = HttpContext.Session.GetInt32(SessionOrgJedID);
            if (ModelState.IsValid)
            {
                if (djelatnikRepository.DodajNovogDjelatnika(djelatnik))
                {
                    HttpContext.Session.SetInt32(SessionDjelatnik, djelatnik.MBR);
                    TempData["Uspješno"] = "Uspješno dodan novi djelatnik!";
                }
                else
                {
                    TempData["Neuspješno"] = "Djelatnik s tim podatcima već postoji!";
                }
            }
            ModelState.Clear();
            var noviDjelatnik = new DjelatnikVM();
            noviDjelatnik = djelatnikRepository.PopuniFiltereSPodatcima(orgJedID, noviDjelatnik);
            return View(noviDjelatnik);
        }

        //GET-EDIT
        public IActionResult Edit(int? id, int? orgJedID)
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            TempData["OrgJedID"] = orgJedID;
            var djelatnik = djelatnikRepository.DohvatiDjelatnika((int)id);
            var modeliLaptopa= djelatnikRepository.PopuniListuModeliLaptopa();
            var uloge= djelatnikRepository.PopuniListuUloga();
            var orgJedinice=new List<SelectListItem>();
            if (orgJedID != 0)
            {
                orgJedinice=djelatnikRepository.PopuniListuOrgJedinica((int)orgJedID);
            }
            else
            {
                orgJedinice = djelatnikRepository.PopuniListuOrgJedinica();
            }
            HttpContext.Session.SetString(SessionStaraLozinkaDjelatnika, djelatnik.Lozinka);
            var djelatnikPrikaz = new DjelatnikVM { Id = djelatnik.Id, MBR = djelatnik.MBR, Ime = djelatnik.Ime, Prezime = djelatnik.Prezime, MaxBrojDanaFirma=djelatnik.MaxBrojDanaFirma, Uloga=djelatnik.UlogaID.ToString(), OrganizacijskeJedinice= orgJedinice, ModeliLaptopa = modeliLaptopa, TipLaptopa =djelatnik.TipLaptopaId.ToString(),Uloge= uloge, OrganizacijskaJedinica =djelatnik.OrgJedinicaId.ToString(), KorisnickoIme = djelatnik.KorisnickoIme, Email=djelatnik.Email};
            
            
            return View(djelatnikPrikaz);

        }

        //POST-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DjelatnikVM djelatnik)
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            TempData["OrgJedID"] = HttpContext.Session.GetInt32(SessionOrgJedID);
            var orgJedID = TempData["OrgJedID"];
            var staraLozinka = HttpContext.Session.GetString(SessionStaraLozinkaDjelatnika);
            if (djelatnikRepository.EditDjelatnik(djelatnik, staraLozinka))
            {
                TempData["Uspješno"] = "Uspješno promijenjene informacije o djelatniku!";
            }
            else
            {
                TempData["Neuspješno"] = "Neuspješna promjena informacija o djelatniku, taj djelatnik već postoji!";
            }

            ModelState.Clear();
            djelatnik.ModeliLaptopa= djelatnikRepository.PopuniListuModeliLaptopa();
            djelatnik.Uloge = djelatnikRepository.PopuniListuUloga();
            if ((int)orgJedID != 0)
            {
               djelatnik.OrganizacijskeJedinice= djelatnikRepository.PopuniListuOrgJedinica((int)orgJedID);
            }
            else
            {
                djelatnik.OrganizacijskeJedinice = djelatnikRepository.PopuniListuOrgJedinica();
            }
            return View(djelatnik);
        }

        [HttpPost]
        public IActionResult Delete(int id, int? orgJedID)
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            TempData["OrgJedID"] = orgJedID;
            if (ModelState.IsValid)
            {
                if (djelatnikRepository.IzbrisiDjelatnika(id))
                {
                    TempData["Uspješno"] = "Uspješno izbrisan djelatnik!";
                }
                else
                {
                    TempData["Neuspješno"] = "Neuspješno brisanje djelatnika!";
                }
            }
            ModelState.Clear();
            
            if (TempData["OrgJedID"] != null)
            {
                return RedirectToAction("Index", new
                {
                    orgJedID = orgJedID

                });
            }
            return RedirectToAction("Index");

        }

     

    }
    }


