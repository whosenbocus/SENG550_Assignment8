namespace SmartMeal.Model
{
    public record Ingredient
    {
        // Properties
        public string Name { get; }
        public double Quantity { get; }
        public string Unit { get; }

        // Constructor
        public Ingredient(string name, double quantity, string unit)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity > 0 ? quantity : throw new ArgumentOutOfRangeException(nameof(quantity));
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        // Methods for creating updated instances
        public Ingredient WithQuantity(double quantity) => new Ingredient(Name, quantity, Unit);
        public Ingredient WithUnit(string unit) => new Ingredient(Name, Quantity, unit);
        public Ingredient Update(string name, float quantity, string unit) => new Ingredient(name, quantity, unit);

        // Additional helper methods (optional)
        public override string ToString() => $"{Quantity} {Unit} of {Name}";
    }
}