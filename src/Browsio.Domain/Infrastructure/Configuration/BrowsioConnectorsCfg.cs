namespace Browsio.Domain
{
    using System.Collections.Generic;
    using System.Configuration;

    public class BrowsioConnectorsCfg
    {
        public KeyValuePair<string, string> Facebook { get { return GetCredential("AppFacebook"); } }

        public KeyValuePair<string, string> Twitter { get { return GetCredential("AppTwitter"); } }

        static KeyValuePair<string, string> GetCredential(string key)
        {
            string[] split = ConfigurationManager.AppSettings[key].Split(" ".ToCharArray());
            return new KeyValuePair<string, string>(split[0], split[1]);
        }
    }
}
