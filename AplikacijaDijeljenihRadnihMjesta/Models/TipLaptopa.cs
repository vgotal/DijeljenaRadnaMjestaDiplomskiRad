using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models
{
    public class TipLaptopa
    {
        public int Id
        {
            get; set;
        }

        public string Model
        {
            get; set;
        }

        public IEnumerable<Djelatnik> Djelatnici
        {
            get; set;
        }

        public IEnumerable<RadnoMjesto> RadnaMjesta
        {
            get; set;
        }
    }
}
