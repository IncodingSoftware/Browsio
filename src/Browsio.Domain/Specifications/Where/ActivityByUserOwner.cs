using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using System.Linq.Expressions;
    using Incoding;

    public class ActivityByUserOwner : Specification<Activity>
    {
        public string UserOwnerId { get; set; }

        public ActivityByUserOwner(string userOwnerId)
            : base()
        {
            UserOwnerId = userOwnerId;
        }

        public override Expression<Func<Activity, bool>> IsSatisfiedBy()
        {
            return r => r.ActivityToUser.Id == UserOwnerId;
        }
    }
}
