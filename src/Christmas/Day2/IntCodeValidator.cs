using System.Collections.Generic;
using System.Linq;
using Christmas.Day5;
using NUnit.Framework.Constraints;

namespace Christmas.Day2
{
    public interface IIntCodeValidator
    {
        bool Validate(string input);
        void ExtractOrdinals(IEnumerable<int> intList, out int o, out int o1, out int o2, int startIndex = 0);
        IEnumerable<int> SplitString(string input);
        string Join(IEnumerable<int> input);
        bool ValidateProgramConfiguration(string program, int startIndex, int expected);
    }

    public class IntCodeValidator : IIntCodeValidator
    {
        public virtual bool Validate(string input) => input.Contains(",") && input.Split(',').All(i => int.TryParse(i, out _));

        public void ExtractOrdinals(string input, out int o, out int o1, out int o2, int startIndex = 0) => ExtractOrdinals(SplitString(input).ToArray(), out o, out o1, out o2, startIndex);

        public void ExtractOrdinals(IEnumerable<int> intList, out int o, out int o1, out int o2, int startIndex = 0)
        {
            var arr = intList.ToArray();
            o = arr[startIndex+ 1];
            o1 = (arr.Length > startIndex+2) ? arr[startIndex+ 2] : 0;
            o2 = (arr.Length > startIndex+3) ? arr[startIndex+ 3] : 0;
        }

        public IEnumerable<int> SplitString(string input) => input.Split(',').Select(s => int.TryParse(s, out var number) ? number : 0);

        public string Join(IEnumerable<int> input) => string.Join(",",input);

        public bool ValidateProgramConfiguration(string program, int startIndex, int expected)
        {
            var configuration = Join(SplitString(program).Skip(startIndex).Take(4).ToArray() );
            if (ProgramConfiguration.GetOpCode(configuration) != expected) throw new InvalidOpCodeException("Invalid input: op code (first integer)");
            return true;
        }
    }
}
