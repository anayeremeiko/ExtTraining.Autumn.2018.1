using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BookLibrary.Tests
{
    [TestFixture]
    public class BookTests
    {
        private Book book;

        [OneTimeSetUp]
        public void CreateBookObject()
        {
            book = new Book("C# in Depth", "Jon Skeet", 2019, "Manning", 4, 900, 40);
        }

        [TestCase("G", ExpectedResult = "Book record: Jon Skeet, C# in Depth, Manning, 2019, 4, 900, $40.000")]
        [TestCase("F", ExpectedResult = "Book record: Jon Skeet, C# in Depth, Manning, 2019, 4, 900, $40.000")]
        [TestCase("S", ExpectedResult = "Book record: Jon Skeet, C# in Depth, Manning")]
        [TestCase("B", ExpectedResult = "Book record: Jon Skeet, C# in Depth")]
        [TestCase("P", ExpectedResult = "Book record: C# in Depth, Manning")]
        [TestCase("T", ExpectedResult = "Book record: C# in Depth")]
        [TestCase(null, ExpectedResult = "Book record: Jon Skeet, C# in Depth, Manning, 2019, 4, 900, $40.000")]
        [TestCase("   ", ExpectedResult = "Book record: Jon Skeet, C# in Depth, Manning, 2019, 4, 900, $40.000")]
        public string ToString_ReturnFormatString(string format)
        {
            return book.ToString(format);
        }

        [TestCase("K")]
        [TestCase("M")]
        [TestCase(".")]
        [TestCase("6432")]
        public void ToString_ThrowFormatException(string format)
        {
            Assert.Throws<FormatException>(() => book.ToString(format, CultureInfo.CurrentCulture));
        }
    }
}
