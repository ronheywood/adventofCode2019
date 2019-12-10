using System;
using Christmas.Day2;
using Christmas.Day5;
using Christmas.NUnit.Tests.Day5;
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
        [TestCase("222,1")]
        public void Should_validate_op_code_is_two(string input)
        {
            var validator = new IntCodeValidator();
            var m = new MultiplierProgram(validator);
            var ex = Assert.Throws<InvalidOpCodeException>(() => m.Process(input));
            Assert.That(ex.Message, Is.EqualTo("Invalid input: op code (first integer)"));
        }
        [TestCase("11102,1,2,3,99")]
        [TestCase("11002,1,2,3,99")]
        [TestCase("02,1,2,3,99")]
        [TestCase("102,1,2,3,99")]
        public void Should_validate_in_program_configuration(string input)
        {
            var m = new MultiplierProgram(new IntCodeValidator());
            Assert.DoesNotThrow(() => m.Process(input));
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
        [TestCase("102,5,5,9,1,1,2,9,99,0","102,5,5,9,1,1,2,9,99,5")]
        [TestCase("1102,5,5,9,1,1,2,9,99,0","1102,5,5,9,1,1,2,9,99,25")]
        public void Should_support_immediate_mode(string input,string expected)
        {
            Assert.That(new MultiplierProgram(new IntCodeValidator()).Process(input, 0), Is.EqualTo(expected));
        }
    }
}
