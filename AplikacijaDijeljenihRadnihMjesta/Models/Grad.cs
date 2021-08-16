using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models
{
    public class Grad
    {
        public int Id
        {
            get; set;
        }

        public string Naziv
        {
            get; set;
        }

        public string Oznaka
        {
            get; set;
        }

        public IEnumerable<Lokacija> Lokacije
        {
            get; set;
        }
    }
}
