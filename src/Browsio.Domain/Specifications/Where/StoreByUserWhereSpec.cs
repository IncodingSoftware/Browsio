namespace Browsio.Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class StoreByUserWhereSpec : Specification<Store>
    {
        readonly string userId;

        public StoreByUserWhereSpec(string userId)
        {
            this.userId = userId;
        }

        public override Expression<Func<Store, bool>> IsSatisfiedBy()
        {
            if (!string.IsNullOrWhiteSpace(userId))
                return r => r.User.Id == this.userId;
            return null;
        }
    }
}