using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class Book : IFormattable, IEquatable<Book>, IComparable<Book>
    {
        private string title;
        private string author;
        private string year;
        private string publishingHouse;
        private int edition;
        private int pages;
        private decimal price;

        public Book(string title, string author, int year, string publishingHouse, int edition, int pages, decimal price)
        {
            Title = title;
            Author = author;
            Year = year.ToString();
            PublishingHouse = publishingHouse;
            Edition = edition;
            Pages = pages;
            Price = price;
        }

        public string Title { get => title; private set => title = value; }
        public string Author { get => author; private set => author = value; }
        public string Year { get => year; private set => year = value; }
        public string PublishingHouse { get => publishingHouse; private set => publishingHouse = value; }
        public int Edition { get => edition; private set => edition = value; }
        public int Pages { get => pages; private set => pages = value; }
        public decimal Price { get => price; private set => price = value; }

        public int CompareTo(Book other)
        {
            if (ReferenceEquals(other, null))
            {
                return -1;
            }

            return (int)(Price - other.Price);
        }

        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(other, this))
            {
                return true;
            }

            return other.Author == Author && other.Title == Title;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="formatProvider">Format Provider</param>
        /// <returns>String representation.</returns>
        public string ToString(IFormatProvider formatProvider)
        {
            return ToString("G", formatProvider);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="format">The format:
        /// G, F - for full representation,
        /// S - for short representation,
        /// B - for info about author and title of the book,
        /// P - for info about title and publishing house of the book,
        /// T - for title of the book.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        /// <exception cref="FormatException">When the format string is not supported.</exception>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                format = "G";
            }
            
            if (formatProvider == null)
            {
                formatProvider = CultureInfo.CurrentCulture;
            }

            switch (format.ToUpperInvariant())
            {
                case "G":
                case "F":
                    return $"Book record: {Author, 15}, {Title, 15}, {PublishingHouse, 15}, {Year, 4}, {Edition.ToString(formatProvider)}, {Pages.ToString(formatProvider)}, {Price.ToString("C3", formatProvider)}";
                case "S":
                    return $"Book record: {Author}, {Title}, {PublishingHouse}";
                case "B":
                    return $"Book record: {Author}, {Title}";
                case "P":
                    return $"Book record: {Title}, {PublishingHouse}";
                case "T":
                    return $"Book record: {Title}";
                default:
                    throw new FormatException($"The {format} format string is not supported.");
            }
        }
    }
}
