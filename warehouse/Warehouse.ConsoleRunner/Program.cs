namespace Warehouse.ConsoleRunner
{
    using System;
    using NHibernate;
    using Screens;

    internal class Program
    {
        private static void Main(string[] args)
        {
            ISessionFactory _sessionFactory;
            try
            {
                _sessionFactory = SessionFactoryConfigurator.CreateSessionFactory();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine($"{e}");
                Console.ReadLine();
                return;
            }

            ScreenHandler screenHandler = new ScreenHandler(_sessionFactory);

            screenHandler.ShowScreen(typeof(MainScreen));

        }
    }
}
