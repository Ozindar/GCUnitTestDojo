namespace Warehouse.Session
{
    using System.IO;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using Mapping;
    using Models;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Context;
    using NHibernate.Tool.hbm2ddl;

    public static class SessionFactoryConfigurator
    {
        public static bool CanSeed = false;

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
            CanSeed = !File.Exists("Warehouse.db");

            var schemaUpdate = new SchemaUpdate(config);
            schemaUpdate.Execute(false, true);
        }

        public static void SeedData(ISessionFactory sessionFactory)
        {
            if (!CanSeed)
                return;

            using (var s = sessionFactory.OpenSession())
            using (var t = s.BeginTransaction())
            {
                var noord = new Building {Name = "Noord"};

                var rij1 = new Aisle {Name = "Links"};
                var rij2 = new Aisle {Name = "Rechts"};
                var rek1_1 = new Rack {Name = "A"};
                var rek1_2 = new Rack {Name = "B"};
                var rek2_1 = new Rack {Name = "C"};
                var rek2_2 = new Rack {Name = "D"};
                var shelf1_1_1 = new Shelf {Name = "1"};
                var shelf1_1_2 = new Shelf {Name = "2"};
                var shelf1_2_1 = new Shelf {Name = "3"};
                var shelf1_2_2 = new Shelf {Name = "4"};
                var shelf2_1_1 = new Shelf {Name = "5"};
                var shelf2_1_2 = new Shelf {Name = "6"};
                var shelf2_2_1 = new Shelf {Name = "7"};
                var shelf2_2_2 = new Shelf {Name = "8"};

                noord.Ailses.Add(rij1);
                noord.Ailses.Add(rij2);
                rij1.Racks.Add(rek1_1);
                rij1.Racks.Add(rek1_2);
                rij2.Racks.Add(rek2_1);
                rij2.Racks.Add(rek2_2);
                rek1_1.Shelves.Add(shelf1_1_1);
                rek1_1.Shelves.Add(shelf1_1_2);
                rek1_2.Shelves.Add(shelf1_2_1);
                rek1_2.Shelves.Add(shelf1_2_2);
                rek2_1.Shelves.Add(shelf2_1_1);
                rek2_1.Shelves.Add(shelf2_1_2);
                rek2_2.Shelves.Add(shelf2_2_1);
                rek2_2.Shelves.Add(shelf2_2_2);

                var zuid = new Building {Name = "Zuid"};

                var rija = new Aisle {Name = "Voor"};
                var rijb = new Aisle {Name = "Achter"};
                var reka_a = new Rack {Name = "E"};
                var reka_b = new Rack {Name = "F"};
                var rekb_a = new Rack {Name = "G"};
                var rekb_b = new Rack {Name = "H"};
                var shelfa_a_a = new Shelf {Name = "9"};
                var shelfa_a_b = new Shelf {Name = "10"};
                var shelfa_b_a = new Shelf {Name = "11"};
                var shelfa_b_b = new Shelf {Name = "12"};
                var shelfb_a_a = new Shelf {Name = "13"};
                var shelfb_a_b = new Shelf {Name = "14"};
                var shelfb_b_a = new Shelf {Name = "15"};
                var shelfb_b_b = new Shelf {Name = "16"};

                zuid.Ailses.Add(rija);
                zuid.Ailses.Add(rijb);
                rija.Racks.Add(reka_a);
                rija.Racks.Add(reka_b);
                rijb.Racks.Add(rekb_a);
                rijb.Racks.Add(rekb_b);
                reka_a.Shelves.Add(shelfa_a_a);
                reka_a.Shelves.Add(shelfa_a_b);
                reka_b.Shelves.Add(shelfa_b_a);
                reka_b.Shelves.Add(shelfa_b_b);
                rekb_a.Shelves.Add(shelfb_a_a);
                rekb_a.Shelves.Add(shelfb_a_b);
                rekb_b.Shelves.Add(shelfb_b_a);
                rekb_b.Shelves.Add(shelfb_b_b);

                s.SaveOrUpdate(noord);
                s.SaveOrUpdate(zuid);

                var bananen = new Product {Name = "Bananen", Weight = 0.150m};
                bananen.StoreProduct(shelfa_a_a, 17);
                var tomaten = new Product {Name = "Tomaten", Weight = 0.160m};
                tomaten.StoreProduct(shelf1_1_1, 61);

                s.SaveOrUpdate(bananen);
                s.SaveOrUpdate(tomaten);

                t.Commit();
            }
        }
    }
}