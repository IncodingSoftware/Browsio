using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using System.Collections;
    using Incoding.Block.IoC;
    using Incoding.CQRS;

    public class GetProductInWishlist : QueryBase<IList<GetProductInWishlist.Response>>
    {
        protected override IList<Response> ExecuteResult()
        {
            ISessionContext sessionContext = IoCFactory.Instance.TryResolve<ISessionContext>();
            var user = this.Repository.GetById<User>(sessionContext.UserId);
            return user.WishProducts.Select(product => new Response(product)).ToList();
            //var responses = new List<Response>();
            //Response response = null;
            //for(int i = 0; i < user.WishProducts.Count; i++)
            //{
            //    response = new Response();
            //    response.Id = user.WishProducts[i].Id.ToString();
            //    response.Name = user.WishProducts[i].Name;
            //    if (i == 0 || i % 3 == 0)
            //    {
            //        response.isNewRow = true;
            //    }
            //    else
            //    {
            //        response.isNewRow = false;
            //    }
            //    responses.Add(response);

            //}
            //return responses;
        }

        public class Response
        {
            public Response() { }

            public Response(Product product)
            {
                this.Id = product.Id.ToString();
                this.Name = product.Name;
                this.Asin = product.Asin;
            }

            public string Id { get; set; }

            public string Name { get; set; }

            public string Asin { get; set; }
        }
    }
}
