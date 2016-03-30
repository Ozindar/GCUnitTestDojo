namespace Warehouse.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Shelf : ModelBase
    {
        public virtual decimal MaxWeight { get; set; }
        public virtual Rack Rack { get; set; }
        public virtual IList<StoredProduct> StoredProducts { get; protected set; } = new List<StoredProduct>();

        public override string ToString()
        {
            return $"{Name} - {Rack?.Name ?? "No Rack"} - {Rack?.Aisle?.Name ?? "No Aisle"} - {Rack?.Aisle?.Building?.Name ?? "No Building"}";
            //return $"{Name} - ({string.Join(", ", StoredProducts.Select(sp => $"{sp.Amount}x{sp.Product.Name}"))})";
        }




    }
}
