using CookBook.Common;

namespace CookBook.DAL.Entities
{
    public class IngredientAmountEntity : EntityBase
    {
        public double Amount { get; set; }
        public Unit Unit { get; set; }
        public IngredientEntity Ingredient { get; set; }
        public RecipeEntity Recipe { get; set; }
    }
}