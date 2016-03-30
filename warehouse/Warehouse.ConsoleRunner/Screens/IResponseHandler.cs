namespace Warehouse.ConsoleRunner.Screens
{
    using System;

    public interface IResponseHandler
    {
        void WriteLine(string s);
        void Write(string s);
        void Clear();
    }

    public class ResponseHandler : IResponseHandler
    {
        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }

        public void Write(string s)
        {
            Console.Write(s);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
