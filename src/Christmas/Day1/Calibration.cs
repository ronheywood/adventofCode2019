using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Christmas.Day1
{
    public class Calibration
    {
        private readonly IStringConverter _stringConverter;
        public readonly int Frequency;

        public Calibration(IStringConverter stringConverter,int frequency)
        {
            _stringConverter = stringConverter;
            Frequency = frequency;
        }

        public int Calibrate(string input) => Frequency + input.Split(',').Sum(_stringConverter.ParseInt);

        public int Calibrate(StreamReader sr) => Calibrate(_stringConverter.GetCsv(sr));

        public int FindMatch(string input)
        {
            var current = Frequency;
            var seen = new List<int> {Frequency};
            while (true)
            {
                foreach (var frequency in input.Split(','))
                {
                    current += _stringConverter.ParseInt(frequency);
                    if (seen.Contains(current)) return current;
                    seen.Add(current);
                }
            }
        }

        public int FindMatch(StreamReader input) => FindMatch(_stringConverter.GetCsv(input));
    }
}