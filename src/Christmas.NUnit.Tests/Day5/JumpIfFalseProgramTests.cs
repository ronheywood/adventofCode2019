using System;
using Christmas.Day2;
using Christmas.Day5;
using FakeItEasy;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day5
{
    [TestFixture]
    public class JumpIfFalseProgramTests
    {
        [Test]
        public void Should_validate_input_is_a_valid_program()
        {
            var validatorSpy = A.Fake<IIntCodeValidator>();
            var jumpIfTrue = new JumpIfFalseProgram(validatorSpy);
            var program = "a,0,99,0";
            A.CallTo(() => validatorSpy.SplitString(program)).Returns(new IntCodeValidator().SplitString(program));
            Assert.Throws<Exception>(()=>jumpIfTrue.Process(program));
            A.CallTo(() => validatorSpy.Validate(program)).MustHaveHappened();
        } 
        [TestCase("1,0,99")]
        [TestCase("101,0,99")]
        [TestCase("145,0,99")]
        [TestCase("115,0,99")]
        public void Should_validate_op_code_6(string program)
        {
            var jumpIfTrueProgram = new JumpIfFalseProgram(new IntCodeValidator());
            var ex = Assert.Throws<InvalidOpCodeException>(() => jumpIfTrueProgram.Process(program));
            Assert.That(ex.Message, Does.Contain("Invalid input: op code (first integer)"));
        }
        [TestCase("6,3,2,99")]
        [TestCase("6,3,3,99,0")]
        public void DoesNothing_when_parameter_is_not_zero(string program)
        {
            var jumpIfFalseProgram = new JumpIfFalseProgram(new IntCodeValidator());
            Assert.That(jumpIfFalseProgram.Process(program), Is.EqualTo(string.Empty), "Program should not change");
            Assert.That(jumpIfFalseProgram.InstructionLength, Is.EqualTo(3));
        }
        [TestCase("6,4,5,99,0,3",3)]
        [TestCase("6,8,9,1101,3,3,4,99,0,7",7)]
        [TestCase("6,8,9,1101,3,3,4,99,0,6",6)]
        [TestCase("6,8,9,1101,3,3,4,99,0,0",0)]
        public void Sets_instruction_pointer_increment_to_move_to_position_given_at_index(string program, int expectedInputPosition)
        {
            var startIndex = 0;
            var jumpProgram = new JumpIfFalseProgram(new IntCodeValidator());
            jumpProgram.Process(program,startIndex);
            startIndex += jumpProgram.InstructionLength;
            Assert.That(startIndex, Is.EqualTo(expectedInputPosition));
        }
        [TestCase("106,0,4,99,10",10)]
        [TestCase("106,0,4,99,-10",-10)]
        [TestCase("1106,0,4,99,-10",4)]
        [TestCase("1106,0,0,99,-10",0)]
        public void Accepts_parameter_mode_immediate(string program, int expected)
        {
            var jumpIfFalseProgram = new JumpIfFalseProgram(new IntCodeValidator());
            jumpIfFalseProgram.Process(program);
            Assert.That(jumpIfFalseProgram.InstructionLength, Is.EqualTo(expected));
        }
        [TestCase("3,6,1106,0,4,99,-10",2,2)]
        [TestCase("1106,0,4,1106,0,0,99,-10",3,-3)]
        [TestCase("1106,0,4,6,1,2,99,-10",3,1)]
        public void Should_calculate_instruction_length_from_start_index(string program, int startIndex, int expected)
        {
            var jumpIfFalseProgram = new JumpIfFalseProgram(new IntCodeValidator());
            jumpIfFalseProgram.Process(program,startIndex);
            Assert.That(jumpIfFalseProgram.InstructionLength, Is.EqualTo(expected));
        }
    }
}