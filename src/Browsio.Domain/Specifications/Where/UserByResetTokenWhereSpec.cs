namespace Browsio.Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class UserByResetTokenWhereSpec:Specification<User>
    {

        readonly string ResetToken;

        public UserByResetTokenWhereSpec(string resetTokin)
        {
            this.ResetToken = resetTokin;
        }

        public override Expression<Func<User, bool>> IsSatisfiedBy()
        {
            return user => user.ResetToken == this.ResetToken;
        }
    }
}