using System;
using NUnit.Framework;

namespace ExceptionHandlingTask2.Tests
{
    public class StringConverterTests
    {
        private StringConverter converter;

        [SetUp]
        public void Setup()
        {
            converter = new StringConverter();
        }

        [TestCase("120", 120)]
        [TestCase("0", 0)]
        [TestCase("-100", -100)]
        [TestCase("-0", 0)]
        public void ConvertToInteger_OK(string input, int expected)
        {
            var actual = converter.ConvertToInteger(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertToInteger_InputIsNull_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => converter.ConvertToInteger(null));
        }

        [Test]
        public void ConvertToInteger_InputIsWhiteSpace_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => converter.ConvertToInteger(" "));
        }

        [Test]
        public void ConvertToInteger_InputIsEmpty_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => converter.ConvertToInteger(""));
        }

        [TestCase("120,9")]
        [TestCase("120.9")]
        [TestCase("-12!")]
        [TestCase("abc")]
        public void ConvertToInteger_InputContainsInvalidCharacter_ThrowArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => converter.ConvertToInteger(input));
        }
    }
}
