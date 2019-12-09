using System;
using Christmas.Day2;
using Christmas.Day5;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day5
{
    public class IntCodeProgramFactoryTests
    {
        [TestCase(1, typeof(AdderProgram))]
        [TestCase(2, typeof(MultiplierProgram))]
        [TestCase(3, typeof(TransposeProgram))]
        [TestCase(4, typeof(OutputProgram))]
        [TestCase(5, typeof(JumpIfTrueProgram))]
        [TestCase(6, typeof(JumpIfFalseProgram))]
        [TestCase(7, typeof(LessThanProgram))]
        [TestCase(8, typeof(EqualToProgram))]
        public void Should_create_program_for_op_code(int opcode, Type expected)
        {
            var program = new IntCodeProgramFactory().GetProgram(opcode);
            Assert.That(program.GetType(),Is.EqualTo(expected));
        }
    }
}
