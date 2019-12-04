using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day4
{
    [TestFixture]
    public class ValidPasswordFilterTests
    {
        [Test]
        public void Should_get_valid_passwords_between_a_range()
        {
            var matchingPasswords = ValidPassswordFilter.GetValidPasswords(254032,789860);
            Assert.That(matchingPasswords, Is.EqualTo(2));
        }
    }
}