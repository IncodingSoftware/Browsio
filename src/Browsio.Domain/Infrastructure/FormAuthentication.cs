namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Web;
    using System.Web.Security;
    using Incoding.CQRS;

    #endregion

    ////ncrunch: no coverage start
    public class FormAuthentication : IFormAuthentication
    {
        #region Fields

        readonly IDispatcher dispatcher;

        readonly HttpContextBase httpContext;

        #endregion

        #region Constructors

        public FormAuthentication(IDispatcher dispatcher, HttpContextBase httpContext)
        {
            this.dispatcher = dispatcher;
            this.httpContext = httpContext;
        }

        #endregion

        #region IFormAuthentication Members

        public void SignIn(string id,bool persistence = true)
        {
            const int countPersistence = 10;
            var expiration = persistence ? DateTime.Now.AddDays(countPersistence) : DateTime.Now.AddMinutes(countPersistence);
            string encryptTick = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(2, id, DateTime.Now, expiration, true, string.Empty));

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTick);
            cookie.Expires = expiration;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public string GetUserData()
        {
            if (HttpContext.Current != null)
            {
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null && cookie.Value != null)
                {
                    try
                    {
                        var ticket = FormsAuthentication.Decrypt(cookie.Value);
                        if (ticket != null)
                            return ticket.UserData;
                    }
                    catch { }
                }
            }

            return null;
        }

        public bool IsAuthentication()
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            var user = this.dispatcher.Query(new GetEntityByIdQuery<User>(BrowsioApp.UserId));
            bool hasUser = user != null;

            if (!hasUser)
                SignOut();

            return hasUser;
        }

        #endregion
    }

    ////ncrunch: no coverage end
}