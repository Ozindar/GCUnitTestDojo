namespace Warehouse.ConsoleRunner
{
    using System;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using Mapping;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;

    public static class SessionFactoryConfigurator
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("Warehouse.db"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProductMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<BuildingMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<AircoMap>())
                .ExposeConfiguration(UpdateSchema)
                .BuildSessionFactory();
        }

        private static void UpdateSchema(Configuration config)
        {
            var schemaUpdate = new SchemaUpdate(config);
            schemaUpdate.Execute(false, true);
        }

    }
}