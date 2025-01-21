using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Users.Dto
{
    public class UserHistoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Username { get; set; }
        public string RoleAssigned { get; set; }
        public bool IsActive { get; set; }
    }
}
