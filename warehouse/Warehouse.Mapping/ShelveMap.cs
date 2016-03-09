namespace Warehouse.Mapping
{
    using Models;

    public class ShelveMap : ModelBaseMap<Shelve>
    {
        public ShelveMap()
        {
            References(x => x.Rack);
            HasMany(x => x.StoredProducts);
        }
    }
}