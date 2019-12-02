using System.IO;
using Christmas.Day1;
using NUnit.Framework;

namespace Christmas.Integration.Tests.Day1
{
    [TestFixture]
    public class FuelAdderUpperIntegrationTest
    {
        [Test]
        public void Should_calculate_mass_from_input_file()
        {
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory + "\\TestData\\input.txt"))
            {
                Assert.That(FuelAdderUpper.GetFuel(sr), Is.EqualTo(3336439));
            }
        }
        [Test]
        public void Should_calculate_total_fuel_from_input_file()
        {
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory + "\\TestData\\input.txt"))
            {
                Assert.That(FuelAdderUpper.GetTotalFuelPlusExtra(sr), Is.EqualTo(5001791));
            }
        }
    }
}
