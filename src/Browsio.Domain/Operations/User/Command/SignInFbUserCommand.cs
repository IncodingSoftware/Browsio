namespace Browsio.Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding.Block.IoC;
    using Incoding.CQRS;

    #endregion

    public class SignInFbUserCommand : CommandBase
    {
        #region Properties

        public string FBId { get; set; }

        #endregion

        public override void Execute()
        {
            var user = Repository.Query(whereSpecification: new UserByFBIdWhereSpec(FBId)).Single();
            IoCFactory.Instance.TryResolve<IFormAuthentication>()
                      .SignIn(user.Id.ToString(), true);
        }
    }
}