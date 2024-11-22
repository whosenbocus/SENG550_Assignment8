using Moq;
using SmartMeal.Data;
using SmartMeal.Manager;
using SmartMeal.Model;

namespace SmartMeal.Test;

public class RecipeManagerTests
{
    private readonly Mock<RecipeStorage> _mockRecipeStorage;
    private readonly RecipeManager _recipeManager;

    public RecipeManagerTests()
    {
        _mockRecipeStorage = new Mock<RecipeStorage>();
        _recipeManager = new RecipeManager(_mockRecipeStorage.Object);
    }

    [Fact]
    public void RemoveRecipe_ShouldRemoveRecipe_WhenRecipeExists()
    {
        // Arrange
        var recipeName = "Test Recipe";
        _mockRecipeStorage.Setup(rs => rs.RemoveRecipe(recipeName)).Returns(true);

        // Act
        _recipeManager.RemoveRecipe(recipeName);

        // Assert
        _mockRecipeStorage.Verify(rs => rs.RemoveRecipe(recipeName), Times.Once);
    }

    [Fact]
    public void RemoveRecipe_ShouldNotRemoveRecipe_WhenRecipeDoesNotExist()
    {
        // Arrange
        var recipeName = "Nonexistent Recipe";
        _mockRecipeStorage.Setup(rs => rs.RemoveRecipe(recipeName)).Returns(false);

        // Act
        _recipeManager.RemoveRecipe(recipeName);

        // Assert
        _mockRecipeStorage.Verify(rs => rs.RemoveRecipe(recipeName), Times.Once);
    }

    [Fact]
    public void SearchRecipes_ShouldReturnRecipes_WhenKeywordMatches()
    {
        // Arrange
        var keyword = "Chicken";
        var recipes = new List<Recipe>
        {
            new RecipeItem("Chicken Alfredo", "Description", "Instructions", 4, 30, "Pasta"),
            new RecipeItem("Beef Tacos", "Description", "Instructions", 4, 20, "Mexican")
        };
        _mockRecipeStorage.Setup(rs => rs.GetRecipes()).Returns(recipes);

        // Act
        var result = _recipeManager.SearchRecipes(keyword);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public void BrowseRecipesByCategory_ShouldReturnRecipes_WhenCategoryMatches()
    {
        // Arrange
        var category = "Pasta";
        var recipes = new List<Recipe>
        {
            new RecipeItem("Chicken Alfredo", "Description", "Instructions", 4, 30, "Pasta"),
            new RecipeItem("Beef Tacos", "Description", "Instructions", 4, 20, "Mexican")
        };
        _mockRecipeStorage.Setup(rs => rs.GetRecipes()).Returns(recipes);

        // Act
        var result = _recipeManager.BrowseRecipesByCategory(category);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public void FilterRecipesByPreferences_ShouldReturnRecipes_WhenPreferencesMatch()
    {
        // Arrange
        var preferences = new Dictionary<string, string> { { "DietaryRestrictions", "Gluten-Free" } };
        var recipe = new RecipeItem("Chicken Alfredo", "Description", "Instructions", 4, 30, "Pasta");
        recipe
            .AddDietaryRestriction("Gluten-Free");

        var recipes = new List<Recipe>
        {
            recipe,
            new RecipeItem("Beef Tacos", "Description", "Instructions", 4, 20, "Mexican")
        };
        _mockRecipeStorage.Setup(rs => rs.GetRecipes()).Returns(recipes);

        // Act
        var result = _recipeManager.FilterRecipesByPreferences(preferences);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public void ImportRecipe_ShouldAddRecipe_WhenUrlIsValid()
    {
        // Arrange
        var url = "https://www.example.com/recipe";

        // Act
        _recipeManager.ImportRecipe(url);

        // Assert
        _mockRecipeStorage.Verify(rs => rs.AddRecipe(It.IsAny<Recipe>()), Times.Once);
    }

    [Fact]
    public void CreateRecipe_ShouldAddRecipe_WhenParametersAreValid()
    {
        // Arrange
        var name = "Test Recipe";
        var description = "Test Description";
        var instructions = "Test Instructions";
        var servings = 4;
        var prepTime = 30;
        var category = "Test Category";
        var ingredients = new List<Ingredient>
        {
            new Ingredient("Test Ingredient", 1, "unit")
        };

        // Act
        _recipeManager.CreateRecipe(name, description, instructions, servings, prepTime, category, ingredients);

        // Assert
        _mockRecipeStorage.Verify(rs => rs.AddRecipe(It.IsAny<Recipe>()), Times.Once);
    }

    [Fact]
    public void CreateRecipeWithDietaryRestrictions_ShouldAddRecipe_WhenParametersAreValid()
    {
        // Arrange
        var name = "Test Recipe";
        var description = "Test Description";
        var instructions = "Test Instructions";
        var servings = 4;
        var prepTime = 30;
        var category = "Test Category";
        var ingredients = new List<Ingredient>
        {
            new Ingredient("Test Ingredient", 1, "unit")
        };
        var dietaryRestrictions = new List<string> { "Gluten-Free" };

        // Act
        _recipeManager.CreateRecipe(name, description, instructions, servings, prepTime, category, ingredients, dietaryRestrictions);

        // Assert
        _mockRecipeStorage.Verify(rs => rs.AddRecipe(It.IsAny<Recipe>()), Times.Once);
    }

    [Fact]
    public void DisplayRecipes_ShouldDisplayRecipe_WhenRecipeIsNotNull()
    {
        // Arrange
        var recipe = new Mock<Recipe>("Test Recipe", "Description", "Instructions", 4, 30, "Category");
        recipe.Setup(r => r.ViewRecipe()).Returns("Recipe Details");

        // Act
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            _recipeManager.DisplayRecipes(recipe.Object);

            // Assert
            var result = sw.ToString().Trim();
            Assert.Equal("Recipe Details", result);
        }
    }

    [Fact]
    public void DisplayRecipes_ShouldNotDisplayRecipe_WhenRecipeIsNull()
    {
        // Act
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            _recipeManager.DisplayRecipes(null);

            // Assert
            var result = sw.ToString().Trim();
            Assert.Equal(string.Empty, result);
        }
    }

    [Fact]
    public void ShareRecipe_ShouldShareRecipe_WhenRecipeIsNotNull()
    {
        // Arrange
        var recipe = new Mock<Recipe>("Test Recipe", "Description", "Instructions", 4, 30, "Category");
        recipe.Setup(r => r.ShareRecipe()).Returns("Shared Recipe Details");

        // Act
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            _recipeManager.ShareRecipe(recipe.Object);

            // Assert
            var result = sw.ToString().Trim();
            Assert.Equal("Sharing Recipe:\nShared Recipe Details", result);
        }
    }

    [Fact]
    public void ShareRecipe_ShouldNotShareRecipe_WhenRecipeIsNull()
    {
        // Act
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            _recipeManager.ShareRecipe(null);

            // Assert
            var result = sw.ToString().Trim();
            Assert.Equal(string.Empty, result);
        }
    }
}
