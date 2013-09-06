namespace Browsio.Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class SearchItemByOwnerWhereSpec:Specification<SearchItem>
    {
        readonly string ownerId;

        public SearchItemByOwnerWhereSpec(string ownerId)
        {
            this.ownerId = ownerId;
        }

        public override Expression<Func<SearchItem, bool>> IsSatisfiedBy()
        {
            return item => item.OwnerId == this.ownerId;
        }
    }
}