using System.IO;

namespace Christmas
{
    public interface IStringConverter
    {
        int ParseInt(string input);
        string GetCsv(StreamReader input);
    }
}