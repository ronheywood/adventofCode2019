using System;
using System.Linq;
using Christmas.Day2;

namespace Christmas.Day5
{
    public class OutputProgram : IIntCodeProgram
    {
        private readonly IIntCodeValidator _intCodeValidator;

        public int InstructionLength => 2;

        public OutputProgram(IIntCodeValidator intCodeValidator)
        {
            _intCodeValidator = intCodeValidator;
        }

        public string Process(string program, int startIndex = 0)
        {
            if (!_intCodeValidator.Validate(program)) throw new Exception("Program input is invalid");
            _intCodeValidator.ValidateProgramConfiguration(program, startIndex, 4);
            var intList = _intCodeValidator.SplitString(program).ToArray();
            var position = intList[startIndex + 1];
            return intList[position].ToString();
        }

        public string Process(string program, int input, int startIndex)
        {
            throw new NotImplementedException();
        }
    }
}