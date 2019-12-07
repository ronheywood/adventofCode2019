using System.IO;
using Christmas.Day2;
using FakeItEasy;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day2
{
    [TestFixture]
    public class MultipleProgramTests
    {
        [Test]
        public void Program_should_accepT_argument_for_positions()
        {
            var validator = new IntCodeValidator();
            var input = "1,9,10,3,2,3,11,0,1,2,3,3,99,30,40,50";
            var adder = new AdderProgram(validator);
            var multiplier = new MultiplierProgram(validator);
            Assert.DoesNotThrow(() => multiplier.Process(input,4));
            Assert.That(multiplier.Process(input, 4), Is.EqualTo("9,9,10,3,2,3,11,0,1,2,3,3,99,30,40,50"));
            Assert.DoesNotThrow(() => adder.Process(input,8));
        }
        [Test]
        public void Should_check_int_code_at_start_position()
        {
            var validator = new IntCodeValidator();
            var adder = new AdderProgram(validator);
            var input = "27,0,0,0,1,0,0,0";
            Assert.DoesNotThrow(() => adder.Process(input, 4));
        }
        [TestCase("1,0,0,3,99", "1,0,0,2,99")]
        [TestCase("2,0,0,3,99", "2,0,0,4,99")]
        [TestCase("2,0,0,3,99,0,0,0,0", "2,0,0,4,99,0,0,0,0")]
        [TestCase("1,0,1,3,2,4,4,7,99", "1,0,1,1,2,4,4,4,99")]
        [TestCase("1,9,10,3,2,3,11,0,99,30,40,50", "3500,9,10,70,2,3,11,0,99,30,40,50")]
        [TestCase("1,0,0,0,99","2,0,0,0,99")]
        [TestCase("2,3,0,3,99","2,3,0,6,99")]
        [TestCase("2,4,4,5,99,0","2,4,4,5,99,9801")]
        [TestCase("1,1,1,4,99,5,6,0,99","30,1,1,4,2,5,6,0,99")]
        public void Program_should_iterate_until_opcode_99(string input, string expected)
        {
            var actual = new RecursiveProgram(new IntCodeValidator(),new IntCodeProgramFactory()).Process(input);
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void Program_should_get_program_from_factory()
        {
            var program = "1,0,0,3,99";
            var intcodeProgramFactory = A.Fake<IIntcodeProgramFactory>();
            A.CallTo(() => intcodeProgramFactory.GetProgram(1)).Returns(new AdderProgram(new IntCodeValidator()));
            new RecursiveProgram(new IntCodeValidator(),intcodeProgramFactory).Process(program);
            A.CallTo(() => intcodeProgramFactory.GetProgram(1)).MustHaveHappened();
        }
        [Test]
        public void Program_should_iterate_using_program_value_length_configuration([Random(1,255,3)] int diagnosticSystem)
        {
            var program = "3,0,4,0,99";
            var diagnostics = new RecursiveProgram(new IntCodeValidator(), new IntCodeProgramFactory());
            var output = diagnostics.RunDiagnostics(diagnosticSystem, program);
            Assert.That(output, Is.EqualTo(diagnosticSystem.ToString()));
        }
        [Ignore("Slow test")]
        [Test]
        public void Program_can_determine_input_pair_that_produces_desired_ouput()
        {
            var program = string.Empty;
            using (var sr = new StreamReader(TestContext.CurrentContext.WorkDirectory + "\\TestData\\day2.unit.txt"))
            {
                program = sr.ReadToEnd();
            }

            var expectedNoun = 12;
            var expectedVerb = 2;
            var outputValue = 3500;

            var tuple = new RecursiveProgram(new IntCodeValidator(), new IntCodeProgramFactory()).GetForOutput(program,outputValue);
            Assert.That(tuple.Item1, Is.EqualTo(expectedNoun));
            Assert.That(tuple.Item2, Is.EqualTo(expectedVerb));
        }
    }
}
