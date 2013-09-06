namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Collections.Generic;

    #endregion

    public interface IOAuthProvider
    {
        string GetAuthorizationUri(Uri callbackUrl);

        string GetAuthorizeToken(Dictionary<string, string> args);
    }
}