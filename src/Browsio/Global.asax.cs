namespace Browsio
{
    #region << Using >>

    using System.Globalization;
    using System.Threading;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Browsio.Domain;
    using FluentValidation.Mvc;
    using Incoding.Block.Logging;
    using Incoding.MvcContrib;

    #endregion

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        ////ncrunch: no coverage start
        protected void Application_Start()
        {
            var enCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = enCulture;
            Thread.CurrentThread.CurrentUICulture = enCulture;

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            ControllerBuilder.Current.SetControllerFactory(new IncControllerFactory());
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new IncValidatorFactory()));
            Bootstrapper.Start();
        }

        protected void Application_Error()
        {
            var lastEx = Server.GetLastError();
            LoggingFactory.Instance.LogException(LogType.Debug, lastEx);
        }

        ////ncrunch: no coverage end    
    }
}