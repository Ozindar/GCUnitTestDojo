namespace Warehouse.ConsoleRunner
{
    using System;
    using Models;
    using NHibernate;
    using Screens;
    using Session;

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

            if (SessionFactoryConfigurator.CanSeed)
            {
                Console.WriteLine("Seeding");
                SessionFactoryConfigurator.SeedData(_sessionFactory);
                Console.WriteLine("Seeded");
            }

            ScreenHandler screenHandler = new ScreenHandler(_sessionFactory, new RequestHandler(), new ResponseHandler());
            screenHandler.ShowScreen(typeof(MainScreen), true);
        }
    }
}
