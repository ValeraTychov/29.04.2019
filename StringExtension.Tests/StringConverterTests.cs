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

        [TestCase("A", 1, ExpectedResult = "A")]
        [TestCase("A", 2, ExpectedResult = "A")]
        [TestCase("AB", 1, ExpectedResult = "AB")]
        [TestCase("AB", 2, ExpectedResult = "AB")]
        [TestCase("AB", 3, ExpectedResult = "AB")]
        [TestCase("123456789", 1, ExpectedResult = "135792468")]
        [TestCase("123456789", 2, ExpectedResult = "159483726")]
        [TestCase("123456789", 3, ExpectedResult = "198765432")]
        [TestCase("123456789", 4, ExpectedResult = "186429753")]
        [TestCase("123456789", 5, ExpectedResult = "162738495")]
        [TestCase("123456789", 6, ExpectedResult = "123456789")]
        [TestCase("123456789", 7, ExpectedResult = "135792468")]
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
        [TestCase(@"!#%')+-/13579;=?ACEGIKMOQSU""$&(*,.02468:<>@BDFHJLNPRT", 5, ExpectedResult = @"!,7BM#.9DO%0;FQ'2=HS)4?JU+6AL""-8CN$/:EP&1<GR(3>IT*5@K")]
        [TestCase(@"!#%')+-/13579;=?ACEGIKMOQSU""$&(*,.02468:<>@BDFHJLNPRT", 20, ExpectedResult = @"!QLGB=83.)$TOJE@;61,'""RMHC>94/*%UPKFA<72-(#SNID?:50+&")]
        [TestCase(@"!#%')+-/13579;=?ACEGIKMOQSU""$&(*,.02468:<>@BDFHJLNPRT", 955031, ExpectedResult = @"!""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTU")]
        [TestCase(@"!snid_ZUPKFA<72-(#upkfa\WRMHC>94/*%wrmhc^YTOJE@;61,'""toje`[VQLGB=83.)$vqlgb]XSNID?:50+&x", 2147483637, ExpectedResult = @"!/=KYgu,:HVdr)7ESao&4BP^l#1?M[iw.<JXft+9GUcq(6DR`n%3AO]k""0>LZhv-;IWes*8FTbp'5CQ_m$2@N\jx")]
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
        public void Convert_WhiteSpacedStringWithInvisibleChars_ArgumentException()
            => Assert.Throws<ArgumentException>(
                () => _stringConverter.Convert("  \t\n  \t \r", int.MaxValue));

        [Test]
        public void Convert_NegativeIterationCounter_ArgumentOutOfRangeException()
        => Assert.Throws<ArgumentOutOfRangeException>(
            () => _stringConverter.Convert("Привет Епам!", -1));
    }
}
