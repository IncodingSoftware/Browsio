namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Linq.Expressions;
    using Incoding;

    #endregion

    public class ProductByNameOptWhereSpec : Specification<Product>
    {
        #region Fields

        readonly string productName;

        #endregion

        #region Constructors

        public ProductByNameOptWhereSpec(string productName)
        {
            this.productName = productName;
        }

        #endregion

        public override Expression<Func<Product, bool>> IsSatisfiedBy()
        {
            if (string.IsNullOrWhiteSpace(this.productName))
                return null;

            return product => product.Name.Contains(this.productName);
        }
    }
}