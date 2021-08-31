using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AplikacijaDijeljenihRadnihMjesta.Repository
{
    public class GradController : Controller
    {
        private GradRepository gradRepository;
        const string SessionGrad = "_GradID";

        public GradController(AppDbContext db)
        {
            this.gradRepository = new GradRepository(db);
        }

        [HttpGet]
        public IActionResult Index()
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            return View(gradRepository.DohvatiListuGradova());
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
        public IActionResult Create(GradVM grad)
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            if (ModelState.IsValid)
            {
                if (gradRepository.DodajNoviGrad(grad))
                {
                    TempData["Uspješno"] = "Uspješno dodan novi grad!";
                }
                else
                {
                    TempData["Neuspješno"] = "Grad već postoji!";
                }
            }
            ModelState.Clear();
            var noviGrad = new GradVM();
            return View(noviGrad);
        }


        //GET-EDIT
        public IActionResult Edit(int id)
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            if (id != 0)
            {
                var grad = gradRepository.DohvatiGradPoId(id);
                HttpContext.Session.SetInt32(SessionGrad, grad.Id);
                return View(new GradVM { Naziv=grad.Naziv, Oznaka=grad.Oznaka });
            }
            return NotFound();
        }

        //POST-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GradVM grad)
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            if (ModelState.IsValid)
            {
                var gradId = HttpContext.Session.GetInt32(SessionGrad);
                if (gradRepository.EditGrad(grad, (int)gradId))
                {
                    TempData["Uspješno"] = "Uspješno promijenjen naziv grada!";
                }
                else
                {
                    TempData["Neuspješno"] = "Neuspješna promjena naziva grada! Provjerite postoji li već grad s tim imenom.";
                }
            }
            ModelState.Clear();
            return View(grad);
        }

        //GET-DELETE
        public IActionResult Delete(int id)
        {
            TempData["ImePrezime"] = HttpContext.Session.GetString("ImePrezime");
            if (ModelState.IsValid)
            {
                if (gradRepository.IzbrisiGrad(id))
            {
                TempData["Uspješno"] = "Uspješno izbrisan grad!";
            }
            else
            {
                TempData["Neuspješno"] = "Neuspješno brisanje grada jer postoje povezane lokacije!";
            }
            }
            ModelState.Clear();
            return RedirectToAction("Index");
            
        }
    }
}
