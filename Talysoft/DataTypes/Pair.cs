using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talysoft.DataTypes
{
    public struct Pair<T, U>
    {
        public T left;
        public U right;

        public Pair(T t, U u)
        {
            left = t;
            right = u;
        }

        public override string ToString()
        {
            return $"({left}, {right})";
        }
    }

    public struct Pair<T>
    {
        public T left;
        public T right;

        public Pair(T t, T u)
        {
            left = t;
            right = u;
        }

        public override string ToString()
        {
            return $"({left}, {right})";
        }
    }
}
