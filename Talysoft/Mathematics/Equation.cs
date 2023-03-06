using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talysoft.Mathematics
{
    /// <summary>
    /// Equations represent two Tokens, on either side of the EQUALS sign.
    /// </summary>
    public class Equation : IMathematical<Equation>
    {
        /// <summary>
        /// The left side of the Equation.
        /// </summary>
        private Token left;

        /// <summary>
        /// The right side of the Equation.
        /// </summary>
        private Token right;

        /// <summary>
        /// Creates a new Equation with the given left and right tokens.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public Equation(Token left, Token right)
        {
            this.left = left;
            this.right = right;
        }

        /// <summary>
        /// Solves this Equation for all unknown Variables.
        /// </summary>
        /// <returns></returns>
        public Scope Solve()
        {
            //check out TODO.txt

            throw new NotImplementedException();
        }

        public Equation Evaluate(Scope scope)
        {
            Equation clone = Clone();

            clone.left = clone.left.Evaluate(scope);
            clone.right = clone.right.Evaluate(scope);

            return clone;
        }

        public Equation Simplify()
        {
            Equation clone = Clone();

            clone.left = clone.left.Simplify();
            clone.right = clone.right.Simplify();

            return clone;
        }

        public Equation Reduce()
        {
            Equation clone = Clone();

            clone.left = clone.left.Reduce();
            clone.right = clone.right.Reduce();

            return clone;
        }

        public Equation Expand()
        {
            Equation clone = Clone();

            clone.left = clone.left.Expand();
            clone.right = clone.right.Expand();

            return clone;
        }

        public void FillScope(Scope scope)
        {
            left.FillScope(scope);
            right.FillScope(scope);
        }

        public override string ToString()
        {
            //if sides are constant and not equal, print the not equals sign instead
            //we can leave as = if a side is not constant since there is still a variable
            return $"{left} {(left.IsConstant && right.IsConstant && left.ToNumber() != right.ToNumber() ? Symbols.NOT_EQUALS : Symbols.EQUALS)} {right}";
        }

        public Equation Clone()
        {
            return new Equation(left.Clone(), right.Clone());
        }

        public override bool Equals(object obj)
        {
            return obj is Equation other && left.Equals(other.left) && right.Equals(other.right);
        }

        public override int GetHashCode()
        {
            return left.GetHashCode() ^ right.GetHashCode();
        }

        #region Parsing

        /// <summary>
        /// Parses a string into an Equation.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Equation Parse(string s)
        {
            string[] split = s.ToString().Split(Symbols.EQUALS);

            if (split.Length < 2)
            {
                throw new ParsingException("The given object must include an equals sign!", Symbols.EQUALS);
            }
            else if (split.Length > 2)
            {
                throw new ParsingException("The given object must include only one equals sign!", Symbols.EQUALS);
            }

            //if split correctly, parse each side for the equation
            return new Equation(Token.Parse(split[0]), Token.Parse(split[1]));
        }

        /// <summary>
        /// Attempts to pare a string into an Equation.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="equation"></param>
        /// <returns></returns>
        public static bool TryParse(string s, ref Equation equation)
        {
            try
            {
                equation = Parse(s);
            }
            catch
            {
                // did not parse
                return false;
            }

            // parsed successfully
            return true;
        }

        #endregion
    }
}
