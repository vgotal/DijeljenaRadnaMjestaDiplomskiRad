using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models
{
    public class TipZahtjeva
    {
        public int Id{get; set;}

        public string Tip{get; set;}

        public IEnumerable<Zahtjev> Zahtjevi{get; set;}
    }
}

