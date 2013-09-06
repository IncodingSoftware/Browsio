namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using System;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(GetUserDetailQuery))]
    public class When_get_user_detial
    {
        #region Establish value

        static MockMessage<GetUserDetailQuery, GetUserDetailQuery.Response> mockQuery;

        static User expected;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<GetUserDetailQuery>(dsl => dsl.Tuning(r => r.Id, BrowsioPleasure.UserId));
                                      expected = Pleasure.MockAsObject<User>(mock =>
                                                                                       {
                                                                                           mock.SetupGet(r => r.Id).Returns(Pleasure.Generator.String());
                                                                                           mock.SetupGet(r => r.FullName).Returns(Pleasure.Generator.String());
                                                                                           mock.SetupGet(r => r.Stores).Returns(Pleasure.ToReadOnly(Pleasure.Generator.Invent<Store>(),
                                                                                                                                                    Pleasure.Generator.Invent<Store>()));

                                                                                           mock.SetupGet(r => r.Followers).Returns(Pleasure.ToReadOnly(Pleasure.Generator.Invent<Store>(),
                                                                                                                                                       Pleasure.Generator.Invent<Store>(),
                                                                                                                                                       Pleasure.Generator.Invent<Store>()));
                                                                                       });

                                      mockQuery = MockQuery<GetUserDetailQuery, GetUserDetailQuery.Response>
                                              .When(query)
                                              .StubGetById(query.Id, expected);
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result
                = () =>
                      {
                          Action<GetUserDetailQuery.Response> verifyStores = r => r.Stores.ShouldEqualWeakEach(expected.Stores,
                                                                                                               (factoryDsl, i) => factoryDsl.IgnoreBecauseNotUse(s => s.Selected)
                                                                                                                                            .IgnoreBecauseNotUse(s => s.Title)
                                                                                                                                            .ForwardToValue(s => s.Value, expected.Stores[i].Id.ToString())
                                                                                                                                            .ForwardToValue(s => s.Text, expected.Stores[i].Name));
                          mockQuery.ShouldBeIsResult(response => response.ShouldEqualWeak(expected, dsl => dsl.ForwardToValue(r => r.Id, expected.Id.ToString())
                                                                                                              .ForwardToValue(r => r.IsOwner, true)
                                                                                                              .ForwardToAction(r => r.Stores, verifyStores)
                                                                                                              .ForwardToValue(r => r.Following, 3)
                                                                                                              .ForwardToValue(r => r.FullName, expected.FullName)));
                      };
    }
}