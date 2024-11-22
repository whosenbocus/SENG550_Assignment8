using SmartMeal.Model;

namespace SmartMeal.Data;

public class RecipeStorage
{
    private List<Recipe> recipesStorage;

    public RecipeStorage()
    {
        recipesStorage = [];
    }

    public virtual void AddRecipe(Recipe recipe)
    {
        if (recipe == null)
            throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");
        recipesStorage.Add(recipe);
    }

    public virtual IEnumerable<Recipe> GetRecipes()
    {
        return recipesStorage;
    }

    public virtual bool RemoveRecipe(string recipeName)
    {
        var recipe = recipesStorage
            .Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

        if (recipe != null)
        {
            recipesStorage.Remove(recipe);
            return true;
        }
        else
        {
            return false;
        }
    }
}
