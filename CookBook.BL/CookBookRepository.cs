using System;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;
using CookBook.BL.Models;
using CookBook.DAL;
using CookBook.DAL.Entities;

namespace CookBook.BL
{
    public class CookBookRepository
    {
        private readonly IMapper _mapper;

        public CookBookRepository(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public RecipeListDto[] GetAllRecipes()
        {
            using (var dbx = new CookBookDbContext())
            {
                return dbx.Recipes.Select(this._mapper.Map<RecipeListDto>).ToArray();
            }
        }

        public RecipeDetailDto GetRecipeById(Guid id)
        {
            using (var dbx = new CookBookDbContext())
            {
                return this._mapper.Map<RecipeDetailDto>(dbx.Recipes.Include(
                    r => r.Ingredients.Select(i => i.Ingredient)
                ).FirstOrDefault(r => r.Id == id));
            }
        }

        /// <summary>
        /// https://rogerjohansson.blog/2013/12/01/why-mapping-dtos-to-entities-using-automapper-and-entityframework-is-horrible/
        /// </summary>
        /// <param name="recipeDetail"></param>
        public void InsertOrUpdateRecipe(RecipeDetailDto recipeDetail)
        {
            var config = new MapperConfigurationExpression();
            using (var dbx = new CookBookDbContext())
            {
                config.CreateMap<RecipeDetailDto, RecipeEntity>()
                    .ConstructUsing((RecipeDetailDto recipeDetailDto) =>
                    {
                        if (recipeDetailDto.Id == Guid.Empty)
                        {
                            var recipeEntity = new RecipeEntity();
                            recipeDetailDto.Id = recipeEntity.Id;
                            dbx.Recipes.Add(recipeEntity);
                            return recipeEntity;
                        }
                        return dbx.Recipes.Include(recipeEntity =>recipeEntity.Ingredients.Select(i=>i.Ingredient)).First(recipeEntity => recipeEntity.Id == recipeDetailDto.Id);
                    })
                    //find out what details no longer exist in the DTO and delete the corresponding entities 
                    //from the dbcontext
                    .BeforeMap((dto, o) =>
                    {
                        o
                            .Ingredients
                            .Where(d => dto.Ingredients.All(ddto => ddto.Id != d.Id)).ToList()
                            .ForEach(deleted => dbx.Set<IngredientAmountEntity>().Remove(deleted));
                    });

                config.CreateMap<IngredientDetailDto, IngredientAmountEntity>()
                    .ConstructUsing((IngredientDetailDto ingredientDetailDto) =>
                    {
                        IngredientEntity ingredientEntity;
                        if (ingredientDetailDto.Id == Guid.Empty)
                        {
                            ingredientEntity = new IngredientEntity();
                            ingredientDetailDto.IngredientId = ingredientEntity.Id;
                            dbx.Set<IngredientEntity>().Add(ingredientEntity);
                           
                        }
                        else
                        {
                            ingredientEntity = dbx.Set<IngredientEntity>().First(d => d.Id == ingredientDetailDto.IngredientId);
                        }
                        

                        IngredientAmountEntity ingredientAmountEntity;
                        if (ingredientDetailDto.Id == Guid.Empty)
                        {
                            ingredientAmountEntity = new IngredientAmountEntity {Ingredient = ingredientEntity};
                            ingredientDetailDto.Id = ingredientAmountEntity.Id;
                            dbx.Set<IngredientAmountEntity>().Add(ingredientAmountEntity);
                        }
                        else
                        {
                            ingredientAmountEntity = dbx.Set<IngredientAmountEntity>().First(d => d.Id == ingredientDetailDto.Id);
                            ingredientAmountEntity.Ingredient = ingredientEntity;
                        }
                       
                        return ingredientAmountEntity;
                    })
                    .ForPath(entity => entity.Ingredient.Name,opt=> opt.MapFrom(dto => dto.Name))
                    .ForPath(entity => entity.Ingredient.Description,opt=> opt.MapFrom(dto => dto.Description))
                    ;

                //create an instanced mapper instead of the static API
                var mapper = new Mapper(new MapperConfiguration(config)) as IMapper;
                mapper.Map<RecipeDetailDto, RecipeEntity>(recipeDetail);

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

        public IngredientListDto[] GetAllIngredients()
        {
            using (var dbx = new CookBookDbContext())
            {
                return dbx.Ingredients.Select(this._mapper.Map<IngredientListDto>).ToArray();
            }
        }

        public void InsertOrUpdateIngredient(IngredientListDto ingredient)
        {
            var config = new MapperConfigurationExpression();
                using (var dbx = new CookBookDbContext())
                {
                    config.CreateMap<IngredientListDto, IngredientEntity>()
                        .ConstructUsing((IngredientListDto ingredientListDto) =>
                        {
                            if (ingredientListDto.Id == Guid.Empty)
                            {
                                var ingredientEntity = new IngredientEntity();
                                ingredientListDto.Id = ingredientEntity.Id;
                                dbx.Ingredients.Add(ingredientEntity);
                                return ingredientEntity;
                            }
                            return dbx.Ingredients.First(
                                ingredientEntity => ingredientEntity.Id == ingredientListDto.Id);
                        });

                    //create an instanced mapper instead of the static API
                    var mapper = new Mapper(new MapperConfiguration(config)) as IMapper;
                    mapper.Map<IngredientListDto, IngredientEntity>(ingredient);

                    dbx.SaveChanges();
            }
        }
    }
}