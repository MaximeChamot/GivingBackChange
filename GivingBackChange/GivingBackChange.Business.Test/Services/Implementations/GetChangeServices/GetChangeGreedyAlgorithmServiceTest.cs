using GivingBackChange.Business.BusinessObjects;
using GivingBackChange.Business.Services;
using GivingBackChange.Business.Services.Implementations.GetChangeServices;
using GivingBackChange.Referential;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GivingBackChange.Business.Test.Services.Implementations.GetChangeServices
{
    public class GetChangeGreedyAlgorithmServiceTest
    {
        private Mock<ICoinBoxService> _coinBoxService;

        private GetChangeGreedyAlgorithmService _getChangeService;

        [SetUp]
        public void Setup()
        {
            this._coinBoxService = new Mock<ICoinBoxService>();
            this._getChangeService = new GetChangeGreedyAlgorithmService(this._coinBoxService.Object);
        }

        [Test]
        [TestCase(0, 1)]       // total 1 : 1 coin (1.-)
        [TestCase(1, 0)]        // total 0 : 0 coin
        [TestCase(2, 1.45)]     // total 3 : 1 coin (1.-), 2 coins (0,20.-), 1 coin (0,05.-)
        [TestCase(3, 0.05)]     // total 1 : 1 coin (0,05.-)
        [TestCase(4, 1.85)]     // total 5 : 1 coin (1.-), 1 coin (0,50.-), 1 coins (0,20.-), 1 coin (0,10.-), 1 coin (0,05.-)
        [TestCase(5, 2.40)]     // total 2 : 1 coin (2.-), 2 coins (0,20.-)
        [TestCase(6, 5)]        // total 1 : 1 coin (5.-)
        [TestCase(7, 7)]        // total 2 : 1 coin (5.-), 1 coin (2.-)
        [TestCase(8, 15.50)]    // total 2 : 3 coins (5.-), 1 coin (0,50.-)
        [TestCase(9, 22)]       // total 2 : 4 coins (5.-), 1 coin (2.-)
        public async Task WhenCallingGetChangeWithEnoughCoinsThenReturnsOptimizedListOfCoins(int index, decimal remaningAmount)
        {
            IList<CoinBo> coinReferential = SwissFrancCoinReferential.GetAll().Select(c => new CoinBo(c.Id, c.Label, c.Value, c.Quantity)).ToList();
            this._coinBoxService.Setup(m => m.GetCoins()).Returns(Task.Run(() => coinReferential));

            var result = await this._getChangeService.GetChange(remaningAmount);

            switch (index)
            {
                case 0:
                    // 1
                    Assert.That(result.Count, Is.EqualTo(1));
                    CheckCoinTypeExistance(
                        result,
                        Is.False,
                        Is.False,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False);
                    break;
                case 1:
                    // 0
                    Assert.That(result, Is.Empty);
                    break;
                case 2:
                    // 1.45
                    Assert.That(result.Count, Is.EqualTo(3));
                    CheckCoinTypeExistance(
                        result,
                        Is.False,
                        Is.False,
                        Is.True,
                        Is.False,
                        Is.True,
                        Is.False,
                        Is.True);
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.OneFranc.Id).Quantity, Is.EqualTo(1));
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.TwentyCents.Id).Quantity, Is.EqualTo(2));
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.FiveCents.Id).Quantity, Is.EqualTo(1));
                    break;
                case 3:
                    // 0.05
                    Assert.That(result.Count, Is.EqualTo(1));
                    CheckCoinTypeExistance(
                        result,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.True);
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.FiveCents.Id).Quantity, Is.EqualTo(1));
                    break;
                case 4:
                    // 1.85
                    Assert.That(result.Count, Is.EqualTo(5));
                    CheckCoinTypeExistance(
                        result,
                        Is.False,
                        Is.False,
                        Is.True,
                        Is.True,
                        Is.True,
                        Is.True,
                        Is.True);
                    break;
                case 5:
                    // 2.40
                    Assert.That(result.Count, Is.EqualTo(2));
                    CheckCoinTypeExistance(
                        result,
                        Is.False,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.True,
                        Is.False,
                        Is.False);
                    break;
                case 6:
                    // 5
                    Assert.That(result.Count, Is.EqualTo(1));
                    CheckCoinTypeExistance(
                        result,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False);
                    break;
                case 7:
                    // 7
                    Assert.That(result.Count, Is.EqualTo(2));
                    CheckCoinTypeExistance(
                        result,
                        Is.True,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False);
                    break;
                case 8:
                    // 15.50
                    Assert.That(result.Count, Is.EqualTo(2));
                    CheckCoinTypeExistance(
                        result,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.False);
                    break;
                case 9:
                    // 22
                    Assert.That(result.Count, Is.EqualTo(2));
                    CheckCoinTypeExistance(
                        result,
                        Is.True,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False);
                    break;
                default:
                    Assert.Fail("Index not specified for Unit test");
                    break;
            }
        }

        [Test]
        [TestCase(0, 1)]       // total 1 : 1 coin (1.-)
        [TestCase(1, 0)]        // total 0 : 0 coin
        [TestCase(2, 1.45)]     // total 3 : 1 coin (1.-), 2 coins (0,20.-), 1 coin (0,05.-)
        [TestCase(3, 0.05)]     // total 1 : 1 coin (0,05.-)
        [TestCase(4, 1.85)]     // total 5 : 1 coin (1.-), 1 coin (0,50.-), 1 coins (0,20.-), 1 coin (0,10.-), 1 coin (0,05.-)
        [TestCase(5, 2.40)]     // total 2 : 1 coin (2.-), 2 coins (0,20.-)
        //[TestCase(6, 5)]        // total 1 : 1 coin (5.-)
        //[TestCase(7, 7)]        // total 2 : 1 coin (5.-), 1 coin (2.-)
        //[TestCase(8, 15.50)]    // total 2 : 3 coins (5.-), 1 coin (0,50.-)
        //[TestCase(9, 22)]       // total 2 : 4 coins (5.-), 1 coin (2.-)
        public async Task WhenCallingGetChangeWithMissingCoinsThenReturnsOptimizedListOfCoins(int index, decimal remaningAmount)
        {
            IList<CoinBo> coinReferential = SwissFrancCoinReferential
                                            .GetAll()
                                            .Where(c => c != SwissFrancCoinReferential.OneFranc && c != SwissFrancCoinReferential.TwentyCents)
                                            .Select(c => new CoinBo(c.Id, c.Label, c.Value, 2)).ToList();
            this._coinBoxService.Setup(m => m.GetCoins()).Returns(Task.Run(() => coinReferential));

            var result = await this._getChangeService.GetChange(remaningAmount);

            switch (index)
            {
                case 0:
                    // 1
                    Assert.That(result.Count, Is.EqualTo(1));
                    Assert.That(result.Sum(c => c.ValueInCent * c.Quantity), Is.EqualTo(100));
                    CheckCoinTypeExistance(
                        result,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.False);
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.FiftyCents.Id).Quantity, Is.EqualTo(2));
                    break;
                case 1:
                    // 0
                    Assert.That(result, Is.Empty);
                    break;
                case 2:
                    // 1.45
                    Assert.That(result.Count, Is.EqualTo(3));
                    Assert.That(result.Sum(c => c.ValueInCent * c.Quantity), Is.EqualTo(130));
                    CheckCoinTypeExistance(
                        result,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.True,
                        Is.False,
                        Is.True,
                        Is.True);

                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.FiftyCents.Id).Quantity, Is.EqualTo(2));
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.TenCents.Id).Quantity, Is.EqualTo(2));
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.FiveCents.Id).Quantity, Is.EqualTo(2));
                    break;
                case 3:
                    // 0.05
                    Assert.That(result.Count, Is.EqualTo(1));
                    Assert.That(result.Sum(c => c.ValueInCent * c.Quantity), Is.EqualTo(5));
                    CheckCoinTypeExistance(
                        result,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.True);
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.FiveCents.Id).Quantity, Is.EqualTo(1));
                    break;
                case 4:
                    // 1.85
                    Assert.That(result.Count, Is.EqualTo(3));
                    Assert.That(result.Sum(c => c.ValueInCent * c.Quantity), Is.EqualTo(130));
                    CheckCoinTypeExistance(
                        result,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.True,
                        Is.False,
                        Is.True,
                        Is.True);
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.FiftyCents.Id).Quantity, Is.EqualTo(2));
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.TenCents.Id).Quantity, Is.EqualTo(2));
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.FiveCents.Id).Quantity, Is.EqualTo(2));
                    break;
                case 5:
                    // 2.40
                    Assert.That(result.Count, Is.EqualTo(3));
                    Assert.That(result.Sum(c => c.ValueInCent * c.Quantity), Is.EqualTo(230));
                    CheckCoinTypeExistance(
                        result,
                        Is.False,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.True,
                        Is.True);
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.TwoFrancs.Id).Quantity, Is.EqualTo(1));
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.TenCents.Id).Quantity, Is.EqualTo(2));
                    Assert.That(result.Find(c => c.Id == SwissFrancCoinReferential.FiveCents.Id).Quantity, Is.EqualTo(2));
                    break;
                case 6:
                    // 5
                    Assert.That(result.Count, Is.EqualTo(1));
                    CheckCoinTypeExistance(
                        result,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False);
                    break;
                case 7:
                    // 7
                    Assert.That(result.Count, Is.EqualTo(2));
                    CheckCoinTypeExistance(
                        result,
                        Is.True,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False);
                    break;
                case 8:
                    // 15.50
                    Assert.That(result.Count, Is.EqualTo(2));
                    CheckCoinTypeExistance(
                        result,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.False);
                    break;
                case 9:
                    // 22
                    Assert.That(result.Count, Is.EqualTo(2));
                    CheckCoinTypeExistance(
                        result,
                        Is.True,
                        Is.True,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False,
                        Is.False);
                    break;
                default:
                    Assert.Fail("Index not specified for Unit test");
                    break;
            }
        }

        private static void CheckCoinTypeExistance(
            IReadOnlyCollection<CoinBo> result,
            IResolveConstraint fiveFrancs,
            IResolveConstraint twoFrancs,
            IResolveConstraint oneFrancs,
            IResolveConstraint fiftyCents,
            IResolveConstraint twentyCents,
            IResolveConstraint tenCents,
            IResolveConstraint fiveCents)
        {
            Assert.That(result.Any(c => c.Id == SwissFrancCoinReferential.FiveFrancs.Id), fiveFrancs);
            Assert.That(result.Any(c => c.Id == SwissFrancCoinReferential.TwoFrancs.Id), twoFrancs);
            Assert.That(result.Any(c => c.Id == SwissFrancCoinReferential.OneFranc.Id), oneFrancs);
            Assert.That(result.Any(c => c.Id == SwissFrancCoinReferential.FiftyCents.Id), fiftyCents);
            Assert.That(result.Any(c => c.Id == SwissFrancCoinReferential.TwentyCents.Id), twentyCents);
            Assert.That(result.Any(c => c.Id == SwissFrancCoinReferential.TenCents.Id), tenCents);
            Assert.That(result.Any(c => c.Id == SwissFrancCoinReferential.FiveCents.Id), fiveCents);
        }
    }
}
