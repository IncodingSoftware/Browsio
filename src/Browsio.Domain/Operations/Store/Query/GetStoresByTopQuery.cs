namespace Browsio.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;
    using Incoding.Data;

    #endregion

    public class GetStoresByTopQuery : QueryBase<List<Store>>
    {
        protected override List<Store> ExecuteResult()
        {
            return Repository.Query(paginatedSpecification: new PaginatedSpecification(1, 10), 
                                    fetchSpecification: new StoreWithUserFetchSpec())
                             .ToList();
        }
    }
}