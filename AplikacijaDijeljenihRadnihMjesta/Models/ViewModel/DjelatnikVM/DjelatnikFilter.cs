using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class DjelatnikFilter
    {
        public IEnumerable<DjelatnikVM> ListaDjelatnika
        {
            get; set;
        }

        public List<SelectListItem> ListaUloga
        {
            get; set;
        }

        public int? Uloga
        {
            get; set;
        }

       
    }
}
