namespace Warehouse.ConsoleRunner.Screens
{
    using Models;
    using NHibernate;

    public class RacksScreen : CrudScreen<Rack>
    {
        public RacksScreen(ISessionFactory sessionFactory, IScreenHandler screenHandler, IRequestHandler requestHandler, IResponseHandler responseHandler) : base(sessionFactory, screenHandler, requestHandler, responseHandler)
        {
        }

        protected override Rack ScreenAdd(Rack rack)
        {

            return rack;
        }
    }
}