using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talysoft.Mathematics
{
    /// <summary>
    /// Provides basic Mathematical functionality.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMathematical<T> : IComparable<T>, IEquatable<T>
    {
        //T Magnitude();
        //T MagnitudeSquared();
    }
}
