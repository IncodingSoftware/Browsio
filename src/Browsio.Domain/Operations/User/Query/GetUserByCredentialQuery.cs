namespace Browsio.Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding.CQRS;
    using Incoding.Extensions;

    #endregion

    public class GetUserByCredentialQuery : QueryBase<User>
    {
        #region Properties

        public string Login { get; set; }

        public string Password { get; set; }

        #endregion

        protected override User ExecuteResult()
        {
            return this.Repository.Query(whereSpecification: new UserByLoginWhereSpec(this.Login)
                                            .And(new UserByPasswordWhereSpec(this.Password)))
                             .SingleOrDefault();
        }
    }
}