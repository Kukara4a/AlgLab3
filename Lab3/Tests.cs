using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Numerics;

namespace Lab3
{
    [TestFixture]
    class Test_RationalNumbers
    {
        [Test]
        public void CreatureAndToString()
        {
            Assert.AreEqual("1/3", new RationalNumbers(1, 3).ToString());
            Assert.AreEqual("-1/3", new RationalNumbers(-1, 3).ToString());
            Assert.AreEqual("-1/3", new RationalNumbers(1, -3).ToString());
            Assert.AreEqual("1/3", new RationalNumbers(-1, -3).ToString());
            Assert.AreEqual("0", new RationalNumbers(0, 3).ToString());
            Assert.AreEqual("1", new RationalNumbers(3, 3).ToString());
            Assert.AreEqual("0", new RationalNumbers(0).ToString());
            Assert.AreEqual("11", new RationalNumbers(11).ToString());
            Assert.AreEqual("11/3", new RationalNumbers(11, 3).ToString());

            try
            {
                new RationalNumbers(1, 0);
            }
            catch (DivideByZeroException)
            {
                Assert.Pass();
            };

            try
            {
                new RationalNumbers(0, 0);
            }
            catch (DivideByZeroException)
            {
                Assert.Pass();
            };
        }

        [Test]
        public void GreatestCommonDivisor()
        {
            Assert.AreEqual(new BigInteger(1), RationalNumbers.GetGreatestCommonDivisor(1, 1));
            Assert.AreEqual(new BigInteger(5), RationalNumbers.GetGreatestCommonDivisor(25, 5));
            Assert.AreEqual(new BigInteger(1), RationalNumbers.GetGreatestCommonDivisor(13, 7));
            Assert.AreEqual(new BigInteger(4), RationalNumbers.GetGreatestCommonDivisor(12, 4));
            Assert.AreEqual(new BigInteger(32), RationalNumbers.GetGreatestCommonDivisor(64, 32));
            Assert.AreEqual(new BigInteger(6), RationalNumbers.GetGreatestCommonDivisor(42, 12));
            Assert.AreEqual(new BigInteger(50), RationalNumbers.GetGreatestCommonDivisor(1000, 50));
            Assert.AreEqual(new BigInteger(50), RationalNumbers.GetGreatestCommonDivisor(50, 1000));
        }
    }

    [TestFixture]
    class Test_AdditiveOperations
    {    
        [Test]
        public void SumOperatorWorksCorrectly()
        {
            Assert.AreEqual("0", (new RationalNumbers(0) + new RationalNumbers(0)).ToString());
            Assert.AreEqual("-11", (new RationalNumbers(-2) + new RationalNumbers(-9)).ToString());
            Assert.AreEqual("2", (new RationalNumbers(1) + new RationalNumbers(1)).ToString());
            Assert.AreEqual("0", (new RationalNumbers(-1) + new RationalNumbers(1)).ToString());
            Assert.AreEqual("0", (new RationalNumbers(1) + new RationalNumbers(-1)).ToString());
            Assert.AreEqual("10", (new RationalNumbers(1) + new RationalNumbers(9)).ToString());
            Assert.AreEqual("9", (new RationalNumbers(11) + new RationalNumbers(-2)).ToString());
            Assert.AreEqual("9", (new RationalNumbers(-2) + new RationalNumbers(11)).ToString());
            Assert.AreEqual("-2", (new RationalNumbers(-1) + new RationalNumbers(-1)).ToString());
            Assert.AreEqual("-5", (new RationalNumbers(-10) + new RationalNumbers(5)).ToString());
            Assert.AreEqual("-5", (new RationalNumbers(5) + new RationalNumbers(-10)).ToString());
            Assert.AreEqual("1000", (new RationalNumbers(999) + new RationalNumbers(1)).ToString());
            Assert.AreEqual("38", (new RationalNumbers(19) + new RationalNumbers(19)).ToString());
            Assert.AreEqual("300", (new RationalNumbers(100) + new RationalNumbers(200)).ToString());
            Assert.AreEqual("4986658478322", (new RationalNumbers(4985693874527) + new RationalNumbers(964603795)).ToString());
        }

        [Test]
        public void DifferenceOperatorWorksCorrectly()
        {
            Assert.AreEqual("0", (new RationalNumbers(1) - new RationalNumbers(1)).ToString());
            Assert.AreEqual("-2", (new RationalNumbers(-1) - new RationalNumbers(1)).ToString());
            Assert.AreEqual("0", (new RationalNumbers(-1) - new RationalNumbers(-1)).ToString());
            Assert.AreEqual("9", (new RationalNumbers(10) - new RationalNumbers(1)).ToString());
            Assert.AreEqual("-9", (new RationalNumbers(1) - new RationalNumbers(10)).ToString());
            Assert.AreEqual("-11", (new RationalNumbers(-2) - new RationalNumbers(9)).ToString());
            Assert.AreEqual("-119", (new RationalNumbers(-20) - new RationalNumbers(99)).ToString());
            Assert.AreEqual("7", (new RationalNumbers(9) - new RationalNumbers(2)).ToString());
            Assert.AreEqual("999", (new RationalNumbers(1000) - new RationalNumbers(1)).ToString());
            Assert.AreEqual("1000", (new RationalNumbers(999) - new RationalNumbers(-1)).ToString());
        }
    }

    [TestFixture]
    class Test_MultiplicativeOperations
    {
        [Test]
        public void MultiplicationOperatorWorksCorrectly()
        {
            Assert.AreEqual("0", (new RationalNumbers(0) * new RationalNumbers(0)).ToString());
            Assert.AreEqual("0", (new RationalNumbers(1) * new RationalNumbers(0)).ToString());
            Assert.AreEqual("0", (new RationalNumbers(0) * new RationalNumbers(1)).ToString());
            Assert.AreEqual("1", (new RationalNumbers(1) * new RationalNumbers(1)).ToString());
            Assert.AreEqual("100", (new RationalNumbers(10) * new RationalNumbers(10)).ToString());
            Assert.AreEqual("-1", (new RationalNumbers(1) * new RationalNumbers(-1)).ToString());
            Assert.AreEqual("-1", (new RationalNumbers(-1) * new RationalNumbers(1)).ToString());
            Assert.AreEqual("123", (new RationalNumbers(123) * new RationalNumbers(1)).ToString());
            Assert.AreEqual("246420", (new RationalNumbers(555) * new RationalNumbers(444)).ToString());
            Assert.AreEqual("-133649026", (new RationalNumbers(-386) * new RationalNumbers(346241)).ToString());
            Assert.AreEqual("25", (new RationalNumbers(-5) * new RationalNumbers(-5)).ToString());
        }

        [Test]
        public void DivisionOperatorWorksCorrectly()
        {
            try
            {
                var division = new RationalNumbers(1) / new RationalNumbers(0);
                Assert.Fail();
            }
            catch { Assert.Pass(); }

            Assert.AreEqual("10", (new RationalNumbers(0) / new RationalNumbers(10)).ToString());
            Assert.AreEqual("10", (new RationalNumbers(100) / new RationalNumbers(10)).ToString());
            Assert.AreEqual("10", (new RationalNumbers(100) / new RationalNumbers(10)).ToString());
            Assert.AreEqual("-10", (new RationalNumbers(-100) / new RationalNumbers(10)).ToString());
            Assert.AreEqual("-10", (new RationalNumbers(100) / new RationalNumbers(-10)).ToString());
            Assert.AreEqual("99", (new RationalNumbers(99) / new RationalNumbers(1)).ToString());
            Assert.AreEqual("1", (new RationalNumbers(99) / new RationalNumbers(99)).ToString());
            Assert.AreEqual("11", (new RationalNumbers(99) / new RationalNumbers(9)).ToString());
            Assert.AreEqual("0", (new RationalNumbers(5) / new RationalNumbers(9)).ToString());
            Assert.AreEqual("3", (new RationalNumbers(15) / new RationalNumbers(4)).ToString());
            Assert.AreEqual("40", (new RationalNumbers(200) / new RationalNumbers(5)).ToString());
            Assert.AreEqual("1473861", (new RationalNumbers(346357457) / new RationalNumbers(235)).ToString());
        }
    }

    [TestFixture]
    class Test_TransformationNumber 
    {
        [Test]
        public void FromRegularToPeriodicFraction()
        {
            Assert.AreEqual("107.5", new RationalNumbers(3870, 36).GetPeriod());
            Assert.AreEqual("1.(532818)", new RationalNumbers(397, 259).GetPeriod());
            Assert.AreEqual("5097.1(6)", new RationalNumbers(183498, 36).GetPeriod());
            Assert.AreEqual("0.6713(709677419354838)", new RationalNumbers(333, 496).GetPeriod());
            Assert.AreEqual("0.(3)", new RationalNumbers(1, 3).GetPeriod());
            Assert.AreEqual("8.(538461)", new RationalNumbers(111, 13).GetPeriod());
            Assert.AreEqual("41.8(3)", new RationalNumbers(251, 6).GetPeriod());
            Assert.AreEqual("1.(54)", new RationalNumbers(17, 11).GetPeriod());
        }

        [Test]
        public void FromPeriodicToRegularFraction()
        {
            Assert.AreEqual("397/259", RationalNumbers.GetRational("1.(532818)"));
            Assert.AreEqual("30583/6", RationalNumbers.GetRational("5097.1(6)"));
            Assert.AreEqual("1/3", RationalNumbers.GetRational("0.(3)"));
            Assert.AreEqual("111/13", RationalNumbers.GetRational("8.(538461)"));
            Assert.AreEqual("251/6", RationalNumbers.GetRational("41.8(3)"));
            Assert.AreEqual("17/11", RationalNumbers.GetRational("1.(54)"));
        }
    }
}