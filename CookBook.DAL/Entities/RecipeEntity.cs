﻿using System;
using System.Collections.Generic;
using CookBook.Common;
using CookBook.Common.Enums;

namespace CookBook.DAL.Entities
{
    public class RecipeEntity : EntityBase
    {
        public string Name { get; set; }
        public FoodType Type { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public ICollection<IngredientAmountEntity> Ingredients { get; set; } = new List<IngredientAmountEntity>();
    }
}