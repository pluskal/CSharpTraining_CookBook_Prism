using System;
using System.Linq;
using CookBook.BL.Models;
using CookBook.DAL.Entities;

namespace CookBook.BL
{
    public class RecipeMapper
    {
        public RecipeListModel Map(RecipeEntity recipeEntity)
        {
            return new RecipeListModel
            {
                Id = recipeEntity.Id,
                Name = recipeEntity.Name,
                Type = recipeEntity.Type,
                Duration = recipeEntity.Duration
            };
        }

        public RecipeDetailModel MapDetailModel(RecipeEntity recipeEntity)
        {
            return new RecipeDetailModel
            {
                Id = recipeEntity.Id,
                Name = recipeEntity.Name,
                Description = recipeEntity.Description,
                Type = recipeEntity.Type,
                Duration = recipeEntity.Duration,
                Ingredients = recipeEntity.Ingredients.Select(ia => new IngredienceModel
                {
                    Id = ia.Id,
                    Name = ia.Ingredient.Name,
                    Description = ia.Ingredient.Description,
                    Amount = ia.Amount,
                    Unit = ia.Unit
                }).ToList()
            };
        }

        internal RecipeEntity Map(RecipeDetailModel recipeDetailModel)
        {
            var recipeEntity = new RecipeEntity
            {
                Name = recipeDetailModel.Name,
                Description = recipeDetailModel.Description,
                Type =  recipeDetailModel.Type,
                Duration = recipeDetailModel.Duration
            };

            if (recipeDetailModel.Id == Guid.Empty)
                recipeDetailModel.Id = recipeEntity.Id;
            else
                recipeEntity.Id = recipeDetailModel.Id;

            foreach (var ingredientModel in recipeDetailModel.Ingredients)
            {
                var ingredientAmount = new IngredientAmountEntity
                {
                    Amount = ingredientModel.Amount,
                    Unit =  ingredientModel.Unit
                };

                var ingredient = new IngredientEntity
                {
                    Name = ingredientModel.Name,
                    Description = ingredientModel.Description
                };
                if (ingredientModel.Id == Guid.Empty)
                    ingredientModel.Id = ingredient.Id;
                else
                    ingredient.Id = ingredientModel.Id;

                ingredientAmount.Ingredient = ingredient;
                recipeEntity.Ingredients.Add(ingredientAmount);
            }
            return recipeEntity;
        }


        public IngredienceDetailModel Map(IngredientEntity recipeEntity)
        {
            return new IngredienceDetailModel
            {
                Id = recipeEntity.Id,
                Name = recipeEntity.Name,
                Description = recipeEntity.Description
            };
        }

        public IngredientEntity Map(IngredienceDetailModel recipeDetailModel)
        {
            var ingredientEntity = new IngredientEntity
            {
                Name = recipeDetailModel.Name,
                Description = recipeDetailModel.Description
            };

            if (recipeDetailModel.Id == Guid.Empty)
                ingredientEntity.Id = recipeDetailModel.Id;
            else
                recipeDetailModel.Id = ingredientEntity.Id;
            return ingredientEntity;
        }
    }
}