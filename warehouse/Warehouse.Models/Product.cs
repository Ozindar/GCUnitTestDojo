namespace Warehouse.Models
{
    using Enums;

    public class Product : ModelBase
    {
        public virtual string Name { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
