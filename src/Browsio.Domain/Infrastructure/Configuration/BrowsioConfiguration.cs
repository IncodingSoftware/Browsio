namespace Browsio.Domain
{
    public static class BrowsioConfiguration
    {
        public static BrowsioConnectorsCfg Connectors
        {
            get
            {
                return new BrowsioConnectorsCfg();
            }
        }
    }
}
