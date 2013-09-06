namespace Browsio.Domain
{
    #region << Using >>

    using Incoding.Maybe;
    using Incoding.MvcContrib;

    #endregion

    public class ProductVm : EntityVm
    {
        #region Constructors

        public ProductVm(Product product)
                : base(product)
        {
            Name = product.Name;
            Price = product.Price.ToString();
            Description = product.Description;
            Asin = product.Asin;
            Author = product.Author;
            HasImage = product.Image.If(r => r.Length > 0).Has();
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public string Price { get; set; }

        public string Asin { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public bool HasImage { get; set; }

        public bool IsAddWishList { get; set; }

        #endregion
    }
}