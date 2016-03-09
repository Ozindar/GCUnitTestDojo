namespace Warehouse.ConsoleRunner.Screens
{
    using System;
    using FluentNHibernate.Conventions;
    using Models;
    using NHibernate;
    using NHibernate.Linq;
    using NHibernate.Util;

    public class ProductScreen : ScreenBase
    {
        public ProductScreen(ISessionFactory sessionFactory, ScreenHandler screenHandler) : base(sessionFactory, screenHandler)
        {
        }

        public void ShowAll()
        {
            var products = Session.Query<Product>();

            if (!products.Any())
            {
                Console.WriteLine("No products");
                return;
            }

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name}");
            }
        }

        public override string Name => "Products";

        public override void ScreenShow()
        {
            Console.WriteLine("**** Products ****");
            ShowAll();
        }

        public override void ScreenOptions()
        {
            Console.WriteLine("A) Add Product");
        }

        public override void HandleKey(char key)
        {
            switch (key)
            {
                case 'a':
                    AddProduct();

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
    }
}