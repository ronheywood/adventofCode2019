using Christmas.Day2;
using NUnit.Framework;

namespace Christmas.NUnit.Tests.Day2
{
    [TestFixture]
    public class IntCodeValidatorTests
    {
        [TestCase("1,2,3,4",true)]
        [TestCase("a,2,3,4",false)]
        [TestCase("1234",false)]
        public void Should_validate_IntList(string input, bool expected)
        {
            Assert.That(new IntCodeValidator().Validate(input), Is.EqualTo(expected));
        }
        [Test]
        public void Should_extract_values_from_string_input()
        {
            var input = "1,2,3,4";
            new IntCodeValidator().ExtractOrdinals(input,out var i1,out var i2,out var pointer);
            
            Assert.That(i1, Is.EqualTo(2));
            Assert.That(i2, Is.EqualTo(3));
            Assert.That(pointer, Is.EqualTo(4));
        }
        [Test]
        public void Should_extract_values_from_enumerable_int_input()
        {
            var input = new []{1,2,3,4};
            new IntCodeValidator().ExtractOrdinals(input,out var i1,out var i2,out var pointer);
            
            Assert.That(i1, Is.EqualTo(2));
            Assert.That(i2, Is.EqualTo(3));
            Assert.That(pointer, Is.EqualTo(4));
        }
        [Test]
        public void Should_split_string_to_int_array()
        {
            var input = "1,2,3,4";
            var expected = new[] {1, 2, 3, 4};
            CollectionAssert.AreEquivalent(new IntCodeValidator().SplitString(input),expected);
        }
        [Test]
        public void Should_join_int_array_to_csv()
        {
            var input = new[] {1, 2, 3, 4};
            var expected = "1,2,3,4";
            CollectionAssert.AreEquivalent(new IntCodeValidator().Join(input),expected);;
        }
        [Test]
        public void Should_extract_values_given_start_position_given_string()
        {
            var input = "1,2,3,4,2,0,1,2,99,10,20,30";
            
            new IntCodeValidator().ExtractOrdinals(input,out var i1,out var i2,out var pointer, startIndex: 4);
            
            Assert.That(i1, Is.EqualTo(0));
            Assert.That(i2, Is.EqualTo(1));
            Assert.That(pointer, Is.EqualTo(2));

        }
        [Test]
        public void Should_extract_values_given_start_position_given_iEnumerable()
        {
            var input = new []{1,2,3,4,2,0,1,2,99,10,20,30};
            new IntCodeValidator().ExtractOrdinals(input,out var i1,out var i2,out var pointer, startIndex:4);
            
            Assert.That(i1, Is.EqualTo(0));
            Assert.That(i2, Is.EqualTo(1));
            Assert.That(pointer, Is.EqualTo(2));
        }
        [TestCase("1,1,2,3,99",0,1)]
        public void Should_validate_op_code(string program, int startIndex,int expected)
        {
            Assert.That(new IntCodeValidator().ValidateProgramConfiguration(program,startIndex,expected), Is.True);
        }
    }
}
