namespace GivingBackChange.Entity
{
    public class Coin
    {
        public Coin(int id, string label, float value, int quantity)
        {
            this.Id = id;
            this.Label = label;
            this.Value = value;
            this.Quantity = quantity;
        }

        public int Id { get; set; }

        public string Label { get; set; }

        public float Value { get; set; }

        public int Quantity { get; set; }
    }
}
