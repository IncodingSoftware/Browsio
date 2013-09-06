namespace Browsio.Contrib
{
    #region << Using >>

    using Incoding.MvcContrib;

    #endregion

    ////ncrunch: no coverage start
    public class BrowsioIncodingHelper
    {
        #region Fields

        readonly IIncodingMetaLanguageCallbackInstancesDsl dsl;

        #endregion

        #region Constructors

        public BrowsioIncodingHelper(IIncodingMetaLanguageCallbackInstancesDsl dsl)
        {
            this.dsl = dsl;
        }

        #endregion

        #region Api Methods

        public IExecutableSetting CenterDialog()
        {
            return SetDialogOption("position", "center");
        }

        #endregion

        IExecutableSetting SetDialogOption(string option, string value)
        {
            return this.dsl.Core().Eval("$(this.target).dialog('option', '" + option + "', '" + value + "');");
        }
    }

    ////ncrunch: no coverage end
}