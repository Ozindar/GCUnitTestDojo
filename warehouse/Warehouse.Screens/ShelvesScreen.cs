namespace Warehouse.Screens
{
    using Handlers;
    using Models;
    using NHibernate;

    public class ShelvesScreen : CrudScreen<Shelf>
    {
        public ShelvesScreen(ISessionFactory sessionFactory, IScreenHandler screenHandler, IRequestHandler requestHandler, IResponseHandler responseHandler) : base(sessionFactory, screenHandler, requestHandler, responseHandler)
        {
        }

        protected override Shelf ScreenAdd(Shelf item)
        {
            var rack = RequestHandler.RequestChoice<Rack>(ResponseHandler, Session);

            item.Rack = rack;

            return item;
        }
    }
}