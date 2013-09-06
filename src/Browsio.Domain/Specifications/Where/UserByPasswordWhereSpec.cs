namespace Browsio.Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class UserByPasswordWhereSpec : Specification<User>
    {
        readonly string password;

        public UserByPasswordWhereSpec(string password)
        {
            this.password = password;
        }

        public override Expression<Func<User, bool>> IsSatisfiedBy()
        {
            return user => user.Password == this.password;
        }
    }
}