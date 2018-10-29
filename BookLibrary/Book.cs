using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class Book : IFormattable
    {
        private string title;
        private string author;
        private string year;
        private string publishingHouse;
        private string edition;
        private string pages;
        private string price;

        public Book(string title, string author, int year, string publishingHouse, int edition, int pages, decimal price)
        {
            Title = title;
            Author = author;
            Year = year.ToString();
            PublishingHouse = publishingHouse;
            Edition = edition.ToString();
            Pages = pages.ToString();
            Price = price.ToString("C3", CultureInfo.CreateSpecificCulture("en-US"));
        }

        public string Title { get => title; private set => title = value; }
        public string Author { get => author; private set => author = value; }
        public string Year { get => year; private set => year = value; }
        public string PublishingHouse { get => publishingHouse; private set => publishingHouse = value; }
        public string Edition { get => edition; private set => edition = value; }
        public string Pages { get => pages; private set => pages = value; }
        public string Price { get => price; private set => price = value; }

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
                    return $"Book record: {Author}, {Title}, {PublishingHouse}, {Year}, {Edition}, {Pages}, {Price}";
                case "S":
                    return $"Book record: {Author}, {Title}, {PublishingHouse}";
                case "B":
                    return $"Book record: {Author}, {Title}";
                case "P":
                    return $"Book record: {Title}, {PublishingHouse}";
                case "T":
                    return "Book record: {Title}";
                default:
                    throw new FormatException($"The {format} format string is not supported.");
            }
        }
    }
}
