namespace Warehouse.ConsoleRunner
{
    using Screens;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var _sessionFactory = SessionFactoryConfigurator.CreateSessionFactory();
            
            ScreenHandler screenHandler = new ScreenHandler(_sessionFactory);

            screenHandler.ShowScreen(typeof(MainScreen));

        }
    }
}
