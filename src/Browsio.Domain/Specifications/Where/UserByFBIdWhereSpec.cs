namespace Browsio.Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class UserByFBIdWhereSpec:Specification<User>
    {
        readonly string fbId;

        public UserByFBIdWhereSpec(string fbId)
        {
            this.fbId = fbId;
        }

        public override Expression<Func<User, bool>> IsSatisfiedBy()
        {
            return user => user.FbId == this.fbId;
        }
    }
}