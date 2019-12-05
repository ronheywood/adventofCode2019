using System;
using System.Linq;

namespace Christmas.Day2
{
    public class AdderProgram : IIntCodeProgram
    {
        private readonly IIntCodeValidator _validator;


        public AdderProgram(IIntCodeValidator validator)
        {
            _validator = validator;
        }

        public string Process(string program, int startIndex = 0)
        {
            if(!_validator.Validate(program)) throw new Exception("Invalid input: must be comma separated list of numbers");
            
            var intList = _validator.SplitString(program).ToArray();
            if (intList[startIndex]!=1) throw new Exception("Invalid input: op code (first integer) must be 1");

            _validator.ExtractOrdinals(intList, out var i1, out var i2, out var pointer,startIndex:startIndex);

            intList[pointer] = intList[i1] + intList[i2];
            return _validator.Join(intList);
        }

        public string Process(string program, int input, int startIndex)
        {
            throw new NotImplementedException();
        }
    }
}
