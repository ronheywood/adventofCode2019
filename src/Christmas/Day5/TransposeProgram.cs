using System;
using System.Linq;
using Christmas.Day2;

namespace Christmas.Day5
{
    public class TransposeProgram : IIntCodeProgram
    {
        private readonly IIntCodeValidator _intCodeValidator;

        public TransposeProgram(IIntCodeValidator intCodeValidator)
        {
            _intCodeValidator = intCodeValidator;
        }

        public string Process(string program, int startIndex = 0)
        {
            throw new System.NotImplementedException();
        }

        public string Process(string program, int input, int startIndex)
        {
            if (!_intCodeValidator.Validate(program)) throw new Exception("Invalid program");
            _intCodeValidator.ValidateProgramConfiguration(program, startIndex, 3);
            var intList = _intCodeValidator.SplitString(program).ToArray();

            var target = intList[startIndex+1];
            intList[target] = input;
            return _intCodeValidator.Join(intList);
        }
    }
}