using System;
using System.Linq;
using CookBook.BL.Models;
using CookBook.DAL.Entities;

namespace CookBook.BL
{
    public class RecipeMapper
    {
        public RecipeListDto Map(RecipeEntity recipeEntity)
        {
            return new RecipeListDto
            {
                Id = recipeEntity.Id,
                Name = recipeEntity.Name,
                Type = recipeEntity.Type,
                Duration = recipeEntity.Duration
            };
        }

        public RecipeDetailDto MapDetailModel(RecipeEntity recipeEntity)
        {
            return new RecipeDetailDto
            {
                Id = recipeEntity.Id,
                Name = recipeEntity.Name,
                Description = recipeEntity.Description,
                Type = recipeEntity.Type,
                Duration = recipeEntity.Duration,
                Ingredients = recipeEntity.Ingredients.Select(ia => new IngredientDto
                {
                    Id = ia.Id,
                    Name = ia.Ingredient.Name,
                    Description = ia.Ingredient.Description,
                    Amount = ia.Amount,
                    Unit = ia.Unit
                }).ToList()
            };
        }

        internal RecipeEntity Map(RecipeDetailDto recipeDetailDto)
        {
            var recipeEntity = new RecipeEntity
            {
                Name = recipeDetailDto.Name,
                Description = recipeDetailDto.Description,
                Type =  recipeDetailDto.Type,
                Duration = recipeDetailDto.Duration
            };

            if (recipeDetailDto.Id == Guid.Empty)
                recipeDetailDto.Id = recipeEntity.Id;
            else
                recipeEntity.Id = recipeDetailDto.Id;

            foreach (var ingredientModel in recipeDetailDto.Ingredients)
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


        public IngredientDetailDto Map(IngredientEntity recipeEntity)
        {
            return new IngredientDetailDto
            {
                Id = recipeEntity.Id,
                Name = recipeEntity.Name,
                Description = recipeEntity.Description
            };
        }

        public IngredientEntity Map(IngredientDetailDto recipeDetailDto)
        {
            var ingredientEntity = new IngredientEntity
            {
                Name = recipeDetailDto.Name,
                Description = recipeDetailDto.Description
            };

            if (recipeDetailDto.Id == Guid.Empty)
                ingredientEntity.Id = recipeDetailDto.Id;
            else
                recipeDetailDto.Id = ingredientEntity.Id;
            return ingredientEntity;
        }
    }
}