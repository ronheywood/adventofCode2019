using System;
using System.IO;

namespace Christmas
{
    public class AdventStringConverter : IStringConverter
    {
        public int ParseInt(string input)
        {
            return int.TryParse(input, out var isInt) ? isInt : 0;
        }
        public string GetCsv(StreamReader input)
        {
            var csv = string.Empty;
            while (input.Peek() >= 0)
            {
                var value = input.ReadLine();
                csv += $"{value},";
            }

            csv = csv.Substring(0, csv.LastIndexOf(",", StringComparison.Ordinal));
            return csv;
        }
    }
}