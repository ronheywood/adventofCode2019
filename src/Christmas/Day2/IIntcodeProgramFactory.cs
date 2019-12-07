namespace Christmas.Day2
{
    public interface IIntcodeProgramFactory
    {
        IIntCodeProgram GetProgram(int opCode);
    }
}