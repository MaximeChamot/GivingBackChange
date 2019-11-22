using GivingBackChange.Entity;

namespace GivingBackChange.Business.BusinessObjects
{
    public class CoinBo : Coin
    {
        public CoinBo(int id, string label, decimal value, int quantity) : base(id, label, value, quantity)
        {
        }

        public int ValueInCent => (int)(this.Value * 100);
    }
}