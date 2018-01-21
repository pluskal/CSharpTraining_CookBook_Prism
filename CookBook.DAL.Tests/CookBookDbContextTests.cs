using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.DAL.Entities;
using Xunit;

namespace CookBook.DAL.Tests
{
    public class CookBookDbContextTests : IDisposable
    {
        public CookBookDbContextTests()
        {
            this.DbContext = new CookBookDbContext();
        }

        public void Dispose()
        {
            this.DbContext?.Dispose();
        }

        public CookBookDbContext DbContext { get; set; }

        private RecipeEntity GetRecipeEntity(IEnumerable<RecipeEntity> recipeEntities, RecipeEntity recipeEntity)
        {
            return recipeEntities.FirstOrDefault(i => i.Name == recipeEntity.Name &&
                                                      i.Description == recipeEntity.Description &&
                                                      i.Duration == recipeEntity.Duration &&
                                                      i.Type == recipeEntity.Type);
        }

        private IngredientEntity GetIngredientEntity(IEnumerable<IngredientEntity> ingredientEntities,
            IngredientEntity ingredientEntity)
        {
            return ingredientEntities.FirstOrDefault(i =>
                i.Name == ingredientEntity.Name && i.Description == ingredientEntity.Description);
        }

        [Fact]
        public void _AddIngredient_Ingredient()
        {
            const string ingredientName = "Ingredient name";
            const string ingredientDescription = "Ingredient description";

            var ingredient = new IngredientEntity
            {
                Name = ingredientName,
                Description = ingredientDescription
            };

            this.DbContext.Ingredients.Add(ingredient);
            this.DbContext.SaveChanges();

            var ingredientEntity = this.DbContext.Ingredients.FirstOrDefault(i =>
                i.Name == ingredientName && i.Description == ingredientDescription);

            Assert.NotNull(ingredientEntity);
            Assert.Equal(ingredient.Name, ingredientEntity.Name);
            Assert.Equal(ingredient.Description, ingredientEntity.Description);
        }

        [Fact]
        public void _GetAllIngredients_Any()
        {
            var ingredientEntities = this.DbContext.Ingredients.ToArray();
            var ingredients = CookBookDbInitializer.Recipe1.Ingredients.Select(i => i.Ingredient)
                .Concat(CookBookDbInitializer.Recipe2.Ingredients.Select(i => i.Ingredient));

            Assert.NotEmpty(ingredientEntities);

            foreach (var ingredientEntity in ingredients)
                Assert.NotNull(this.GetIngredientEntity(ingredientEntities, ingredientEntity));
        }

        [Fact]
        public void _GetAllRecipes_Recipe1AndRecipe2Present()
        {
            var recipeEntities = this.DbContext.Recipes.ToArray();
            Assert.NotEmpty(recipeEntities);

            Assert.NotNull(this.GetRecipeEntity(recipeEntities, CookBookDbInitializer.Recipe1));
            Assert.NotNull(this.GetRecipeEntity(recipeEntities, CookBookDbInitializer.Recipe2));
        }

        [Fact]
        public void _TruncateTables_TablesAreEmpty()
        {
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                Assert.NotEmpty(this.DbContext.Recipes);
                Assert.NotEmpty(this.DbContext.Ingredients);

                this.DbContext.TruncateTables();

                Assert.Empty(this.DbContext.Recipes);
                Assert.Empty(this.DbContext.Ingredients);

                transaction.Rollback();
            }
        }
    }
}