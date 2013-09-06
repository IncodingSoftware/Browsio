namespace Browsio.Controllers
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Browsio.Domain;
    using Facebook;
    using Incoding.Block.IoC;
    using Incoding.CQRS;
    using Incoding.Extensions;
    using Incoding.Maybe;
    using Incoding.MvcContrib;

    #endregion

    public class ConnectorController : IncControllerBase
    {
        #region Constructors

        public ConnectorController(IDispatcher dispatcher)
            : base(dispatcher) { }

        #endregion

        #region Http action

        [HttpGet]
        public JsonResult AddOAuth(int type)
        {            
            if (IoCFactory.Instance.TryResolve<IFormAuthentication>().IsAuthentication())
                return IncRedirectToAction("Index", "Browsio");


            var connectorOfType = (ConnectorOfType)type;
            IOAuthProvider oAuthProvider = IoCFactory.Instance.TryResolveByNamed<IOAuthProvider>(connectorOfType.ToString());
            var callbackUrl = GetCallbackOauthUri(connectorOfType);
            
            return IncRedirect(oAuthProvider.GetAuthorizationUri(callbackUrl));

        }

        [HttpGet]
        public ActionResult Success(ConnectorOfType connectorOfType)
        {
            string accessToken = IoCFactory.Instance.TryResolveByNamed<IOAuthProvider>(connectorOfType.ToString()).GetAuthorizeToken(GetExtendArgs(connectorOfType));
            var socialUser = IoCFactory.Instance.TryResolveByNamed<IConnectProviderClient>(connectorOfType.ToString()).Client(accessToken);
            if (!this.dispatcher.Query(new ExistFacebookUserQuery { FBId = socialUser.Id }).Value)
            {
                this.dispatcher.Push(new AddUserOnOAuthCommand
                                         {
                                             ConnectorType = connectorOfType,
                                             Login = socialUser.EMail,
                                             FirstName = socialUser.FirstName,
                                             LastName = socialUser.LastName,
                                             Sex = socialUser.Gender,
                                             FbId = socialUser.Id,
                                             Friends = socialUser.Friends
                                         });
            }

            return TryPush(new SignInFbUserCommand { FBId = socialUser.Id }, setting => setting.SuccessResult = () => RedirectToAction("Index", "Browsio"));
        }

        #endregion

        Dictionary<string, string> GetExtendArgs(ConnectorOfType connectorOfType)
        {
            var extendArgs = new Dictionary<string, string>();

            bool isOAuth = connectorOfType.IsAnyEquals(ConnectorOfType.Facebook);
            if (isOAuth)
            {
                extendArgs.Add("code", HttpContext.Request.QueryString["code"]);
                extendArgs.Add("redirect_uri", GetCallbackOauthUri(connectorOfType).ToString());
            }

            return extendArgs;
        }

        Uri GetCallbackOauthUri(ConnectorOfType connectorOfType)
        {
            return new Uri(Request.Url.Scheme + "://" + Request.Url.Authority + Url.Action("Success", "Connector", new { connectorOfType, type = "client_cred" }));
        }
    }
}