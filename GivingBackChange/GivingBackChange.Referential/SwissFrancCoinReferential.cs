﻿using GivingBackChange.Referential.Enums;
using System.Collections.Generic;
using System.Linq;
using GivingBackChange.Entity;

namespace GivingBackChange.Referential
{
    public static class SwissFrancCoinReferential
    {
        public static List<Coin> GetAll()
        {
            return SwissCoins.ToList();
        }

        private static IEnumerable<Coin> SwissCoins =>
            new List<Coin>
            {
                FiveCents,
                TenCentimes,
                TwentyCents,
                FiftyCents,
                OneFranc,
                TwoFrancs,
                FiveFrancs
            };

        public static readonly Coin FiveCents = new Coin((int)SwissFrancCoin.FiveCents, "0,05.-", 0.05f, 10);
        public static readonly Coin TenCentimes = new Coin((int)SwissFrancCoin.TenCents, "0,10.-", 0.1f, 10);
        public static readonly Coin TwentyCents = new Coin((int)SwissFrancCoin.TwentyCents, "0,20.-", 0.2f, 10);
        public static readonly Coin FiftyCents = new Coin((int)SwissFrancCoin.FiftyCents, "0,50.-", 0.5f, 10);
        public static readonly Coin OneFranc = new Coin((int)SwissFrancCoin.OneFranc, "1.-", 1f, 10);
        public static readonly Coin TwoFrancs = new Coin((int)SwissFrancCoin.Twofrancs, "2.-", 2f, 10);
        public static readonly Coin FiveFrancs = new Coin((int)SwissFrancCoin.FiveFrancs, "5.-", 5f, 10);
    }
}
