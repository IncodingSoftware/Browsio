using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using Incoding.Block.IoC;
    using Incoding.CQRS;

    public class GetUserLoginQuery : QueryBase<IncStructureResponse<string>>
    {
        protected override IncStructureResponse<string> ExecuteResult()
        {
            ISessionContext sessionContext = IoCFactory.Instance.TryResolve<ISessionContext>();
            return  new IncStructureResponse<string>(this.Repository.GetById<User>(sessionContext.UserId).FullName);
        }
    }
}
