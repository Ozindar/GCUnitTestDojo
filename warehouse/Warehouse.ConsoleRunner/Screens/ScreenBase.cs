namespace Warehouse.ConsoleRunner.Screens
{
    using System;
    using NHibernate;

    public abstract class ScreenBase : IScreen
    {
        protected readonly ScreenHandler ScreenHandler;
        protected readonly ISessionFactory SessionFactory;
        protected ISession Session;

        protected ScreenBase(ISessionFactory sessionFactory, ScreenHandler screenHandler)
        {
            SessionFactory = sessionFactory;
            ScreenHandler = screenHandler;
            Session = sessionFactory.OpenSession();
        }

        public abstract string Name { get; }

        public virtual void Show()
        {
            Console.Clear();

            ScreenShow();
            Options();
        }

        public abstract void ScreenShow();

        protected void Options()
        {
            ScreenOptions();
            Console.WriteLine("X) Return to mainscreen");
            Console.WriteLine("Q) Quit");

            var consoleKeyInfo = Console.ReadKey();
            HandleKey(consoleKeyInfo.KeyChar);
        }

        public abstract void ScreenOptions();

        public virtual void HandleKey(char key)
        {
            switch (key)
            {
                case 'x':
                    ScreenHandler.ShowScreen(typeof (MainScreen));
                    break;
                case 'q':
                    Environment.Exit(0);
                    return;
            }

            var consoleKeyInfo = Console.ReadKey();
            HandleKey(consoleKeyInfo.KeyChar);
        }
    }
}
