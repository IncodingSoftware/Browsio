namespace Browsio.Domain
{
    #region << Using >>

    using Incoding.Block.IoC;

    #endregion

    public static class BrowsioApp
    {        
        #region Properties

        public static string UserId { get { return IoCFactory.Instance.TryResolve<ISessionContext>().UserId; } }

        public static string AmazonStoreId { get { return IoCFactory.Instance.TryResolve<IAmazonConfig>().AmazonStoreId; } }

        #endregion
    }
}