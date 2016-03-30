namespace Warehouse.Screens.Handlers
{
    using NHibernate;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ScreenHandler : IScreenHandler
    {
        private readonly IList<IScreen> _activeScreens = new List<IScreen>();
        private readonly ISessionFactory _sessionFactory;
        private readonly IRequestHandler _requestHandler;
        private readonly IResponseHandler _responseHandler;

        public ScreenHandler(ISessionFactory sessionFactory, IRequestHandler requestHandler, IResponseHandler responseHandler)
        {
            _sessionFactory = sessionFactory;
            _requestHandler = requestHandler;
            _responseHandler = responseHandler;
        }

        public void ShowScreen(Type type, bool clear, params object[] args)
        {
            var screen = _activeScreens.FirstOrDefault(s => s.GetType() == type);

            if (screen == null)
            {
                List<object> allArgs = new List<object> {_sessionFactory, this, _requestHandler, _responseHandler };
                allArgs.AddRange(args);
                screen = (IScreen) Activator.CreateInstance(type, allArgs.ToArray());
                _activeScreens.Add(screen);
            }
            if (clear)
                _responseHandler.Clear();

            screen.Show();
        }

        public void CloseScreen(Type type)
        {
            var screen = _activeScreens.FirstOrDefault(s => s.GetType() == type);

            if (screen != null)
            {
                _activeScreens.Remove(screen);
            }
        }
    }

    public interface IScreenHandler
    {
        void ShowScreen(Type type, bool clear, params object[] args);
        void CloseScreen(Type type);
    }
}
