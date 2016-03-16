using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Mapping
{
    using Models;
    using Models.Enums;

    public class AircoMap : ModelBaseMap<Airco>
    {
        public AircoMap()
        {
            Map(x => x.AircoStatus).CustomType<AircoStatus>();
        }
    }
}
