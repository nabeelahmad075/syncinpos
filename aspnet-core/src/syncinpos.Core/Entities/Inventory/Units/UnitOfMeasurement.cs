﻿using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Inventory.Units
{
    public class UnitOfMeasurement : Entity
    {
        [MaxLength(32)]
        public string Title { get; set; }
    }
}
