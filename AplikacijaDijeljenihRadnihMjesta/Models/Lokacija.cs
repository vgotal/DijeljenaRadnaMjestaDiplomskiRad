using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models
{
    public class Lokacija
    {
        public int Id{get; set;}
        public string Adresa{get; set;}
        public IEnumerable<OrganizacijskaJedinica> OrganizacijskeJedinice{get; set;}
        public IEnumerable<LokacijaOrganizacijskaJedinica> LokacijaOrganizacijskaJedinica {get; set;}
        public Grad Grad{get; set;}
        public int GradId{get; set;}
        public IEnumerable<RadnoMjesto> RadnaMjesta{get; set;}
    }
}
