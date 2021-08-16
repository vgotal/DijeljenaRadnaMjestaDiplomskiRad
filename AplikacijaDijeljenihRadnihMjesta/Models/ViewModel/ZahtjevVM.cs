using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class ZahtjevVM
    {
        [Key]
        public int ZahtjevID
        {
            get; set;
        }
        public DateTime ZahtjevDatum
        {
            get; set;
        }
        public int DjelatnikID
        {
            get; set;
        }
        public string Tip
        {
            get; set;
        }
    }
}
