namespace Browsio.Domain
{
    #region << Using >>

    using System.Web;
    using System.Web.Mvc;
    using Incoding.Block.IoC;
    using Incoding.MvcContrib;

    #endregion

    ////ncrunch: no coverage start

    public class BrowsioAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var urlHelper = new UrlHelper(filterContext.RequestContext);
            string url = urlHelper.Action("Index", "Browsio", new { area = "" });
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = IncodingResult.RedirectTo(url);
                return;
            }
            filterContext.Result = new RedirectResult(url);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var formAuthentication = IoCFactory.Instance.TryResolve<IFormAuthentication>();
            return formAuthentication.IsAuthentication();
        }
    }

    ////ncrunch: no coverage end
}