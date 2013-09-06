namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(ForgotUserPasswordCommand))]
    public class When_forgot_user_password_with_wrong_login
    {
        #region Establish value

        static MockMessage<ForgotUserPasswordCommand, object> mockCommand;

        static IncWebException exception;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<ForgotUserPasswordCommand>();

                                      mockCommand = MockCommand<ForgotUserPasswordCommand>
                                              .When(command)
                                              .StubQuery(whereSpecification: new UserByLoginWhereSpec(command.Email), 
                                                         entities: null);
                                  };

        Because of = () => { exception = Catch.Exception(() => mockCommand.Original.Execute()) as IncWebException; };

        It should_be_exception = () => exception.Should(webException =>
                                                            {
                                                                webException.Message.ShouldNotBeEmpty();
                                                                webException.Property.ShouldEqual("Email");
                                                            });
    }
}