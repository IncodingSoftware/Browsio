namespace Browsio.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;
    using Incoding.MvcContrib;
    using Incoding.Maybe;

    #endregion

    public class GetUserDetailQuery : QueryBase<GetUserDetailQuery.Response>
    {
        #region Properties

        public string Id { get; set; }

        public string SelectedStoreId { get; set; }

        #endregion

        #region Nested classes

        public class Response
        {
            #region Properties

            public string FullName { get; set; }

            public string Id { get; set; }

            public List<KeyValueVm> Stores { get; set; }

            public bool IsOwner { get; set; }

            public int Following { get; set; }

            #endregion
        }

        #endregion

        protected override Response ExecuteResult()
        {
            var user = Repository.GetById<User>(Id);
            return new Response
                       {
                           Id = user.Id.ToString(),
                           IsOwner = Id == BrowsioApp.UserId,
                           FullName = user.FullName,
                           Stores = user.Stores.Select(r => new KeyValueVm(r.Id, r.Name))
                                        .ToList(),
                           Following = user.Followers.Count
                       };
        }
    }
}