using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models
{
    public class OrganizacijskaJedinica
    {
        public int Id{get; set;}

        public string Naziv{get; set;}

        public IEnumerable<Lokacija> Lokacije{get; set;}

        public IEnumerable<Djelatnik> Djelatnici{get; set;}
        public IEnumerable<LokacijaOrganizacijskaJedinica> LokacijaOrganizacijskaJedinica
        {
            get; set;
        }
    }
}
