using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models
{
    public class Status
    {
        public int Id
        {
            get; set;
        }

        public string Tip
        {
            get; set;
        }

        public IEnumerable<RezervacijaOtkazivanje> RezervacijeOtkazivanje
        {
            get; set;
        }
    }
}
