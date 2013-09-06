namespace Browsio
{
    #region << Using >>

    using System.Web.Mvc;

    #endregion

    public class FilterConfig
    {
        ////ncrunch: no coverage start

        #region Factory constructors

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        #endregion

        ////ncrunch: no coverage end    
    }
}