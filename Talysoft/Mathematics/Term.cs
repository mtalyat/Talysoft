﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Talysoft.Mathematics
{
    /// <summary>
    /// Terms are what make up expressions.
    /// Terms combine fractions, and the parts that make up a term: a coefficient, variables and exponents.
    /// The term has 1 denominator, which is an Expression.
    /// The term can have any amount of numerators, which are Elements.
    /// Each Element in the Numerator also has an exponent, which is an Expression.
    /// 
    /// Handles the multiplication and division operations.
    /// </summary>
    public partial class Term : Token
    {
        #region Common Terms

        /// <summary>
        /// Shorthand to create a new Term that is equal to 1.
        /// </summary>
        public static Term One => new Term(Number.One);

        /// <summary>
        /// Shorthand to create a new Term that is equal to 0.
        /// </summary>
        public static Term Zero => new Term(Number.Zero);

        #endregion

        //the term is only constant if all elements and exponents are constant
        //we know the coefficients will be constant since they are numbers
        public override bool IsConstant => denominators.All(e => e.IsConstant) && numerators.All(e => e.IsConstant);

        public override bool HasConstantOrVariable => numerators.Any(e => e.HasConstantOrVariable) || denominators.Any(e => e.HasConstantOrVariable);

        /// <summary>
        /// True if the denominator is equal to 1.
        /// </summary>
        private bool IsDenominatorOne => coefficientDenominator.IsOne && denominators.All(d => d.IsOne);

        public bool IsFraction => !IsDenominatorOne;

        public override bool IsNumber => numerators.Count == 1 && numerators[0].IsOne && IsDenominatorOne;

        public override bool IsOne => coefficientNumerator.IsOne && numerators.All(n => n.IsOne) && IsDenominatorOne;

        public override bool IsZero => coefficientNumerator.IsZero || numerators.Any(n => n.IsZero);

        //as long as the numerator and denominator isNegative are different, the term is negative
        public override bool IsNegative => coefficientNumerator.IsNegative ^ coefficientDenominator.IsNegative;

        /// <summary>
        /// The coefficient for the numerator.
        /// </summary>
        private Number coefficientNumerator;

        /// <summary>
        /// The coefficient for the denominator.
        /// </summary>
        private Number coefficientDenominator;

        /// <summary>
        /// The list of Elements in the numerator.
        /// </summary>
        private List<TermToken> numerators;

        /// <summary>
        /// The list of Elements in the denominator.
        /// </summary>
        private List<TermToken> denominators;

        #region Constructors

        /// <summary>
        /// Creates a Term from any type of Token.
        /// </summary>
        /// <param name="token"></param>
        public Term(Token token)
        {
            if(token is Term t)
            {
                //copy values if it is another term
                coefficientNumerator = t.coefficientNumerator;
                coefficientDenominator = t.coefficientDenominator;
                numerators = t.numerators;
                denominators = t.denominators;
            } else
            {
                //otherwise, just put it in the numerator and do defaults
                coefficientNumerator = Number.One;
                coefficientDenominator = Number.One;

                numerators = new List<TermToken>();
                AddToNumerator(token is TermToken te ? te : new TermToken((Element)token, Number.One));

                denominators = new List<TermToken>();
                AddToDenominator(TermToken.One);
            }

            EnsureNomAndDenomAreNotEmpty();
        }

        /// <summary>
        /// Creates a new Term using the given Elements as the numerator.
        /// </summary>
        /// <param name="numerators"></param>
        public Term(Token[] numerators) : this(Number.One, numerators) { }

        /// <summary>
        /// Creates a new Term with the given coefficient, and the given Elements as the numerator.
        /// </summary>
        /// <param name="coefficient"></param>
        /// <param name="numerators"></param>
        public Term(Number coefficient, Token[] numerators) : this(coefficient, Number.One, numerators, null) { }

        /// <summary>
        /// Creates a new Term with the given coefficient, and the given Element as the numerator.
        /// </summary>
        /// <param name="coefficient"></param>
        /// <param name="numerator"></param>
        public Term(Number coefficient, Token numerator) : this(coefficient, numerator, Number.One) { }

        /// <summary>
        /// Creates a new Term with the given coefficient, and an Element with its corresponding Exponent that will go in the numerator.
        /// </summary>
        /// <param name="coefficient"></param>
        /// <param name="numerator"></param>
        /// <param name="exponent"></param>
        public Term(Number coefficient, Token numerator, Token exponent) : this(coefficient, new Token[] { new TermToken(numerator, exponent) }) { }

        /// <summary>
        /// Creates a new Term with the given list of Elements for both the numerator, and denominator.
        /// </summary>
        /// <param name="numerators"></param>
        /// <param name="denominators"></param>
        public Term(Token[] numerators, Token[] denominators) : this(Number.One, Number.One, numerators, denominators) { }

        /// <summary>
        /// Creates a new Term with both the coefficient and Elements for both the nominator and denominator.
        /// </summary>
        /// <param name="coefficientNumerator"></param>
        /// <param name="coefficientDenominator"></param>
        /// <param name="numerators"></param>
        /// <param name="denominators"></param>
        public Term(Number coefficientNumerator, Number coefficientDenominator, Token[] numerators, Token[] denominators)
        {
            this.coefficientNumerator = coefficientNumerator;
            this.coefficientDenominator = coefficientDenominator;
            this.numerators = new List<TermToken>();
            this.denominators = new List<TermToken>();

            foreach (TermToken te in ConvertElementsToTermTokens(numerators ?? new Element[0]))
                AddToNumerator(te);

            foreach (TermToken te in ConvertElementsToTermTokens(denominators ?? new Element[0]))
                AddToDenominator(te);

            EnsureNomAndDenomAreNotEmpty();
        }

        #endregion

        #region Numerator/Denominator Management

        /// <summary>
        /// Adds a 1 to the numerator or denominator if either one is empty.
        /// </summary>
        private void EnsureNomAndDenomAreNotEmpty()
        {
            //make sure there is at least something
            if (!numerators.Any()) numerators.Add(TermToken.One);
            if (!denominators.Any()) denominators.Add(TermToken.One);
        }

        /// <summary>
        /// Converts the given list of Elements into TermElements. If the Element already is a TermElement, is uses that. 
        /// If not, it puts the Element inside of a TermElement.
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        private TermToken[] ConvertElementsToTermTokens(Token[] elements)
        {
            return elements.Select(e => e is TermToken te ? te : new TermToken(e, Number.One)).ToArray();
        }

        /// <summary>
        /// Adds the given TermElement to the given List of TermElements, or multiplies it into the given coefficient if the TermElement is a Number.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="list"></param>
        /// <param name="coefficient"></param>
        private void AddToList(TermToken e, List<TermToken> list, ref Number coefficient)
        {
            if (e.Exponent.IsZero) return;//if it is 1, it literally does not matter

            //if the value is a number, but not a constant, we want to multiply it into the coefficient
            if (e.IsNumber)
            {
                coefficient *= e.ToNumber();
            } else
            {
                //otherwise just add it to the list

                //if it is negative, however, we want that as part of the coefficient
                if(e.IsNegative)
                {
                    Token t = e.Multiply(Number.NegativeOne);
                    e = t is TermToken tt ? tt : new TermToken(t, Number.One);

                    coefficient *= -1;
                }

                //if the list only has 1 in it, replace the 1
                if (list.Count == 1 && list[0].IsOne)
                {
                    list.Clear();
                }

                list.Add(e);
            }
        }

        /// <summary>
        /// Shorthand to call AddToList for the numerator.
        /// </summary>
        /// <param name="e"></param>
        private void AddToNumerator(TermToken e) => AddToList(e, numerators, ref coefficientNumerator);

        /// <summary>
        /// Shorthand to call AddToList for the denominator.
        /// </summary>
        /// <param name="e"></param>
        private void AddToDenominator(TermToken e) => AddToList(e, denominators, ref coefficientDenominator);

        #endregion

        #region General

        /// <summary>
        /// Determines if the given Term is a like Term, when compared to this Term.
        /// 
        /// Terms are like when all the non-constant values match within the two Terms.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True if the terms are like, otherwise false.</returns>
        public bool IsLikeTerm(Term other)
        {
            //terms are like if they have the same variables with the same exponents

            //cannot be the same if the non-constant terms are different amounts
            if (numerators.Count != other.numerators.Count || denominators.Count != other.denominators.Count) return false;

            //then actually check the terms

            //terms are not necessarily in order
            foreach (TermToken te in numerators)
            {
                if (!other.numerators.Any(t => t.Equals(te))) return false;
            }

            foreach (TermToken te in denominators)
            {
                if (!other.denominators.Any(t => t.Equals(te))) return false;
            }

            //if it found it all, then it must be a like term
            return true;
        }

        /// <summary>
        /// Finds the highest power within this Term.
        /// </summary>
        /// <returns>The highest power within this Term. 
        /// It can be negative if the variables with powers are on the bottom of the fraction.</returns>
        public Number HighestPower()
        {
            Number n = FindHighestPower(numerators);
            Number d = FindHighestPower(denominators);

            //anything in the denom should be negated, since it is on the bottom part of the fraction
            //so, as long as the numerator returns a value, it should be used
            if (double.IsNaN(n.Value))
                return d;
            else
                return n;
        }

        /// <summary>
        /// Tests if this Term and the other Term share a common denominator.
        /// The Terms have a common denominator when both Terms denominators match.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True if the Terms have the same denominator, otherwise false.</returns>
        public bool HasCommonDenominator(Term other)
        {
            //assume arguments are already simplified

            //check for the coeffienct and the count first
            if (coefficientDenominator != other.coefficientDenominator || denominators.Count != other.denominators.Count) return false;

            //now check each element
            foreach (TermToken te in denominators)
            {
                if (!other.denominators.Contains(te)) return false;
            }

            return true;
        }

        /// <summary>
        /// Creates a clone, changes it so the denominator matches the given term, and returns the clone.
        /// The real value of the clone does not differ from this Term.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Term EnsureCommonDenominator(Term other)
        {
            if (HasCommonDenominator(other)) return (Term)Clone();

            //no common denominator
            Term clone = (Term)Clone();

            clone.coefficientNumerator *= other.coefficientDenominator;
            clone.coefficientDenominator *= other.coefficientDenominator;

            foreach (TermToken te in other.denominators)
            {
                clone.AddToNumerator(te);
                clone.AddToDenominator(te);
            }

            //now it has a common denominator
            //DO NOT SIMPLIFY, because we want it to stay as a common denominator
            return (Term)clone;
        }

        /// <summary>
        /// Gets the inverse of this Term.
        /// </summary>
        /// <returns></returns>
        public Term Inverse()
        {
            //clone the term
            Term clone = (Term)Clone();

            //flip the numerator and denominator
            return new Term(clone.coefficientDenominator, clone.coefficientNumerator, clone.denominators.ToArray(), clone.numerators.ToArray());
        }

        public override Token Expand()
        {
            //expand the numerator and denominator
            return CreateFraction(ExpandList(numerators, coefficientNumerator), ExpandList(denominators, coefficientDenominator));
        }

        /// <summary>
        /// Expands the given list into an Expression.
        /// Expanding is done by multiplying every Token in the list by every other Token in the list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="coefficient"></param>
        /// <returns></returns>
        private Token ExpandList(List<TermToken> list, Number coefficient)
        {
            Expression output = new Expression(coefficient);

            for (int i = 0; i < list.Count; i++)
            {
                TermToken te = (TermToken)list[i].Expand();

                Token reduced = te.Reduce();

                if (reduced is Expression e)
                {
                    output = output.FOIL(e);
                }
                else if (reduced is Term t && !t.IsFraction)
                {
                    //term that cannot be reduced
                    //if the term is not a fraction, we can extract the contents

                    foreach (Token extracted in t.Extract())
                    {
                        if (extracted is Expression ee)
                        {
                            output = output.FOIL(ee);
                        }
                        else
                        {
                            output = output.MultiplyAll(extracted);
                        }
                    }
                }
                else
                {
                    //another type... multiply all elements by the token
                    output = output.MultiplyAll(reduced);
                }
            }

            //reduce at the end, in case it is only one element
            return output.Reduce();
        }

        /// <summary>
        /// Extracts the Tokens from the Numerator.
        /// </summary>
        /// <returns></returns>
        public Token[] Extract()
        {
            List<Token> output = new List<Token>();

            output.Add(coefficientNumerator.Clone());

            foreach(TermToken te in numerators)
            {
                Token reduced = te.Reduce();

                //only extract if it is not a fraction
                if(reduced is Term t && !t.IsFraction)
                {
                    output.AddRange(t.Extract());
                } else
                {
                    output.Add(reduced);
                }
            }

            return output.ToArray();
        }

        /// <summary>
        /// Raises this Term by the given exponent.
        /// </summary>
        /// <param name="exponent"></param>
        /// <returns></returns>
        public Term RaiseByPower(Element exponent)
        {
            //reduce if able
            Token reduced = Reduce();

            //put terms in expressions so it is an element, if needed
            if (reduced is Term t)
            {
                reduced = new Expression(t);
            }

            //return that in a term element
            return new Term(new TermToken((Element)reduced, exponent));
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Finds the highest power within the given list of TermElements.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private Number FindHighestPower(List<TermToken> list)
        {
            Number highest = Number.NaN;

            foreach(TermToken te in list)
            {
                //we don't care about constants, only variables
                if(te.Exponent.IsNumber)
                {
                    //if it is a constant, then the power is 0 regardless
                    Number n;
                    if(te.Token.IsConstant)
                    {
                        n = Number.Zero;
                    } else
                    {
                        n = te.Exponent.ToNumber();
                    }

                    if(double.IsNaN(highest.Value) || n > highest)
                    {
                        highest = n;
                    }
                }
            }

            return highest;
        }

        #endregion

        #region Solving

        public override Token Evaluate(Scope scope)
        {
            //firstly, if there are any zeros, the whole thing turns into a zero regardless
            if (coefficientNumerator == 0) return Number.Zero;

            if (coefficientDenominator == 0) return Number.NaN;

            // get coefficients
            Number cNum = (Number)coefficientNumerator.Clone();
            Number cDenom = (Number)coefficientDenominator.Clone();

            // get values
            List<Token> num = new List<Token>();
            List<Token> denom = new List<Token>();

            Number n;

            // evaluate each value, add to coefficient if constant
            foreach(TermToken t in numerators)
            {
                // eval
                Token token = t.Evaluate(scope);

                // extract numbers
                n = t.ExtractNumbers();

                if(!n.IsNaN)
                {
                    cNum = (Number)cNum.Multiply(n);
                }

                num.Add(token);
            }

            foreach (TermToken t in denominators)
            {
                // eval
                Token token = t.Evaluate(scope);

                // extract numbers
                n = t.ExtractNumbers();

                if (!n.IsNaN)
                {
                    cDenom = (Number)cDenom.Multiply(n);
                }

                denom.Add(token);
            }

            // return simplified version
            return new Term(cNum, cDenom, num.ToArray(), denom.ToArray());
        }

        public override Token Simplify()
        {
            //simplify the coefficients if able
            //treat them like a fraction
            //ensure there are whole numbers

            Term clone = (Term)Clone();

            Number temp;

            foreach(TermToken t in numerators)
            {
                temp = t.ExtractNumbers();

                if(temp.IsNaN)
                {
                    continue;
                }

                clone.coefficientNumerator = (Number)clone.coefficientNumerator.Multiply(temp);
            }

            foreach (TermToken t in denominators)
            {
                temp = t.ExtractNumbers();

                if (temp.IsNaN)
                {
                    continue;
                }

                clone.coefficientDenominator = (Number)clone.coefficientDenominator.Multiply(temp);
            }

            //first, make sure they are both whole numbers
            int places = Math.Max(Functions.DecimalPlaces(clone.coefficientNumerator).Integer, Functions.DecimalPlaces(clone.coefficientDenominator).Integer);

            if(places > 0)
            {
                clone.coefficientNumerator *= Math.Pow(10, places);
                clone.coefficientDenominator *= Math.Pow(10, places);
            }

            //then divide both by their GCF
            int gcf = Functions.GCF(clone.coefficientNumerator, clone.coefficientDenominator).Integer;

            clone.coefficientNumerator /= gcf;
            clone.coefficientDenominator /= gcf;

            //now that that is done, check the numerator values
            //TODO: combine any variables and add their exponents together
            Dictionary<Token, TermToken> newElements = new Dictionary<Token, TermToken>();

            foreach(TermToken e in numerators)
            {
                //start by simplifying the TermElement
                TermToken simplified = (TermToken)e.Simplify();

                //if exponent is zero, ignore it
                if (simplified.Exponent.IsZero) continue;

                Token ele = simplified.Token.Reduce();
                Token exp = simplified.Exponent.Reduce();

                //if it simplifies to a number, multiply it to the coefficient
                if (exp.IsOne && ele is Number n && !n.HasSymbol)
                {
                    clone.coefficientNumerator *= n;
                    continue;
                }

                TermToken ne;

                if(newElements.TryGetValue(ele, out ne))
                {
                    //add to the exponent expression
                    //set it again in the dictionary
                    ne = new TermToken(ne.Token, (Element)ne.Exponent.Add(exp).Simplify());

                    newElements[ele] = ne;
                } else
                {
                    //add whole thing
                    newElements.Add(ele, simplified);
                }
            }

            //replace the numerators in the clone
            clone.numerators = newElements.Select(e => e.Value).ToList();

            clone.EnsureNomAndDenomAreNotEmpty();

            return clone;
        }

        public override Token Reduce()
        {
            //cannot reduce if it is a fraction/still division going on
            if (numerators.Count > 1 || !IsDenominatorOne) return Clone();

            //cannot reduce if there is still multiplication going on
            if (!numerators[0].IsOne && !coefficientNumerator.IsOne) return Clone();

            //return the one that is not one
            if(coefficientNumerator.IsOne)
            {
                return numerators[0].Reduce();
            } else
            {
                return coefficientNumerator.Reduce();
            }
        }

        public override void FillScope(Scope scope)
        {
            foreach(TermToken token in numerators)
            {
                token.FillScope(scope);
            }

            foreach(TermToken token in denominators)
            {
                token.FillScope(scope);
            }
        }

        internal override Number ExtractNumbers()
        {
            // use coefficients
            // divide numerator by denominator at end
            Number num = coefficientNumerator;
            Number denom = coefficientDenominator;

            coefficientNumerator = Number.One;
            coefficientDenominator = Number.One;

            Number n;

            foreach(Token token in numerators)
            {
                n = token.ExtractNumbers();
                
                //ignore invalid
                if(n.IsNaN)
                {
                    continue;
                }

                num = (Number)num.Multiply(n);
            }

            foreach (Token token in denominators)
            {
                n = token.ExtractNumbers();

                //ignore invalid
                if (n.IsNaN)
                {
                    continue;
                }

                denom = (Number)denom.Multiply(n);
            }

            if(num.IsZero || denom.IsZero)
            {
                return Number.Zero;
            }

            return num / denom;
        }

        #endregion

        #region Operations

        public override Token Add(Token token)
        {
            //if it as an expression, add that way
            if(token is Expression e)
            {
                return e.Add((Term)Clone());
            } else if (token is Term t)
            {
                //if it is a term, ensure they share a common denominator, then add and simplify
                Term clone = (Term)Clone();

                //make sure they have a common denomitor if they do not
                Term cdT = t.EnsureCommonDenominator(clone);
                clone = clone.EnsureCommonDenominator(t);

                Element newNumerator;
                
                if(clone.IsLikeTerm(cdT))
                {
                    //if they are like terms, just add the coefficients
                    clone.coefficientNumerator += cdT.coefficientNumerator;
                    return clone;
                }
                else
                {
                    //if not, put them into an expression
                    //then just add the numerators together
                    newNumerator = new Expression(new Term(clone.coefficientNumerator, clone.numerators.ToArray()), new Term(cdT.coefficientNumerator, cdT.numerators.ToArray()));
                    //then put that into the numerator of a new term with the correct values
                    return new Term(Number.One, clone.coefficientDenominator, new TermToken[] { new TermToken(newNumerator, Number.One) }, clone.denominators.ToArray());
                }
            } else
            {
                //if it is anything else, just throw it into an expression
                //turn the nominator into an expression and add it

                //nevermind, just throw it into an expression

                return ToExpression().Add(token);
            }
        }

        public override Token Multiply(Token token)
        {
            if (token.IsNumber)
            {
                //if it is a number, then multiply it into the coefficient
                Term clone = (Term)Clone();

                clone.coefficientNumerator *= token.ToNumber();

                return clone;
            }
            else if (token is Term t)
            {
                //if it is another term, merge num and denom
                return new Term(coefficientNumerator * t.coefficientNumerator, coefficientDenominator * t.coefficientDenominator,
                    numerators.Concat(t.numerators).ToArray(), denominators.Concat(t.denominators).ToArray());
            } else if (token is TermToken te)
            {
                Term clone = (Term)Clone();

                //do not worry about negative exponent here

                ////if it has a negative exponent, put it on the bottom
                //if (te.Exponent.IsNumber && te.Exponent.ToNumber() < 0)
                //{
                //    Number exp = te.Exponent.ToNumber();
                //    if(exp < 0)
                //    {
                //        clone.AddToDenominator(new TermToken(te.Token, exp));
                //        return clone;
                //    }
                //}

                clone.AddToNumerator(te);

                return clone;
            } else
            {
                Term clone = (Term)Clone();

                //if it is anything else, just add it to the numerator
                clone.AddToNumerator(new TermToken((Element)token, Number.One));

                return clone;
            }
        }

        public override Number ToNumber()
        {
            Number numerator = coefficientNumerator;
            Number denominator = coefficientDenominator;

            numerators.ForEach(e => numerator *= e.ToNumber());
            denominators.ForEach(e => denominator *= e.ToNumber());

            return numerator / denominator;
        }

        public override bool Equals(object obj)
        {
            return obj is Term other &&
                coefficientNumerator == other.coefficientNumerator &&
                coefficientDenominator == other.coefficientDenominator &&
                numerators.SequenceEqual(other.numerators) &&
                denominators.SequenceEqual(other.denominators);
        }

        public override int GetHashCode()
        {
            return coefficientNumerator.GetHashCode() << 3 ^ coefficientDenominator.GetHashCode() << 2 ^ numerators.GetHashCode() << 1 ^ denominators.GetHashCode();
        }

        #endregion

        public override Token Clone()
        {
            return new Term((Number)coefficientNumerator.Clone(), (Number)coefficientDenominator.Clone(), numerators.Select(e => (TermToken)e.Clone()).ToArray(), denominators.Select(e => (TermToken)e.Clone()).ToArray());
        }

        #region ToString

        private string CoefficientToString(Number coefficient, bool showNumberIfOne, bool showNegativeSign)
        {
            if(coefficient.RawValue == 1 && !showNumberIfOne)
            {
                if (showNegativeSign && coefficient.IsNegative)
                {
                    return "-";
                }
                else
                {
                    return "";
                }
            } else
            {
                if (showNegativeSign)
                {
                    return coefficient.Value.ToString();
                }
                else
                {
                    return coefficient.RawValue.ToString();
                }
            }
        }

        public string ToString(bool showNegativeSign)
        {
            StringBuilder sb = new StringBuilder();

            if (coefficientNumerator == 0 || coefficientDenominator == 0) return "0";

            bool printDenom = !IsDenominatorOne;
            bool printNum = !numerators.All(e => e.IsOne);

            //if the coefficient numerator != 1, we print it
            //or if the denominator != 1, we print it
            if (coefficientNumerator != 1 || (printDenom && !printNum))
            {
                sb.Append(CoefficientToString(coefficientNumerator, !printNum, showNegativeSign));
            }

            if (printNum)
            {
                foreach (TermToken te in numerators)
                {
                    sb.Append(te);
                }
            }

            if (printDenom)
            {
                sb.Append(Symbols.DIVISION);

                bool denomsAllOne = denominators.All(e => e.IsOne);

                if (coefficientDenominator != 1 || denomsAllOne)
                {
                    sb.Append(CoefficientToString(coefficientDenominator, denomsAllOne, showNegativeSign));
                }

                if (!denomsAllOne)
                {
                    foreach (TermToken te in denominators)
                    {
                        sb.Append(te);
                    }
                }
            }

            //if nothing was printed, it must be 1
            if (sb.Length == 0) sb.Append("1");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString(true);
        }

        #endregion

        /// <summary>
        /// Creates a new Term that represents a fraction.
        /// </summary>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        /// <returns></returns>
        public static Term CreateFraction(Token numerator, Token denominator)
        {
            return new Term(new Token[] { numerator }, new Token[] { denominator });
        }
    }
}
