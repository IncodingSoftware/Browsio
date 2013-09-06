using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using System.Linq.Expressions;
    using Incoding;

    public class ViewsByStoreId : Specification<Views>
    {
        readonly string storeId;

        public ViewsByStoreId(string StoreId)
        {
            storeId = StoreId;
        }

        public override Expression<Func<Views, bool>> IsSatisfiedBy()
        {
            return r => r.Store.Id == storeId;
        }
    }
}
