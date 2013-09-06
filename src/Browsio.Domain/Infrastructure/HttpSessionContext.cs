namespace Browsio.Domain
{
    #region << Using >>

    using System.Web;
    using Incoding.Block.IoC;

    #endregion

    ////ncrunch: no coverage start
    public class HttpSessionContext : ISessionContext
    {
        // public string UserId { get { return GetByKey("userId"); } set { SetByKey("userId", value); } }
        #region ISessionContext Members

        //public string UserId { get { return IoCFactory.Instance.TryResolve<IFormAuthentication>().GetUserData(); } }
        public string UserId { get { return HttpContext.Current.User.Identity.Name; } }

        #endregion
    }

    ////ncrunch: no coverage end
}