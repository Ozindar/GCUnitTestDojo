namespace Warehouse.Screens
{
    using Handlers;
    using Models;
    using NHibernate;

    public class RacksScreen : CrudScreen<Rack>
    {
        public RacksScreen(ISessionFactory sessionFactory, IScreenHandler screenHandler, IRequestHandler requestHandler, IResponseHandler responseHandler) : base(sessionFactory, screenHandler, requestHandler, responseHandler)
        {
        }

        protected override Rack ScreenAdd(Rack item)
        {
            var aisle = RequestHandler.RequestChoice<Aisle>(ResponseHandler, Session);

            item.Aisle = aisle;

            return item;
        }
    }
}