using Christmas.Day4;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day4
{
    [TestFixture]
    public class PasswordValidationTests
    {
        private PasswordStructureValidator _strucureValidator;
        private PasswordSequenceValidator _sequenceValidator;

        [SetUp]
        public void SetUp()
        {
            // Add code that runs before each test method
            _strucureValidator = new PasswordStructureValidator();
            _sequenceValidator = new PasswordSequenceValidator();
        }
        [TestCase("123456",true)]
        [TestCase("000000",false)]
        [TestCase("012345",false)]
        [TestCase("12345A",false)]
        [TestCase("12345678",false)]
        public void Should_be_a_six_digit_number(string test,bool expected)
        {
            Assert.That(_strucureValidator.ValidateFormat(test), Is.EqualTo(expected));
        }
        [TestCase("123432",false)]
        [TestCase("123456",true)]
        [TestCase("111116",true)]
        [TestCase("112336",true)]
        [TestCase("123432",false)]
        [TestCase("212345",false)]
        public void Should_validate_numbers_are_increasing(string test,bool expected)
        {
            Assert.That(_strucureValidator.ValidateFormat(test), Is.EqualTo(expected));
        }
        
        [TestCase("123456",false)]
        [TestCase("111112",true)]
        [TestCase("126788",true)]
        [TestCase("112336",true)]
        [TestCase("121212",false)]
        [TestCase("212121",false)]
        public void Should_validate_two_adjacent_numbers(string test,bool expected)
        {
            Assert.That(_sequenceValidator.ValidateAdjacent(test), Is.EqualTo(expected));
        }
        
        [TestCase("123456",false)]
        [TestCase("111116",true)]
        [TestCase("11233",false)]
        [TestCase("11233A",false)]
        [TestCase("002334",false)]
        [TestCase("111111",true)]
        [TestCase("223450",false)]
        [TestCase("123789",false)]
        public void Should_test_against_both_conditions(string test,bool expected)
        {
            Assert.That(new PasswordValidation(_strucureValidator,_sequenceValidator).Validate(test), Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class PasswordSequencePairedValidatorTest
    {
        
        [TestCase("123456",false)]
        [TestCase("111116",false)]
        [TestCase("112233",true)]
        [TestCase("123444",false)]
        [TestCase("111122",true)]
        public void Should_not_allow_more_than_two_sequential_numbers(string password, bool expected)
        {
            Assert.That(new PasswordSequencePairedValidation().ValidateAdjacent(password), Is.EqualTo(expected));
        }
    }
}