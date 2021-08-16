using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class AdminVM
    {
        public Djelatnik Djelatnik
        {
            get; set;
        }

        public Uloga Uloga
        {
            get; set;
        }

        public OrganizacijskaJedinica OrganizacijskaJedinica
        {
            get; set;
        }

        public TipLaptopa TipLaptopa
        {
            get; set;
        }
    }
}
