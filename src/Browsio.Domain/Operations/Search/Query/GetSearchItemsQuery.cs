namespace Browsio.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class GetSearchItemsQuery : QueryBase<List<SearchItem>>
    {
        #region Properties

        public string Keyword { get; set; }

        #endregion

        protected override List<SearchItem> ExecuteResult()
        {
            return this.Repository.Query(whereSpecification: new SearchItemByQueryWhereSpec(this.Keyword))
                             .ToList();
        }
    }
}