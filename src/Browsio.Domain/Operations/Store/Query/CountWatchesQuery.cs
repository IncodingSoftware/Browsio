using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using Incoding.CQRS;

    public class  CountWatchesQuery : QueryBase<IncStructureResponse<int>>
    {
        public string StoreId { get; set; }

        protected override IncStructureResponse<int> ExecuteResult()
        {
            IncStructureResponse<int> countWatches = new IncStructureResponse<int>(this.Repository.Query<Views>(whereSpecification: new ViewsByStoreId(StoreId)).Count());
            return countWatches;
        }
    }
}
