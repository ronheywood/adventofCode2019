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
        [Test]
        public void Should_validate_op_code_4()
        {
            var outputProgram = new OutputProgram(new IntCodeValidator());
            var program = "1,0,99";
            var ex = Assert.Throws<Exception>(() => outputProgram.Process(program));
            Assert.That(ex.Message, Does.Contain("Invalid Op Code"));
        }
        [TestCase("4,1,99","1")]
        [TestCase("4,-5,99,0,0,0","-5")]
        [TestCase("4,27,99,0,0,0","27")]
        public void Should_return_value_at_position(string program, string expected)
        {
            var outputProgram = new OutputProgram(new IntCodeValidator());
            Assert.That(outputProgram.Process(program),Is.EqualTo(expected));
        }
        [Test]
        public void Should_run_program_from_start_position()
        {
            var outputProgram = new OutputProgram(new IntCodeValidator());
            var program = "1,2,3,4,99,4,10,99";
            Assert.That(outputProgram.Process(program,5),Is.EqualTo("10"));
        }
    }
}
