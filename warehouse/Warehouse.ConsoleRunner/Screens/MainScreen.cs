namespace Warehouse.ConsoleRunner.Screens
{
    using ConsoleRunner;
    using NHibernate;

    public class MainScreen : ScreenBase
    {
        public MainScreen(ISessionFactory sessionFactory, IScreenHandler screenHandler, IRequestHandler requestHandler, IResponseHandler responseHandler)
            : base(sessionFactory, screenHandler, requestHandler, responseHandler)
        {
        }

        public override string Name => "Main";

        protected override bool HandleKey(char key)
        {
            switch (key)
            {
                case 'b':
                    ScreenHandler.ShowScreen(typeof (BuildingsScreen), true);
                    return true;
                case 'a':
                    ScreenHandler.ShowScreen(typeof(AislesScreen), true);
                    return true;
                case 'r':
                    ScreenHandler.ShowScreen(typeof(RacksScreen), true);
                    return true;
                case 's':
                    ScreenHandler.ShowScreen(typeof(ShelvesScreen), true);
                    return true;
                case 'p':
                    ScreenHandler.ShowScreen(typeof (ProductsScreen), true);
                    return true;
            }
            return false;
        }

        protected override void ScreenOptions()
        {
            ResponseHandler.WriteLine("P) Products");
            ResponseHandler.WriteLine("A) Aisles");
            ResponseHandler.WriteLine("R) Racks");
            ResponseHandler.WriteLine("S) Shelves");
            ResponseHandler.WriteLine("B) Buildings");
        }

        protected override void ScreenShow()
        {
            ResponseHandler.WriteLine("**** MAIN SCREEN ****");
        }
    }
}
