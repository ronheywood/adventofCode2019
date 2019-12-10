using System;
using Christmas.Day2;
using Christmas.Day5;
using FakeItEasy;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day5
{
    [TestFixture]
    public class OutputProgramTests
    {
        [Test]
        public void Should_validate_input_is_a_valid_program()
        {
            var validatorSpy = A.Fake<IIntCodeValidator>();
            var outputProgram = new OutputProgram(validatorSpy);
            var program = "a,0,99,0";
            A.CallTo(() => validatorSpy.SplitString(program)).Returns(new IntCodeValidator().SplitString(program));
            Assert.Throws<Exception>(()=>outputProgram.Process(program));
            A.CallTo(() => validatorSpy.Validate(program)).MustHaveHappened();
        }
        [TestCase("1,0,99")]
        [TestCase("101,0,99")]
        [TestCase("144,0,99")]
        [TestCase("114,0,99")]
        public void Should_validate_op_code_4(string program)
        {
            var outputProgram = new OutputProgram(new IntCodeValidator());
            var ex = Assert.Throws<InvalidOpCodeException>(() => outputProgram.Process(program));
            Assert.That(ex.Message, Does.Contain("Invalid input: op code (first integer)"));
        }
        [TestCase("4,0,99")]
        [TestCase("104,0,99")]
        [TestCase("1004,0,99")]
        [TestCase("04,0,99")]
        public void Should_validate_op_code_in_program_configuration(string program)
        {
            var outputProgram = new OutputProgram(new IntCodeValidator());
            Assert.DoesNotThrow(() => outputProgram.Process(program));
        }
        [TestCase("4,0,99","4")]
        [TestCase("4,3,99,-5,0,0","-5")]
        [TestCase("4,4,99,-5,27,0","27")]
        public void Should_return_value_at_position(string program, string expected)
        {
            var outputProgram = new OutputProgram(new IntCodeValidator());
            Assert.That(outputProgram.Process(program),Is.EqualTo(expected));
        }
        [Test]
        public void Should_run_program_from_start_position()
        {
            var outputProgram = new OutputProgram(new IntCodeValidator());
            var program = "1,2,3,4,99,4,10,99,0,0,10";
            Assert.That(outputProgram.Process(program,5),Is.EqualTo("10"));
        }
        [Test]
        public void Should_respect_immediate_mode()
        {
            var program = "104,99,99";
            var outputProgram = new OutputProgram(new IntCodeValidator());
            Assert.That(outputProgram.Process(program,0), Is.EqualTo("99"));
        }
    }
}
