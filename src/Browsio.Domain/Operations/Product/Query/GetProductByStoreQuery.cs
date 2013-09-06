namespace Browsio.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;
    using Incoding.Extensions;

    #endregion

    public class GetProductByStoreQuery : QueryBase<List<Product>>
    {
        #region Properties

        public string StoreId { get; set; }

        public string Content { get; set; }

        #endregion

        protected override List<Product> ExecuteResult()
        {
            return Repository.Query(whereSpecification: new ProductByStoreWhereSpec(StoreId)
                                            .And(new ProductByNameOptWhereSpec(Content)))
                             .ToList();
        }
    }
}