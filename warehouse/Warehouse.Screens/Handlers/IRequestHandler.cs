namespace Warehouse.Screens.Handlers
{
    using Models;
    using NHibernate;

    public interface IRequestHandler
    {
        char ReadKey();
        string ReadLine();
        string RequestString(IResponseHandler responseHandler,string firstQuestion);
        T RequestStruct<T>(IResponseHandler responseHandler, string firstQuestion) where T : struct;
        T RequestChoice<T>(IResponseHandler responseHandler, ISession session) where T : ModelBase;
    }
}