using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class RadnoMjestoFilter
    {
        public IEnumerable<RadnoMjestoVM> ListaRadnihMjesta
        {
            get; set;
        }
        public List<SelectListItem> ListaTipovaLaptopa
        {
            get; set;
        }

        public int TipLaptopaID
        {
            get; set;
        }
    }
}
