namespace Warehouse.ConsoleRunner.Screens
{
    public interface IScreen
    {
        string Name { get; }

        void Show();
    }
}