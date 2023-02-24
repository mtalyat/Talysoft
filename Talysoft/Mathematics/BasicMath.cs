using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talysoft.Mathematics
{
    /// <summary>
    /// Provides addition math calculations.
    /// </summary>
    public static class BasicMath
    {
        #region Trigonometry

        /// <summary>
        /// Returns the radians of the specified degrees.
        /// </summary>
        public static float DegreesToRadians(float degrees)
        {
            return degrees * (float)System.Math.PI / 180f;
        }

        /// <summary>
        /// Returns the degrees of the specified radians.
        /// </summary>
        public static float RadiansToDegrees(float radians)
        {
            return radians * 180f / (float)System.Math.PI;
        }

        #endregion

        #region Algebra

        /// <summary>
        /// Returns the number within the specified min and max values.
        /// </summary>
        public static float Clamp(float val, float min, float max)
        {
            return System.Math.Max(min, System.Math.Min(val, max));
        }

        /// <summary>
        /// Returns the number within the specified min and max values.
        /// </summary>
        public static int Clamp(int val, int min, int max)
        {
            return System.Math.Max(min, System.Math.Min(val, max));
        }

        #endregion
    }
}