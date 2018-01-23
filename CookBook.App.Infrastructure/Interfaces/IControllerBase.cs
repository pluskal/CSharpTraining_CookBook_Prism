using Prism.Events;
using Prism.Logging;
using Prism.Regions;

namespace CookBook.App.Infrastructure.Interfaces
{
    public interface IControllerBase
    {
        IRegionManager RegionManager { get; set; }
        ILoggerFacade Logger { get; set; }
        IEventAggregator EventAggregator { get; set; }
    }
}