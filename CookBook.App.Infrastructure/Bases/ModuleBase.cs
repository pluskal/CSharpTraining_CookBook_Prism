using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Logging;
using Prism.Regions;

namespace CookBook.App.Infrastructure.Bases
{
    public class ModuleBase
    {
        public IRegionManager RegionManager { get; set; }
        public ILoggerFacade Logger { get; set; }
    }
}
