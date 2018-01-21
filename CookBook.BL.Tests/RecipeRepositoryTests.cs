using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
            this._cookBookRepository.ClearDatabase();
        }

        private CookBookRepository _cookBookRepository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this._cookBookRepository = new CookBookRepository(new Mapper(new CookBookMapperConfiguration()));
        }
        [SetUp]
        public void SetUp()
        {
            this._cookBookRepository.ClearDatabase();
        }

        private void GetAssertRecipeDetailModel(RecipeDetailDto recipe)
        {
            var detailModel = this._cookBookRepository.GetRecipeById(recipe.Id);

            Assert.NotNull(detailModel);
            Assert.AreEqual(detailModel, recipe);
        }

        private static void AssertIngredients(IEnumerable<IngredientDetailDto> ingredientDetailDtos,
            IngredientListDto[] ingredientListDtos)
        {
            var detailDtos = ingredientDetailDtos as IngredientDetailDto[] ?? ingredientDetailDtos.ToArray();
            foreach (var ingredient in detailDtos)
            {
                Assert.Contains(ingredient.Name, ingredientListDtos.Select(i => i.Name).ToList());
                Assert.Contains(ingredient.Description, ingredientListDtos.Select(i => i.Description).ToList());
                Assert.Contains(ingredient.IngredientId, ingredientListDtos.Select(i => i.Id).ToList());
            }
        }

        private RecipeDetailDto CreateRecipeDetail()
        {
            var recipe = new RecipeDetailDto
            {
                Name = "RecipeName",
                Description = "RecipeDescription",
                Duration = new TimeSpan(0, 0, 1),
                Type = FoodType.MainCourse,
                Ingredients = new List<IngredientDetailDto>
                {
                    new IngredientDetailDto
                    {
                        Name = "Ingredient1Name",
                        Description = "Ingredient1Description",
                        Unit = Unit.Kg,
                        Amount = 3.14
                    },
                    new IngredientDetailDto
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

        private void AssertIngredients(IngredientListDto[] ingredientListDto, IngredientListDto[] allIngredients)
        {
            foreach (var ingredient in ingredientListDto)
            {
                Assert.Contains(ingredient.Name, allIngredients.Select(i => i.Name).ToList());
                Assert.Contains(ingredient.Description, allIngredients.Select(i => i.Description).ToList());
                Assert.Contains(ingredient.Id, allIngredients.Select(i => i.Id).ToList());
            }
        }

        private IngredientListDto IngredientListDto()
        {
            return new IngredientListDto {Name = "IngredientListDtoName", Description = "IngredientListDtoDescription"};
        }

        [Test]
        public void _GetAll_RecipesNull()
        {
            var recipes = this._cookBookRepository.GetAllRecipes();
            Assert.IsEmpty(recipes);
        }

        [Test]
        public void InsertNewRecipe_GetAllIngredients_IngredientsPresent()
        {
            var recipe = this.CreateRecipeDetail();
            this._cookBookRepository.InsertOrUpdateRecipe(recipe);
            var allIngredients = this._cookBookRepository.GetAllIngredients();
            AssertIngredients(recipe.Ingredients, allIngredients);

            Assert.AreEqual(2, allIngredients.Length);
        }

        [Test]
        public void InsertNewRecipeWithExistingIngredients_InsertOrUpdateRecipe_RecipesInserted()
        {
            var recipe = this.CreateRecipeDetail();
            this._cookBookRepository.InsertOrUpdateRecipe(recipe);
            var allIngredients = this._cookBookRepository.GetAllIngredients();
            AssertIngredients(recipe.Ingredients, allIngredients);

            var existingIngredient = allIngredients.First();
            var recipe1 = this.CreateRecipeDetail();
            recipe1.Ingredients.Clear();
            recipe1.Ingredients.Add(new IngredientDetailDto
            {
                IngredientId = existingIngredient.Id,
                Amount = 2,
                Unit = Unit.LittleSpoon
            });
            this._cookBookRepository.InsertOrUpdateRecipe(recipe1);
            this.GetAssertRecipeDetailModel(recipe1);

            Assert.AreEqual(2, allIngredients.Length);
        }

        [Test]
        public void NewIngredient_InsertOrUpdateIngredient_IngredientInserted()
        {
            var ingredient = this.IngredientListDto();
            this._cookBookRepository.InsertOrUpdateIngredient(ingredient);

            var allIngredients = this._cookBookRepository.GetAllIngredients();
            this.AssertIngredients(new[] {ingredient}, allIngredients);

            Assert.AreEqual(1, allIngredients.Length);
        }

        [Test]
        public void TwoNewIngredients_InsertOrUpdateIngredient_IngredientsInserted()
        {
            var ingredient = this.IngredientListDto();
            this._cookBookRepository.InsertOrUpdateIngredient(ingredient);

            var allIngredients = this._cookBookRepository.GetAllIngredients();
            this.AssertIngredients(new[] { ingredient }, allIngredients);

            ingredient = this.IngredientListDto();
            this._cookBookRepository.InsertOrUpdateIngredient(ingredient);

            allIngredients = this._cookBookRepository.GetAllIngredients();
            this.AssertIngredients(new[] { ingredient }, allIngredients);

            Assert.AreEqual(2, allIngredients.Length);
        }

        [Test]
        public void ModifyIngredient_InsertOrUpdateIngredient_IngredientModified()
        {
            this._cookBookRepository.ClearDatabase();

            var ingredient = this.IngredientListDto();
            this._cookBookRepository.InsertOrUpdateIngredient(ingredient);

            var allIngredients = this._cookBookRepository.GetAllIngredients();
            this.AssertIngredients(new[] { ingredient }, allIngredients);

            ingredient.Name = "UpdatedName";
            this._cookBookRepository.InsertOrUpdateIngredient(ingredient);

            allIngredients = this._cookBookRepository.GetAllIngredients();
            this.AssertIngredients(new[] { ingredient }, allIngredients);

            Assert.AreEqual(1,allIngredients.Length);
        }

        [Test]
        public void NewRecipe_InsertOrUpdateRecipe_RecipeInserted()
        {
            var recipe = this.CreateRecipeDetail();
            this._cookBookRepository.InsertOrUpdateRecipe(recipe);

            this.GetAssertRecipeDetailModel(recipe);
        }
        

        [Test]
        public void TwoNewRecipes_InsertOrUpdateRecipe_RecipesInserted()
        {
            var recipe1 = this.CreateRecipeDetail();
            this._cookBookRepository.InsertOrUpdateRecipe(recipe1);
            var recipe2 = this.CreateRecipeDetail();
            this._cookBookRepository.InsertOrUpdateRecipe(recipe2);

            this.GetAssertRecipeDetailModel(recipe1);
            this.GetAssertRecipeDetailModel(recipe2);
            
        }
    }
}