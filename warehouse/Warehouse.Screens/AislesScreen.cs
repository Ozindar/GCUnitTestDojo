namespace Warehouse.Screens
{
    using Handlers;
    using Models;
    using NHibernate;

    public class AislesScreen : CrudScreen<Aisle>
    {
        public AislesScreen(ISessionFactory sessionFactory, IScreenHandler screenHandler, IRequestHandler requestHandler, IResponseHandler responseHandler) : base(sessionFactory, screenHandler, requestHandler, responseHandler)
        {

        }

        protected override Aisle ScreenAdd(Aisle item)
        {
            var building = RequestHandler.RequestChoice<Building>(ResponseHandler, Session);

            item.Building = building;

            return item;
        }
    }
}