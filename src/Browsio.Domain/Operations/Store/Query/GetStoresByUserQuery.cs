namespace Browsio.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class GetStoresByUserQuery : QueryBase<List<Store>>
    {
        #region Properties

        public string UserId { get; set; }

        #endregion

        protected override List<Store> ExecuteResult()
        {
            return Repository.Query(whereSpecification: new StoreByUserWhereSpec(UserId), 
                                    fetchSpecification: new StoreWithGenreFetchSpec())
                             .ToList();
        }
    }
}