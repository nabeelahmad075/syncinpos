using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Setups.Tables.Dto
{
    public class TableHistoryDto
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string FloorTitle { get; set; }
        public string TableTitle { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
