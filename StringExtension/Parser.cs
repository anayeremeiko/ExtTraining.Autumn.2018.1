using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringExtension
{
    public static class Parser
    {
        private const int lowBoundary = 2;
        private const int highBoundary = 16;

        /// <summary>
        /// Converts to decimal.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="base">The base.</param>
        /// <returns>Decimal number.</returns>
        /// <exception cref="ArgumentNullException">Source number need to be not null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Base need to be from 2 to 16.</exception>
        /// <exception cref="ArgumentException">Number doesn't match.</exception>
        /// <exception cref="OverflowException">Number is too big.</exception>
        public static int ToDecimal(this string source, int @base)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} need not null source.");
            }

            if (@base > highBoundary || @base < lowBoundary)
            {
                throw new ArgumentOutOfRangeException($"{nameof(@base)} need to be from 2 to 16.");
            }

            string alphabet = "0123456789ABCDEF";
            string matching = alphabet.Substring(0, @base);

            int result = 0;
            long degree = 1;

            for(int i = source.Length - 1; i >= 0; i--)
            {
                int value = matching.IndexOf(source.ToUpper()[i]);
                if (value == -1)
                {
                    throw new ArgumentException($"{nameof(source)} is not in base.");
                }

                try
                {
                    checked
                    {
                        result += (int)degree * value;
                        degree *= @base;
                    }
                }
                catch (OverflowException exception)
                {
                    throw new ArgumentException($"{nameof(source)} is too big.");
                }
            }

            return result;
        }
    }
}
