using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncinpos.Utility
{
    public class NullDisposable : IDisposable
    {
        // Singleton instance to avoid creating multiple objects
        public static NullDisposable Instance { get; } = new NullDisposable();

        // Private constructor to enforce singleton pattern
        private NullDisposable() { }

        // No-op Dispose method
        public void Dispose()
        {
            // Intentionally left blank
        }
    }
}
