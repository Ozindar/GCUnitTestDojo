namespace Warehouse.ConsoleRunner.Screens
{
    using System;
    using NHibernate;

    public class MainScreen : ScreenBase
    {
        public MainScreen(ISessionFactory sessionFactory, ScreenHandler screenHandler) : base(sessionFactory, screenHandler)
        {
        }

        public override void HandleKey(char key)
        {
            switch (key)
            {
                case 'p':
                    ScreenHandler.ShowScreen(typeof (ProductScreen));
                    return;
            }

            base.HandleKey(key);
        }

        public override void ScreenOptions()
        {
            Console.WriteLine("P) Products");
        }

        public override string Name => "Main";
        public override void ScreenShow()
        {
            Console.WriteLine("**** MAIN SCREEN ****");
        }
    }
}