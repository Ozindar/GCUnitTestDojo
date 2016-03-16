namespace Warehouse.Mapping
{
    using Models;

    public class StoredProductMap : ModelBaseMap<StoredProduct>
    {
        public StoredProductMap()
        {
            Map(x => x.Amount);
            References(x => x.Product);
            References(x => x.Shelf);
        }
    }
}