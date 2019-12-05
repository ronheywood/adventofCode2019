using System.Linq;
using Christmas.Day2;
using NUnit.Framework.Constraints;

namespace Christmas.Day5
{
    public static class ProgramConfiguration
    {
        public static int GetOpCode(string program)
        {
            var config = GetProgramConfigInt(program);
            if (config < 100) return config;

            return int.TryParse(config.ToString().Substring(config.ToString().Length -2, 2), out var number) ? number : throw new InvalidOpCodeException("OP code was not numeric");
        }

        private static int GetProgramConfigInt(string program)
        {
            return new IntCodeValidator().SplitString(program).First();
        }

        public static void GetParameterMode(string program, out ParameterMode position1,
            out ParameterMode position2,
            out ParameterMode position3)
        {
            var charArray = GetProgramConfigInt(program).ToString().ToCharArray();
            position1 =  ParameterMode.Positional;
            position2 =  ParameterMode.Positional;
            position3 =  ParameterMode.Positional;

            if (charArray.Length > 2) position1 = charArray[charArray.Length - 3]=='1' ? ParameterMode.Immediate : ParameterMode.Positional;
            if (charArray.Length > 3) position2 = charArray[charArray.Length - 4] == '1' ? ParameterMode.Immediate : ParameterMode.Positional;
        }
    }
}