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
    public class OtkazivanjeController : Controller
    {
        private OtkazivanjeRepository otkazivanjeRepository;
        public OtkazivanjeController(AppDbContext db)
        {
            this.otkazivanjeRepository = new OtkazivanjeRepository(db);
        }

        //public IActionResult Index()
        //{
        //    ModelState.Clear();
        //    var djelatnikID = HttpContext.Session.GetInt32("DjelatnikID");
        //    var djelatnikUloga = otkazivanjeRepository.DohvatiDjelatnikovuUlogu((int)djelatnikID);
        //    if (djelatnikUloga == "Administrator")
        //    {
        //        TempData["uloga"] = "ADMIN";
        //    }
        //    else
        //        TempData["uloga"] = "DJELATNIK";
        //    var otkazivanje = new OtkazivanjeVM();
        //    return View(otkazivanje);
        //}
        //[HttpPost]
        public IActionResult Index(OtkazivanjeVM otkazivanje)
        {
            var djelatnikID = HttpContext.Session.GetInt32("DjelatnikID");
            var djelatnikUloga = otkazivanjeRepository.DohvatiDjelatnikovuUlogu((int)djelatnikID);
            if (djelatnikUloga == "Administrator")
            {
                TempData["uloga"] = "ADMIN";
            }
            else
                TempData["uloga"] = "DJELATNIK";

            otkazivanje = otkazivanjeRepository.DohvatiMogucaOtkazivanja(otkazivanje, (int)djelatnikID);
            if (otkazivanje.PovratnaInfo != null)
            {
                TempData["Info"] = otkazivanje.PovratnaInfo;
            }
            if (otkazivanje.PovratnaInfoUspjeh != null)
            {
                TempData["Uspješno"] = otkazivanje.PovratnaInfoUspjeh;
            }
            if (otkazivanje.PovratnaInfoNeuspjeh != null)
            {
                TempData["Neuspješno"] = otkazivanje.PovratnaInfoNeuspjeh;
            }

            return View(otkazivanje);
        }

    }
}
