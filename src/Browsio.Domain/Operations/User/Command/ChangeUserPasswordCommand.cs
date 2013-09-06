namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Incoding.CQRS;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class ChangeUserPasswordCommand : CommandBase
    {
        #region Constructors

        [UsedImplicitly, Obsolete(ObsoleteMessage.SerializeConstructor, true), ExcludeFromCodeCoverage]
        public ChangeUserPasswordCommand() { }

        public ChangeUserPasswordCommand(string ResetToken)
        {
            this.ResetToken = ResetToken;
        }

        #endregion

        #region Properties

        public string ResetToken { get; set; }

        public string Password { get; set; }

        #endregion

        public override void Execute()
        {
            var user = Repository.Query(whereSpecification: new UserByResetTokenWhereSpec(ResetToken)).Single();
            user.ResetToken = null;
            user.Password = Password;
        }
    }
}