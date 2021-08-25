using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class LokacijaFilter
    {
        public IEnumerable<LokacijaVM> ListaLokacija
        {
            get; set;
        }

        public List<SelectListItem> ListaGradova
        {
            get; set;
        }

        public int GradID
        {
            get; set;
        }
        public IEnumerable<string> ListaOrganizacijskihJedinica
        {
            get; set;
        }




    }
}
