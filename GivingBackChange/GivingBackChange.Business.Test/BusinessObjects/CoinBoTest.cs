using GivingBackChange.Business.BusinessObjects;
using NUnit.Framework;

namespace GivingBackChange.Business.Test.BusinessObjects
{
    public class CoinBoTest
    {
        [Test]
        [TestCase(0, 0)]
        [TestCase(0.05, 5)]
        [TestCase(0.10, 10)]
        [TestCase(0.20, 20)]
        [TestCase(0.50, 50)]
        [TestCase(1, 100)]
        [TestCase(2, 200)]
        [TestCase(5, 500)]
        [TestCase(0.85, 85)]
        [TestCase(10, 1000)]
        [TestCase(15, 1500)]
        [TestCase(50, 5000)]
        [TestCase(100, 10000)]
        [TestCase(1000000, 100000000)]
        public void WhenCreatingCoinBoThenValueInCentsIsCorrect(decimal value, int valueInCent)
        {
            var coinBo = new CoinBo(0, "test", value, 1);

            Assert.That(coinBo.ValueInCent, Is.EqualTo(valueInCent));
        }
    }
}
