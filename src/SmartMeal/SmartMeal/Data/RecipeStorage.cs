using SmartMeal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeal.Data
{
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
            Console.WriteLine($"Recipe '{recipe.Name}' added successfully.");
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
                Console.WriteLine($"Recipe '{recipeName}' removed successfully.");
                return true;
            }
            else
            {
                Console.WriteLine($"Recipe '{recipeName}' not found.");
                return false;
            }
        }
    }
}
