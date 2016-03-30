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
    }
}