using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talysoft.Math
{
    public interface IMathematical<T> : IComparable<T>, IEquatable<T>
    {
        float Magnitude();
        float MagnitudeSquared();
    }
}
