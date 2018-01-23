using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.App.Infrastructure.Bases;
using CookBook.App.Recipes.Views;
using Prism.Logging;
using Prism.Modularity;

namespace CookBook.App.Recipes
{
    public class RecipesModule : ModuleBase, IModule
    {
        public void Initialize()
        {
            this.RegionManager.RegisterViewWithRegion(RegionNames.BodyRegion, typeof(RecipeListView));
            this.RegionManager.RegisterViewWithRegion(RegionNames.BodyRegion, typeof(RecipeDetailView));
            this.Logger.Log($"{nameof(RecipesModule)} was Initialized.", Category.Debug, Priority.None);
        }
    }
}
