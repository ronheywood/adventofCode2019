using Christmas.Day2;
using NUnit.Framework;
namespace Christmas.NUnit.Tests.Day5
{
    public class ImmediateModeTests
    {
        class AdderProgramMode
        {
            [Test]
            public void Should_accept_immediate_mode_configuration_for_arg1()
            {
                var adderProgram = new AdderProgram(new IntCodeValidator());
                var program = "101,5,1,5,99,0";
                Assert.That(adderProgram.Process(program), Is.EqualTo("101,5,1,5,99,10"));
            }
            [Test]
            public void Should_accept_immediate_mode_configuration_for_arg2()
            {
                var adderProgram = new AdderProgram(new IntCodeValidator());
                var program = "1101,5,1,5,99,0";
                Assert.That(adderProgram.Process(program), Is.EqualTo("1101,5,1,5,99,6"));
            }
            [Test]
            public void Should_accept_start_position_when_running_progrma_mode()
            {
                var adderProgram = new AdderProgram(new IntCodeValidator());
                var program = "101,5,1,5,99,0,1101,5,1,11,99,0";
                Assert.That(adderProgram.Process(program,6), Is.EqualTo("101,5,1,5,99,0,1101,5,1,11,99,6"));
            }
        }

        public class MultiplierProgramMode
        {
            [Test]
            public void Should_accept_immediate_mode_configuration_for_arg1()
            {
                var testProgram = new MultiplierProgram(new IntCodeValidator());
                var program = "102,5,1,5,99,0";
                Assert.That(testProgram.Process(program), Is.EqualTo("102,5,1,5,99,25"));
            }
            [Test]
            public void Should_accept_immediate_mode_configuration_for_arg2()
            {
                var testProgram = new MultiplierProgram(new IntCodeValidator());
                var program = "1102,5,1,5,99,0";
                Assert.That(testProgram.Process(program), Is.EqualTo("1102,5,1,5,99,5"));
            }
            [Test]
            public void Should_accept_start_position_when_running_progrma_mode()
            {
                var testProgram = new MultiplierProgram(new IntCodeValidator());
                var program = "102,5,1,5,99,0,1102,5,1,11,99,0";
                Assert.That(testProgram.Process(program,6), Is.EqualTo("102,5,1,5,99,0,1102,5,1,11,99,5"));
            }
        }
    }
}
