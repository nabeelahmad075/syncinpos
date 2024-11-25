using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.Types
{
    public class MainType : Entity
    {
        [MaxLength(16)]
        public string Title { get; set; }
        [MaxLength(4)]
        public string Alias { get; set; }
        public bool IsActive { get; set; }
    }
}
