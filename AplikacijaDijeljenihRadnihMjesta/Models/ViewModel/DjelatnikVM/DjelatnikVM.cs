using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class DjelatnikVM
    {
        public int Id
        {
            get; set;
        }
        [RegularExpression(@"^(\d{9})$", ErrorMessage = "MBR mora imati devet znamenki.")]
        //[Required(ErrorMessage = "Obavezan unos MBR djelatnika!")]
        public int MBR
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos imena djelatnika!")]
        public string Ime
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos prezimena djelatnika!")]
        public string Prezime
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos korisničkog imena!")]
        public string KorisnickoIme
        {
            get; set;
        }
        
        public string Lozinka
        {
            get; set;
        }
        //[Required(ErrorMessage = "Obavezan unos maksimalnog broja dana koje djelatnik smije raditi u tvrtki!")]
        [Range(1, 5, ErrorMessage = "Maksimalan broj dana mora bit između 1 i 5")]
        public int MaxBrojDanaFirma
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos organizacijske jedinice kojoj pripada djelatnik!")]
        public string OrganizacijskaJedinica
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos modela laptopa koji posjeduje djelatnik!")]
        public string TipLaptopa
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos uloge djelatnika!")]
        public string Uloga
        {
            get; set;
        }
        public List<SelectListItem> OrganizacijskeJedinice
        {
            set; get;
        }
        [Required(ErrorMessage = "Obavezan unos e-mail-a djelatnika!")]
        public string Email
        {
            get; set;
        }
        public List<SelectListItem> ModeliLaptopa
        {
            set; get;
        }
        public List<SelectListItem> Uloge
        {
            set; get;
        }
    }
}
