namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(ExistFacebookUserQuery))]
    public class When_exist_facebook_user
    {
        #region Establish value

        static MockMessage<ExistFacebookUserQuery, IncStructureResponse<bool>> mockQuery;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<ExistFacebookUserQuery>();

                                      mockQuery = MockQuery<ExistFacebookUserQuery, IncStructureResponse<bool>>
                                              .When(query)
                                              .StubNotEmptyQuery(whereSpecification: new UserByFBIdWhereSpec(query.FBId));
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result = () => mockQuery.ShouldBeIsResult(new IncStructureResponse<bool>(true));
    }
}