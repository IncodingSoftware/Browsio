namespace Browsio.Domain
{
    using System;
    using Incoding.Data;

    public class StoreWithGenreFetchSpec : FetchSpecification<Store> {
        public override Action<AdHocFetchSpecification<Store>> FetchedBy()
        {
            return specification => specification.Join(r => r.Genre);
        }
    }
}