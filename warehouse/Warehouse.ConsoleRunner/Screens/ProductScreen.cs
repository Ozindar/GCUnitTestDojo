namespace Warehouse.ConsoleRunner.Screens
{
    using System;
    using System.Collections;
    using Models;
    using NHibernate;

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
