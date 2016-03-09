namespace Warehouse.ConsoleRunner.Screens
{
    using System;
    using System.Collections;
    using Models;
    using NHibernate;
    using NHibernate.Util;

    public class ProductScreen : ScreenBase
    {
        public ProductScreen(ISessionFactory sessionFactory, ScreenHandler screenHandler) : base(sessionFactory, screenHandler)
        {
        }

        public override string Name => "Products";

        public void ShowAll()
        {
            var products = Session.QueryOver<Product>().List();

            if (!products.Any())
            {
                Console.WriteLine("No products");
                return;
            }

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} ({product.AmountAvailable})");
            }
        }

        public override void ScreenShow()
        {
            Console.WriteLine("**** Products ****");
            ShowAll();
        }

        public override void ScreenOptions()
        {
            Console.WriteLine("A) Add Product");
            Console.WriteLine("S) Store Product");
        }

        public override void HandleKey(char key)
        {
            switch (key)
            {
                case 'a':
                    AddProduct();
                    break;
                case 's':
                    StoreOnShelve();
                    break;

                default:
                    return;
            }
        }

        private void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("Type productname to add:");
            var productName = Console.ReadLine();

            var p = new Product {Name = productName};
            Session.SaveOrUpdate(p);

            Console.WriteLine($"{productName} saved.");
            Console.ReadLine();
            Show();
        }

        private void StoreOnShelve()
        {
            var prods = new Hashtable();
            Console.Clear();
            Console.WriteLine("Choose a product");

            var i = 0;
            foreach (var product in Session.QueryOver<Product>().OrderBy(p => p.Name).Asc.List())
            {
                prods.Add(++i, product);
                Console.WriteLine($"{i,3}) {product.Name}");
            }

            var productNumber = Console.ReadLine();
        }
    }
}
