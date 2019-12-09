using System;
using Christmas.Day2;
using Christmas.Day5;
using FakeItEasy;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day5
{
    [TestFixture]
    public class LessThanProgramTests
    {
        [Test]
        public void Should_validate_input_is_a_valid_program()
        {
            var validatorSpy = A.Fake<IIntCodeValidator>();
            var jumpIfTrue = new LessThanProgram(validatorSpy);
            var program = "a,0,99,0";
            A.CallTo(() => validatorSpy.SplitString(program)).Returns(new IntCodeValidator().SplitString(program));
            Assert.Throws<Exception>(()=>jumpIfTrue.Process(program));
            A.CallTo(() => validatorSpy.Validate(program)).MustHaveHappened();
        } 
        [TestCase("1,0,99")]
        [TestCase("101,0,99")]
        [TestCase("147,0,99")]
        [TestCase("117,0,99")]
        public void Should_validate_op_code_7(string program)
        {
            var jumpIfTrueProgram = new LessThanProgram(new IntCodeValidator());
            var ex = Assert.Throws<InvalidOpCodeException>(() => jumpIfTrueProgram.Process(program));
            Assert.That(ex.Message, Does.Contain("Invalid input: op code (first integer)"));
        }
        [TestCase("7,3,1,5,99,-1","7,3,1,5,99,0")]
        [TestCase("7,6,1,5,99,-1,220","7,6,1,5,99,0,220")]
        public void Stores_zero_in_pointer_position_when_parameter_2_greater_than_parameter_1(string program,  string expected)
        {
            var LessThanProgram = new LessThanProgram(new IntCodeValidator());
            Assert.That(LessThanProgram.Process(program), Is.EqualTo(expected), "Program should store 0");
            Assert.That(LessThanProgram.InstructionLength, Is.EqualTo(4));
        }
        [TestCase("7,5,2,5,99,-1","7,5,2,5,99,1")]
        [TestCase("7,1,4,5,99,-1,220","7,1,4,5,99,1,220")]
        public void Stores_one_in_pointer_position_when_parameter_2_less_than_parameter_1(string program,  string expected)
        {
            var LessThanProgram = new LessThanProgram(new IntCodeValidator());
            Assert.That(LessThanProgram.Process(program), Is.EqualTo(expected), "Program should store 0");
            Assert.That(LessThanProgram.InstructionLength, Is.EqualTo(4));
        }
        [Test]
        public void Supports_start_index()
        {
            var program = "3,9,7,9,10,9,4,9,99,-1,8";
            var expected = "3,9,7,9,10,9,4,9,99,1,8";

            var LessThanProgram = new LessThanProgram(new IntCodeValidator());
            Assert.That(LessThanProgram.Process(program,2), Is.EqualTo(expected), "Program should store 1");
        }
        [TestCase("107,4,1,5,99,10","107,4,1,5,99,0")]
        [TestCase("107,100,0,5,99,10","107,100,0,5,99,1")]
        [TestCase("1107,0,4,5,99,-10","1107,0,4,5,99,1")]
        [TestCase("1107,4,0,5,99,-10","1107,4,0,5,99,0")]
        public void Accepts_parameter_mode_immediate(string program, string expected)
        {
            var LessThanProgram = new LessThanProgram(new IntCodeValidator());
            var output = LessThanProgram.Process(program);
            Assert.That(output, Is.EqualTo(expected));
        }
    }
}
