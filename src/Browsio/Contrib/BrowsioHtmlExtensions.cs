namespace Browsio.Contrib
{
    #region << Using >>

    using System.Web.Mvc;
    using Incoding.MvcContrib;

    #endregion

    ////ncrunch: no coverage start
    public static class BrowsioHtmlExtensions
    {
        #region Factory constructors

        public static BrowsioHtmlHelper<TModel> Browsio<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return new BrowsioHtmlHelper<TModel>(htmlHelper);
        }

        public static BrowsioIncodingHelper Browsio(this IIncodingMetaLanguageCallbackInstancesDsl dsl)
        {
            return new BrowsioIncodingHelper(dsl);
        }

        #endregion
    }

    ////ncrunch: no coverage end
}