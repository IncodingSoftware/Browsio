namespace Browsio.Domain
{
    #region << Using >>

    using DotNetOpenAuth.OAuth2;

    #endregion

    public class OAuthProviderParams
    {
        #region Properties

        public AuthorizationServerDescription Authorization { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string AccessTokenUrl { get; set; }

        public string[] Scope { get; set; }

        #endregion
    }
}