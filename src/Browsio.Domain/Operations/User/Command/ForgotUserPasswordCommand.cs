namespace Browsio.Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding;
    using Incoding.CQRS;
    using Incoding.Extensions;

    #endregion

    public class ForgotUserPasswordCommand : CommandBase
    {
        #region Properties

        public string Email { get; set; }

        public string Token { get; set; }

        public string ChangePasswordUrl { get; set; }

        #endregion

        public override void Execute()
        {
            var user = Repository.Query(whereSpecification: new UserByLoginWhereSpec(Email)).SingleOrDefault();
            if (user == null)
                throw IncWebException.For<ForgotUserPasswordCommand>(r => r.Email, "User not exists");

            user.ResetToken = Token;
            EventBroker.Publish(new OnSendEmailEvent
                                    {
                                            To = Email, 
                                            Subject = "Your new random password from Browsio", 
                                            Body = "Please follow the link to change password : <a=href\"{0}\"".F(ChangePasswordUrl)
                                    });
        }
    }
}