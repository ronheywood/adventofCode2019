using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NSubstitute.Exceptions;
using NUnit.Framework;
using NUnit.Framework.Constraints;

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
            _calibration = new Calibration(_frequency);

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

        [Test]
        public void It_reads_lines_from_a_file()
        {
            int answer;
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory + "\\TestData\\input.txt"))
            {
                answer = _calibration.Calibrate(sr);
            }

            Assert.That(answer, Is.EqualTo(582), "Some useful error message");
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
        
        [Test]
        public void It_finds_match_from_a_file()
        {
            int answer;
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory + "\\TestData\\input.txt"))
            {
                answer = _calibration.FindMatch(sr);
            }

            Assert.That(answer, Is.EqualTo(582), "Some useful error message");
        }
    }

    [TestFixture]
    public class CalibrationStringConverter
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
            Assert.That(Calibration.ParseFrequency(input), Is.EqualTo(expected));
        }
    }

    public class Calibration
    {
        public readonly int Frequency;

        public Calibration(int frequency)
        {
            Frequency = frequency;
        }

        public static int ParseFrequency(string s)
        {
            return (int.TryParse(s,out var parsed)) ? parsed : 0;
        }

        public int Calibrate(string input) => Frequency + input.Split(',').Sum(ParseFrequency);

        public int Calibrate(StreamReader sr)
        {
            var start = Frequency;
            while (sr.Peek() >= 0) {
                start += ParseFrequency(sr.ReadLine());
            }
            
            return start;
        }

//        public int FindMatch(StreamReader sr,int current =0, List<int> seen = null)
//        {
//            if (seen == null) seen = new List<int>();
//            while (sr.Peek() >= 0)
//            {
//                current += ParseFrequency(sr.ReadLine());
//                if (seen.Contains(current)) return current;
//                seen.Add(current);
//            }
//            sr.DiscardBufferedData();
//            sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin); 
//            return FindMatch(sr,current,seen);
//        }
        public int FindMatch(string input)
        {
            var current = Frequency;
            var seen = new List<int> {Frequency};
            while (true)
            {
                var parts = input.Split(',');
                foreach (var frequency in parts)
                {
                    var calibrate = ParseFrequency(frequency);
                    current += calibrate;
                    if (seen.Contains(current)) return current;
                    seen.Add(current);
                }
            }
        }

        public int FindMatch(StreamReader input)
        {
            var csv = String.Empty;
            while (input.Peek() >= 0)
            {
                var value = input.ReadLine();
                csv += $"{value},";
            }

            csv = csv.Substring(0, csv.LastIndexOf(",", StringComparison.Ordinal));
            return FindMatch(csv);
        }
    }
}
