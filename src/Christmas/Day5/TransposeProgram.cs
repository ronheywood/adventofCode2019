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
            var intList = _intCodeValidator.SplitString(program).ToArray();
            if (intList[0]!=3) throw new Exception("Invalid Op Code");

            _intCodeValidator.ExtractOrdinals(intList,out int target, out int i2,out int i3, startIndex);

            intList[target] = input;
            return _intCodeValidator.Join(intList);
        }
    }
}