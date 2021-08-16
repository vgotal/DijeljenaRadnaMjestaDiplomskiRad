using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models
{
    public class Uloga
    {
        public int Id
        {
            get; set;
        }

        public string Naziv

        {
            get; set;
        }

        public IEnumerable<Djelatnik> Djelatnici
        {
            get; set;
        }
    }
}
