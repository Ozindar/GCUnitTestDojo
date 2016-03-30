namespace Warehouse.Models
{
    using System.Reflection;

    public class StoredProduct : ModelBase
    {
        protected StoredProduct()
        {
        }

        public StoredProduct(Shelf shelf, Product product)
        {
            Shelf = shelf;
            Shelf.StoredProducts.Add(this);
            Product = product;
            Product.StoredProducts.Add(this);
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

        public override string ToString()
        {
            return $"{Product?.Name} - {Shelf}";
        }
    }
}
