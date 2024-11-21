using SmartMeal.Model;


namespace SmartMeal.Manager
{

    public class RecipeManager
    {
        // Private collection of recipes
        private List<Recipe> Recipes { get; set; }

        // Constructor
        public RecipeManager()
        {
            Recipes = new List<Recipe>();
        }

        // Add a recipe to the collection
        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
                throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");
            Recipes.Add(recipe);
            Console.WriteLine($"Recipe '{recipe.Name}' added successfully.");
        }

        // Remove a recipe by name
        public void RemoveRecipe(string recipeName)
        {
            var recipe = Recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe != null)
            {
                Recipes.Remove(recipe);
                Console.WriteLine($"Recipe '{recipeName}' removed successfully.");
            }
            else
            {
                Console.WriteLine($"Recipe '{recipeName}' not found.");
            }
        }

        // Search recipes by keyword
        public List<Recipe> SearchRecipes(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Recipe>();

            var results = Recipes
                .FindAll(r => r.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                              r.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine($"Found {results.Count} recipe(s) matching '{keyword}'.");
            return results;
        }

        // Browse recipes by category
        public List<Recipe> BrowseRecipesByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return new List<Recipe>();

            var results = Recipes
                .FindAll(r => r.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine($"Found {results.Count} recipe(s) in category '{category}'.");
            return results;
        }

        // Filter recipes by preferences
        public List<Recipe> FilterRecipesByPreferences(Dictionary<string, string> preferences)
        {
            if (preferences == null || preferences.Count == 0)
                return Recipes;

            var results = Recipes
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

            AddRecipe(importedRecipe);
            Console.WriteLine($"Recipe '{importedRecipe.Name}' imported successfully.");
        }

        // Create a new recipe
        public void CreateRecipe()
        {
            Console.WriteLine($"Creating recipe.");

            // Simulated data creation
            string name = "Created Chocolate Cake";
            string description = "A rich created chocolate cake recipe.";
            string instructions = "Mix created ingredients, bake at 350°F for 30 minutes.";
            int servings = 8;
            int prepTime = 45;
            string category = "Dessert";

            // Create and add a RecipeItem
            var createdRecipe = new RecipeItem(name, description, instructions, servings, prepTime, category);
            createdRecipe.AddIngredient(new Ingredient("Flour", 250.0f, "grams"));
            createdRecipe.AddIngredient(new Ingredient("Cocoa Powder", 50.0f, "grams"));
            createdRecipe.AddIngredient(new Ingredient("Sugar", 150.0f, "grams"));

            AddRecipe(createdRecipe);
            Console.WriteLine($"Recipe '{createdRecipe.Name}' created successfully.");
        }

        // Display all recipes
        public void DisplayRecipes()
        {
            if (Recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.WriteLine("Available Recipes:");
            foreach (var recipe in Recipes)
            {
                Console.WriteLine($" - {recipe.Name} ({recipe.Category})");
            }
        }
    }

}
