namespace Browsio.Domain
{
    #region << Using >>

    using System.Web;
    using Incoding.Block.IoC;
    using Incoding.CQRS;

    #endregion

    public class GetStoreDetailQuery : QueryBase<GetStoreDetailQuery.Response>
    {
        #region Properties

        public string Id { get; set; }

        #endregion

        #region Nested classes

        public class Response
        {
            #region Properties

            public string Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public string User { get; set; }

            public string UserId { get; set; }

            public bool IsUserFollow { get; set; }

            public string VisitorUserId { get; set; }

            #endregion
        }

        #endregion

        protected override Response ExecuteResult()
        {
            var store = Repository.GetById<Store>(Id);
            var sessionContext = IoCFactory.Instance.TryResolve<ISessionContext>();
            var visitorUser = this.Repository.GetById<User>(sessionContext.UserId);
            string visitorUserId = "";
            if (visitorUser != null)
                visitorUserId = visitorUser.Id.ToString();
            var isUserFollow = store.Followers.Contains(visitorUser);
            return new Response
                       {
                               Id = store.Id.ToString(), 
                               Name = store.Name, 
                               Description = store.Description, 
                               User = store.User.FullName, 
                               UserId = store.User.Id.ToString(),
                               IsUserFollow = isUserFollow,
                               VisitorUserId = visitorUserId
                       };
        }
    }
}