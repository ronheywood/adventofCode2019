using System.IO;
using NUnit.Framework;

namespace Christmas.NUnit.Tests
{
    [TestFixture]
    public class CalibrationStringConverterTests
    {
        [TestCase("+1",1)]
        [TestCase("+10",10)]
        [TestCase("+10750",10750)]
        [TestCase("-1",-1)]
        [TestCase("-10",-10)]
        [TestCase("-10750",-10750)]
        [TestCase("NaN",0)]
        [TestCase("3.141592",0)]
        public void Should_convert_string_to_int(string input,int expected)
        {
            var stringConverter = new AdventStringConverter();
            Assert.That(stringConverter.ParseInt(input), Is.EqualTo(expected));
        }
        [TestCase()]
        public void Should_convert_stream_reader_to_csv()
        {
            var fileToRead = TestContext.CurrentContext.TestDirectory + "\\TestData\\stream-reader-test.txt";
            using (var sr = new StreamReader(fileToRead))
            {
                Assert.That(new AdventStringConverter().GetCsv(sr), Is.EqualTo("line1,line2,line3"));
            }
        }
    }
}