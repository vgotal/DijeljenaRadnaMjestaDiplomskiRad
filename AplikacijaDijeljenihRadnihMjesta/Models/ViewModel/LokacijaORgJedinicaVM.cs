using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class LokacijaORgJedinicaVM
    {
        public int OrgJedID
        {
            get; set;
        }

        public string Adresa
        {
            get; set;
        }
        public List<SelectListItem> Adrese
        {
            set; get;
        }

        public string Grad
        {
            get; set;
        }
        
        public List<SelectListItem> Gradovi
        {
            set; get;
        }
      
    }
}
