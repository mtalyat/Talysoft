using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Talysoft.IO
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CSVAttribute : Attribute
    {
        public readonly int Order;

        public CSVAttribute([CallerLineNumber]int order = 0)
        {
            Order = order;
        }
    }
}
