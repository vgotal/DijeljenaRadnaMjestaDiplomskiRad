using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class LokacijaVM
    {
        public int Id
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos adrese lokacije!")]
        public string Adresa
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos grada u kojem se nalazi lokacija!")]
        public string Grad
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan odabir grada u kojem se nalazi lokacija!")]
        public List<SelectListItem> Gradovi
        {
            set; get;
        }
        public string OrgJedinica
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan odabir prganizacijske jedinice u koja se nalazi na lokaciji!")]
        public List<SelectListItem> OrgJedinice
        {
            set; get;
        }
        public string ListaOrganizacijskihJedinica
        {
            get; set;
        }

    }
}
