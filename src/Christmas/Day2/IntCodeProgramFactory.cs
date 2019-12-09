using Christmas.Day5;

namespace Christmas.Day2
{
    public class IntCodeProgramFactory : IIntcodeProgramFactory
    {
        public IIntCodeProgram GetProgram(int opCode)
        {
            var intCodeValidation = new IntCodeValidator();
            switch (opCode)
            {
                case 1:
                    return new AdderProgram(intCodeValidation);
                case 2:
                    return new MultiplierProgram(intCodeValidation);
                case 3:
                    return new TransposeProgram(intCodeValidation);
                case 4:
                    return new OutputProgram(intCodeValidation);
                case 5:
                    return new JumpIfTrueProgram(intCodeValidation);
                case 6:
                    return new JumpIfFalseProgram(intCodeValidation);
            }
            throw new InvalidOpCodeException($"Opcode {opCode} not found.");
        }
    }
}