using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringExtension
{
    public static class Parser
    {
        /// <summary>
        /// Converts to decimal.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="base">The base.</param>
        /// <returns>Decimal number.</returns>
        /// <exception cref="ArgumentNullException">Source number need to be not null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Base need to be less then 17 and bigger then 1.</exception>
        /// <exception cref="ArgumentException">Number doesn't match.</exception>
        /// <exception cref="OverflowException">Number is too big.</exception>
        public static int ToDecimal(this string source, int @base)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} need not null source.");
            }

            if (@base > 16 || @base < 2)
            {
                throw new ArgumentOutOfRangeException($"{nameof(@base)} need to be bigger then 1 and less then 17.");
            }
            
            foreach (char number in source)
            {
                if ((char.IsDigit(number) && int.Parse(number.ToString()) > @base - 1) || (char.IsLetter(number) && @base < 11))
                {
                    throw new ArgumentException($"{nameof(source)} is not in base.");
                }
            }

            return Convert(source, @base);
        }

        /// <summary>
        /// Converts the specified number to decimal.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="base">The base.</param>
        /// <returns>Decimal number.</returns>
        /// <exception cref="OverflowException">Number is too big.</exception>
        private static int Convert(string number, int @base)
        {
            Dictionary<char, int> matching = new Dictionary<char, int>()
            {
                { '0', 0 },
                { '1', 1 },
                { '2', 2 },
                { '3', 3 },
                { '4', 4 },
                { '5', 5 },
                { '6', 6 },
                { '7', 7 },
                { '8', 8 },
                { '9', 9 },
                { 'A', 10 },
                { 'B', 11 },
                { 'C', 12 },
                { 'D', 13 },
                { 'E', 14 },
                { 'F', 15 },
            };

            int result = 0;
            for (int i = 0; i < number.Length; i++)
            {
                result += matching[char.ToUpper(number[number.Length - i - 1])] * Pow(@base, i);
            }

            if (result <= 0)
            {
                throw new OverflowException($"{nameof(number)} is too big.");
            }

            return result;
        }

        /// <summary>
        /// Grades base in specified degree.
        /// </summary>
        /// <param name="base">The base.</param>
        /// <param name="degree">The degree.</param>
        /// <returns>Base in specified degree.</returns>
        private static int Pow(int @base, int degree)
        {
            int result = 1;
            for (int i = 0; i < degree; i++)
            {
                result *= @base;
            }

            return result;
        }
    }
}
