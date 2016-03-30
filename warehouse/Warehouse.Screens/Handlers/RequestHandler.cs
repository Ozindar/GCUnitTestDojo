namespace Warehouse.Screens.Handlers
{
    using System;
    using System.Collections;
    using Models;
    using NHibernate;

    public class RequestHandler : IRequestHandler
    {
        public char ReadKey()
        {
            return Console.ReadKey().KeyChar;
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public string RequestString(IResponseHandler responseHandler, string firstQuestion)
        {
            Console.WriteLine(firstQuestion);
            return Console.ReadLine();
        }

        public T RequestStruct<T>(IResponseHandler responseHandler, string firstQuestion) where T : struct
        {
            responseHandler.WriteLine(firstQuestion);
            var input = ReadLine();

            while (true)
            {
                if (input.Equals("x", StringComparison.InvariantCultureIgnoreCase))
                    throw new NoChoiceException();

                try
                {
                    T result = (T)Convert.ChangeType(input, typeof(T));
                    return result;
                }
                catch
                {

                }

                responseHandler.WriteLine($"Type a {typeof(T).Name} please:");
                input = Console.ReadLine();
            }
        }

        public T RequestChoice<T>(IResponseHandler responseHandler, ISession session) where T : ModelBase
        {
            var itemChooser = 0;
            var items = new Hashtable();
            foreach (var item in session.QueryOver<T>().OrderBy(p => p.Name).Asc.List())
            {
                items.Add(++itemChooser, item);
                responseHandler.WriteLine($"{itemChooser,3}) {item.Name}");
            }

            var chosenItem = RequestStruct<int>(responseHandler, $"Choose a {typeof (T).Name}:");
            while (!items.ContainsKey(chosenItem))
            {
                chosenItem = RequestStruct<int>(responseHandler, $"Choose a valid {typeof (T).Name}:");
            }
            return items[chosenItem] as T;
        }
    }
}