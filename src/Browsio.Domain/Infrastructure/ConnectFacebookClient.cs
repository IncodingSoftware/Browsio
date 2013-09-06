namespace Browsio.Domain
{
    using System.Collections.Generic;
    using Facebook;
    using Incoding.Extensions;
    using Incoding.Maybe;

    public class ConnectFacebookClient : IConnectProviderClient
    {
        public ISocialUser Client(string accessToken)
        {
            var client = new FacebookClient(accessToken);
            var me = client.Get("me") as JsonObject;
            return new FacebookUser
                                   {
                                           Id = me["id"].ToString(),
                                           EMail = me.GetOrDefault("email")
                                                       .With(r => r.ToString())
                                                       .Recovery(() => me.GetOrDefault("username").With(r => r.ToString())),
                                           FirstName = me.GetOrDefault("first_name")
                                                           .With(r => r.ToString()),
                                           LastName = me.GetOrDefault("last_name")
                                                          .With(r => r.ToString()),
                                           Gender = me.GetOrDefault("gender")
                                                     .With(r => r.ToString()) == "male" ? SexOfType.Men : SexOfType.Women,
                                           Friends = GetFriends(client)
                                   }; 
        }
        IList<ISocial> GetFriends(FacebookClient fbClient)
        {
            IList<ISocial> friends = new List<ISocial>();
            var dataFriends = fbClient.Get("me/friends") as dynamic;
            ViewModel.User user;
            foreach (var dataFriend in dataFriends["data"])
            {
                user = new ViewModel.User();
                user.Id = dataFriend.id;
                friends.Add(user);
            }
                return friends;
        }
    }
}
