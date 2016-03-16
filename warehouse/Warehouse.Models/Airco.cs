using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Models.Enums;

namespace Warehouse.Models
{
    public class Airco : ModelBase
    {
        public virtual AircoStatus AircoStatus { get; protected set; }

        protected virtual void SetAircoStatus(AircoStatus status)
        {
            AircoStatus = status;
        }
    }
}
