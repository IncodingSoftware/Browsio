namespace Browsio
{
    #region << Using >>

    using System.Web.Http;

    #endregion

    ////ncrunch: no coverage start

    public static class WebApiConfig
    {
        #region Factory constructors

        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                                       name: "DefaultApi",
                                       routeTemplate: "api/{controller}/{id}",
                                       defaults: new { id = RouteParameter.Optional }
                    );
        }

        #endregion
    }

    ////ncrunch: no coverage end
}