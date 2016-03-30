namespace Warehouse.Models
{
    using System.Collections.Generic;

    public class Rack : ModelBase
    {

        public virtual decimal MaxWeight { get; set; }
        public virtual Aisle Aisle { get; set; }

        public virtual IList<Shelf> Shelves { get; protected set; } = new List<Shelf>();

        public override string ToString()
        {
            return $"{Name} - {Aisle?.Name ?? "No Aisle"} - {Aisle?.Building?.Name ?? "No Building"}";
        }
    }
}