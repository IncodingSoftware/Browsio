namespace Browsio.Domain
{
    #region << Using >>

    using System.ComponentModel;

    #endregion

    public enum CategoryOfType
    {
        [Description("Books")]
        Book, 

        [Description("Movies")]
        Movie, 

        [Description("TV Shows")]
        TVShow, 

        [Description("Video Games")]
        VideoGame
    }
}