namespace Warehouse.Mapping
{
    using Models;

    public class AisleMap : ModelBaseMap<Aisle>
    {
        public AisleMap()
        {
            References(x => x.Building);
            HasMany(x => x.Racks).Cascade.All();
        }
    }
}