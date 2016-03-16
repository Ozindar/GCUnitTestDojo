namespace Warehouse.Mapping
{
    using Models;

    public class BuildingMap : ModelBaseMap<Building>
    {
        public BuildingMap()
        {
            HasMany(x => x.Ailses);
            HasOne(x => x.Airco);
        }
    }
}