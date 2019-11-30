using System.IO;
using System.Text;
using Christmas.Day1;
using NUnit.Framework;

namespace Christmas.NUnit.Tests
{
    [TestFixture]
    public class GivenAStartingFrequency
    {
        private readonly int _frequency = 0;
        private Calibration _calibration;

        [SetUp]
        public void SetUp()
        {
            _calibration = new Calibration(new AdventStringConverter(),_frequency);

        }
        [Test]
        public void When_input_is_empty()
        {
            Assert.That(_calibration.Frequency,Is.EqualTo(_frequency));
        }
        [Test]
        public void When_input_is_positive([Random(0,255,3)] int input)
        {
            var frequency = _calibration.Calibrate($"+{input}");
            Assert.That(frequency,Is.EqualTo(input));
        }
        [Test]
        public void When_input_is_negative([Random(0,255,3)] int input)
        {
            var frequency = _calibration.Calibrate($"-{input}");
            Assert.That(frequency,Is.EqualTo(input*-1));
        }
        [TestCase("+1,-1",0)]
        [TestCase("+1,+1",2)]
        [TestCase("-1,-1",-2)]
        [TestCase("+100000,-1",99999)]
        public void Calibration_list_separated_by_commas(string list,int expected)
        {
            Assert.That(_calibration.Calibrate(list), Is.EqualTo(expected));
        }
        [TestCase("+1,-1",0)]
        [TestCase("+3, +3, +4, -2, -4",10)]
        [TestCase("-6, +3, +8, +5, -6",5)]
        [TestCase("+7, +7, -2, -7, -4",14)]
        public void It_checks_for_duplicate_frequency(string list, int expected)
        {
            Assert.That(_calibration.FindMatch(list), Is.EqualTo(expected));
        }
        
        [TestCase("+1,-1",0)]
        [TestCase("+3, +3, +4, -2, -4",10)]
        [TestCase("-6, +3, +8, +5, -6",5)]
        [TestCase("+7, +7, -2, -7, -4",14)]
        public void It_checks_for_duplicate_frequency_from_stream_reader(string list, int expected)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes( list );
            MemoryStream stream = new MemoryStream( byteArray );
            using (var sr = new StreamReader(stream))
            {
                Assert.That(_calibration.FindMatch(sr), Is.EqualTo(expected));
            }

        }
    }
}
