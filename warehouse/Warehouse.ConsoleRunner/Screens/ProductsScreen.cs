namespace Warehouse.ConsoleRunner.Screens
{
    using System;
    using System.Collections;
    using Models;
    using NHibernate;

    public class ProductsScreen : CrudScreen<Product>
    {
        public ProductsScreen(ISessionFactory sessionFactory, IScreenHandler screenHandler, IRequestHandler requestHandler, IResponseHandler responseHandler) : base(sessionFactory, screenHandler, requestHandler, responseHandler)
        {
        }

        protected override void ScreenOptions()
        {
            base.ScreenOptions();
            ResponseHandler.WriteLine("S) Store Product");
        }

        protected override bool HandleKey(char key)
        {
            switch (key)
            {
                case 's':
                    StoreOnShelf();
                    return true;
                default:
                    return base.HandleKey(key);
            }
        }

        private void RemoveFromShelf()
        {
                
        }

        private void StoreOnShelf()
        {
            try
            {
                var prods = new Hashtable();
                ResponseHandler.Clear();
                ResponseHandler.WriteLine("Choose a product");

                var prodChooser = 0;
                foreach (var product in Session.QueryOver<Product>().OrderBy(p => p.Name).Asc.List())
                {
                    prods.Add(++prodChooser, product);
                    ResponseHandler.WriteLine($"{prodChooser,3}) {product.Name}");
                }

                var prodChoice = RequestHandler.RequestStruct<int>(ResponseHandler, "Choose a product:");
                while (!prods.ContainsKey(prodChoice))
                {
                    prodChoice = RequestHandler.RequestStruct<int>(ResponseHandler, "Choose a valid product:");
                }

                var amount = RequestHandler.RequestStruct<int>(ResponseHandler, "Choose an amount:");
                while (amount < 1)
                {
                    amount = RequestHandler.RequestStruct<int>(ResponseHandler, "Choose a positive amount:");
                }

                var chosenShelf = RequestHandler.RequestChoice<Shelf>(ResponseHandler, Session);
                var chosenProduct = RequestHandler.RequestChoice<Product>(ResponseHandler, Session);

                chosenProduct.StoreProduct(chosenShelf, amount);

                Session.SaveOrUpdate(chosenProduct);
                Session.Flush();

                ResponseHandler.Clear();
                ResponseHandler.WriteLine("Stuff stored.");
            }
            catch (NoChoiceException)
            {
                ResponseHandler.Clear();
                ResponseHandler.WriteLine("Aborted");
            }

            Show();
        }
    }
}
