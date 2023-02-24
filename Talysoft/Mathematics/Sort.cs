using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talysoft.Mathematics
{
    /// <summary>
    /// Provides methods for sorting data.
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// Sorts the given array using the Quicksort method.
        /// </summary>
        /// <typeparam name="T">The type of data to be sorted. Must implement IComparable\<<typeparamref name="T"/>\>.</typeparam>
        /// <param name="array">The array to be sorted.</param>
        /// <param name="left">The leftmost index to be sorted.</param>
        /// <param name="right">The rightmost index to be sorted.</param>
        /// <returns>The inputted array, but sorted.</returns>
        public static T[] Quicksort<T>(T[] array, int left, int right) where T : IComparable<T>
        {
            int i = left;
            int j = right;
            T pivot = array[left];

            while(i <= j)
            {
                while(array[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while(array[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if(i <= j)
                {
                    //swap values
                    T temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;

                    i++;
                    j--;
                }
            }

            if (left < j)
            {
                Quicksort(array, left, j);
            }

            if(i < right)
            {
                Quicksort(array, i, right);
            }

            return array;
        }
    }
}
