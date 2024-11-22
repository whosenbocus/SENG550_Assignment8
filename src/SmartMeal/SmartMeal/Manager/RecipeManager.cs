using SmartMeal.Data;
using SmartMeal.Model;


namespace SmartMeal.Manager
{

    public class RecipeManager(RecipeStorage recipeStorage)
    {
        // Private collection of recipes
        private readonly RecipeStorage _recipeStorage = recipeStorage;

        // Remove a recipe by name
        public void RemoveRecipe(string recipeName)
        {
            if (_recipeStorage.RemoveRecipe(recipeName))
            {
                Console.WriteLine($"Recipe '{recipeName}' removed successfully.");
            }
            else
            {
                Console.WriteLine($"Recipe '{recipeName}' not found.");
            }
        }

        // Search recipes by keyword
        public IEnumerable<Recipe>? SearchRecipes(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return null;

            var results = _recipeStorage
                .GetRecipes()
                .Where(r => r.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                              r.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine($"Found {results.Count()} recipe(s) matching '{keyword}'.");
            return results;
        }

        // Browse recipes by category
        public IEnumerable<Recipe>? BrowseRecipesByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return null;

            var results = _recipeStorage
                .GetRecipes()
                .Where(r => r.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine($"Found {results.Count()} recipe(s) in category '{category}'.");
            return results;
        }

        // Filter recipes by preferences
        public IEnumerable<Recipe>? FilterRecipesByPreferences(Dictionary<string, string> preferences)
        {
            if (preferences == null || preferences.Count == 0)
                return null;

            var results = _recipeStorage
                .GetRecipes()
                .Where(r =>
                {
                    if (preferences.TryGetValue("DietaryRestrictions", out var restriction))
                    {
                        if (r is RecipeItem recipeItem &&
                            (recipeItem.DietaryRestrictions == null ||
                             !recipeItem.DietaryRestrictions.Contains(restriction)))
                        {
                            return false;
                        }
                    }
                    return true;
                }).ToList();

            Console.WriteLine($"Found {results.Count} recipe(s) matching preferences.");
            return results;
        }

        // Import recipe from an external source (e.g., URL)
        public void ImportRecipe(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url), "URL cannot be empty.");

            Console.WriteLine($"Importing recipe from: {url}");

            // Simulated data fetched from URL
            string name = "Imported Chocolate Cake";
            string description = "A rich imported chocolate cake recipe.";
            string instructions = "Mix imported ingredients, bake at 350°F for 30 minutes.";
            int servings = 8;
            int prepTime = 45;
            string category = "Dessert";

            // Create and add a RecipeItem
            var importedRecipe = new RecipeItem(name, description, instructions, servings, prepTime, category);
            importedRecipe.AddIngredient(new Ingredient("Flour", 250.0f, "grams"));
            importedRecipe.AddIngredient(new Ingredient("Cocoa Powder", 50.0f, "grams"));
            importedRecipe.AddIngredient(new Ingredient("Sugar", 150.0f, "grams"));

            _recipeStorage.AddRecipe(importedRecipe);
            Console.WriteLine($"Recipe '{importedRecipe.Name}' imported successfully.");
        }

        // Create a new recipe
        public void CreateRecipe(string name, string description, string instructions, int servings, int prepTime, string category, IEnumerable<Ingredient> ingredients)
        {
            Console.WriteLine("Creating recipe.");

            // Create and add a RecipeItem
            var createdRecipe = new RecipeItem(name, description, instructions, servings, prepTime, category);

            AddIngredientsToRecipe(createdRecipe, ingredients);

            _recipeStorage.AddRecipe(createdRecipe);
            Console.WriteLine($"Recipe '{createdRecipe.Name}' created successfully.");
        }

        // Create a new recipe with dietary restrictions
        public void CreateRecipe(string name, string description, string instructions, int servings, int prepTime, string category, IEnumerable<Ingredient> ingredients, IEnumerable<string> dietaryRestrictions)
        {
            Console.WriteLine("Creating recipe with dietary restrictions.");

            // Create and add a RecipeItem
            var createdRecipe = new RecipeItem(name, description, instructions, servings, prepTime, category);

            AddIngredientsToRecipe(createdRecipe, ingredients);
            AddDietaryRestrictionsToRecipe(createdRecipe, dietaryRestrictions);

            _recipeStorage.AddRecipe(createdRecipe);
            Console.WriteLine($"Recipe '{createdRecipe.Name}' created successfully.");
        }


        // Display all recipes
        public void DisplayRecipes(Recipe recipe)
        {
            if (recipe is null)
            {
                return;
            }

            Console.WriteLine(recipe!.ViewRecipe());
        }

        public void ShareRecipe(Recipe recipe)
        {
            if (recipe is null)
            {
                return;
            }
            Console.WriteLine($"Sharing Recipe:\n{ recipe!.ShareRecipe()}");
        }

        private void AddIngredientsToRecipe(RecipeItem recipe, IEnumerable<Ingredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                recipe.AddIngredient(ingredient);
            }
        }

        private void AddDietaryRestrictionsToRecipe(RecipeItem recipe, IEnumerable<string> dietaryRestrictions)
        {
            foreach (var restriction in dietaryRestrictions)
            {
                recipe.AddDietaryRestriction(restriction);
            }
        }
    }

}
