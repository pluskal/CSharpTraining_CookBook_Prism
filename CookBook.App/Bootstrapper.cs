using System.Windows;
using Castle.Windsor.Installer;
using CookBook.App.Recipes;
using CookBook.App.Views;
using Prism.Logging;
using Prism.Modularity;
using Prism.Mvvm;
using PrismContrib.WindsorExtensions;

namespace CookBook.App
{
    public class Bootstrapper : WindsorBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            this.Container.Install(FromAssembly.InThisApplication());
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new TextLogger();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            // TODO moduleCatalog.AddModule(typeof(<moduleName>));
            moduleCatalog.AddModule(typeof(ShellModule));
            moduleCatalog.AddModule(typeof(RecipesModule));
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewModelFactory(type => this.Container.Resolve(type));
        }

        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<ShellWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        public ILoggerFacade BoostrapperLogger => this.Logger;
    }
}