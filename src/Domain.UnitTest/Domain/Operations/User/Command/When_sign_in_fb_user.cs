namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.Block.IoC;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    #endregion

    [Subject(typeof(SignInFbUserCommand))]
    public class When_sign_in_fb_user
    {
        #region Establish value

        static MockMessage<SignInFbUserCommand, object> mockCommand;

        static User user;

        static Mock<IFormAuthentication> formAuthentication;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<SignInFbUserCommand>();

                                      user = Pleasure.Generator.Invent<User>();
                                      formAuthentication = Pleasure.MockStrict<IFormAuthentication>(mock => mock.Setup(r => r.SignIn(user.Id.ToString(), true)));
                                      IoCFactory.Instance.StubTryResolve(formAuthentication.Object);

                                      mockCommand = MockCommand<SignInFbUserCommand>
                                              .When(command)
                                              .StubQuery(whereSpecification: new UserByFBIdWhereSpec(command.FBId), 
                                                         entities: user);
                                  };

        Because of = () => mockCommand.Original.Execute();

        It should_be_verify = () => formAuthentication.VerifyAll();
    }
}