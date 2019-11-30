using System.IO;
using Christmas.Day1;
using NUnit.Framework;
namespace Christmas.Integration.Tests
{
    public class CalibrationIntegrationTest
    {
        [Test]
        public void It_reads_lines_from_a_file()
        {
            int answer;
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory + "\\TestData\\input.txt"))
            {
                answer = new Calibration(new AdventStringConverter(), 0).Calibrate(sr);
            }

            Assert.That(answer, Is.EqualTo(582), "Some useful error message");
        }
        [Test]
        public void It_finds_match_from_a_file()
        {
            int answer;
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory + "\\TestData\\input.txt"))
            {
                answer = new Calibration(new AdventStringConverter(), 0).FindMatch(sr);
            }

            Assert.That(answer, Is.EqualTo(488), "Some useful error message");
        }
    }
}
