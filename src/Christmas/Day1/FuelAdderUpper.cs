using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Christmas.Day1
{

    public static class FuelAdderUpper
    {
        public static int GetFuel(int mass)
        {
            var d = (double) mass / 3;
            return (int) Math.Floor(d) -2;
        }

        public static int GetFuel(IEnumerable<int> moduleMassList)
        {
            return moduleMassList.Sum(GetFuel);
        }

        public static int GetFuel(StreamReader sr)
        {
            var massList = new List<int>();
            while (sr.Peek() >= 0)
            {
                var readLine = sr.ReadLine();
                var mass = MassParsedFromLine(readLine);
                massList.Add(mass);
            }

            return GetFuel(massList);
        }

        private static int MassParsedFromLine(string readLine)
        {
            return int.TryParse(readLine, out var parsed) ? parsed : throw new Exception("Text file contains non numeric values");
        }

        public static int GetExtraFuelMass(int fuel)
        {
            var fuels = new List<int>();

            while (true)
            {
                fuel = GetFuel(fuel);
                if (fuel<= 0) return fuels.Sum();
                
                fuels.Add(fuel);
            }
        }

        public static int GetTotalFuel(int mass)
        {
            var massFuel = GetFuel(mass);
            var extraFuel = GetExtraFuelMass(massFuel);
            return massFuel + extraFuel;
        }

        public static int GetTotalFuelPlusExtra(StreamReader sr)
        {
            var total = 0;
            while (sr.Peek() >= 0)
            {
                total += GetTotalFuel(MassParsedFromLine(sr.ReadLine()));
            }

            return total;
        }
    }
}