using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class GradVM
    {

        public int Id
        {
            get; set;
        }

        [Required(ErrorMessage = "Obavezan unos naziva grada!")]
        public string Naziv
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos oznake grada!")]
        [MaxLength(4)]
        public string Oznaka //nisam postavila da bude jedinstveno
        {
            get; set;
        }
    }
}
