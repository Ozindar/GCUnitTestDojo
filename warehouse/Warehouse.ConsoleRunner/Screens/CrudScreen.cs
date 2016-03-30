namespace Warehouse.ConsoleRunner.Screens
{
    using System;
    using Humanizer;
    using Models;
    using NHibernate;
    using NHibernate.Util;

    public abstract class CrudScreen<T> : ScreenBase where T : ModelBase, new()
    {
        protected CrudScreen(ISessionFactory sessionFactory, IScreenHandler screenHandler, IRequestHandler requestHandler, IResponseHandler responseHandler) : base(sessionFactory, screenHandler, requestHandler, responseHandler)
        {
        }

        public override string Name => typeof(T).Name.Pluralize();

        protected virtual void List(bool showNumbers = false)
        {
            var items = Session.QueryOver<T>().List();

            if (!items.Any())
            {
                ResponseHandler.WriteLine($"No {typeof(T).Name.Pluralize()}");
                return;
            }

            int i = 0;
            foreach (var item in items)
            {
                if (showNumbers)
                    ResponseHandler.Write($"{i++}) ");
                ResponseHandler.WriteLine(item.ToString());
            }
        }

        protected override void ScreenShow()
        {
            ResponseHandler.WriteLine($"**** {typeof(T).Name.Pluralize()} ****");
        }

        protected override void ScreenOptions()
        {
            ResponseHandler.WriteLine($"A) Add {typeof(T).Name}");
            ResponseHandler.WriteLine($"L) List {typeof(T).Name}");
        }

        protected override bool HandleKey(char key)
        {
            switch (key)
            {
                case 'a':
                    Add();
                    break;
                case 'l':
                    ResponseHandler.Clear();
                    List();
                    ResponseHandler.WriteLine("");
                    Show();
                    break;

                default:
                    return false;
            }
            return true;
        }

        protected virtual void Add()
        {
            ResponseHandler.Clear();
            ResponseHandler.WriteLine("Type name:");
            var name = RequestHandler.ReadLine();

            var p = new T {Name = name};

            p = ScreenAdd(p);
            Session.SaveOrUpdate(p);
            ResponseHandler.Clear();
            ResponseHandler.WriteLine($"{typeof(T).Name} '{name}' saved.");
            Show();
        }

        protected virtual T ScreenAdd(T item)
        {
            return item;
        }
    }
}