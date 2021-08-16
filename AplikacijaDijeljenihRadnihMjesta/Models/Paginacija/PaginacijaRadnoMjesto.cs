using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using cloudscribe.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.Paginacija
{
    public class PaginacijaRadnoMjesto
    {
        public RadnoMjestoFilter radnoMjestoFilter
        {
            get; set;
        }


        public PagedResult<RadnoMjestoVM> paginationModel
        {
            get; set;
        }
    }
}
