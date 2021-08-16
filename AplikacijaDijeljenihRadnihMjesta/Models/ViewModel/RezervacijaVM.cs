using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class RezervacijaVM
    {
        public Boolean IskljuciPodnosenjeZahtjeva
        {
            get; set;
        }
        public string Lokacija
        {
            get; set;
        }
        public string Adresa
        {
            get; set;
        }

        public string Datumi
        {
            get; set;
        }
        public List<SelectListItem> Lokacije
        {
            get; set;
        }

        public List<RezervacijaModel> Rezervacije
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
