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
}
