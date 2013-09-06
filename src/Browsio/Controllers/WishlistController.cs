using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Browsio.Controllers
{
    using Browsio.Domain;
    using Incoding.Block.IoC;
    using Incoding.CQRS;
    using Incoding.MvcContrib;

    [BrowsioAuthorize]
    public class WishlistController : IncControllerBase
    {
        //
        // GET: /Wishlist/

        public WishlistController(IDispatcher dispatcher)
                : base(dispatcher) { }


        [HttpGet]
        public ActionResult index()
        {
            return IncPartialView("index");
        }

        [HttpPost]
        public ActionResult FetchWishlist(GetProductInWishlist query)
        {
            var vm = this.dispatcher.Query(query);
            return IncJson(vm);
        }

        [HttpPost]
        public ActionResult AddProductToWishList(AddProductToWishlistCommand command)
        {
            return TryPush(command);
        }
    }
}
