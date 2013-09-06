namespace Browsio.Amazon
{
    #region << Using >>

    using System.Collections.Generic;

    #endregion

    public interface IAmazonService
    {
        List<AmazonItem> Fetch(string title,int page, string category);
    }
}