namespace Warehouse.Models
{
    using System.Collections.Generic;
    public class Building : ModelBase
    {
        public virtual IList<Aisle> Ailses { get; protected set; } = new List<Aisle>();
    }
}