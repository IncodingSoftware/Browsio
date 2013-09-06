namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Browsio.Amazon;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class ProductAmazonVm
    {
        #region Constructors

        [UsedImplicitly, Obsolete(ObsoleteMessage.SerializeConstructor, true), ExcludeFromCodeCoverage]
        public ProductAmazonVm() { }

        public ProductAmazonVm(AmazonItem amazonItem)
        {
            ASIN = amazonItem.ASIN;
            Title = amazonItem.Title.Replace("\"", string.Empty);
            Description = amazonItem.Description.Replace("\"", string.Empty);
            Image = amazonItem.Image;
            Author = amazonItem.Author;
            Amount = amazonItem.Price.ToString();            
        }

        #endregion

        #region Properties

        public bool Exist { get; set; }

        public bool IsLast { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string ASIN { get; set; }

        public string Image { get; set; }

        public string Amount { get; set; }

        #endregion
    }
}