namespace Browsio.Domain
{
    using System.Collections.Generic;
    using Incoding.CQRS;
    using System.Linq;

    public class GetStoreIdsByFriends : QueryBase<string[]>
    {
        protected override string[] ExecuteResult()
        {
            List<string> storeIds = new List<string>();
            var currentUser = Repository.GetById<User>(BrowsioApp.UserId);
            foreach (var friend in currentUser.Friends)
            {
                storeIds.AddRange(friend.Stores.ToList().Select(store => store.Id.ToString()).ToList());
            }
            return storeIds.ToArray();
        }
    }
}