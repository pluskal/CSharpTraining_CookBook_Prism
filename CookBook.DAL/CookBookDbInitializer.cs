using System;
using System.Collections.Generic;
using System.Data.Entity;
using CookBook.Common;
using CookBook.Common.Enums;
using CookBook.DAL.Entities;

namespace CookBook.DAL
{
    internal class CookBookDbInitializer : DropCreateDatabaseAlways<CookBookDbContext>
    {
        internal static RecipeEntity Recipe2 { get; } = new RecipeEntity
        {
            Name = "RecipeName1",
            Description = "RecipeDescription1",
            Duration = new TimeSpan(0, 0, 1),
            Type = FoodType.MainCourse,
            Ingredients = new List<IngredientAmountEntity>
            {
                new IngredientAmountEntity
                {
                    Amount = 2,
                    Unit = Unit.Kg,
                    Ingredient = new IngredientEntity
                    {
                        Name = "Ingredient1Name1",
                        Description = "Ingredient1Description1"
                    }
                },
                new IngredientAmountEntity
                {
                    Amount = 3,
                    Unit = Unit.L,
                    Ingredient = new IngredientEntity
                    {
                        Name = "Ingredient2Name1",
                        Description = "Ingredient2Description1"
                    }
                }
            }
        };

        internal static RecipeEntity Recipe1 { get; } = new RecipeEntity
        {
            Name = "RecipeName",
            Description = "RecipeDescription",
            Duration = new TimeSpan(0, 0, 1),
            Type = FoodType.MainCourse,
            Ingredients = new List<IngredientAmountEntity>
            {
                new IngredientAmountEntity
                {
                    Amount = 2,
                    Unit = Unit.Kg,
                    Ingredient = new IngredientEntity
                    {
                        Name = "Ingredient1Name",
                        Description = "Ingredient1Description"
                    }
                },
                new IngredientAmountEntity
                {
                    Amount = 3,
                    Unit = Unit.L,
                    Ingredient = new IngredientEntity
                    {
                        Name = "Ingredient2Name",
                        Description = "Ingredient2Description"
                    }
                }
            }
        };

        protected override void Seed(CookBookDbContext context)
        {
            context.Recipes.Add(Recipe1);
            context.Recipes.Add(Recipe2);
            base.Seed(context);
        }
    }
}