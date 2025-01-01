using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Sessions
{
    public interface ISyncInPosSession : IAbpSession
    {
        public DateTime? OpenedDay { get; }
    }
}
