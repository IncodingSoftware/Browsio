namespace Browsio.Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class UserByLoginWhereSpec:Specification<User>
    {
        readonly string login;

        public UserByLoginWhereSpec(string login)
        {
            this.login = login;
        }

        public override Expression<Func<User, bool>> IsSatisfiedBy()
        {
            return user => user.Login == this.login;
        }
    }
}