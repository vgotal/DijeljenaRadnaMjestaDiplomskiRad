using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public class RadnoMjestoVM
    {
        public string Sifra
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos modela laptopa!")]
        public string TipLaptopa
        {
            get; set;
        }

        public string Adresa
        {
            get; set;
        }
       
        public string Grad
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos kata na kojem se nalazi radno mjesto!")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Kat mora imati numeričku vrijednost!")]
        public string Kat
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos prostorije u kojoj se nalazi radno mjesto!")]
        public string Prostorija
        {
            get; set;
        }
        [Required(ErrorMessage = "Obavezan unos broja radnog mjesta!")]
        public string BrojRadnogMjesta
        {
            get; set;
        }
      
        public string InicijaliGrada
        {
            get; set;
        }
        
        public List<SelectListItem> tipoviLaptopa
        {
            set; get;
        }

        public int Id 
        {
            get; set;
        }
        public int LokacijaId 
        {
            get; set;
        }
        public string Lokacija
        {
            get; set;
        }
   
        public List<SelectListItem> Lokacije
        {
            get; set;
        }



    }
}
