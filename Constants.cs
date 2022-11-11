using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talysoft
{
    /// <summary>
    /// Provides constants used for formatting purposes, data calculation, and more.
    /// </summary>
    internal class Constants
    {
        public const int BITS = 8;
        public const int BYTE_SIZE = sizeof(byte);
        public const int BYTE_SIZE_BITS = BYTE_SIZE * BITS;
        public const int SHORT_SIZE = sizeof(short);
        public const int INT_SIZE = sizeof(int);
        public const int LONG_SIZE = sizeof(long);

        public const char IN_CHAR = '>';
        public const char OUT_CHAR = '<';

        public const char ARR_OPEN = '[';
        public const char ARR_CLOSE = ']';

        public const char OBJ_OPEN = '{';
        public const char OBJ_CLOSE = '}';

        public const char OPEN = '(';
        public const char CLOSE = ')';

        public const char SEPARATOR = ',';
    }
}
