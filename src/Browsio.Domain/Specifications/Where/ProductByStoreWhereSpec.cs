namespace Browsio.Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class ProductByStoreWhereSpec:Specification<Product>
    {
        readonly string storeId;

        public ProductByStoreWhereSpec(string storeId)
        {
            this.storeId = storeId;
        }

        public override Expression<Func<Product, bool>> IsSatisfiedBy()
        {
            return product => product.Store.Id == this.storeId;
        }
    }
}