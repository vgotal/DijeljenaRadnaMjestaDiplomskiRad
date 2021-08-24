using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class OrgJedinicaVM
    {
        public int Id
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos naziva organizacijske jedinice!")]
        public string Naziv
        {
            get; set;
        }

        //public string Lokacija
        //{
        //    get; set;
        //}
        //public List<SelectListItem> Lokacije
        //{
        //    set; get;
        //}
        //public List<SelectListItem> Djelatnici
        //{
        //    set; get;
        //}

        public int BrojDjelatnika
        {
            get; set;
        }
        public int BrojLokacija
        {
            get; set;
        }
    }
}
