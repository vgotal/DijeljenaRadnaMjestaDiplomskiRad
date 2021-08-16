using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models
{
    public class Djelatnik
    {
        public int Id
        {
            get; set;
        }

        public int MBR
        {
            get; set;
        }

        public string Ime
        {
            get; set;
        }

        public string Prezime
        {
            get; set;
        }

        public OrganizacijskaJedinica OrgJedinica
        {
            get; set;
        }
        public int OrgJedinicaId
        {
            get; set;
        }

        public TipLaptopa TipLaptopa
        {
            get; set;
        }

        public int TipLaptopaId
        {
            get; set;
        }

        public int MaxBrojDanaFirma
        {
            get; set;
        }

        public string KorisnickoIme
        {
            get; set;
        }

        public string Lozinka
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }
        public int UlogaID
        {
            get; set;
        }

        public Uloga Uloga
        {
            get; set;
        }

    }
}
