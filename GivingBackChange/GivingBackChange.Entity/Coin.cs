using GivingBackChange.Entity.Base;

namespace GivingBackChange.Entity
{
    public class Coin : EntityBase
    {
        public Coin(int id, string label, float value, int quantity)
        {
            this.Id = id;
            this.Label = label;
            this.Value = value;
            this.Quantity = quantity;
        }

        public string Label { get; set; }

        public float Value { get; set; }

        public int Quantity { get; set; }
    }
}
