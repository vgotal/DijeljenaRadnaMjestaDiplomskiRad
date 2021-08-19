using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class RezervacijaModel
    {
        public int Id
        {
            get; set;
        }
        public DateTime ZeljeniDatum
        {
            get; set;
        }


        public string SifraRadnogMjesta
        {
            get; set;
        }

        public bool OdgovorCheckBox
        {
            get; set;
        }

        public bool Rezervirano
        {
            get; set;
        }
        public bool Otkazano
        {
            get; set;
        }
        public int LokacijaID
        {
            get; set;
        }
        public string Adresa
        {
            get; set;
        }
        public bool ZauzetoRadnoMjesto
        {
            get; set;
        }

        public string Status
        {
            get; set;
        }
        public bool OtkazivanjeZahtjeva
        {
            get; set;
        }

        public string RazlogOtkazivanja
        {
            get; set;
        }
        public string Komentar
        {
            get; set;
        }

    }
}
