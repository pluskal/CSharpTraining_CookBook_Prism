using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.BL.Models;
using CookBook.Common;
using NUnit.Framework;

namespace CookBook.BL.Tests
{
    [TestFixture]
    public class RecipeRepositoryTests
    {
        [TearDown]
        public void TearDown()
        {
            this._recipeRepository.ClearDatabase();
        }

        private RecipeRepository _recipeRepository;

        [OneTimeSetUp]
        public void SetUp()
        {
            this._recipeRepository = new RecipeRepository(new RecipeMapper());
            this._recipeRepository.ClearDatabase();
        }

        private void GetAssertRecipeDetailModel(RecipeDetailModel recipe)
        {
            var detailModel = this._recipeRepository.GetById(recipe.Id);

            Assert.NotNull(detailModel);
            Assert.AreEqual(detailModel, recipe);
        }

        private static void AssertIngredient(RecipeDetailModel recipe, IngredientDetailModel[] ingredients)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                Assert.Contains(ingredient.Name, ingredients.Select(i => i.Name).ToList());
                Assert.Contains(ingredient.Description, ingredients.Select(i => i.Description).ToList());
                Assert.Contains(ingredient.Id, ingredients.Select(i => i.Id).ToList());
            }
        }

        private RecipeDetailModel CreateRecipeDetailModel()
        {
            var recipe = new RecipeDetailModel
            {
                Name = "RecipeName",
                Description = "RecipeDescription",
                Duration = new TimeSpan(0, 0, 1),
                Type = FoodType.MainCourse,
                Ingredients = new List<IngredientModel>
                {
                    new IngredientModel
                    {
                        Name = "Ingredient1Name",
                        Description = "Ingredient1Description",
                        Unit = Unit.Kg,
                        Amount = 3.14
                    },
                    new IngredientModel
                    {
                        Name = "Ingredient2Name",
                        Description = "Ingredient2Description",
                        Unit = Unit.Kg,
                        Amount = 3.14
                    }
                }
            };
            return recipe;
        }

        [Test]
        public void _GetAll_RecipesNotNull()
        {
            var recipes = this._recipeRepository.GetAll();
            Assert.IsEmpty(recipes);
        }

        [Test]
        public void InsertNewRecipe_GetAllIngredients_IngredientsPresent()
        {
            var recipe = this.CreateRecipeDetailModel();
            this._recipeRepository.InsertRecipe(recipe);
            var allIngredients = this._recipeRepository.GetAllIngredients();
            AssertIngredient(recipe, allIngredients);
        }

        [Test]
        public void NewRecipe_InsertRecipe_RecipeInserted()
        {
            var recipe = this.CreateRecipeDetailModel();
            this._recipeRepository.InsertRecipe(recipe);

            this.GetAssertRecipeDetailModel(recipe);
        }

        [Test]
        public void TwoNewRecipes_InsertRecipe_RecipesInserted()
        {
            var recipe1 = this.CreateRecipeDetailModel();
            this._recipeRepository.InsertRecipe(recipe1);
            var recipe2 = this.CreateRecipeDetailModel();
            this._recipeRepository.InsertRecipe(recipe2);

            this.GetAssertRecipeDetailModel(recipe1);
            this.GetAssertRecipeDetailModel(recipe2);
        }
    }
}