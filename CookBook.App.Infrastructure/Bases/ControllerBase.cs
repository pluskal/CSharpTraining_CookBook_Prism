using CookBook.App.Infrastructure.Interfaces;
using Prism.Events;
using Prism.Logging;
using Prism.Regions;

namespace CookBook.App.Infrastructure.Bases
{
    public class ControllerBase : IControllerBase
    {
        public IRegionManager RegionManager { get; set; }
        public ILoggerFacade Logger { get; set; }
        public IEventAggregator EventAggregator { get; set; }
    }
}