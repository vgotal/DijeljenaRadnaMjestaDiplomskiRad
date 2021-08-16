using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models
{
    public class Zahtjev
    {
        public int Id{get; set;}

        public DateTime Datum{get; set;}

        public int TipZahtjevaId{get; set;}

        public TipZahtjeva TipZahtjeva{get; set;}

        public int DjelatnikId{get; set;}

        public Djelatnik Djelatnik{get; set;}

        public IEnumerable<RezervacijaOtkazivanje> RezervacijaOtkazivanje{get; set;}
    }
}
   