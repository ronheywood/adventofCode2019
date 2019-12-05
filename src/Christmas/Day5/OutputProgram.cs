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
            var intList = _intCodeValidator.SplitString(program).ToArray();
            if (intList[startIndex] != 4) throw new Exception("Invalid Op Code");
            return intList[startIndex+1].ToString();
        }

        public string Process(string program, int input, int startIndex)
        {
            throw new NotImplementedException();
        }
    }
}