using Warehouse.Models.WheatherService;

namespace Warehouse.Models
{
    public class StoredProduct : ModelBase
    {
        protected StoredProduct()
        {
        }

        public StoredProduct(Shelve shelve, Product product)
        {
            Shelve = shelve;
            Product = product;
        }

        public virtual int Amount { get; protected set; }
        public virtual Shelve Shelve { get; protected set; }
        public virtual Product Product { get; protected set; }

        public virtual void AddProducts(int amount)
        {
            Amount += amount;
        }

        public virtual void RemoveProducts(int amount)
        {
            Amount -= amount;
        }
    }
}