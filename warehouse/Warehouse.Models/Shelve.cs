namespace Warehouse.Models
{
    using System.Collections;
    using System.Collections.Generic;
    public class Shelve : ModelBase
    {
        public virtual Rack Rack { get; set; }
        public virtual IList<StoredProduct> StoredProducts { get; protected set; } = new List<StoredProduct>();
    }
}