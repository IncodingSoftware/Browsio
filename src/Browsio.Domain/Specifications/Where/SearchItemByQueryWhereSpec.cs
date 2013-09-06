namespace Browsio.Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class SearchItemByQueryWhereSpec : Specification<SearchItem>
    {
        #region Fields

        readonly string query;

        #endregion

        #region Constructors

        public SearchItemByQueryWhereSpec(string query)
        {
            this.query = query;
        }

        #endregion

        public override Expression<Func<SearchItem, bool>> IsSatisfiedBy()
        {
            return item => item.Query.Contains(this.query);
        }
    }
}