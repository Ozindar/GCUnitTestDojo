namespace Warehouse.ConsoleRunner.Screens
{
    using System;
    using Humanizer;
    using Models;
    using NHibernate;
    using NHibernate.Util;

    public abstract class CrudScreen<T> : ScreenBase where T : ModelBase
    {
        public CrudScreen(ISessionFactory sessionFactory, ScreenHandler screenHandler) : base(sessionFactory, screenHandler)
        {
        }

        public override string Name => typeof(T).Name.Pluralize();

        public void ShowAll()
        {
            var items = Session.QueryOver<T>().List();

            if (!items.Any())
            {
                Console.WriteLine($"No {typeof(T).Name.Pluralize()}");
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public override void ScreenShow()
        {
            Console.WriteLine($"**** {typeof(T).Name.Pluralize()} ****");
            ShowAll();
        }

        public override void ScreenOptions()
        {
            Console.WriteLine($"A) Add {typeof(T).Name}");
        }

        public override bool HandleKey(char key)
        {
            switch (key)
            {
                case 'a':
                    Add();
                    break;
                default:
                    return false;
            }
            return true;
        }

        protected virtual void Add()
        {
            Console.Clear();
            Console.WriteLine("Type name:");
            var name = Console.ReadLine();

            var p = new Product {Name = name};
            Session.SaveOrUpdate(p);

            Console.WriteLine($"{name} saved.");
            Console.ReadLine();
            Show();
        }
    }
}