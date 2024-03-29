﻿using System;
using Christmas.Day2;
using Christmas.Day5;
using Christmas.NUnit.Tests.Day5;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day2
{
    [TestFixture]
    public class AdderProgramTests
    {
        [Test]
        public void Should_validate_input()
        {
            var validatorSpy = new ValidatorSpy();
            var a = new AdderProgram(validatorSpy);
            var ex = Assert.Throws<Exception>(() => a.Process("a"));
            Assert.That(validatorSpy.WasCalled,Is.True);
            Assert.That(ex.Message,Does.Contain("Invalid input: must be comma separated list of numbers"));
        }
        [TestCase("5,1,2,3")]
        [TestCase("11,1,2,3")]
        [TestCase("112,1,2,3")]
        public void Should_expect_opcode_at_position_0_to_be_1(string input)
        {
            var a = new AdderProgram(new IntCodeValidator());
            var ex = Assert.Throws<InvalidOpCodeException>(() => a.Process(input));
            Assert.That(ex.Message,Does.Contain("Invalid input: op code (first integer)"));
            Assert.DoesNotThrow(() => a.Process("1,1,2,3"));
        }
        [TestCase("11101,1,2,3,99")]
        [TestCase("11101,1,2,3,99")]
        [TestCase("01,1,2,3,99")]
        [TestCase("101,1,2,3,99")]
        public void Should_validate_in_program_configuration(string input)
        {
            var a = new AdderProgram(new IntCodeValidator());
            Assert.DoesNotThrow(() => a.Process(input));
        }
        [TestCase("1,0,0,0","2,0,0,0")]
        [TestCase("1,5,5,3,99,10","1,5,5,20,99,10")]
        [TestCase("1,5,5,0,99,10","20,5,5,0,99,10")]

        public void It_adds_values_from_specified_positions_and_updates_another_position(string input, string expected)
        {
            var a = new AdderProgram(new IntCodeValidator());
            var answer = a.Process(input);
            Assert.That(answer, Is.EqualTo(expected), "Some useful error message");
        }
        [Test]
        public void It_accepts_a_start_index_and_runs_program_from_there()
        {
            var input = "1,5,5,0,1,1,2,9,99,0";
            var expected = "1,5,5,0,1,1,2,9,99,10";
            Assert.That(new AdderProgram(new IntCodeValidator()).Process(input,4), Is.EqualTo(expected));
        }
        [TestCase("101,5,5,9,1,1,2,9,99,0","101,5,5,9,1,1,2,9,99,6")]
        [TestCase("1101,5,5,9,1,1,2,9,99,0","1101,5,5,9,1,1,2,9,99,10")]
        public void Should_support_immediate_mode(string input,string expected)
        {;
            Assert.That(new AdderProgram(new IntCodeValidator()).Process(input, 0), Is.EqualTo(expected));
        }
    }

    public class ValidatorSpy : IntCodeValidator
    {
        public bool WasCalled;

        public override bool Validate(string input)
        {
            WasCalled = true;
            return false;
        }
    }
}
