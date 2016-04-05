namespace Warehouse.Screens
{
    using System.Collections;
    using Handlers;
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
                ResponseHandler.Clear();

                var chosenProduct = RequestHandler.RequestChoice<Product>(ResponseHandler, Session);

                var amount = RequestHandler.RequestStruct<int>(ResponseHandler, "Choose an amount:");
                while (amount < 1)
                {
                    amount = RequestHandler.RequestStruct<int>(ResponseHandler, "Choose a positive amount:");
                }

                var chosenShelf = RequestHandler.RequestChoice<Shelf>(ResponseHandler, Session);

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
