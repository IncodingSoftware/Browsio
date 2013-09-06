namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(AddUserCommand))]
    public class When_add_user_with_not_unique_login
    {
        #region Establish value

        static MockMessage<AddUserCommand, object> mockCommand;

        static IncWebException exception;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<AddUserCommand>();

                                      mockCommand = MockCommand<AddUserCommand>
                                              .When(command)
                                              .StubQuery(whereSpecification: new UserByLoginWhereSpec(command.Login),
                                                         entities: Pleasure.Generator.Invent<User>());
                                  };

        Because of = () => { exception = Catch.Exception(() => mockCommand.Original.Execute()) as IncWebException; };

        It should_be_exception = () =>
                                     {
                                         exception.Property.ShouldEqual("Login");
                                         exception.Message.ShouldNotBeEmpty();
                                     };
    }
}