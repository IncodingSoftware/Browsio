namespace Browsio.UnitTest.Domain
{
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    [Subject(typeof(ExistUserByTokenQuery))]
    public class When_exist_user_by_token_false
    {
        #region Establish value

        static MockMessage<ExistUserByTokenQuery, IncStructureResponse<bool>> mockQuery;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<ExistUserByTokenQuery>();

                                      mockQuery = MockQuery<ExistUserByTokenQuery, IncStructureResponse<bool>>
                                              .When(query)
                                              .StubEmptyQuery(whereSpecification: new UserByResetTokenWhereSpec(query.ResetToken));
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result = () => mockQuery.ShouldBeIsResult(new IncStructureResponse<bool>(false));
    }
}