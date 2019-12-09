using System;
using Christmas.Day2;
using Christmas.Day5;
using FakeItEasy;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day5
{
    [TestFixture]
    public class EqualToProgramTests
    {
        [Test]
        public void Should_validate_input_is_a_valid_program()
        {
            var validatorSpy = A.Fake<IIntCodeValidator>();
            var jumpIfTrue = new EqualToProgram(validatorSpy);
            var program = "a,0,99,0";
            A.CallTo(() => validatorSpy.SplitString(program)).Returns(new IntCodeValidator().SplitString(program));
            Assert.Throws<Exception>(()=>jumpIfTrue.Process(program));
            A.CallTo(() => validatorSpy.Validate(program)).MustHaveHappened();
        } 
        [TestCase("1,0,99")]
        [TestCase("101,0,99")]
        [TestCase("148,0,99")]
        [TestCase("118,0,99")]
        public void Should_validate_op_code_8(string program)
        {
            var equalToProgram = new EqualToProgram(new IntCodeValidator());
            var ex = Assert.Throws<InvalidOpCodeException>(() => equalToProgram.Process(program));
            Assert.That(ex.Message, Does.Contain("Invalid input: op code (first integer)"));
        }
        [TestCase("8,1,2,5,99,-1","8,1,2,5,99,0")]
        [TestCase("8,1,6,5,99,-1,220","8,1,6,5,99,0,220")]
        public void Stores_zero_in_pointer_position_when_parameter_2_greater_than_parameter_1(string program,  string expected)
        {
            var equalToProgram = new EqualToProgram(new IntCodeValidator());
            Assert.That(equalToProgram.Process(program), Is.EqualTo(expected), "Program should store 0");
            Assert.That(equalToProgram.InstructionLength, Is.EqualTo(4));
        }
        [TestCase("8,3,2,5,99,-1","8,3,2,5,99,0")]
        [TestCase("8,6,1,5,99,-1,220","8,6,1,5,99,0,220")]
        public void Stores_zero_in_pointer_position_when_parameter_2_less_than_parameter_1(string program,  string expected)
        {
            var equalToProgram = new EqualToProgram(new IntCodeValidator());
            Assert.That(equalToProgram.Process(program), Is.EqualTo(expected), "Program should store 0");
            Assert.That(equalToProgram.InstructionLength, Is.EqualTo(4));
        }
        [TestCase("8,3,3,5,99,-1","8,3,3,5,99,1")]
        [TestCase("8,1,1,5,99,-1,220","8,1,1,5,99,1,220")]
        public void Store_one_in_pointer_position_when_parameter_2_same_as_parameter_1(string program,  string expected)
        {
            var greaterThanProgram = new EqualToProgram(new IntCodeValidator());
            Assert.That(greaterThanProgram.Process(program), Is.EqualTo(expected), "Program should store 1");
            Assert.That(greaterThanProgram.InstructionLength, Is.EqualTo(4));
        }
        [TestCase("108,0,1,5,99,10","108,0,1,5,99,1")]
        [TestCase("108,100,4,5,99,10","108,100,4,5,99,0")]
        [TestCase("1108,0,4,5,99,-10","1108,0,4,5,99,0")]
        [TestCase("1108,4,4,5,99,-10","1108,4,4,5,99,1")]
        public void Accepts_parameter_mode_immediate(string program, string expected)
        {
            var equalToProgram = new EqualToProgram(new IntCodeValidator());
            var output = equalToProgram.Process(program);
            Assert.That(output, Is.EqualTo(expected));
        }
    }
}
