namespace Warehouse.ConsoleRunner.Screens
{
    using System;
    using System.ComponentModel;
    using FluentNHibernate.Utils;
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
            HandleKeyBase(consoleKeyInfo.KeyChar);
        }

        public abstract void ScreenOptions();
        public abstract bool HandleKey(char key);

        protected virtual void HandleKeyBase(char key)
        {
            key = char.ToLower(key);

            if (HandleKey(key))
                return;

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
            HandleKeyBase(consoleKeyInfo.KeyChar);
        }

        protected T Request<T>(string firstQuestion) where T : struct
        {
            Console.WriteLine(firstQuestion);
            var input = Console.ReadLine();

            while (true)
            {
                try
                {
                    T result = (T) Convert.ChangeType(input, typeof (T));
                    return result;
                }
                catch
                {

                }

                Console.WriteLine($"Type a {typeof(T).Name} please:");
                input = Console.ReadLine();
            }
        }
    }
}
