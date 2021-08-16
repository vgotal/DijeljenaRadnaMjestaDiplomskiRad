using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class TipLaptopaVM
    {
        public int Id
        {
            get; set;
        }

        [Required(ErrorMessage ="Obavezan unos modela laptopa!")]
        public string Model
        {
            get; set;
        }

    }
}
