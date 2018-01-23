using CookBook.App.Infrastructure.Bases;
using CookBook.App.Views;
using Prism.Logging;
using Prism.Modularity;

namespace CookBook.App
{
    public class ShellModule : ModuleBase, IModule
    {

        public void Initialize()
        {
            this.RegionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof(HeaderView));
            this.Logger.Log($"{nameof(ShellModule)} was Initialized.", Category.Debug, Priority.None);
        }
    }
}