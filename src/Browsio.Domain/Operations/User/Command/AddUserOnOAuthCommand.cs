namespace Browsio.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using Incoding.CQRS;
    using System.Linq;
    using FluentNHibernate.Conventions;

    #endregion

    public class AddUserOnOAuthCommand : CommandBase
    {
        #region Properties

        public ConnectorOfType ConnectorType { get; set; }

        public string AccessToken { get; private set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public SexOfType Sex { get; set; }

        public string FbId { get; set; }

        public IEnumerable<ISocial> Friends { get; set; }

        #endregion

        public override void Execute()
        {
            var user = new User
                           {
                                   AccessToken = AccessToken, 
                                   Login = Login, 
                                   FirstName = FirstName, 
                                   FbId = FbId, 
                                   LastName = LastName, 
                                   Sex = Sex, 
                                   Activated = true
                           };
            foreach (var friend in Friends)
            {
                var friendUser = Repository.Query(whereSpecification: new UserByFBIdWhereSpec(friend.Id)).FirstOrDefault();
                if (friendUser != null)
                {
                    user.AddFriend(friendUser);
                    friendUser.AddFriend(user);
                }
            }
            Repository.Save(user);
            EventBroker.Publish(new OnChangeSearchItemEvent(user));
        }
    }
}