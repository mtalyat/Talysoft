using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talysoft
{
    /// <summary>
    /// Generator contains static methods that generate data, that should allow for quick testing.
    /// </summary>
    public static class Generator
    {
        /// <summary>
        /// Generates a random array of integers.
        /// </summary>
        /// <param name="size">The size of the array.</param>
        /// <param name="rand">The Random object to be used to generate numbers. New Random used if none provided.</param>
        /// <returns>An array of random integers.</returns>
        public static int[] GenerateInts(int size, Random rand = null) => GenerateInts(size, 0, int.MaxValue, rand);

        /// <summary>
        /// Generates a random array of integers with an offset and a maximum value.
        /// </summary>
        /// <param name="size">The size of the array.</param>
        /// <param name="offset">The offset of each element of the array, from 0.</param>
        /// <param name="max">The maximum integer value randomly generated (exclusive).</param>
        /// <param name="rand">The Random object to be used to generate numbers. New Random used if none provided.</param>
        /// <returns>An array of random integers.</returns>
        public static int[] GenerateInts(int size, int offset, int max, Random rand = null)
        {
            if (size <= 0) return new int[0];

            int[] nums = new int[size];

            //if no random given, just make a new one
            if (rand == null) rand = new Random();

            for (int i = 0; i < size; i++)
            {
                if (max <= 0)
                    nums[i] = offset;
                else
                    nums[i] = offset + rand.Next(max);
            }

            return nums;
        }

        /// <summary>
        /// Generates a random array of doubles from 0 (inclusive) to 1 (exclusive).
        /// </summary>
        /// <param name="size">The size of the array.</param>
        /// <param name="rand">The Random object to be used to generate numbers. New Random used if none provided.</param>
        /// <returns>An array of random doubles.</returns>
        public static double[] GenerateDoubles(int size, Random rand = null) => GenerateDoubles(size, 0.0, 1.0, rand);

        /// <summary>
        /// Generates a random array of doubles with an offset and a scale value.
        /// </summary>
        /// <param name="size">The size of the array.</param>
        /// <param name="offset">The offset of each element of the array, from 0.</param>
        /// <param name="scale">A scale that every generated double is multiplied by in the array.</param>
        /// <param name="rand">The Random object to be used to generate numbers. New Random used if none provided.</param>
        /// <returns>An array of random doubles.</returns>
        public static double[] GenerateDoubles(int size, double offset, double scale, Random rand = null)
        {
            if (size <= 0) return new double[0];

            double[] nums = new double[size];

            //if no random given, just make a new one
            if (rand == null) rand = new Random();

            for (int i = 0; i < size; i++)
            {
                if (scale <= 0)
                    nums[i] = offset;
                else
                    nums[i] = offset + rand.NextDouble() * scale;
            }

            return nums;
        }

        /// <summary>
        /// Generates a random array of bytes.
        /// </summary>
        /// <param name="size">The size of the array.</param>
        /// <param name="rand">The Random object to be used to generate numbers. New Random is used if none provided.</param>
        /// <returns>An array of random bytes.</returns>
        public static byte[] GenerateBytes(int size, Random rand = null)
        {
            if (size <= 0) return new byte[0];

            byte[] nums = new byte[size];

            //if no random given, just make a new one
            if (rand == null) rand = new Random();

            rand.NextBytes(nums);

            return nums;
        }

        /// <summary>
        /// Creates an array of type T, initialized using new().
        /// </summary>
        /// <typeparam name="T">The type of array.</typeparam>
        /// <param name="size">The size of the array.</param>
        /// <returns>A populated array.</returns>
        public static T[] Generate<T>(int size) where T : new()
        {
            if (size <= 0) return new T[0];

            T[] ts = new T[size];

            for (int i = 0; i < size; i++)
            {
                ts[i] = new T();
            }

            return ts;
        }
    }
}
