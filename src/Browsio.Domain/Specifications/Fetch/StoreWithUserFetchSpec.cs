namespace Browsio.Domain
{
    using System;
    using Incoding.Data;

    public class StoreWithUserFetchSpec : FetchSpecification<Store>
    {
        public override Action<AdHocFetchSpecification<Store>> FetchedBy()
        {
            return specification => specification.Join(r => r.User);
        }
    }
}