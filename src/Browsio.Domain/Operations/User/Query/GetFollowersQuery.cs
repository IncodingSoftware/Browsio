using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using Incoding.CQRS;

    public class GetFollowersQuery : QueryBase<IncStructureResponse<int>>
    {
        public string UserId { get; set; }

        protected override IncStructureResponse<int> ExecuteResult()
        {
            var user = Repository.GetById<User>(UserId);
            return new IncStructureResponse<int>(user.Stores.Select(r => r.Followers.Count).Sum());
        }
    }
}
