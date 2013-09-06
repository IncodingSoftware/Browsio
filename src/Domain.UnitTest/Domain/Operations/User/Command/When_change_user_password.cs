namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    #endregion

    [Subject(typeof(ChangeUserPasswordCommand))]
    public class When_change_user_password
    {
        #region Establish value

        static MockMessage<ChangeUserPasswordCommand, object> mockCommand;

        static Mock<User> user;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<ChangeUserPasswordCommand>();
                                      user = Pleasure.MockStrict<User>(mock =>
                                                                           {
                                                                               mock.SetupSet(r => r.ResetToken = null);
                                                                               mock.SetupSet(r => r.Password = command.Password);
                                                                           });

                                      mockCommand = MockCommand<ChangeUserPasswordCommand>
                                              .When(command)
                                              .StubQuery(whereSpecification: new UserByResetTokenWhereSpec(command.ResetToken), 
                                                         entities: user.Object);
                                  };

        Because of = () => mockCommand.Original.Execute();

        It should_be_verify = () => user.VerifyAll();
    }
}