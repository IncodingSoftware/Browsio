namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using Incoding.CQRS;
    using Incoding.MvcContrib;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class EditProductCommand : CommandBase, IProductCommand
    {
        #region Constructors

        [UsedImplicitly, Obsolete(ObsoleteMessage.SerializeConstructor, true), ExcludeFromCodeCoverage]
        public EditProductCommand() { }

        public EditProductCommand(Product product)
        {
            Id = product.Id.ToString();
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Author = product.Author;
            Asin = product.Asin;
        }

        #endregion

        #region Properties

        public string Id { get; set; }

        #endregion

        #region IProductCommand Members

        public string Name { get; set; }

        public string Description { get; set; }

        public string Asin { get; set; }

        public string Author { get; set; }

        public float Price { get; set; }

        public HttpPostedFileBase Image { get; set; }

        #endregion

        public override void Execute()
        {
            var product = Repository.GetById<Product>(Id);
            product.Name = Name;
            product.Description = Description;
            product.Asin = Asin;
            product.Price = Price;
            product.Author = Author;
            if (Image != null)
                product.Image = new HttpMemoryPostedFile(Image).ContentAsBytes;

            EventBroker.Publish(new OnChangeSearchItemEvent(product));
        }
    }
}