using Christmas.Day2;
using NUnit.Framework;

namespace Christmas.Integration.Tests.Day5
{
    [TestFixture]
    public class JumpCodeProgramMultipleTests
    {
        public string positionalmode = "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9";
        public string immediatemode = "3,3,1105,-1,9,1101,0,0,12,4,12,99,1";

        [TestCase(10,"1"), Category("copies 10 to position 12, (therfore jump if false does not update position 15) " +
                                      "then it adds 0 and 1 into position 13 - so the last program outputs 1")]
        [TestCase(0,"0"), Category("copies 0 to position 12, (therfore jump if false does update position to 9) " +
                                     "then it outputs position 13 - which is 0")]
        public void Should_output_for_positional_mode(int input,string expected)
        {
            var program = new RecursiveProgram(new IntCodeValidator(), new IntCodeProgramFactory());
            var diagnostics = program.RunDiagnostics(input,positionalmode);
            Assert.That(diagnostics, Is.EqualTo(expected));
        }
    }
    [TestFixture]
    public class MultiProgramTest
    {
        private string program = @"3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
            1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
            999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";

        private readonly RecursiveProgram _recursiveProgram = new RecursiveProgram(new IntCodeValidator(), new IntCodeProgramFactory());

        [Test]
        public void Larger_program_outputs_999_if_less_than_8()
        {
            Assert.That(_recursiveProgram.RunDiagnostics(7,program), Is.EqualTo("999"));
        }

        [Test]
        public void Larger_program_outputs_1000_if_equal_to_8()
        {
            Assert.That(_recursiveProgram.RunDiagnostics(8, program), Is.EqualTo("1000"));
        }

        [Test]
        public void Larger_program_outputs_1001_if_greater_than_8()
        {
            Assert.That(_recursiveProgram.RunDiagnostics(27,program), Is.EqualTo("1001"));
        }
    }
}
