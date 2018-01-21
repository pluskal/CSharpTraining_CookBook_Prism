using System;
using System.Data.Entity;
using System.Linq;
using CookBook.BL.Models;
using CookBook.DAL;

namespace CookBook.BL
{
    public class RecipeRepository
    {
        private readonly RecipeMapper _mapper;

        public RecipeRepository(RecipeMapper mapper)
        {
            this._mapper = mapper;
        }

        public RecipeListDto[] GetAll()
        {
            using (var dbx = new CookBookDbContext())
            {
                return dbx.Recipes.Select(this._mapper.Map).ToArray();
            }
        }

        public RecipeDetailDto GetById(Guid id)
        {
            using (var dbx = new CookBookDbContext())
            {
                return this._mapper.MapDetailModel(
                    dbx.Recipes.Include(
                        r => r.Ingredients.Select(i => i.Ingredient)
                    ).FirstOrDefault(r => r.Id == id));
            }
        }

        public void InsertRecipe(RecipeDetailDto recipeDetailDto)
        {
            using (var dbx = new CookBookDbContext())
            {
                dbx.Recipes.Add(this._mapper.Map(recipeDetailDto));
                dbx.SaveChanges();
            }
        }

        public void ClearDatabase()
        {
            using (var dbx = new CookBookDbContext())
            {
                dbx.TruncateTables();
            }
        }

        public IngredientDetailDto[] GetAllIngredients()
        {
            using (var dbx = new CookBookDbContext())
            {
                return dbx.Ingredients.Select(this._mapper.Map).ToArray();
            }
        }

        public void InsertOrUpdateRecipe(RecipeDetailDto detail)
        {
            using (var dbx = new CookBookDbContext())
            {
                var state = detail.Id == Guid.Empty ? EntityState.Added : EntityState.Modified;
                dbx.Entry(this._mapper.Map(detail)).State = state;
                dbx.SaveChanges();
            }
        }
    }
}