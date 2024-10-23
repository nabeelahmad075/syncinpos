using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Utility.SelectItemDto
{
    public class SelectItemDto
    {
        public string Label { get; set; }
        public string Code { get; set; }
        public long Value { get; set; }
        public object Other { get; set; }
    }
    public class SelectItemComparer : IEqualityComparer<SelectItemDto>
    {
        public bool Equals(SelectItemDto x, SelectItemDto y)
        {
            return x.Value == y.Value && x.Label.Equals(y.Label);
        }

        public int GetHashCode(SelectItemDto obj)
        {
            return (int)obj.Value;
        }
    }
}
