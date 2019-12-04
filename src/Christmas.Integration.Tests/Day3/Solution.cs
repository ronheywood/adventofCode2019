using System.IO;
using Christmas.Day3;
using NUnit.Framework;

namespace Christmas.Integration.Tests.Day3
{
    [TestFixture]
    public class Solution
    {
        [Test]
        public void Distance_solution()
        {
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory + "\\TestData\\day3.txt"))
            {
                var wire = new Wire( sr.ReadLine());
                var path2 = sr.ReadLine();
                Assert.That(wire.ClosestIntersect(path2), Is.EqualTo(2129));
            }
        }
        [Test]
        public void Closest_solution()
        {
            using (var sr = new StreamReader(TestContext.CurrentContext.TestDirectory + "\\TestData\\day3.txt"))
            {
                var wire = new Wire( sr.ReadLine());
                var path2 = sr.ReadLine();
                Assert.That(wire.GetShortestIntersect(path2), Is.EqualTo(134662));
            }
        }
    }
}
