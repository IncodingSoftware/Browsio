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

    [Subject(typeof(GetStoreDetailQuery))]
    public class When_get_store_detail
    {
        #region Establish value

        static MockMessage<GetStoreDetailQuery, GetStoreDetailQuery.Response> mockQuery;

        static Store expected;

        static string userFullName;

        static string userId;

        static Mock<ISessionContext> sessionContext;

        static bool isUserFollow;

        static User expectedFollowerUser;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<GetStoreDetailQuery>();

                                      userFullName = Pleasure.Generator.String();
                                      userId = Pleasure.Generator.String();
                                      expectedFollowerUser = Pleasure.Generator.Invent<User>();
                                      sessionContext = Pleasure.Mock<ISessionContext>();
                                      IoCFactory.Instance.StubTryResolve(sessionContext.Object);
                                      isUserFollow = false;
                                      expected = Pleasure.Generator.Invent<Store>(dsl => dsl.Tuning(r => r.User, Pleasure.MockAsObject<User>(mock =>
                                                                                                                                                       {
                                                                                                                                                           mock.SetupGet(r => r.FullName).Returns(userFullName);
                                                                                                                                                           mock.SetupGet(r => r.Id).Returns(userId);
                                                                                                                                                       })));

                                      mockQuery = MockQuery<GetStoreDetailQuery, GetStoreDetailQuery.Response>
                                              .When(query)
                                              .StubGetById(query.Id, expected)
                                              .StubGetById<User>(sessionContext.Object.UserId, expectedFollowerUser);
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result
                = () => mockQuery.ShouldBeIsResult(response => response.ShouldEqualWeak(expected,
                                                                                        dsl => dsl.ForwardToValue(r => r.UserId, userId)
                                                                                                  .ForwardToValue(r => r.User, userFullName)
                                                                                                  .ForwardToValue(r => r.IsUserFollow, isUserFollow)
                                                                                                  .ForwardToValue(r => r.VisitorUserId, expectedFollowerUser.Id.ToString())));
    }
}