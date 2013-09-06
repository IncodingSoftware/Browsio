namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.Extensions;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(GetUserByCredentialQuery))]
    public class When_get_user_by_credential
    {
        #region Establish value

        static MockMessage<GetUserByCredentialQuery, User> mockQuery;

        static User expected;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<GetUserByCredentialQuery>();
                                      expected = Pleasure.Generator.Invent<User>();

                                      mockQuery = MockQuery<GetUserByCredentialQuery, User>
                                              .When(query)
                                              .StubQuery(whereSpecification: new UserByLoginWhereSpec(query.Login)
                                                                 .And(new UserByPasswordWhereSpec(query.Password)),
                                                         entities: expected);
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result = () => mockQuery.ShouldBeIsResult(expected);
    }
}