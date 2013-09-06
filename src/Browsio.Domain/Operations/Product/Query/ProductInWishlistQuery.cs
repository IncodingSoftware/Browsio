using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using Incoding.Block.IoC;
    using Incoding.CQRS;

    public class ProductInWishlistQuery : QueryBase<IList<ProductVm>>
    {
        IList<ProductVm> ProductVms;

        public ProductInWishlistQuery() : base() {  }

        public ProductInWishlistQuery(IList<ProductVm> productVms) : base()
        {
            ProductVms = productVms;
        }

        protected override IList<ProductVm> ExecuteResult()
        {
            ISessionContext sessionContext = IoCFactory.Instance.TryResolve<ISessionContext>();
            var user = this.Repository.GetById<User>(sessionContext.UserId);
            if (user != null)
            {
                var wishProducts = user.WishProducts.ToList();
                foreach (var productVm in ProductVms)
                {
                    productVm.IsAddWishList = wishProducts.Any(r => r.Id.ToString() == productVm.Id);
                }
            }
            return ProductVms;
        }
    }
}
