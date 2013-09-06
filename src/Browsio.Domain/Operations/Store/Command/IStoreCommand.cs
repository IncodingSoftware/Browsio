namespace Browsio.Domain
{
    #region << Using >>

    using System.Web;

    #endregion

    public interface IStoreCommand
    {
        int Category { get; set; }

        string Description { get; set; }

        string Name { get; set; }

        HttpPostedFileBase Image { get; set; }

        string GenreId { get; set; }
    }
}