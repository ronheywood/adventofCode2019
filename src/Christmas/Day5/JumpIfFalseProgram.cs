using System;
using System.Linq;
using Christmas.Day2;

namespace Christmas.Day5
{
    public class JumpIfFalseProgram : IIntCodeProgram
    {
        private readonly IIntCodeValidator _intCodeValidator;
        private int _jump;

        public JumpIfFalseProgram(IIntCodeValidator intCodeValidator)
        {
            _intCodeValidator = intCodeValidator;
        }

        public string Process(string program, int startIndex = 0)
        {
            _jump = 3;

            if (!_intCodeValidator.Validate(program)) throw new Exception("Program input is invalid");
            _intCodeValidator.ValidateProgramConfiguration(program, startIndex, 6);
            var intList = _intCodeValidator.SplitString(program).ToArray();
            _intCodeValidator.ExtractOrdinals(intList,out var i1,out var pointer, out var ignored, startIndex: startIndex);
            ProgramConfiguration.GetParameterMode(program, out var arg1Mode,out var arg2Mode, out var pointerMode, startIndex);
            var arg1 = (arg1Mode == ParameterMode.Immediate) ? intList[startIndex + 1] : intList[i1];
            var arg2 = (arg2Mode == ParameterMode.Immediate) ? intList[startIndex + 2] : intList[pointer];
            if (arg1 == 0)
            {
                _jump = arg2 - startIndex;
            }

            return string.Empty;
        }

        public string Process(string program, int input, int startIndex)
        {
            throw new System.NotImplementedException();
        }

        public int InstructionLength => _jump;
    }
}