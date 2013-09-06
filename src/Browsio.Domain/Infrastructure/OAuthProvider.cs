namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using DotNetOpenAuth.OAuth2;
    using Incoding.Extensions;
    using Incoding.Maybe;

    #endregion

    public class OAuthProvider : WebServerClient, IOAuthProvider
    {
        #region Fields

        readonly OAuthProviderParams providerParams;

        readonly int typeConnector;

        #endregion

        #region Constructors

        public OAuthProvider(OAuthProviderParams oAuthProviderParams, int typeConnector)
                : base(oAuthProviderParams.Authorization, oAuthProviderParams.AppId, oAuthProviderParams.AppSecret)
        {
            this.typeConnector = typeConnector;
            this.providerParams = oAuthProviderParams;
        }

        #endregion

        #region IOAuthProvider Members

        public string GetAuthorizationUri(Uri callbackUrl)
        {
            var respone = PrepareRequestUserAuthorization(this.providerParams.Scope, callbackUrl);
            return respone.Headers["location"].AppendToQueryString(new { access_type = "offline", approval_prompt = "force" });
        }

        public string GetAuthorizeToken(Dictionary<string, string> args)
        {
            var routes = new
                             {
                                     client_id = this.providerParams.AppId, 
                                     client_secret = this.providerParams.AppSecret, 
                                     redirect_uri = args["redirect_uri"], 
                                     code = args["code"], 
                                     grant_type = "authorization_code"
                             };

            string readToEnd = GetResponseStream(this.providerParams.AccessTokenUrl, routes);
            return readToEnd.Split("&".ToCharArray())[0].Split("=".ToCharArray())[1];
        }

        #endregion

        string GetResponseStream(string url, object routes)
        {
            var data = Encoding.UTF8.GetBytes(string.Empty
                                                    .AppendToQueryString(routes ?? new { })
                                                    .Replace("?", string.Empty));
            var webRequest = WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = data.Length;
            using (var streamRequest = webRequest.GetRequestStream())
                streamRequest.Write(data, 0, data.Length);

            var responseStream = webRequest
                    .ReturnOrDefault(r => r.GetResponse(), null)
                    .ReturnOrDefault(r => r.GetResponseStream(), Stream.Null);
            using (var reader = new StreamReader(responseStream))
            {
                string readToEnd = reader.ReadToEnd();
                return readToEnd;
            }
        }
    }
}