namespace Browsio.Domain
{
    #region << Using >>

    using System.Web;
    using Incoding.CQRS;
    using Incoding.Maybe;
    using Incoding.MvcContrib;

    #endregion

    public class AddProductCommand : CommandBase, IProductCommand
    {
        #region Properties

        public string StoreId { get; set; }

        #endregion

        #region IProductCommand Members

        public string Author { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Asin { get; set; }

        public HttpPostedFileBase Image { get; set; }

        #endregion

        public override void Execute()
        {
            var store = Repository.GetById<Store>(StoreId);
            var product = new Product
                              {
                                      Name = Name, 
                                      Description = Description, 
                                      Price = Price, 
                                      Asin = Asin, 
                                      Author = Author, 
                                      Image = Image.ReturnOrDefault(r => new HttpMemoryPostedFile(r).ContentAsBytes, new byte[0])
                              };
            store.AddProduct(product);

            EventBroker.Publish(new OnChangeSearchItemEvent(product));
        }
    }
}