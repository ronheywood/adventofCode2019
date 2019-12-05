using System;
using Christmas.Day2;
using Christmas.Day5;
using FakeItEasy;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Christmas.NUnit.Tests.Day5
{
    public class TransposeProgramTests
    {
        [Test]
        public void Should_validate_input_is_a_valid_program()
        {
            var validatorSpy = A.Fake<IIntCodeValidator>();
            var transposeProgram = new TransposeProgram(validatorSpy);
            var program = "3,0,99,0";
            A.CallTo(() => validatorSpy.SplitString(program)).Returns(new IntCodeValidator().SplitString(program));
            Assert.Throws<Exception>(()=>transposeProgram.Process(program,1,0));
            A.CallTo(() => validatorSpy.Validate(program)).MustHaveHappened();
        }
        [Test]
        public void Should_validate_op_code_is_3()
        {
            var program = "1,0,99,0";
            var transposeProgram = new TransposeProgram(new IntCodeValidator());
            var ex = Assert.Throws<InvalidOpCodeException>(()=>transposeProgram.Process(program,1,0));
            Assert.That(ex.Message, Is.EqualTo("Invalid input: op code (first integer)"));
        }
        [TestCase("3,0,99")]
        [TestCase("103,0,99")]
        [TestCase("1003,0,99")]
        [TestCase("03,0,99")]
        public void Should_validate_op_code_in_program_configuration(string program)
        {
            var transposeProgram = new TransposeProgram(new IntCodeValidator());
            Assert.DoesNotThrow(() => transposeProgram.Process(program,1,0));
        }
        [TestCase("3,3,99,0",1,"3,3,99,1")]
        [TestCase("3,3,99,0",10,"3,3,99,10")]
        [TestCase("3,3,99,0",-1,"3,3,99,-1")]
        [TestCase("3,2,99,0",0,"3,2,0,0")]
        public void Should_write_input_value_at_position_given_in_index_1(string program, int input, string expected)
        {
            Assert.That(new TransposeProgram(new IntCodeValidator()).Process(program,input,0), Is.EqualTo(expected));
        }
    }
}
