namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Linq.Expressions;
    using Incoding;

    #endregion

    public class ProductByASINWhereSpec : Specification<Product>
    {
        #region Fields

        readonly string asin;

        #endregion

        #region Constructors

        public ProductByASINWhereSpec(string asin)
        {
            this.asin = asin;
        }

        #endregion

        public override Expression<Func<Product, bool>> IsSatisfiedBy()
        {
            return product => product.Asin == this.asin;
        }
    }
}