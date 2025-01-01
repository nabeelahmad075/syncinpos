using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Sales.Customers.Dto
{
    public class CustomerHistoryPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
