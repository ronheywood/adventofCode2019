using System;
using System.Linq;
using Christmas.Day5;
using NUnit.Framework.Constraints;

namespace Christmas.Day2
{
    public class RecursiveProgram : IIntCodeProgram
    {
        private readonly IIntCodeValidator _validator;
        private readonly IIntcodeProgramFactory _intcodeProgramFactory;
        private IIntCodeProgram _adder;
        private IIntCodeProgram _multiplier;
        private int _systemIdentifier;
        private string _diagnosticCode;

        public int InstructionLength => 0;

        public RecursiveProgram(IIntCodeValidator validator, IIntcodeProgramFactory intcodeProgramFactory)
        {
            _validator = validator;
            _intcodeProgramFactory = intcodeProgramFactory;
            _adder = new AdderProgram(_validator);
            _multiplier = new MultiplierProgram(_validator);
        }
        public string Process(string program, int startIndex = 0)
        {
            var output = program;
            while (true)
            {
                var intList = _validator.SplitString(output).ToArray();
                var opCode = ProgramConfiguration.GetOpCode( intList[startIndex].ToString() );
                if (opCode == 99) return output;
                var logic = _intcodeProgramFactory.GetProgram(opCode);

                if (opCode == 4)
                {
                    _diagnosticCode = logic.Process(output, startIndex);
                }
                else if (opCode == 5 || opCode == 6)
                {
                    logic.Process(output, startIndex);
                }
                else
                {
                    output = (opCode == 3)
                        ? logic.Process(output, _systemIdentifier, startIndex)
                        : logic.Process(output, startIndex);
                }

                startIndex += logic.InstructionLength;
            }
        }
        public string Process(string program, int input, int startIndex)
        {
            throw new NotImplementedException();
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

        public string RunDiagnostics(int systemIdentifier, string diagnosticProgram)
        {
            _diagnosticCode = string.Empty;
            _systemIdentifier = systemIdentifier;
            var programResult = Process(diagnosticProgram);
            return _diagnosticCode;
        }
    }
}