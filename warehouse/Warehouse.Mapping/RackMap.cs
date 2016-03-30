namespace Warehouse.Mapping
{
    using Models;

    public class RackMap : ModelBaseMap<Rack>
    {
        public RackMap()
        {
            Map(x => x.MaxWeight);
            References(x => x.Aisle);
            HasMany(x => x.Shelves).Cascade.All();
        }
    }
}
