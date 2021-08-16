using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class DetaljiVM
    {
        [Key]
        public int ID
        {
            get; set;
        }
        public DateTime Datum
        {
            get; set;
        }
        public string Sifra
        {
            get; set;
        }
        public string TipLaptopa
        {
            get; set;
        }
        public string Grad
        {
            get; set;
        }
        public string Adresa
        {
            get; set;
        }
        public List<DetaljiVM> Detalji
        {
            get; set;
        }

        public string Status
        {
            get; set;
        }
    }
}
