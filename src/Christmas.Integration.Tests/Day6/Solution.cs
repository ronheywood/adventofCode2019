using System.IO;
using Christmas.Day6;
using NUnit.Framework;

namespace Christmas.Integration.Tests.Day6
{
    [TestFixture]
    public class Solution
    {
        [Test]
        public void Part_one_solution()
        {
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory+"\\TestData\\Day6.txt"))
            {
                var map = new OrbitMap(sr);
                Assert.That(map.TotalOrbits(), Is.EqualTo(453028));
            }
        }
        [Test]
        public void Part_two_solution()
        {
            
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory+"\\TestData\\Day6.txt"))
            {
                var map = new OrbitMap(sr);
                Assert.That(map.GetTransferCount("YOU","SAN") -2, Is.EqualTo(562));
            }
        }
        
    }
}
