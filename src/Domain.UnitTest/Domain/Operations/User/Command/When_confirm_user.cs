namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    #endregion

    [Subject(typeof(ConfirUserCommand))]
    public class When_confirm_user
    {
        #region Establish value

        static MockMessage<ConfirUserCommand, object> mockCommand;

        static Mock<User> user;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<ConfirUserCommand>();
                                      user = Pleasure.MockStrict<User>(mock => mock.SetupSet(r => r.Activated = true));

                                      mockCommand = MockCommand<ConfirUserCommand>
                                              .When(command)
                                              .StubGetById(command.UserId, user.Object);
                                  };

        Because of = () => mockCommand.Original.Execute();

        It should_be_verify = () => user.VerifyAll();
    }
}