using System;
using System.IO;
using System.Linq;
using System.Text;
using Christmas.Day1;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day1
{
    [TestFixture]
    public class FuelAdderUpperTest
    {
        [TestCase(12,2)]
        [TestCase(14,2)]
        [TestCase(1969,654)]
        [TestCase(100756,33583)]
        public void Should_Take_the_mass_and_calculate_fuel(int mass, int expected)
        {
            Assert.That(FuelAdderUpper.GetFuel(mass), Is.EqualTo(expected));
        }
        [Test]
        public void Should_sum_total_fuel_for_many()
        {
            var modulelist = new []{12,14,1969,100756};
            var expected = new[]{2,2,654,33583}.Sum();
            Assert.That(FuelAdderUpper.GetFuel(modulelist), Is.EqualTo(expected));
        }
        [Test]
        public void Should_sum_values_from_stream_reader()
        {
            var list = @"12
14
1969
100756";
            var m = new MemoryStream(Encoding.ASCII.GetBytes(list));
            
            var expected = new[]{2,2,654,33583}.Sum();
            using (var sr = new StreamReader(m))
            {
                Assert.That(FuelAdderUpper.GetFuel(sr), Is.EqualTo(expected));
            }
        }
        [Test]
        public void should_throw_when_Given_a_non_numeric_value_in_the_file()
        {var list = @"12
14
1969
100756
abcd";
            var m = new MemoryStream(Encoding.ASCII.GetBytes(list));
            using (var sr = new StreamReader(m))
            {
                var ex = Assert.Throws<Exception>(() => FuelAdderUpper.GetFuel(sr));
                Assert.That(ex.Message,Does.Contain("Text file contains non numeric values"));
            }
        }
        [TestCase(2,0)]
        [TestCase(654,312)]
        [TestCase(33583,16763)]
        public void Should_get_fuel_recursively(int fuel,int expected)
        {
            Assert.That(FuelAdderUpper.GetExtraFuelMass(fuel), Is.EqualTo(expected));
        }
        [TestCase(14,2)]
        [TestCase(1969,966)]
        [TestCase(100756,50346)]
        public void GetTotalFuel_should_add_fuel_for_module_and_fuel_for_fuel(int mass, int expected)
        {
            Assert.That(FuelAdderUpper.GetTotalFuel(mass),Is.EqualTo(expected));
        }
        [Test]
        public void GetFuelTotal_reads_from_a_file()
        { var list = @"12
14
1969
100756";
            var expected = new[] {2,2, 966, 50346}.Sum();
            
            var m = new MemoryStream(Encoding.ASCII.GetBytes(list));
            using (var sr = new StreamReader(m))
            {
                Assert.That(FuelAdderUpper.GetTotalFuelPlusExtra(sr), Is.EqualTo(expected));
            }
        }
    }
}
