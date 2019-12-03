using System;
using System.IO;
using System.Linq;
using Christmas.Day2;
using Christmas.NUnit.Tests.Day2;
using NUnit.Framework;

namespace Christmas.Integration.Tests.Day2
{
    [TestFixture]
    public class Day2IntegrationTest
    {
        [Test]
        public void Solution()
        {
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory + "\\TestData\\day2.txt"))
            {
                
                var validator = new IntCodeValidator();
                var input = sr.ReadToEnd();
                var modified = validator.SplitString(input).ToArray();
                modified[1] = 12;
                modified[2] = 2;
                input = validator.Join(modified);

                var actual = new RecursiveProgram(new IntCodeValidator()).Process(input);
                Assert.That(validator.SplitString(actual).First(), Is.EqualTo(4690667));
            }
        }
        [Ignore("Slow to brute force")]
        [Test]
        public void Solution2()
        {
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory + "\\TestData\\day2.txt"))
            {
                var input = sr.ReadToEnd();
                
                var (item1, item2) = new RecursiveProgram(new IntCodeValidator()).GetForOutput(input,19690720);
                var calculated = 100 * item1 + item2;
                Assert.That(calculated, Is.EqualTo(6255));
            }
        }
    }
}
