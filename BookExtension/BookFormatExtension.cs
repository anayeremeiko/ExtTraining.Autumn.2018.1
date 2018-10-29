using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLibrary;

namespace BookExtension
{
    public class BookFormatExtension : IFormatProvider, ICustomFormatter
    {
        private IFormatProvider parent;

        public BookFormatExtension() : this(CultureInfo.CurrentCulture) { }

        public BookFormatExtension(IFormatProvider parent) => this.parent = parent;

        /// <summary>
        /// Returns an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">An object that specifies the type of format object to return.</param>
        /// <returns>
        /// An instance of the object specified by <paramref name="formatType" />, if the <see cref="T:System.IFormatProvider" /> implementation can supply that type of object; otherwise, <see langword="null" />.
        /// </returns>
        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        /// <summary>
        /// Converts the value of a specified object to an equivalent string representation using specified format and culture-specific formatting information.
        /// </summary>
        /// <param name="format">A format string containing formatting specifications.</param>
        /// <param name="arg">An object to format.</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        /// <returns>
        /// The string representation of the value of <paramref name="arg" />, formatted as specified by <paramref name="format" /> and <paramref name="formatProvider" />.
        /// </returns>
        /// <exception cref="FormatException">
        /// The format of format is invalid.
        /// </exception>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            // Provide default formatting if arg is not a Book.
            if (arg.GetType() != typeof(Book))
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException($"The format of '{format}' is invalid.", e);
                }

            if (format is null)
            {
                format = "G";
            }

            // Provide default formatting for unsupported format strings.
            if (format.ToUpper(CultureInfo.InvariantCulture) != "I")
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException($"The format of '{format}' is invalid.", e);
                }

            Book book = (Book)arg;
            return "Book record: " + book.Author + ", " + book.Title + ", " + book.Year + ", " + book.Price;
        }

        /// <summary>
        /// Handles the other formats.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg">The argument.</param>
        /// <returns>The string representation of the value of <paramref name="arg" />, formatted as specified by <paramref name="format" />.</returns>
        private string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable)
            {
                return ((IFormattable)arg).ToString(format, parent);
            }

            return arg != null ? arg.ToString() : string.Empty;
        }
    }
}
