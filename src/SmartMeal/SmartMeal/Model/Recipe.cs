namespace SmartMeal.Model;

public abstract class Recipe(string name, string description, string instructions, int servings, int prepTime, string category)
{
    // Properties
    public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));
    public string Description { get; } = description ?? throw new ArgumentNullException(nameof(description));
    public string Instructions { get; } = instructions ?? throw new ArgumentNullException(nameof(instructions));
    public int Servings { get; } = servings > 0 ? servings : throw new ArgumentOutOfRangeException(nameof(servings));
    public int PrepTime { get; } = prepTime >= 0 ? prepTime : throw new ArgumentOutOfRangeException(nameof(prepTime));
    public string Category { get; } = category ?? throw new ArgumentNullException(nameof(category));
    protected List<Ingredient> Ingredients { get; } = new List<Ingredient>();

    // Ingredient management methods
    public void AddIngredient(Ingredient ingredient)
    {
        ArgumentNullException.ThrowIfNull(ingredient);
        Ingredients.Add(ingredient);
    }

    public void RemoveIngredient(string name)
    {
        var ingredient = Ingredients.Find(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (ingredient != null)
        {
            Ingredients.Remove(ingredient);
        }
    }

    public IReadOnlyList<Ingredient> GetIngredients() => Ingredients.AsReadOnly();

    private string DisplayIngredients()
    {
        if (Ingredients.Count == 0)
        {
            return string.Empty;
        }

        string ingredients = "Ingredients:\n";
        foreach (var ingredient in Ingredients)
        {
            ingredients += $" - {ingredient}\n";
        }
        return ingredients;
    }

    internal virtual string ViewRecipe()
    {
        return $"{Name} - {Category}: {Description} (Servings: {Servings}, Prep Time: {PrepTime} mins). {DisplayIngredients()}";
    }

    internal abstract string ShareRecipe();
}