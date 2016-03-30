namespace Warehouse.Screens
{
    using System;
    using Handlers;
    using NHibernate;

    public abstract class ScreenBase : IScreen
    {
        protected readonly IScreenHandler ScreenHandler;
        protected readonly ISessionFactory SessionFactory;
        protected readonly ISession Session;
        protected readonly IRequestHandler RequestHandler;
        protected readonly IResponseHandler ResponseHandler;

        protected ScreenBase(ISessionFactory sessionFactory, IScreenHandler screenHandler, IRequestHandler requestHandler, IResponseHandler responseHandler)
        {
            SessionFactory = sessionFactory;
            ScreenHandler = screenHandler;
            RequestHandler = requestHandler;
            ResponseHandler = responseHandler;
            Session = SessionFactory.GetCurrentSession();
        }

        public abstract string Name { get; }

        public virtual void Show()
        {
            ScreenShow();
            Options();
        }

        protected abstract void ScreenShow();

        protected virtual void Options()
        {
            ScreenOptions();
            ResponseHandler.WriteLine("X) Return to mainscreen");
            ResponseHandler.WriteLine("Q) Quit");

            var responseChar = RequestHandler.ReadKey();
            HandleKeyBase(responseChar);
        }

        protected abstract void ScreenOptions();
        protected abstract bool HandleKey(char key);

        protected virtual void HandleKeyBase(char key)
        {
            key = char.ToLower(key);

            if (HandleKey(key))
                return;

            switch (key)
            {
                case 'x':
                    ScreenHandler.ShowScreen(typeof (MainScreen), true);
                    break;
                case 'q':
                    Environment.Exit(0);
                    return;
            }

            var responseChar = RequestHandler.ReadKey();
            HandleKeyBase(responseChar);
        }
    }
}
