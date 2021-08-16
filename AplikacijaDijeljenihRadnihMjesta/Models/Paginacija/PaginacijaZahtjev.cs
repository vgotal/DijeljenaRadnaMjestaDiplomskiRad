using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using cloudscribe.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.Paginacija
{
    public class PaginacijaZahtjev
    {
        public PregledZahtjevaVM pregledZahtjeva
        {
            get; set;
        }


        public PagedResult<ZahtjevVM> paginationModel
        {
            get; set;
        }
    }
}
