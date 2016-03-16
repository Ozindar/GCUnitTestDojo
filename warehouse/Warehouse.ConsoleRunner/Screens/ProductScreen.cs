namespace Warehouse.ConsoleRunner.Screens
{
    using System;
    using System.Collections;
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

    public class ProductScreen : CrudScreen<Product>
    {
        public ProductScreen(ISessionFactory sessionFactory, ScreenHandler screenHandler) : base(sessionFactory, screenHandler)
        {
        }

        public override void ScreenOptions()
        {
            base.ScreenOptions();
            Console.WriteLine("S) Store Product");
        }

        public override bool HandleKey(char key)
        {
            switch (key)
            {
                case 's':
                    StoreOnShelve();
                    return true;
                default:
                    return base.HandleKey(key);
            }
        }

        private void StoreOnShelve()
        {
            var prods = new Hashtable();
            Console.Clear();
            Console.WriteLine("Choose a product");

            var prodChooser = 0;
            foreach (var product in Session.QueryOver<Product>().OrderBy(p => p.Name).Asc.List())
            {
                prods.Add(++prodChooser, product);
                Console.WriteLine($"{prodChooser,3}) {product.Name}");
            }

            var prodChoice = Request<int>("Choose a product:");
            while (!prods.ContainsKey(prodChoice))
            {
                prodChoice = Request<int>("Choose a valid product:");
            }
            
            var amount = Request<decimal>("Choose an amount:");
            while (amount < 1)
            {
                amount = Request<decimal>("Choose a positive amount:");
            }

            var shelfChooser = 0;
            foreach (var shelve in Session.QueryOver<Shelf>().OrderBy(p => p.Name).Asc.List())
            {
                prods.Add(++shelfChooser, shelve);
                Console.WriteLine($"{shelfChooser,3}) {shelve.Name}");
            }

            var shelfChoice = Request<int>("Choose a shelf:");
            while (!prods.ContainsKey(shelfChoice))
            {
                shelfChoice = Request<int>("Choose a valid shelf:");
            }

        }
    }
}
