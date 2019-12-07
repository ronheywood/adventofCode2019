namespace Christmas.Day2
{
    public interface IIntCodeProgram
    {
        string Process(string program, int startIndex = 0);
        string Process(string program, int input, int startIndex);
        int InstructionLength { get; }
    }
}