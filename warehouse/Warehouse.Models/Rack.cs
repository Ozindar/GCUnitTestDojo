namespace Warehouse.Models
{
    using System.Collections.Generic;

    public class Rack : ModelBase
    {
        public virtual Aisle Aisle { get; set; }

        public virtual IList<Shelve> Shelves { get; protected set; } = new List<Shelve>();
    }
}