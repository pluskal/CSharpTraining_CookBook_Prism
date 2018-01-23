using System;
using System.Threading.Tasks;
using CookBook.Common.Models;

namespace CookBook.Common.Interfaces
{
    public interface ICookBookRepository
    {
        RecipeListDto[] GetAllRecipes();
        Task<RecipeListDto[]> GetAllRecipesAsync();
        RecipeDetailDto GetRecipeById(Guid id);
        Task<RecipeDetailDto> GetRecipeByIdAsync(Guid id);

        /// <summary>
        /// https://rogerjohansson.blog/2013/12/01/why-mapping-dtos-to-entities-using-automapper-and-entityframework-is-horrible/
        /// </summary>
        /// <param name="recipeDetail"></param>
        void InsertOrUpdateRecipe(RecipeDetailDto recipeDetail);

        Task InsertOrUpdateRecipeAsync(RecipeDetailDto recipeDetail);
        void ClearDatabase();
        IngredientListDto[] GetAllIngredients();
        Task<IngredientListDto[]> GetAllIngredientsAsync();
        void InsertOrUpdateIngredient(IngredientListDto ingredient);
        Task InsertOrUpdateIngredientAsync(IngredientListDto ingredient);
    }
}