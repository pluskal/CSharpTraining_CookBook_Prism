using System.Data.Entity;
using CookBook.DAL.Entities;

namespace CookBook.DAL
{
    public class CookBookDbContext : DbContext
    {
        public CookBookDbContext() : base("CookBookContext")
        {
            Database.SetInitializer(new CookBookDbInitializer());
        }

        public IDbSet<RecipeEntity> Recipes { get; set; }
        public IDbSet<IngredientEntity> Ingredients { get; set; }

        public void TruncateTables()
        {
            this.Set<IngredientAmountEntity>().Clear(this);
            this.Recipes.Clear(this);
            this.Ingredients.Clear(this);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RecipeEntity>().ToTable("Recipes");
            modelBuilder.Entity<IngredientEntity>().ToTable("Ingredients");
            modelBuilder.Entity<IngredientAmountEntity>().ToTable("IngredientAmounts");

            modelBuilder.Entity<RecipeEntity>()
                .HasMany(c => c.Ingredients)
                .WithRequired(x => x.Recipe)
                .WillCascadeOnDelete(true);
        }
    }
}