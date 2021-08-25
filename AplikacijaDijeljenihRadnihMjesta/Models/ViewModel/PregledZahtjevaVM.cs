 using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class PregledZahtjevaVM
    {
        public Boolean IsDisabled
        {
            get; set;
        }

        public int? Id
        {
            get; set;
        }
        public int? TipZahtjeva
        {
            get; set;
        }

        public DateTime? PocetniDatum
        {
            get; set;
        }

        public DateTime? KrajnjiDatum
        {
            get; set;
        }

        public List<ZahtjevVM> Zahtjevi
        {
            get; set;
        }

        public List<SelectListItem> TipoviZahtjeva
        {
            get; set;
        }

        public string povratnaInfo
        {
            get; set;
        }

     
    }
}
