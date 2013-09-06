namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Linq.Expressions;
    using Incoding;

    #endregion

    public class StoreByCategoryOptWhereSpec : Specification<Store>
    {
        #region Fields

        readonly CategoryOfType? category;

        #endregion

        #region Constructors

        public StoreByCategoryOptWhereSpec(CategoryOfType? category)
        {
            this.category = category;
        }

        #endregion

        public override Expression<Func<Store, bool>> IsSatisfiedBy()
        {
            if (!this.category.HasValue)
                return null;

            return r => r.Category == this.category;
        }
    }
}