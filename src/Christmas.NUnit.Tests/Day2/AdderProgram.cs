using System;
using System.Linq;
using Christmas.Day2;

namespace Christmas.NUnit.Tests.Day2
{
    class AdderProgram
    {
        private readonly IntCodeValidator _validatorSpy;

        public AdderProgram(IntCodeValidator validatorSpy)
        {
            _validatorSpy = validatorSpy;
        }

        public string Process(string input)
        {
            if(!_validatorSpy.Validate(input)) throw new Exception("Invalid input: must be comma separated list of numbers");
            if (!input.StartsWith("1,")) throw new Exception("Invalid input: op code (first integer) must be 1");
            var intList = input.Split(',').Select(s => int.TryParse(s, out var number) ? number : 0).ToArray();
            var i1 = intList[1];
            var i2 = intList[2];
            var pointer = intList[3];
            intList[pointer] = intList[i1] + intList[i2];
            return string.Join(",",intList);
        }
    }
}
