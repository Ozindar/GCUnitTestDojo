namespace Warehouse.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Enums;

    public class Product : ModelBase
    {
        public virtual ProductType ProductType { get; set; }
        public virtual IList<StoredProduct> StoredProducts { get; protected set; } = new List<StoredProduct>();

        public virtual int AmountAvailable 
        {
            get { return StoredProducts.Sum(a => a.Amount); }
        }

        public virtual void StoreProduct(Shelve shelve, int amount)
        {
            if (amount < 1)
                throw new ArgumentOutOfRangeException(nameof(amount), "amount of products to store needs to be positve");

            var storedProduct = StoredProducts.FirstOrDefault(sp => sp.Shelve == shelve) ?? new StoredProduct(shelve, this);
            storedProduct.AddProducts(amount);
        }

        public virtual void RemoveProduct(Shelve shelve, int amount)
        {
            if (amount > 1)
                throw new ArgumentOutOfRangeException(nameof(amount), "amount of products to remove needs to be positve");

            var storedProduct = StoredProducts.FirstOrDefault(sp => sp.Shelve == shelve) ?? new StoredProduct(shelve, this);
            storedProduct.RemoveProducts(amount);
        }
    }
}
