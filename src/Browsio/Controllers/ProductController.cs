namespace Browsio.Controllers
{
    #region << Using >>

    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Browsio.Contrib;
    using Browsio.Domain;
    using Incoding.Block.IoC;
    using Incoding.CQRS;
    using Incoding.MvcContrib;

    #endregion

    public class ProductController : IncControllerBase
    {
        #region Constructors

        public ProductController(IDispatcher dispatcher)
                : base(dispatcher) { }

        #endregion

        #region Http action

        [HttpGet]
        public ActionResult Add(string storeId)
        {
            return IncView(new AddProductCommand
                               {
                                       StoreId = storeId
                               });
        }

        [HttpPost]
        public ActionResult Add(AddProductCommand input, string imageUrl)
        {
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                var data = new WebClient().DownloadData(imageUrl);
                input.Image = new HttpMemoryPostedFile(new MemoryStream(data), "image", "img");
            }

            return TryPush(input);
        }

        [HttpPost]
        public ActionResult DeleteByAsin(DeleteProductByAsinCommand input)
        {
            return TryPush(input);
        }

        [HttpGet]
        public ActionResult Edit(GetEntityByIdQuery<Product> query)
        {
            var product = this.dispatcher.Query(query);
            return IncView(new EditProductCommand(product));
        }

        [HttpPost]
        public ActionResult Edit(EditProductCommand input)
        {
            return TryPush(input);
        }

        [HttpGet]
        public ActionResult Fetch(GetProductByStoreQuery query)
        {
            var vms = this.dispatcher.Query(query)
                          .Select(r => new ProductVm(r))
                          .ToList();
           vms = this.dispatcher.Query(new ProductInWishlistQuery(vms)).ToList();
           return IncJson(vms);
        }

        [HttpGet]
        public ActionResult FetchByAmazon(GetProductByAmazonQuery query)
        {            
            var vms = this.dispatcher.Query(query);
            return IncJson(vms);
        }

        [HttpGet]
        public ActionResult SearchInAmazon()
        {
            return IncView(new GetProductByAmazonQuery
                               {
                                       Page = 1 
                               });
        }

        #endregion

        public ActionResult Template(bool isOwner)
        {
            return IncPartialView("Template", new IncStructureResponse<bool>(isOwner));
        }
    }
}