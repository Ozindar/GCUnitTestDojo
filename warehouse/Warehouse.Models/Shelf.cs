namespace Warehouse.Models
{
    using System.Collections.Generic;

    public class Shelf : ModelBase
    {
        public virtual decimal MaxWeight { get; set; }
        public virtual Rack Rack { get; set; }
        public virtual IList<StoredProduct> StoredProducts { get; protected set; } = new List<StoredProduct>();
    }
}
