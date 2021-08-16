using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class OtkazivanjeVM
    {
        public int? LokacijaID
        {
            get; set;
        }

        public List<RezervacijaModel> Rezervacije
        {
            get; set;
        }

        public String Adresa
        {
            get; set;
        }

        public string PovratnaInfoUspjeh
        {
            get; set;
        }
        public string PovratnaInfoNeuspjeh
        {
            get; set;
        }

        public string PovratnaInfo
        {
            get; set;
        }
    }
}
