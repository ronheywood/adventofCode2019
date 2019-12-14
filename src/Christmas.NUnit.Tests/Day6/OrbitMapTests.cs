using System.IO;
using Christmas.Day6;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day6
{
    [TestFixture]
    class OrbitMapTests
    {
        [Test]
        public void Builds_a_map_of_planets_from_input()
        {
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory+"\\TestData\\day6.unit.txt"))
            {
                var map = new OrbitMap(sr);
                Assert.That(map.TotalOrbits(), Is.EqualTo(42));
            }
        }
        [Test]
        public void Calculates_transfer_from_one_planet_to_another()
        {
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory+"\\TestData\\day6.unit.txt"))
            {
                var map = new OrbitMap(sr);
                Assert.That(map.GetTransferCount("K", "I"), Is.EqualTo(4));
            }
        }
    }
}
