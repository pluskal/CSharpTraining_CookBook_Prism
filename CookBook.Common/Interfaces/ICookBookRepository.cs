using System;
using CookBook.Common.Models;

namespace CookBook.Common.Interfaces
{
    public interface ICookBookRepository
    {
        RecipeListDto[] GetAllRecipes();
        RecipeDetailDto GetRecipeById(Guid id);

        /// <summary>
        /// https://rogerjohansson.blog/2013/12/01/why-mapping-dtos-to-entities-using-automapper-and-entityframework-is-horrible/
        /// </summary>
        /// <param name="recipeDetail"></param>
        void InsertOrUpdateRecipe(RecipeDetailDto recipeDetail);

        void ClearDatabase();
        IngredientListDto[] GetAllIngredients();
        void InsertOrUpdateIngredient(IngredientListDto ingredient);
    }
}