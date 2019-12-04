using Christmas.Day4;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day4
{
    [TestFixture]
    public class ValidPasswordFilterTests
    {
        private PasswordStructureValidator _structureValidator;
        private PasswordSequenceValidator _sequenceValidator1;
        private PasswordSequenceValidator _sequenceValidator2;

        [SetUp]
        public void SetUp()
        {
            _structureValidator = new PasswordStructureValidator();
            _sequenceValidator1 = new PasswordSequenceValidator();
            _sequenceValidator2 = new PasswordSequencePairedValidation();

        }
        [Test]
        public void Should_get_valid_passwords_between_a_range()
        {
            var matchingPasswords = new ValidPasswordFilter(_structureValidator,_sequenceValidator1).GetValidPasswords(254032,789860);
            Assert.That(matchingPasswords, Is.EqualTo(1033));
        }
        [Test]
        public void Should_get_valid_passwords_between_a_range_with_pairs()
        {
            var matchingPasswords = new ValidPasswordFilter(_structureValidator,_sequenceValidator2).GetValidPasswords(254032,789860);
            Assert.That(matchingPasswords, Is.EqualTo(670));
        }
    }
}