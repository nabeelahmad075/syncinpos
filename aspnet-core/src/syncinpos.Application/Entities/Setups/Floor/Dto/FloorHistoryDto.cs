using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Setups.Floor.Dto
{
    public class FloorHistoryDto
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
