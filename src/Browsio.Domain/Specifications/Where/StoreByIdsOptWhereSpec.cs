namespace Browsio.Domain
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Incoding;

    public class StoreByIdsOptWhereSpec : Specification<Store>
    {
        readonly object[] storeIds;

        public StoreByIdsOptWhereSpec(string[] storeIds)
        {
            this.storeIds = storeIds;
        }

        public override Expression<Func<Store, bool>> IsSatisfiedBy()
        {
            //if (this.storeIds == null || !this.storeIds.Any())
            if (this.storeIds == null)
                return null;

            return store => this.storeIds.Contains(store.Id);
        }
    }
}