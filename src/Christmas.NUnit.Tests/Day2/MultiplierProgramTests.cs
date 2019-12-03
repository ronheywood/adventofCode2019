using System;
using Christmas.Day2;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day2
{
    [TestFixture]
    public class MultiplierProgramTests
    {
        [Test]
        public void Should_valldate_input()
        {
            var validator = new ValidatorSpy();
            var m = new MultiplierProgram(validator);
            Assert.Throws<Exception>(() => m.Process("abc"));
            Assert.That(validator.WasCalled, Is.True);
        }
        [TestCase("1,1")]
        [TestCase("22,1")]
        public void Should_validate_op_code_is_two(string input)
        {
            var validator = new IntCodeValidator();
            var m = new MultiplierProgram(validator);
            var ex = Assert.Throws<Exception>(() => m.Process(input));
            Assert.That(ex.Message, Is.EqualTo("Invalid input: op code (first digit) must be 2"));
        }
        [TestCase("2,0,0,0","4,0,0,0")]
        [TestCase("2,1,1,3","2,1,1,1")]
        [TestCase("2,5,6,0,99,10,10","100,5,6,0,99,10,10")]
        [TestCase("2,5,6,7,99,10,10,0","2,5,6,7,99,10,10,100")]
        public void Should_multiply_two_numbers_and_update_at_position(string input, string expected)
        {
            var m = new MultiplierProgram(new IntCodeValidator());
            Assert.That(m.Process(input), Is.EqualTo(expected));
        }
    }
}
