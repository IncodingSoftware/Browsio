namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(ExistUserByTokenQuery))]
    public class When_exist_user_by_token
    {
        #region Establish value

        static MockMessage<ExistUserByTokenQuery, IncStructureResponse<bool>> mockQuery;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<ExistUserByTokenQuery>();

                                      mockQuery = MockQuery<ExistUserByTokenQuery, IncStructureResponse<bool>>
                                              .When(query)
                                              .StubQuery(whereSpecification: new UserByResetTokenWhereSpec(query.ResetToken), 
                                                         entities: Pleasure.Generator.Invent<User>());
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result = () => mockQuery.ShouldBeIsResult(new IncStructureResponse<bool>(true));
    }
}