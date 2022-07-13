using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Talysoft.Formatting;

namespace Talysoft
{
    /// <summary>
    /// Extensions for various types.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts this Array to a string.
        /// </summary>
        /// <typeparam name="T">The Type that this Array holds.</typeparam>
        /// <param name="arr">The Array with the data to be processed.</param>
        /// <param name="cutoff"></param>
        /// <returns></returns>
        public static string ArrayToString<T>(this T[] arr, int cutoff = -1)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(ARR_OPEN);

            for (int i = 0; i < arr.Length; i++)
            {
                T o = arr[i];

                if(o != null && (cutoff < 0 || i < cutoff))
                {
                    //if o is an array, recursively print that too
                    if (o.GetType().IsArray)
                    {
                        sb.Append(ArrayToString(o as T[]));
                    }
                    else
                    {
                        //only print the object if not null and in the cutoff range
                        sb.Append(o.ToString());
                    }
                }

                //if not at the end, add a separator
                if (i < arr.Length - 1 && (cutoff < 0 || i < cutoff - 1))
                {
                    sb.Append(SEPARATOR);

                    //only add a space if there are more elements
                    if(cutoff < 0 || i + 1 < cutoff)
                        sb.Append(' ');
                } else if (i == cutoff)
                {
                    sb.Append("...");
                    break;
                }
            }

            sb.Append(ARR_CLOSE);

            return sb.ToString();
        }

        /// <summary>
        /// Loosely "wraps" the given text, using the given width.
        /// Words can go past the given width, if they are long.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string LooseTextWrap(this string text, int width)
        {
            StringBuilder sb = new StringBuilder();

            int w = 0;
            for (int i = 0; i < text.Length; i++, w++)
            {
                char c = text[i];

                if (w >= width && char.IsWhiteSpace(c))
                {
                    sb.Append('\n');
                    w = 0;
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Hard "wraps" the given text, using the given width.
        /// Words will not go past the given width, even if they are long.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string HardTextWrap(this string text, int width)
        {
            StringBuilder sb = new StringBuilder();

            string[] words = text.Split(' ');

            int lineLength = 0;

            foreach (string word in words)
            {
                int wordLength = word.Length;

                if (lineLength + wordLength > width)
                {
                    //new line time
                    sb.Append('\n');
                    sb.Append(word);

                    sb.Append(' ');
                    lineLength = wordLength + 1;
                }
                else
                {
                    //add to this line
                    sb.Append(word);

                    //if with the space, it goes over, don't add the space
                    if (lineLength + wordLength + 1 > width)
                    {
                        lineLength += wordLength;
                    }
                    else
                    {
                        sb.Append(' ');
                        lineLength += wordLength + 1;
                    }
                }
            }

            return sb.ToString();
        }
    }
}
