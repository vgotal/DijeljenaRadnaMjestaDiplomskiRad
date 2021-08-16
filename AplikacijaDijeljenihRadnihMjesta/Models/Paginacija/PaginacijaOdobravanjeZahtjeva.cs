using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using cloudscribe.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.Paginacija
{
    public class PaginacijaOdobravanjeZahtjeva
    {
      
        public OdobravanjeZahtjeva odobravanjeZahtjeva
        {
            get; set;
        }


        public PagedResult<RezervacijeOtkazivanjeVM> paginationModel
        {
            get; set;
        }
    }
}
