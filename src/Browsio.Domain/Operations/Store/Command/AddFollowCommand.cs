using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using Incoding.CQRS;

    public class AddFollowCommand : CommandBase
    {
        public string UserId { get; set; }

        public string StoreId { get; set; }

        public override void Execute()
        {
            var visitorUser = this.Repository.GetById<User>(this.UserId);
            var store = this.Repository.GetById<Store>(this.StoreId);
            if (store.Followers.All(r => r.Id.ToString() != this.UserId))
                store.AddFollower(visitorUser);
            string objectActivity = "\"" + store.Name + "\"";
            string title = visitorUser.FullName + " followed";
            EventBroker.Publish(new OnActivity
                                    {
                                        Title = title,
                                        Type = ActivityOfType.Browsio,
                                        Description = "",
                                        ObjectActivity = objectActivity,
                                        ActivityToUser = store.User,
                                        ActivityFromUser = visitorUser
                                    });
        }
    }
}
