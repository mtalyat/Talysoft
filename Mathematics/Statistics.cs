using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talysoft.Mathematics
{
    /// <summary>
    /// Provides methods to calculate statistical data.
    /// </summary>
    public static class Statistics
    {
        /// <summary>
        /// Gives the Median of the given data. Assuming data has been sorted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static decimal Median(decimal[] data)
        {
            return data[data.Length / 2];
        }

        public static decimal Sum(decimal[] data)
        {
            decimal sum = 0;

            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            
            return sum;
        }

        public static decimal Mean(decimal[] data, decimal total)
        {
            return total / data.Length;
        }

        public static decimal[][] Counts(decimal[] data)
        {
            Dictionary<decimal, decimal> counts = new Dictionary<decimal, decimal>();

            for (int i = 0; i < data.Length; i++)
            {
                if(counts.ContainsKey(data[i]))
                {
                    counts[data[i]]++;
                } else
                {
                    counts.Add(data[i], 1);
                }
            }

            //convert to output format
            decimal[][] output = new decimal[counts.Count][];
            for (int i = 0; i < counts.Count; i++)
            {
                var pair = counts.ElementAt(i);

                output[i] = new decimal[2]
                {
                    pair.Key, pair.Value
                };
            }

            return output;
        }

        public static decimal Max(decimal[] data)
        {
            decimal max = data[0];

            for (int i = 1; i < data.Length; i++)
            {
                if(data[i] > max)
                {
                    max = data[i];
                }
            }

            return max;
        }

        public static decimal Min(decimal[] data)
        {
            decimal min = data[0];

            for (int i = 0; i < data.Length; i++)
            {
                if(data[i] < min)
                {
                    min = data[i];
                }
            }

            return min;
        }

        public static decimal Mode(decimal[] data)
        {
            //get counts
            decimal[][] counts = Counts(data);

            //get max value
            int maxIndex = 0;
            decimal max = counts[0][1];

            for (int i = 1; i < counts.Length; i++)
            {
                if(counts[i][1] > max)
                {
                    max = counts[i][1];
                    maxIndex = i;
                }
            }

            return counts[maxIndex][0];
        }
    }
}
