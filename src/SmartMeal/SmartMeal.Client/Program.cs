using Microsoft.Extensions.DependencyInjection;
using SmartMeal.Data;
using SmartMeal.Manager;
using SmartMeal.Model;


var services = new ServiceCollection()
    .AddSingleton<RecipeStorage>()
    .AddSingleton<RecipeManager>();

await using var serviceProvider = services.BuildServiceProvider();

var recipeManager = serviceProvider.GetRequiredService<RecipeManager>();

// Create some recipes
recipeManager.CreateRecipe(
    "Chicken Alfredo",
    "A creamy pasta dish with chicken",
    "1. Cook pasta according to package instructions. 2. In a large skillet, cook chicken until no longer pink. 3. Add garlic and cook until fragrant. 4. Stir in heavy cream and Parmesan cheese. 5. Add pasta to skillet and toss to combine. 6. Serve immediately.",
    4,
    30,
    "Pasta",
    new List<Ingredient>
    {
                    new("Pasta", 8, "oz"),
                    new("Chicken breast", 2, "pieces"),
                    new("Garlic", 2, "cloves"),
                    new("Heavy cream", 1, "cup"),
                    new("Parmesan cheese", 1, "cup"),
                    new("Black pepper", 1, "tsp")
    },
    new List<string> { "Gluten-Free" }
);

Console.WriteLine();

recipeManager.CreateRecipe(
    "Beef Tacos",
        "A classic Mexican dish",
        "1. Cook ground beef in a skillet until browned. 2. Add taco seasoning and water, simmer until thickened. 3. Warm tortillas in a skillet. 4. Assemble tacos with beef, lettuce, cheese, and salsa. 5. Serve immediately.",
        4,
        20,
        "Mexican",
        new List<Ingredient>
        {
                    new("Ground beef", 1, "lb"),
                    new("Taco seasoning", 1, "packet"),
                    new("Water", 0.5, "cup"),
                    new("Tortillas", 8, "pieces"),
                    new("Lettuce", 1, "cup"),
                    new("Cheddar cheese", 1, "cup"),
                    new("Salsa", 1, "cup")
        },
        new List<string> { "Dairy-Free" }
    );

Console.WriteLine();


recipeManager.CreateRecipe(
    "Caesar Salad",
        "A fresh and crunchy salad",
        "1. In a large bowl, combine lettuce, croutons, and Parmesan cheese. 2. In a small bowl, whisk together olive oil, lemon juice, garlic, Dijon mustard, and Worcestershire sauce. 3. Pour dressing over salad and toss to combine. 4. Serve immediately.",
        4,
        15,
        "Salad",
        new List<Ingredient>
        {
                    new("Romaine lettuce", 1, "head"),
                    new("Croutons", 1, "cup"),
                    new("Parmesan cheese", 0.5, "cup"),
                    new("Olive oil", 0.25, "cup"),
                    new("Lemon juice", 2, "tbsp"),
                    new("Garlic", 1, "clove"),
                    new("Dijon mustard", 1, "tsp"),
                    new("Worcestershire sauce", 1, "tsp")
        }
    );

Console.WriteLine();
Console.WriteLine("Importing Recipe");

//dummy method, will always create the Chocolate Cake recipe
recipeManager.ImportRecipe("https://www.example.com/recipe");


Console.WriteLine();
Console.WriteLine("Filtering by Preference");
//Search for recipes with dietary restrictions
var preferences = new Dictionary<string, string>
        {
            { "DietaryRestrictions", "Gluten-Free" }
        };

var filteredRecipe = recipeManager.FilterRecipesByPreferences(preferences);

//Display the filtered recipes
if (filteredRecipe != null)
{
    foreach (var recipe in filteredRecipe)
    {
        recipeManager.DisplayRecipes(recipe);
    }
}
else
{
    Console.WriteLine("No recipes found matching preferences.");
}

Console.WriteLine();
Console.WriteLine("Browsing by Category");
//Search for recipes in a specific category
var category = "Mexican";
var recipesInCategory = recipeManager.BrowseRecipesByCategory(category);

//Display the recipes in the category
if (recipesInCategory != null)
{
    foreach (var recipe in recipesInCategory)
    {
        recipeManager.DisplayRecipes(recipe);
    }
}
else
{
    Console.WriteLine("No recipes found in category.");
}

Console.WriteLine();
Console.WriteLine("Searching by Keyword");
//Search for recipes by keyword
var keyword = "Chicken";
var recipesByKeyword = recipeManager.SearchRecipes(keyword);

//display the recipes by keyword
if (recipesByKeyword != null)
{
    foreach (var recipe in recipesByKeyword)
    {
        recipeManager.DisplayRecipes(recipe);
    }
}
else
{
    Console.WriteLine("No recipes found matching keyword.");
}

Console.WriteLine();
Console.WriteLine("Sharing Recipe");
//Share a recipe
var recipeToShare = recipeManager.SearchRecipes("Chicken Alfredo")?.FirstOrDefault();
if (recipeToShare != null)
{
    recipeManager.ShareRecipe(recipeToShare);
}
else
{
    Console.WriteLine("Recipe not found.");
}
Console.WriteLine();

Console.WriteLine("Press any key to exit.");
Console.ReadKey();
return 0;
