using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Regions
{
    public class Region : Entity
    {
        //[Key]
        //public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
    }
}
