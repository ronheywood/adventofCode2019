using Christmas.Day5;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Christmas.NUnit.Tests.Day5
{
    [TestFixture]
    public class ProgramConfigurationTests
    {
        [TestCase("1,0,0,0,99",1)]
        [TestCase("2,0,0,0,99",2)]
        [TestCase("3,0,99",3)]
        [TestCase("4,0,99",4)]
        public void Should_extract_opcode_from_single_digit(string program, int expected)
        {
            Assert.That(ProgramConfiguration.GetOpCode(program),Is.EqualTo(expected));
        }
        [TestCase("01,0,0,0,99",1)]
        [TestCase("02,0,0,0,99",2)]
        [TestCase("03,0,99",3)]
        [TestCase("04,0,99",4)]
        [TestCase("10,0,99",10)]
        [TestCase("101,0,99",1)]
        [TestCase("1001,0,99",1)]
        [TestCase("10002,0,99",2)]
        public void Should_extract_opcode_from_last_2_digits(string program, int expected)
        {
            Assert.That(ProgramConfiguration.GetOpCode(program),Is.EqualTo(expected));
        }
        
        [TestCase("01,1,1,3,99",ParameterMode.Positional)]
        [TestCase("11,1,1,3,99",ParameterMode.Positional)]
        [TestCase("011,1,1,3,99",ParameterMode.Positional)]
        [TestCase("1101,1,1,3,99",ParameterMode.Immediate)]
        [TestCase("1111,1,1,3,99",ParameterMode.Immediate)]
        [TestCase("1001,1,1,3,99",ParameterMode.Positional)]
        public void Should_extract_parameter_mode_for_parameter_1(string program, ParameterMode expected)
        {
            ProgramConfiguration.GetParameterMode(program, out var i1, out var ignored,out var unused);
            Assert.That(i1,Is.EqualTo(expected));
        }
        [TestCase("1001,1,1,3,99",ParameterMode.Immediate)]
        [TestCase("0001,1,1,3,99",ParameterMode.Positional)]
        [TestCase("10001,1,1,3,99",ParameterMode.Positional)]
        [TestCase("11001,1,1,3,99",ParameterMode.Immediate)]
        [TestCase("11101,1,1,3,99",ParameterMode.Immediate)]
        public void Should_extract_parameter_mode_for_parameter_2(string program, ParameterMode expected)
        {
            ProgramConfiguration.GetParameterMode(program, out var ignored, out var i2,out var unused);
            Assert.That(i2,Is.EqualTo(expected));
        }
        [TestCase("11101,1,1,3,99",ParameterMode.Positional)]
        public void Parameters_that_an_instruction_writes_to_will_never_be_in_immediate_mode(string program, ParameterMode expected)
        {   
            ProgramConfiguration.GetParameterMode(program, out var ignored, out var unused, out var writeParameterMode);
            Assert.That(writeParameterMode,Is.EqualTo(ParameterMode.Positional));
        }
    }
}
