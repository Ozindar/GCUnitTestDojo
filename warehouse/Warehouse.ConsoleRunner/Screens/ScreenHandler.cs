namespace Warehouse.ConsoleRunner.Screens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;

    public class ScreenHandler
    {
        private readonly IList<IScreen> _activeScreens = new List<IScreen>();
        private readonly ISessionFactory _sessionFactory;

        public ScreenHandler(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void ShowScreen(Type type, params object[] args)
        {
            var screen = _activeScreens.FirstOrDefault(s => s.GetType() == type);

            if (screen == null)
            {
                List<object> allArgs = new List<object> {_sessionFactory, this};
                allArgs.AddRange(args);
                screen = (IScreen) Activator.CreateInstance(type, allArgs.ToArray());
                _activeScreens.Add(screen);
            }

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
}
