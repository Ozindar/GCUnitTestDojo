﻿namespace Warehouse.ConsoleRunner.Screens
{
    using Models;
    using NHibernate;

    public class ShelvesScreen : CrudScreen<Shelf>
    {
        public ShelvesScreen(ISessionFactory sessionFactory, IScreenHandler screenHandler, IRequestHandler requestHandler, IResponseHandler responseHandler) : base(sessionFactory, screenHandler, requestHandler, responseHandler)
        {
        }
    }
}