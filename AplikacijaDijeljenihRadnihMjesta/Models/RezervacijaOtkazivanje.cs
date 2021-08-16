using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models
{
    public class RezervacijaOtkazivanje
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
        public Status Status
        {
            get; set;
        }

        public int StatusId
        {
            get; set;
        }

        public bool OtkazivanjeZahtjeva
        {
            get; set;
        }
    }
}
