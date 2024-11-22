namespace SmartMeal.Model
{
    public class RecipeItem(string name, string description, string instructions, int servings, int prepTime, string category) : Recipe(name, description, instructions, servings, prepTime, category)
    {
        // Additional properties
        public List<string> DietaryRestrictions { get; } = new List<string>();

        // Method to add dietary restriction
        public void AddDietaryRestriction(string restriction)
        {
            if (string.IsNullOrWhiteSpace(restriction))
                throw new ArgumentNullException(nameof(restriction), "Dietary restriction cannot be null or empty.");
            DietaryRestrictions.Add(restriction);
        }

        // Method to remove dietary restriction
        public void RemoveDietaryRestriction(string restriction)
        {
            if (string.IsNullOrWhiteSpace(restriction))
                throw new ArgumentNullException(nameof(restriction), "Dietary restriction cannot be null or empty.");
            DietaryRestrictions.Remove(restriction);
        }

        internal override string ShareRecipe()
        {
            return $"Recipe Details:\n {ViewRecipe()}";

        }

        // Override ToString method to include dietary restrictions
        internal override string ViewRecipe()
        {
            var restrictions = DietaryRestrictions.Count > 0 ? string.Join(", ", DietaryRestrictions) : "None";
            return $"{base.ViewRecipe()} | Dietary Restrictions: {restrictions}";
        }

    }
}