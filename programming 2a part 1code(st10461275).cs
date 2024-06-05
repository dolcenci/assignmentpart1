using System;
using System.Collections.Generic;

namespace RecipeApp
{
    class Recipe
    {
        public string Name { get; set; }
        public List<(string, double, string)> Ingredients { get; set; }
        public string Instructions { get; set; }
        public List<(string, double, string)> OriginalIngredients { get; set; }

        public Recipe(string name, List<(string, double, string)> ingredients, string instructions)
        {
            Name = name;
            Ingredients = ingredients;
            OriginalIngredients = new List<(string, double, string)>(ingredients);
            Instructions = instructions;
        }
    }

    class Program
    {
        static List<Recipe> recipes = new List<Recipe>();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Welcome to Recipe App!");
                Console.WriteLine("1. Add Recipe");
                Console.WriteLine("2. View All Recipes");
                Console.WriteLine("3. Scale a Recipe");
                Console.WriteLine("4. Scale Quantities to Original Values");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddRecipe();
                        break;
                    case "2":
                        ViewRecipes();
                        break;
                    case "3":
                        ScaleRecipe();
                        break;
                    case "4":
                        ScaleToOriginalValues();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddRecipe()
        {
            Console.Write("Enter recipe name: ");
            string name = Console.ReadLine();

            List<(string, double, string)> ingredients = new List<(string, double, string)>();
            Console.WriteLine("Enter ingredients (one per line, type 'done' when finished):");
            string ingredientName = "";
            double ingredientQuantity = 0.0;
            string ingredientUnit = "";
            Console.Write("Ingredient name: ");
            ingredientName = Console.ReadLine();
            while (ingredientName.ToLower() != "done")
            {
                Console.Write("Quantity: ");
                ingredientQuantity = Convert.ToDouble(Console.ReadLine());
                Console.Write("Unit of measurement: ");
                ingredientUnit = Console.ReadLine();
                
                ingredients.Add((ingredientName, ingredientQuantity, ingredientUnit));
                
                Console.Write("Ingredient name: ");
                ingredientName = Console.ReadLine();
            }

            Console.WriteLine("Enter instructions:");
            string instructions = Console.ReadLine();

            Recipe newRecipe = new Recipe(name, ingredients, instructions);
            recipes.Add(newRecipe);
            Console.WriteLine("Recipe added successfully!");
        }

        static void ViewRecipes()
        {
            Console.WriteLine("All Recipes:");
            foreach (var recipe in recipes)
            {
                Console.WriteLine($"Name: {recipe.Name}");
                Console.WriteLine("Ingredients:");
                foreach (var ingredient in recipe.Ingredients)
                {
                    Console.WriteLine($"- {ingredient.Item2} {ingredient.Item3} of {ingredient.Item1}");
                }
                Console.WriteLine("Instructions:");
                Console.WriteLine(recipe.Instructions);
                Console.WriteLine();
            }
        }

        static void ScaleRecipe()
        {
            Console.WriteLine("Enter the name of the recipe to scale:");
            string recipeName = Console.ReadLine();
            Recipe recipeToScale = recipes.Find(r => r.Name.ToLower() == recipeName.ToLower());
            if (recipeToScale == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }

            Console.WriteLine("Enter scaling factor (0.5 for half, 2 for double, 3 for triple):");
            double scalingFactor = Convert.ToDouble(Console.ReadLine());

            List<(string, double, string)> scaledIngredients = new List<(string, double, string)>();
            foreach (var ingredient in recipeToScale.Ingredients)
            {
                double scaledQuantity = ingredient.Item2 * scalingFactor;
                scaledIngredients.Add((ingredient.Item1, scaledQuantity, ingredient.Item3));
            }

            Console.WriteLine("Scaled Recipe:");
            Console.WriteLine($"Name: {recipeToScale.Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in scaledIngredients)
            {
                Console.WriteLine($"- {ingredient.Item2} {ingredient.Item3} of {ingredient.Item1}");
            }
            Console.WriteLine("Instructions:");
            Console.WriteLine(recipeToScale.Instructions);
        }

        static void ScaleToOriginalValues()
        {
            Console.WriteLine("Enter the name of the recipe to scale to original values:");
            string recipeName = Console.ReadLine();
            Recipe recipeToScale = recipes.Find(r => r.Name.ToLower() == recipeName.ToLower());
            if (recipeToScale == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }

            Console.WriteLine("Recipe Ingredients scaled back to original values:");
            Console.WriteLine($"Name: {recipeToScale.Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in recipeToScale.OriginalIngredients)
            {
                Console.WriteLine($"- {ingredient.Item2} {ingredient.Item3} of {ingredient.Item1}");
            }
            Console.WriteLine("Instructions:");
            Console.WriteLine(recipeToScale.Instructions);
        }
    }
}