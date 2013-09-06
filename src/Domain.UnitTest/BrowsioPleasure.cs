namespace Browsio.UnitTest
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.Block.IoC;
    using Incoding.MSpecContrib;

    #endregion

    public static class BrowsioPleasure
    {
        #region Properties

        public static string UserId
        {
            get
            {
                const string userId = "userId";
                IoCFactory.Instance.StubTryResolve(Pleasure.MockStrictAsObject<ISessionContext>(mock => mock.SetupGet(r => r.UserId).Returns(userId)));
                return userId;
            }
        }

        #endregion
    }
}