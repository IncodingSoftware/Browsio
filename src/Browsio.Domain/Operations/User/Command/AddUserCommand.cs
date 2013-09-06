namespace Browsio.Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding;
    using Incoding.CQRS;
    using Incoding.Extensions;
    using Incoding.Maybe;

    #endregion

    public class AddUserCommand : CommandBase
    {
        #region Properties

        public string Login { get; set; }

        public string Password { get; set; }

        public string RetryPassword { get; set; }

        public string PathResult { get; set; }

        #endregion

        public override void Execute()
        {
            if (Repository.Query(whereSpecification: new UserByLoginWhereSpec(Login))
                          .Any())
                throw IncWebException.For<AddUserCommand>(r => r.Login, "Login not unique");

            var user = new User
                           {
                                   Login = Login, 
                                   Password = Password, 
                                   Activated = false
                           };
            Repository.Save(user);

            string urlConfirm = PathResult + "?UserId={0}".F(user.Id.ReturnOrDefault(r => r, "Debug"));
            EventBroker.Publish(new OnSendEmailEvent
                                    {
                                            To = Login, 
                                            Subject = "Confirm registration", 
                                            Body = "Please confirm email {0}".F(urlConfirm)
                                    });

            EventBroker.Publish(new OnChangeSearchItemEvent(user));
        }
    }
}