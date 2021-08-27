using Microsoft.AspNetCore.Mvc;
using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using AplikacijaDijeljenihRadnihMjesta.Repository;
using Microsoft.AspNetCore.Http;
using AplikacijaDijeljenihRadnihMjesta.Models.Paginacija;

namespace AplikacijaDijeljenihRadnihMjesta.Controllers
{
    public class RadnoMjestoController : Controller
    {
        private RadnoMjestoRepository radnoMjestoRepository;
        const string SessionLokacija = "_LokacijaID";
        const string SessionRadnoMjesto = "_RadnoMjestoIDPrijeEdit";
        const string SessionOrgJedID = "_OrgJedinicaID";
        const string SessionModelLaptopa = "_ModelLaptopaID";

        public RadnoMjestoController(AppDbContext db)
        {
            this.radnoMjestoRepository = new RadnoMjestoRepository(db);
        }

        [HttpGet]
        public IActionResult Index(int LokacijaId, int? orgJedID, int pageNumber = 1, int pageSize = 4)
        {
            var radnaMjestaSPaginacijom = new PaginacijaRadnoMjesto();
            radnaMjestaSPaginacijom.radnoMjestoFilter = new RadnoMjestoFilter();
            TempData["LokacijaID"] = LokacijaId;
            var modelLaptopa = 0;
            HttpContext.Session.SetInt32(SessionLokacija, (int)LokacijaId);
            if (orgJedID != null && orgJedID != 0)
            {
                if (HttpContext.Session.GetInt32(SessionModelLaptopa) != null && HttpContext.Session.GetInt32(SessionModelLaptopa) != 0)
                {
                    modelLaptopa = (int)HttpContext.Session.GetInt32(SessionModelLaptopa);
                    radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID = (int)modelLaptopa;
                }

                HttpContext.Session.SetInt32(SessionOrgJedID, (int)orgJedID);
                TempData["OrgJedID"] = orgJedID;
            }

            if (LokacijaId != 0)
            {
                if (HttpContext.Session.GetInt32(SessionModelLaptopa) != null && HttpContext.Session.GetInt32(SessionModelLaptopa) != 0)
                {
                    modelLaptopa = (int)HttpContext.Session.GetInt32(SessionModelLaptopa);
                    radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID = (int)modelLaptopa;
                }
                if (modelLaptopa != 0)
                {
                    radnaMjestaSPaginacijom = radnoMjestoRepository.DohvatiListuRadnihMjesta_LokacijiDI_TipoviLaptopa(radnaMjestaSPaginacijom, (int)LokacijaId, (int)modelLaptopa, pageSize, pageNumber);
                }
                else
                    radnaMjestaSPaginacijom = radnoMjestoRepository.DohvatiListuRadnihMjestapoLokacijiDI(radnaMjestaSPaginacijom, (int)LokacijaId, pageSize, pageNumber);
            }

            else
            {
                if (HttpContext.Session.GetInt32(SessionModelLaptopa) != null && HttpContext.Session.GetInt32(SessionModelLaptopa) != 0)
                {
                    modelLaptopa = (int)HttpContext.Session.GetInt32(SessionModelLaptopa);
                    radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID = (int)modelLaptopa;
                }
                if (modelLaptopa != 0)
                {
                    radnaMjestaSPaginacijom = radnoMjestoRepository.DohvatiListuRadnihMjestailtriranihPoTipuLaptopa(radnaMjestaSPaginacijom, (int)modelLaptopa, pageSize, pageNumber);
                }
                else
                    radnaMjestaSPaginacijom = radnoMjestoRepository.DohvatiListuRadnihMjesta(radnaMjestaSPaginacijom, pageSize, pageNumber);
            }
            if (modelLaptopa != 0)
            {
                radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID = (int)modelLaptopa;
            }
                return View(radnaMjestaSPaginacijom);
        }
        [HttpPost]
        public IActionResult Index(PaginacijaRadnoMjesto radnaMjestaSPaginacijom,  int? lokacijaID, int? tipLaptopaID, int pageNumber = 1, int pageSize = 4)
        {

            if (lokacijaID != 0 && radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID == 0)
            {
                HttpContext.Session.SetInt32(SessionLokacija, (int)lokacijaID);
                HttpContext.Session.SetInt32(SessionModelLaptopa, 0);
                TempData["LokacijaID"] = lokacijaID;
                TempData["AdresaLokacije"] = radnoMjestoRepository.DohvatiAdresuLokacije((int)lokacijaID);
                radnaMjestaSPaginacijom = radnoMjestoRepository.DohvatiListuRadnihMjestapoLokacijiDI(radnaMjestaSPaginacijom, (int)lokacijaID, pageSize, pageNumber);
                TempData["OrgJedID"] = HttpContext.Session.GetInt32(SessionOrgJedID);
                return View(radnaMjestaSPaginacijom);

            }
            else if (lokacijaID == 0 && radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID!=0)
            {
                var tip = radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID;
                HttpContext.Session.SetInt32(SessionModelLaptopa, radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID);
                radnaMjestaSPaginacijom = radnoMjestoRepository.DohvatiListuRadnihMjestailtriranihPoTipuLaptopa(radnaMjestaSPaginacijom, (int)tipLaptopaID, pageSize, pageNumber);
                radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID = tip;
                return View(radnaMjestaSPaginacijom);
            }
            else if (lokacijaID != 0 && radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID != 0)
            {
                HttpContext.Session.SetInt32(SessionModelLaptopa, radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID);
                TempData["LokacijaID"] = lokacijaID;
                TempData["OrgJedID"] = HttpContext.Session.GetInt32(SessionOrgJedID);
                TempData["AdresaLokacije"] = radnoMjestoRepository.DohvatiAdresuLokacije((int)lokacijaID);
                radnaMjestaSPaginacijom = radnoMjestoRepository.DohvatiListuRadnihMjesta_LokacijiDI_TipoviLaptopa(radnaMjestaSPaginacijom, (int)lokacijaID, (int)tipLaptopaID, pageSize, pageNumber);
                radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID = radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID;
                return View(radnaMjestaSPaginacijom);
            }
            else
            {
                HttpContext.Session.SetInt32(SessionModelLaptopa, 0);
                radnaMjestaSPaginacijom = radnoMjestoRepository.DohvatiListuRadnihMjesta(radnaMjestaSPaginacijom, pageSize, pageNumber);
                radnaMjestaSPaginacijom.radnoMjestoFilter.TipLaptopaID = 0;
                return View(radnaMjestaSPaginacijom);
            }
                
        }

        //GET-CREATE
        public IActionResult Create(int LokacijaId, int? orgJedID)
        {
            var lista = new RadnoMjestoVM();
            lista.tipoviLaptopa = radnoMjestoRepository.DohvatiTipoveLaptopa();

            if (LokacijaId != 0)
            {
                TempData["LokacijaID"] = (int)HttpContext.Session.GetInt32(SessionLokacija);
                lista.Lokacije= radnoMjestoRepository.DohvatiLokacije(LokacijaId);
                lista.LokacijaId = LokacijaId;
            }
            if (LokacijaId == 0)
            {
                lista.Lokacije = radnoMjestoRepository.DohvatiLokacije();
            }
            if (orgJedID != 0 && orgJedID != null)
            {
                TempData["OrgJedID"] = orgJedID;
            }
            return View(lista);
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RadnoMjestoVM radnoMjesto, int? orgJedID, int? lokacijaID)
        {
            
            if (radnoMjesto.LokacijaId != 0)
            {
                radnoMjesto.LokacijaId = (int)HttpContext.Session.GetInt32(SessionLokacija);
                TempData["LokacijaID"] = radnoMjesto.LokacijaId; //za back
            }
            else
            {
                //TempData["LokacijaID"] = Int32.Parse(radnoMjesto.Lokacija);
            }
            TempData["OrgJedID"]= HttpContext.Session.GetInt32(SessionOrgJedID);
            if (ModelState.IsValid)
            {
                if (radnoMjesto.LokacijaId != 0)
                {
                    if (radnoMjestoRepository.DodajNovoRadnoMjesto_LokacijaID(radnoMjesto))
                    {
                        TempData["Uspješno"] = $"Uspješno dodano novo radno mjesto {radnoMjesto.Sifra}!";
                    }
                    else
                    {
                        TempData["Neuspješno"] = "Radno mjesto već postoji!";
                    }
                }
                else
                {
                    if (radnoMjestoRepository.DodajNovoRadnoMjesto(radnoMjesto))
                    {
                        TempData["Uspješno"] = $"Uspješno dodano novo radno mjesto {radnoMjesto.Sifra}!";
                    }
                    else
                    {
                        TempData["Neuspješno"] = "Radno mjesto već postoji!";
                    }
                }
            }
            ModelState.Clear();
            var lista = new RadnoMjestoVM();
            lista.tipoviLaptopa = radnoMjestoRepository.DohvatiTipoveLaptopa();
            if (lokacijaID != 0 && lokacijaID!=null)
            {
                TempData["LokacijaID"] = lokacijaID;
                lista.Lokacije = radnoMjestoRepository.DohvatiLokacije((int)lokacijaID);
                lista.LokacijaId = (int)lokacijaID;
            }
            if (lokacijaID == 0 || lokacijaID==null)
            {
                lista.Lokacije = radnoMjestoRepository.DohvatiLokacije();
            }
            return View(lista);
        }

        //GET-DELETE
        public IActionResult Delete(string Sifra, int? lokacijaID, int? orgJedID) 
        {
            TempData["OrgJedID"] = orgJedID;
            var lokID=0;
            if (ModelState.IsValid)
            {
                var radnoMjesto2 = radnoMjestoRepository.PronadjiRadnoMjesto(Sifra);
                lokID = radnoMjestoRepository.IzbrisiRadnoMjesto(radnoMjesto2);
                if (lokID!=0)
                {
                    TempData["Uspješno"] = "Uspješno izbrisano radno mjesto!";
                }
                else
                {
                    TempData["Neuspješno"] = "Neuspješno brisanje radnog mjesta! Radno mjesto mora biti nekorišteno kako bi se moglo obrisati.";
                }
            }
            ModelState.Clear();
            if (orgJedID != 0 && orgJedID != null && lokacijaID!=0 && lokacijaID!=null)
            {
                return RedirectToAction("Index", new
                {
                    LokacijaId = lokacijaID,
                    orgJedID = orgJedID
                });
            }
            else
            {
                return RedirectToAction("Index", new
                {
                    LokacijaId = lokacijaID,
                });
            }
           
        }
        

        //GET-EDIT
        public IActionResult Edit(string Sifra, int? orgJedID, int? lokacijaID) 
        {
            if (Sifra != null)
            {
                var radnoMjesto = radnoMjestoRepository.DohvatiRadnoMjestoPoSifri(Sifra);
                if (lokacijaID != 0 && lokacijaID != null)
                {
                    TempData["LokacijaID"] = lokacijaID; //za back
                }
                    var LokacijaID = radnoMjesto.LokacijaId;
                   
                if (orgJedID != 0)
                {
                    TempData["OrgJedID"] = orgJedID;
                }
                HttpContext.Session.SetInt32(SessionRadnoMjesto, radnoMjesto.Id);
                string str = radnoMjesto.Sifra;
                string[] znakoviZaIzbaciti = new string[] { "K", "P", "BR" };
                foreach (var c in znakoviZaIzbaciti)
                {
                    str = str.Replace(c, string.Empty);
                }
                string[] podjelaSifre = str.Split('-');
                var tipoviLaptopa = radnoMjestoRepository.DohvatiTipoveLaptopa();
                

                return View(new RadnoMjestoVM {Lokacija=LokacijaID.ToString(),Lokacije=radnoMjestoRepository.DohvatiLokacije() , tipoviLaptopa = tipoviLaptopa, TipLaptopa = radnoMjestoRepository.DohvatiModelLaptopa(radnoMjesto).ToString(), Kat = podjelaSifre[1].ToString(), Prostorija = podjelaSifre[2].ToString(), BrojRadnogMjesta = podjelaSifre[3].ToString(), LokacijaId = (radnoMjesto.LokacijaId) }); //drži podatke za prikaz ali klikom na uredi brišu se i uzimaju novi

            }
            return NotFound();
        }

        //POST-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RadnoMjestoVM radnomjesto)
        {
            TempData["OrgJedID"] = HttpContext.Session.GetInt32(SessionOrgJedID);
            var orgJedID = TempData["OrgJedID"];
            radnomjesto.LokacijaId = (int)HttpContext.Session.GetInt32(SessionLokacija);
            radnomjesto.Id = (int)HttpContext.Session.GetInt32(SessionRadnoMjesto);
            
            TempData["LokacijaID"] = (int)HttpContext.Session.GetInt32(SessionLokacija);

            if (radnoMjestoRepository.EditRadnoMjesto(radnomjesto))
                {
                    TempData["Uspješno"] = "Uspješno promijenjene informacije o radnom mjestu!";
                }
                else
                {
                    TempData["Neuspješno"] = "Neuspješna promjena informacija o radnom mjestu";
                }
            
            ModelState.Clear();
            radnomjesto.tipoviLaptopa = radnoMjestoRepository.DohvatiTipoveLaptopa();
            radnomjesto.Lokacije = radnoMjestoRepository.DohvatiLokacije();
            return View(radnomjesto);
        }



    }
}
