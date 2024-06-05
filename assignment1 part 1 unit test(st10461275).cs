using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeApp;
using System;
using System.Collections.Generic;
using System.IO;

namespace RecipeAppTests
{
    [TestClass]
    public class RecipeAppTests
    {
        private StringWriter output;
        private StringReader input;

        [TestInitialize]
        public void Initialize()
        {
            output = new StringWriter();
            input = new StringReader("");
            Console.SetOut(output);
            Console.SetIn(input);
        }

        [TestMethod]
        public void TestAddRecipe_ValidInput_ShouldAddRecipe()
        {
            // Arrange
            var expectedRecipeName = "Test Recipe";
            var expectedIngredients = new List<(string, double, string)> { ("Ingredient1", 1.5, "cups"), ("Ingredient2", 2.0, "tbsp") };
            var expectedInstructions = "Mix all ingredients together.";
            var inputLines = new List<string> { expectedRecipeName, "Ingredient1", "1.5", "cups", "Ingredient2", "2.0", "tbsp", "done", expectedInstructions };

            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(string.Join(Environment.NewLine, inputLines)))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    // Act
                    Program.AddRecipe();

                    // Assert
                    var addedRecipe = Program.recipes[0];
                    Assert.AreEqual(expectedRecipeName, addedRecipe.Name);
                    CollectionAssert.AreEqual(expectedIngredients, addedRecipe.Ingredients);
                    Assert.AreEqual(expectedInstructions, addedRecipe.Instructions);
                }
            }
        }

        [TestMethod]
        public void TestViewRecipes_WithExistingRecipes_ShouldPrintRecipes()
        {
            // Arrange
            var recipes = new List<Recipe>
            {
                new Recipe("Recipe1", new List<(string, double, string)> { ("Ingredient1", 1.0, "cup") }, "Mix and bake."),
                new Recipe("Recipe2", new List<(string, double, string)> { ("Ingredient2", 2.0, "tsp") }, "Stir well.")
            };
            Program.recipes.AddRange(recipes);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                Program.ViewRecipes();

                // Assert
                var expectedOutput = string.Format("All Recipes:{0}", Environment.NewLine);
                foreach (var recipe in recipes)
                {
                    expectedOutput += $"Name: {recipe.Name}{Environment.NewLine}";
                    expectedOutput += "Ingredients:" + Environment.NewLine;
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        expectedOutput += $"- {ingredient.Item2} {ingredient.Item3} of {ingredient.Item1}{Environment.NewLine}";
                    }
                    expectedOutput += "Instructions:" + Environment.NewLine;
                    expectedOutput += $"{recipe.Instructions}{Environment.NewLine}{Environment.NewLine}";
                }

                Assert.AreEqual(expectedOutput, sw.ToString());
            }
        }

        // Add more unit tests for other functionalities as needed
    }
}
