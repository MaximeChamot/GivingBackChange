using GivingBackChange.Referential.Enums;
using System.Collections.Generic;
using System.Linq;
using GivingBackChange.Entity;

namespace GivingBackChange.Referential
{
    public static class EuroCoinReferential
    {
        public static List<Coin> GetAll()
        {
            return SwissCoins.ToList();
        }

        private static IEnumerable<Coin> SwissCoins =>
            new List<Coin>
            {
                OneCent,
                TwoCents,
                FiveCents,
                TenCentimes,
                TwentyCents,
                FiftyCents,
                OneEuro,
                TwoEuro,
            };

        public static readonly Coin OneCent = new Coin((int)EuroCoin.OneCent, "0,01€", 0.01m, 10);
        public static readonly Coin TwoCents = new Coin((int)EuroCoin.TwoCents, "0,02€", 0.02m, 10);
        public static readonly Coin FiveCents = new Coin((int)EuroCoin.FiveCents, "0,05€", 0.05m, 10);
        public static readonly Coin TenCentimes = new Coin((int)EuroCoin.TenCents, "0,10€", 0.1m, 10);
        public static readonly Coin TwentyCents = new Coin((int)EuroCoin.TwentyCents, "0,20€", 0.2m, 10);
        public static readonly Coin FiftyCents = new Coin((int)EuroCoin.FiftyCents, "0,50€", 0.5m, 10);
        public static readonly Coin OneEuro = new Coin((int)EuroCoin.OneEuro, "1€", 1m, 10);
        public static readonly Coin TwoEuro = new Coin((int)EuroCoin.TwoEuro, "2€", 2m, 10);
    }
}
