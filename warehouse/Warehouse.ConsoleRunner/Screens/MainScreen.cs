using System.Linq;
using Warehouse.Models;

namespace Warehouse.ConsoleRunner.Screens
{
    using System;
    using NHibernate;

    public class MainScreen : ScreenBase
    {
        public MainScreen(ISessionFactory sessionFactory, ScreenHandler screenHandler) : base(sessionFactory, screenHandler)
        {
        }

        public override bool HandleKey(char key)
        {
            switch (key)
            {
                case 'b':
                    ScreenHandler.ShowScreen(typeof (BuildingsScreen));
                    return true;
                case 'p':
                    ScreenHandler.ShowScreen(typeof (ProductScreen));
                    return true;
            }
            return false;
        }

        public override void ScreenOptions()
        {
            Console.WriteLine("B) Buildings");
            Console.WriteLine("P) Products");
        }

        public override string Name => "Main";
        public override void ScreenShow()
        {
            Console.WriteLine("**** MAIN SCREEN ****");
        }
    }
}