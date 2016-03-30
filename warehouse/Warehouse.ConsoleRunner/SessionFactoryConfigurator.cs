namespace Warehouse.ConsoleRunner
{
    using System;
    using System.IO;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using Mapping;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Context;
    using NHibernate.Tool.hbm2ddl;

    public static class SessionFactoryConfigurator
    {
        public static bool Seed = false;
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("Warehouse.db"))
                .CurrentSessionContext<ThreadLocalSessionContext>()
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProductMap>())
                .ExposeConfiguration(UpdateSchema)
                .BuildSessionFactory();
        }

        private static void UpdateSchema(Configuration config)
        {
            Seed = !File.Exists("Warehouse.db");

            var schemaUpdate = new SchemaUpdate(config);
            schemaUpdate.Execute(false, true);
        }

    }
}