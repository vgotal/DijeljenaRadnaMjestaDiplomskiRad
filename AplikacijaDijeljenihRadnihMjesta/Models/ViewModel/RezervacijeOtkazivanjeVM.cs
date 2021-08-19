using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class RezervacijeOtkazivanjeVM
    {
        public int Id
        {
            get; set;
        }

        public DateTime Datum
        {
            get; set;
        }

        public int RadnoMjestoId
        {
            get; set;
        }

        public RadnoMjesto RadnoMjesto
        {
            get; set;
        }

        public int? ProvjeraOtkazivanjaRezerviranja // sluzi kada smo rezervirali zahtjev i onda ga otkazujemo
        {
            get; set;
        }

        public int ZahtjevId
        {
            get; set;
        }

        public Zahtjev Zahtjev
        {
            get; set;
        }

        public string ImeIPrezime
        {
            get; set;
        }

        public string TipZahtjeva
        {
            get; set;
        }

        public bool OdgovorCheckBoxOdobreno
        {
            get; set;
        }
        public bool OdgovorCheckBoxOtkazano
        {
            get; set;
        }

        public bool Odobreno
        {
            get; set;
        }

        public string Status
        {
            get; set;
        }
       
        public string DjelatnikEmail
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
