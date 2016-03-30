namespace Warehouse.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Enums;
    using WeatherService;

    public class Product : ModelBase
    {
        public virtual ProductType ProductType { get; set; }
        public virtual IList<StoredProduct> StoredProducts { get; protected set; } = new List<StoredProduct>();
        public virtual decimal Weight { get; set; }

        public virtual int AmountAvailable 
        {
            get { return StoredProducts.Sum(a => a.Amount); }
        }

        public virtual void StoreProduct(Shelf shelf, int amount)
        {
            if (amount < 1)
                throw new ArgumentOutOfRangeException(nameof(amount), "amount of products to store needs to be positve");

            var storedProduct = StoredProducts.FirstOrDefault(sp => sp.Shelf == shelf) ?? new StoredProduct(shelf, this);
            storedProduct.AddProducts(amount);
        }

        public virtual void RemoveProduct(Shelf shelf, int amount)
        {
            if (amount > 1)
                throw new ArgumentOutOfRangeException(nameof(amount), "amount of products to remove needs to be positve");

            var storedProduct = StoredProducts.FirstOrDefault(sp => sp.Shelf == shelf) ?? new StoredProduct(shelf, this);
            storedProduct.RemoveProducts(amount);
        }

        public override string ToString()
        {
            return $"{Name} ({AmountAvailable} x {Weight} = {AmountAvailable* Weight}kg)";

        }
    }
}
