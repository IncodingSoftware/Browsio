namespace Browsio.Amazon
{
    #region << Using >>

    using System;

    #endregion

    [Serializable]
    public class AmazonItem
    {
        #region Properties

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string ASIN { get; set; }

        public string Image { get; set; }

        public string Author { get; set; }

        #endregion
    }
}