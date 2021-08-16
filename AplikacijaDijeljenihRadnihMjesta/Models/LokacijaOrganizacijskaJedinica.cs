using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models
{
    public class LokacijaOrganizacijskaJedinica
    {
      
        public int LokacijeId
        {
            get; set;
        }
       
        public int OrganizacijskeJediniceId
        {
            get; set;
        }

        public Lokacija Lokacija
        {
            get; set;
        }

        public OrganizacijskaJedinica OrganizacijskaJedinica
        {
            get; set;
        }
    }
}
