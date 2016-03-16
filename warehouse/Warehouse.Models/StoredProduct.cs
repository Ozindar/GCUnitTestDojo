namespace Warehouse.Models
{
    public class StoredProduct : ModelBase
    {
        protected StoredProduct()
        {
        }

        public StoredProduct(Shelf shelf, Product product)
        {
            Shelf = shelf;
            Product = product;
        }

        public virtual int Amount { get; protected set; }
        public virtual Shelf Shelf { get; protected set; }
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
