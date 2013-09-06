namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Linq.Expressions;
    using Incoding;

    #endregion

    public class GenreByCategoryOptWhereSpec : Specification<Genre>
    {
        #region Fields

        readonly CategoryOfType? category;

        #endregion

        #region Constructors

        public GenreByCategoryOptWhereSpec(CategoryOfType? category)
        {
            this.category = category;
        }

        #endregion

        public override Expression<Func<Genre, bool>> IsSatisfiedBy()
        {
            if (!this.category.HasValue)
                return null;

            return genre => genre.Category == this.category;
        }
    }
}