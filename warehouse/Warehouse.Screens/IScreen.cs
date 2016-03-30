namespace Warehouse.Screens
{
    public interface IScreen
    {
        string Name { get; }

        void Show();
    }
}