using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StringExtension.Tests
{
    [TestFixture]
    public class StringConverterNuTests
    {
        private readonly StringConverter _stringConverter = new StringConverter();

        [TestCase("Привет Епам!", 1, ExpectedResult = "Пие пмрвтЕа!")]
        [TestCase("Привет Епам!", 2, ExpectedResult = "Пепртаи мвЕ!")]
        [TestCase("1234567890", int.MaxValue, ExpectedResult = "1357924680")]
        [TestCase("Lorem ipsum dolor sit amet consectetur adipisicing elit." +
                  " Excepturi laudantium, vel natus fugiat, illum dignissimos" +
                  " fuga officia maiores ea at ex quis animi incidunt doloremque, " +
                  "dolor quia. Quisquam, veniam hic!", int.MaxValue,
            ExpectedResult = "Ldeodeeamniat e oiaeumsac mtuf a  nua Eiadsn mocav " +
                             "ivsdiau pm rdlirqeinitaei iutigqir  e pis li  ac nus " +
                             "Qteuto egomrp fnittsmqeri uo., ,icuaohr,gn,oass iclalm " +
                             "uieoilfuncnoismudxqlttse itid umuimclii.sxl ougfar!")]
        public string Convert_TestInputs_ValidOutputs(string str, int n)
         =>_stringConverter.Convert(str, n);

        [Test]
        public void Convert_NullReferenceInsteadOfStringValue_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(
                () => _stringConverter.Convert(null, int.MaxValue));

        [Test]
        public void Convert_EmptyString_ArgumentException()
            => Assert.Throws<ArgumentException>(
                () => _stringConverter.Convert(string.Empty, int.MaxValue));

        [Test]
        public void Convert_WhiteSpacedString_ArgumentException()
            => Assert.Throws<ArgumentException>(
                () => _stringConverter.Convert("   ", int.MaxValue));

        [Test]
        public void Convert_NegativeIterationCounter_ArgumentOutOfRangeException()
        => Assert.Throws<ArgumentOutOfRangeException>(
            () => _stringConverter.Convert("Привет Епам!", -1));
    }
}
