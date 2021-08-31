using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class OdobravanjeZahtjeva
    {
        public int Id
        {
            get; set;
        }

        public List<RezervacijeOtkazivanjeVM> RezervacijeOtkazivanje
        {
            get; set;
        }

        public List<string> DjelatnikEmail
        {
            get; set;
        }
        public int? Status
        {
            get; set;
        }
        
        public List<SelectListItem> Statusi
        {
            get; set;
        }

        public string povratnaInfo { get; set; }

    }
}
