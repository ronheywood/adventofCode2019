using System;
using System.Linq;
using Christmas.Day2;

namespace Christmas.Day5
{
    public class OutputProgram : IIntCodeProgram
    {
        private readonly IIntCodeValidator _intCodeValidator;

        public OutputProgram(IIntCodeValidator intCodeValidator)
        {
            _intCodeValidator = intCodeValidator;
        }

        public string Process(string program, int startIndex = 0)
        {
            if (!_intCodeValidator.Validate(program)) throw new Exception("Program input is invalid");
            _intCodeValidator.ValidateProgramConfiguration(program, startIndex, 4);
            var intList = _intCodeValidator.SplitString(program).ToArray();
            return intList[startIndex+1].ToString();
        }

        public string Process(string program, int input, int startIndex)
        {
            throw new NotImplementedException();
        }
    }
}