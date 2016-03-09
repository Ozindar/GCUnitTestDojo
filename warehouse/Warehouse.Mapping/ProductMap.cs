namespace Warehouse.Mapping
{
    using Models;
    using Models.Enums;

    public class ProductMap : ModelBaseMap<Product>
    {
        public ProductMap()
        {
            Map(x => x.Name);
            Map(x => x.ProductType).CustomType<ProductType>();
        }
    }
}
