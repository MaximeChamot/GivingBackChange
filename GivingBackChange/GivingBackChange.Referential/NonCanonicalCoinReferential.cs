using GivingBackChange.Referential.Enums;
using System.Collections.Generic;
using System.Linq;
using GivingBackChange.Entity;

namespace GivingBackChange.Referential
{
    public static class NonCanonicalCoinReferential
    {
        public static List<Coin> GetAll()
        {
            return NonCanonicalCoins.ToList();
        }

        private static IEnumerable<Coin> NonCanonicalCoins =>
            new List<Coin>
            {
                One,
                Three,
                Four
            };

        public static readonly Coin One = new Coin((int)NonCanonicalCoin.One, "1 fm", 1f, 10);
        public static readonly Coin Three = new Coin((int)NonCanonicalCoin.Three, "3 fm", 3f, 10);
        public static readonly Coin Four = new Coin((int)NonCanonicalCoin.Four, "4 fm", 4f, 10);
    }
}
