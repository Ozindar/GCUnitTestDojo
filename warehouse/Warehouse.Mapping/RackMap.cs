namespace Warehouse.Mapping
{
    using Models;

    public class RackMap : ModelBaseMap<Rack>
    {
        public RackMap()
        {
            References(x => x.Aisle);
            HasMany(x => x.Shelves);
        }
    }
}
