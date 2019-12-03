using System;
using System.Linq;

namespace Christmas.Day2
{
    public class RecursiveProgram : IIntCodeProgram
    {
        private readonly IIntCodeValidator _validator;
        private IIntCodeProgram _adder;
        private IIntCodeProgram _multiplier;

        public RecursiveProgram(IIntCodeValidator validator)
        {
            _validator = validator;
            _adder = new AdderProgram(_validator);
            _multiplier = new MultiplierProgram(_validator);
        }
        public string Process(string input, int startIndex = 0)
        {
            var output = input;
            while (true)
            {
                var intList = _validator.SplitString(output).ToArray();
                var opCode = intList[startIndex];
                if (opCode == 99) return output;
                var program = (opCode == 1) ? _adder : (opCode==2) ? _multiplier : throw new Exception($"Unexpected op code {opCode} at position {startIndex}");
                output = program.Process(output, startIndex);
                startIndex += 4;
            }
        }

        public Tuple<int,int> GetForOutput(string program, int outputValue)
        {
            var intList = _validator.SplitString(program).ToArray();
            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    intList[1] = i;
                    intList[2] = j;
                    var output = Process(_validator.Join(intList));
                    var result = _validator.SplitString(output).ToArray()[0];
                    if (result == outputValue)
                    {
                        return new Tuple<int, int>(i, j);
                    }
                }
            }
            throw new Exception("Program did not resolve for inputs up to 100");
        }
    }
}