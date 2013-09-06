namespace Browsio.Domain
{
    #region << Using >>

    using System.Web;

    #endregion

    public interface IProductCommand
    {
        string Name { get; set; }

        string Description { get; set; }

        string Asin { get; set; }

        string Author { get; set; }

        float Price { get; set; }

        HttpPostedFileBase Image { get; set; }
    }
}