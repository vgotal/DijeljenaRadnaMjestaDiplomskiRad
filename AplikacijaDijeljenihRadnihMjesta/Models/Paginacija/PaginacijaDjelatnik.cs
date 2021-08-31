using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using cloudscribe.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.Paginacija
{
    public class PaginacijaDjelatnik
    {
        public DjelatnikFilter djelatnikFilter
        {
            get; set;
        }


        public PagedResult<DjelatnikVM> paginationModel
        {
            get; set;
        }

     
    }
}
