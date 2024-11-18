using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Entities.Accounts.Types
{
    public class AccountType : Entity
    {
        public string Title { get; set; }
        public string Alias { get; set; }

    }
}
