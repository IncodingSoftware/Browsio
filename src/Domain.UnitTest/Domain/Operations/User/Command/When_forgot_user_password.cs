namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    #endregion

    [Subject(typeof(ForgotUserPasswordCommand))]
    public class When_forgot_user_password
    {
        #region Establish value

        static MockMessage<ForgotUserPasswordCommand, object> mockCommand;

        static Mock<User> user;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<ForgotUserPasswordCommand>();
                                      user = Pleasure.MockStrict<User>(mock => mock.SetupSet(r => r.ResetToken = command.Token));

                                      mockCommand = MockCommand<ForgotUserPasswordCommand>
                                              .When(command)
                                              .StubQuery(whereSpecification: new UserByLoginWhereSpec(command.Email), 
                                                         entities: user.Object)
                                              .StubPublish<OnSendEmailEvent>(@event =>
                                                                                 {
                                                                                     @event.To.ShouldEqual(command.Email);
                                                                                     @event.Subject.ShouldNotBeEmpty();
                                                                                     @event.Body.ShouldNotBeEmpty();
                                                                                 });
                                  };

        Because of = () => mockCommand.Original.Execute();

        It should_be_published = () => mockCommand.ShouldBePublished();

        It should_be_verify = () => user.VerifyAll();
    }
}