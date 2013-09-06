namespace Browsio.Controllers
{
    using System.Web;

    public interface IFullUrlBuilder {
        string FromUrl(string action);
    }

    public class FullUrlBuilder : IFullUrlBuilder
    {
        public string FromUrl(string action)
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + action;
        }
    }
}