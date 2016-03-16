namespace Warehouse.Mapping
{
    using Models;

    public class ShelfMap : ModelBaseMap<Shelf>
    {
        public ShelfMap()
        {
            Map(x => x.MaxWeight);
            References(x => x.Rack);
            HasMany(x => x.StoredProducts);
        }
    }
}