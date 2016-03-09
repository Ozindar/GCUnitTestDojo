namespace Warehouse.Mapping
{
    using FluentNHibernate.Mapping;
    using Models;

    public abstract class ModelBaseMap<T> : ClassMap<T> where T : ModelBase
    {
        protected ModelBaseMap()
        {
            Id(x => x.Id);
            Map(x => x.Guid);
            Map(x => x.Name);
        }
    }
}