using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talysoft.IO;

namespace Talysoft
{
    public class Database<T>
    {

    }

    public class Stack<T>
    {
        public T Obj { get; set; }

        public byte Amount { get; set; }

        public Stack()
        {
            Obj = default(T);
            Amount = default(byte);
        }

        public Stack(T t, byte amount)
        {
            Obj = t;
            Amount = amount;
        }
    }
}
