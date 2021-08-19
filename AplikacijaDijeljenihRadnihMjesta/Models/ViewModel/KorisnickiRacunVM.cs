using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class KorisnickiRacunVM
    {
        [Key]
        [Required(ErrorMessage = "Obavezan unos korisničkog imena!")]
        public string KorisnickoIme
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos lozinke!")]
        public string Lozinka
        {
            get; set;
        }
        public int DjelatnikId
        {
            get; set;
        }
        
        [TempData]
        public string Uloga
        {
            get; set;
        }
        public int BrojDjelatnika
        {
            get; set;
        }
        public int BrojLaptopa
        {
            get; set;
        }

        public int BrojRadnihMjesta
        {
            get; set;
        }

        public int Gradovi
        {
            get; set;
        }

        public int Lokacije
        {
            get; set;
        }

        public int OrganizacijskeJedinice
        {
            get; set;
        }
    }
}
