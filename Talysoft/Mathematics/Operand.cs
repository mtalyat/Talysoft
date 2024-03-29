﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talysoft.Mathematics
{
    /// <summary>
    /// Operands are values that can be operated on. 4
    /// Values include both known and unknown values, 
    /// whether that be a constant, an unknown constant,
    /// a number or a variable.
    /// </summary>
    public abstract class Operand : Element
    {
        /// <summary>
        /// Represents if this Operand is negative or not.
        /// </summary>
        protected bool isNegative = false;

        public override bool IsNegative => isNegative;

        /// <summary>
        /// Sets the negative value of this Operand.
        /// </summary>
        /// <param name="neg"></param>
        public void SetNegative(bool neg)
        {
            isNegative = neg;
        }

        //operands cannot ever be expanded
        public override Token Expand()
        {
            return Clone();
        }

        //operands cannot be reduced
        public override Token Reduce()
        {
            return Clone();
        }

        // operands cannot be simplified
        public override Token Simplify()
        {
            //the Number is already as simplified as it can get, so just return a copy
            return Clone();
        }

        public override void FillScope(Scope scope) {}

        internal override Number ExtractNumbers()
        {
            // cannot extract numbers
            return Number.NaN;
        }
    }
}
