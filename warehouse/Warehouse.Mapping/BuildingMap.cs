namespace Warehouse.Mapping
{
    using Models;

    public class BuildingMap : ModelBaseMap<Building>
    {
        public BuildingMap()
        {
            HasMany(x => x.Ailses).Cascade.All();
            HasOne(x => x.Airco).Cascade.All();
        }
    }
}