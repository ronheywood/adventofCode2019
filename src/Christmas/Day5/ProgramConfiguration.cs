using System.Linq;
using Christmas.Day2;
using NUnit.Framework.Constraints;

namespace Christmas.Day5
{
    public static class ProgramConfiguration
    {
        public static int GetOpCode(string program)
        {
            var programConfiguration = GetProgramConfigInt(program);
            if (programConfiguration < 100) return programConfiguration;

            return int.TryParse(programConfiguration.ToString().Substring(programConfiguration.ToString().Length -2, 2), out var number) ? number : throw new InvalidOpCodeException("OP code was not numeric");
        }

        private static int GetProgramConfigInt(string program,int startIndex = 0)
        {
            return new IntCodeValidator().SplitString(program).Skip(startIndex).First();
        }

        public static void GetParameterMode(string program, out ParameterMode position1,
            out ParameterMode position2,
            out ParameterMode position3,
            int startIndex = 0)
        {
            var charArray = GetProgramConfigInt(program,startIndex).ToString().ToCharArray();
            position1 =  ParameterMode.Positional;
            position2 =  ParameterMode.Positional;
            position3 =  ParameterMode.Positional;

            if (charArray.Length > 2) position1 = charArray[charArray.Length - 3]=='1' ? ParameterMode.Immediate : ParameterMode.Positional;
            if (charArray.Length > 3) position2 = charArray[charArray.Length - 4] == '1' ? ParameterMode.Immediate : ParameterMode.Positional;
        }
    }
}