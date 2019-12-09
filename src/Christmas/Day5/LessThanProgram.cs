using System;
using System.Linq;
using Christmas.Day2;

namespace Christmas.Day5
{
    public class LessThanProgram : IIntCodeProgram
    {
        private readonly IIntCodeValidator _intCodeValidator;

        public LessThanProgram(IIntCodeValidator intCodeValidator)
        {
            _intCodeValidator = intCodeValidator;
        }

        public string Process(string program, int startIndex = 0)
        {
            if (!_intCodeValidator.Validate(program)) throw new Exception("Program input is invalid");
            _intCodeValidator.ValidateProgramConfiguration(program, startIndex, 7);
            var intList = _intCodeValidator.SplitString(program).ToArray();
            _intCodeValidator.ExtractOrdinals(intList,out var i1,out var i2, out var pointer, startIndex: startIndex);
            ProgramConfiguration.GetParameterMode(program, out var arg1Mode,out var arg2Mode, out var pointerMode, startIndex);
            var arg1 = (arg1Mode == ParameterMode.Immediate) ? intList[startIndex + 1] : intList[i1];
            var arg2 = (arg2Mode == ParameterMode.Immediate) ? intList[startIndex + 2] : intList[i2];

            intList[pointer] = (arg1 < arg2) ? 1 : 0;
            return _intCodeValidator.Join(intList);
        }

        public string Process(string program, int input, int startIndex)
        {
            throw new NotImplementedException();
        }

        public int InstructionLength => 4;
    }
}