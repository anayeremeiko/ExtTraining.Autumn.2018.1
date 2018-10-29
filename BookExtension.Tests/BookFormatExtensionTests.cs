using System;
using System.Globalization;
using BookLibrary;
using NUnit.Framework;

namespace BookExtension.Tests
{
    [TestFixture()]
    public class BookFormatExtensionTests
    {
        private Book book;
        private BookFormatExtension formatProvider;

        [OneTimeSetUp]
        public void CreateBookObject()
        {
            book = new Book("C# in Depth", "Jon Skeet", 2019, "Manning", 4, 900, 40);
            formatProvider = new BookFormatExtension(CultureInfo.CreateSpecificCulture("en-US"));
        }

        [TestCase("I", ExpectedResult = "Book record: Jon Skeet, C# in Depth, 2019, $40.000")]
        [TestCase("G", ExpectedResult = "Book record: Jon Skeet, C# in Depth, Manning, 2019, 4, 900, $40.000")]
        [TestCase("F", ExpectedResult = "Book record: Jon Skeet, C# in Depth, Manning, 2019, 4, 900, $40.000")]
        [TestCase("S", ExpectedResult = "Book record: Jon Skeet, C# in Depth, Manning")]
        [TestCase("B", ExpectedResult = "Book record: Jon Skeet, C# in Depth")]
        [TestCase("P", ExpectedResult = "Book record: C# in Depth, Manning")]
        [TestCase("T", ExpectedResult = "Book record: C# in Depth")]
        [TestCase(null, ExpectedResult = "Book record: Jon Skeet, C# in Depth, Manning, 2019, 4, 900, $40.000")]
        [TestCase("   ", ExpectedResult = "Book record: Jon Skeet, C# in Depth, Manning, 2019, 4, 900, $40.000")]
        public string Format_ReturnFormattedString(string format)
        {
            return string.Format(formatProvider, "{0:" + format + "}", book);
        }

        [TestCase(12345.6789, ExpectedResult = "$12,345.679")]
        [TestCase(0, ExpectedResult = "$0.000")]
        [TestCase(3, ExpectedResult = "$3.000")]
        public string Format_HandleOtherFormats(object number)
        {
            return string.Format(formatProvider, "{0:C3}", number);
        }

        [TestCase(12345.6789, ExpectedResult = "12345.6789")]
        [TestCase(0, ExpectedResult = "0")]
        [TestCase(3, ExpectedResult = "3")]
        public string Format_HandleNull(object number)
        {
            return string.Format(formatProvider, "{0:" + null + "}", number);
        }

        [TestCase("L")]
        [TestCase("K")]
        [TestCase("123")]
        public void Format_ThrowFormatException(string format)
        {
            Assert.Throws<FormatException>(() => string.Format(formatProvider, "{" + format + "}", book));
        }
    }
}
