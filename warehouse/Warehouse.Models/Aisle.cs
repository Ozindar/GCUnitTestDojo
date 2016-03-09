namespace Warehouse.Models
{
    using System.Collections.Generic;

    public class Aisle : ModelBase
    {
        public virtual Building Building { get; set; }
        public virtual IList<Rack> Racks { get; protected set; } = new List<Rack>();
    }
}