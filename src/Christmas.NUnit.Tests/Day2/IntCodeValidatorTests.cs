using Christmas.Day2;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day2
{
    [TestFixture]
    public class IntCodeValidatorTests
    {
        [TestCase("1,2,3,4",true)]
        [TestCase("a,2,3,4",false)]
        [TestCase("1234",false)]
        public void Should_validate_IntList(string input, bool expected)
        {
            Assert.That(new IntCodeValidator().Validate(input), Is.EqualTo(expected));
        }
    }
}
