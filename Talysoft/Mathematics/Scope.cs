using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talysoft.Mathematics
{
    /// <summary>
    /// Scopes are a collection of values that can be plugged into variables.
    /// </summary>
    public class Scope
    {
        /// <summary>
        /// Shorthand to create a new Scope with no stored values.
        /// </summary>
        public static Scope Empty => new Scope();

        /// <summary>
        /// Determines if Constants will be kept as Constants when Evaluating. 
        /// If false, all Constants will be turned into their double form.
        /// </summary>
        public bool KeepConstants { get; set; } = true;

        /// <summary>
        /// A dictionary to store the variables and their corresponding values.
        /// </summary>
        private Dictionary<Variable, Token> variables;

        /// <summary>
        /// Creates a new Scope with no values.
        /// </summary>
        public Scope()
        {
            variables = new Dictionary<Variable, Token>();
        }

        /// <summary>
        /// Gets the corresponding value for the given Variable.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>The number with the value of the variable, or a clone of the variable if no value exists.</returns>
        public Token Get(Variable variable)
        {
            Token value;

            if (variables.TryGetValue(variable, out value) && value != null)
            {
                return value.Clone();
            }

            return null;
        }

        /// <summary>
        /// Sets the value for the given Variable.
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        public void Set(Variable variable, Token value)
        {
            variables[variable] = value;
        }

        /// <summary>
        /// Adds a Variable to this Scope with the default Number value.
        /// </summary>
        /// <param name="variable"></param>
        public void Add(Variable variable)
        {
            // only add if does not exist
            if(!variables.ContainsKey(variable))
            {
                variables.Add(variable, null);
            }
        }

        /// <summary>
        /// Removes the Variable and its corresponding value from the Scope.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>True if the Variable existed and was removed, otherwise false.</returns>
        public bool Remove(Variable variable)
        {
            return variables.Remove(variable);
        }

        /// <summary>
        /// Clears all values and variables from the Scope.
        /// </summary>
        public void Clear()
        {
            variables.Clear();
        }

        public override string ToString()
        {
            return string.Join("; ", variables.Select(p => $"{p.Key} = {(p.Value == null ? "?" : p.Value.ToString())}").ToArray());
        }
    }
}
