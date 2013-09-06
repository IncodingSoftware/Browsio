using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using Incoding.Block.IoC;
    using Incoding.CQRS;

    public class AddProductToWishlistCommand :CommandBase
    {
        public string ProductId { get; set; }

        public override void Execute()
        {
            ISessionContext sessionContext = IoCFactory.Instance.TryResolve<ISessionContext>();
            var user = this.Repository.GetById<User>(sessionContext.UserId);
            user.AddWishProduct(this.Repository.GetById<Product>(ProductId));
        }
    }
}
