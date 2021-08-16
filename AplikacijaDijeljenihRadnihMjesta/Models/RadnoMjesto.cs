using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models
{
    public class RadnoMjesto
    {
        public int Id
        {
            get; set;
        }

        public string Sifra
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
        public Lokacija Lokacija
        {
            get; set;
        }

        public int LokacijaId
        {
            get; set;
        }
    }
    }
