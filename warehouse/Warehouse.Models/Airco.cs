using Warehouse.Models.Enums;

namespace Warehouse.Models
{
    public class Airco : ModelBase
    {
        public virtual AircoStatus AircoStatus { get; protected set; }

        public virtual void SetAircoStatus(AircoStatus status)
        {
            AircoStatus = status;

        }
    }
}
