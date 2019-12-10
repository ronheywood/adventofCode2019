using Christmas.Day2;
using NUnit.Framework;

namespace Christmas.Integration.Tests.Day5
{
    [TestFixture]
    public class LessThanOrEqualProgramMultipleTests
    {
        public string EqualToPositional = "3,9,8,9,10,9,4,9,99,-1,8";
        public string EqualToImmediate = "3,3,1108,-1,8,3,4,3,99";
        [TestCase(8,"1")]
        [TestCase(4,"0")]
        public void Test_equal_to_ouputs_expected_for_input(int input, string expected)
        {

            var program = new RecursiveProgram(new IntCodeValidator(), new IntCodeProgramFactory());
            var diagnostics = program.RunDiagnostics(input,EqualToPositional);
            Assert.That(diagnostics, Is.EqualTo(expected));
            
            var diagnosticsImmediate = program.RunDiagnostics(input,EqualToImmediate);
            Assert.That(diagnosticsImmediate, Is.EqualTo(expected));
        }
        
        public string LessThatPositional = "3,9,7,9,10,9,4,9,99,-1,8";
        public string LessThatImmediate = "3,3,1107,-1,8,3,4,3,99";
        [TestCase(8,"0")]
        [TestCase(4,"1")]
        public void Test_less_than_expected_for_input(int input, string expected)
        {
            var program = new RecursiveProgram(new IntCodeValidator(), new IntCodeProgramFactory());
            var diagnostics = program.RunDiagnostics(input,LessThatPositional);
            Assert.That(diagnostics, Is.EqualTo(expected));
            
            var diagnosticsImmediate = program.RunDiagnostics(input,LessThatImmediate);
            Assert.That(diagnosticsImmediate, Is.EqualTo(expected));
        }

    }
}
