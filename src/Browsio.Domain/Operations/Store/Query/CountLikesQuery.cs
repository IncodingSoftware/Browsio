using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using Incoding.CQRS;

    public class CountLikesQuery : QueryBase<IncStructureResponse<int>>
    {
        public string StoreId { get; set; }

        protected override IncStructureResponse<int> ExecuteResult()
        {
            var store = this.Repository.GetById<Store>(StoreId);
            IncStructureResponse<int> countLikes = new IncStructureResponse<int>(store.Followers.Count);
            return countLikes;
        }
    }
}
