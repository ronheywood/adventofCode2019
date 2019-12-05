using System;
using System.Linq;
using Christmas.Day5;

namespace Christmas.Day2
{
    public class MultiplierProgram : IIntCodeProgram
    {
        private readonly IIntCodeValidator _validator;

        public MultiplierProgram(IIntCodeValidator validator)
        {
            _validator = validator;
        }

        public string Process(string program, int startIndex = 0)
        {
            if (!_validator.Validate(program))  throw new Exception("");
            _validator.ValidateProgramConfiguration(program, startIndex, 2);
            var intList = _validator.SplitString(program).ToArray();
            
            _validator.ExtractOrdinals(intList, out var i1, out var i2, out var pointer,startIndex:startIndex);

            intList[pointer] = intList[i1] * intList[i2];
            return _validator.Join(intList);
        }

        public string Process(string program, int input, int startIndex)
        {
            throw new NotImplementedException();
        }
    }
}
