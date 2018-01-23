using System.IO;
using System.Reflection;
using AutoMapper;
using Castle.Facilities.Logging;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Services.Logging.Log4netIntegration;
using Castle.Windsor;
using CookBook.App.Infrastructure.Interfaces;
using CookBook.App.Views;
using CookBook.BL;
using CookBook.Common.Interfaces;
using Prism.Modularity;
using IConfigurationStore = Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore;

namespace CookBook.App.Infrastructure
{
    public class IocInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ShellWindow>());
            container.AddFacility<LoggingFacility>(f => f.LogUsing<Log4netFactory>());
            container.AddFacility<TypedFactoryFacility>();

            var currentDirectoryAssemblyFilter = new AssemblyFilter(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CookBook.*");

            container.Register(
                Classes.FromAssemblyInDirectory(currentDirectoryAssemblyFilter)
                    .BasedOn<IModule>()
                    .LifestyleSingleton());

            container.Register(
                Classes.FromAssemblyInDirectory(currentDirectoryAssemblyFilter)
                    .BasedOn<IControllerBase>()
                    .LifestyleSingleton());

            container.Register(
                Classes.FromAssemblyInDirectory(currentDirectoryAssemblyFilter)
                    .Where(p => p.Name.EndsWith("ViewModel"))
                    .LifestyleTransient());

            container.Register(
                Classes.FromAssemblyInDirectory(currentDirectoryAssemblyFilter)
                    .Where(p => p.Name.EndsWith("View"))
                    .LifestyleTransient());

            container.Register(Component.For<ICookBookRepository, CookBookRepository>());

            container.Register(Component.For<IMapper>().UsingFactoryMethod(x =>
            {
                return new MapperConfiguration(c =>
                {
                    c.AddProfile<CookBookMappingProfile>();
                }).CreateMapper();
            }));
            
        }
        
    }
}