namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using Browsio.Domain;
    using Incoding.Extensions;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(SearchStoreQuery))]
    public class When_search_store
    {
        #region Establish value

        static MockMessage<SearchStoreQuery, List<SearchStoreQuery.Response>> mockQuery;

        static List<Store> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<SearchStoreQuery>();
                                      expected = Pleasure.ToList(Pleasure.MockAsObject<Store>(mock =>
                                                                                                        {
                                                                                                            mock.SetupGet(r => r.Id).Returns(Pleasure.Generator.String());
                                                                                                            mock.SetupGet(r => r.Name).Returns(Pleasure.Generator.String());
                                                                                                            mock.SetupGet(r => r.User.FullName).Returns(Pleasure.Generator.String());
                                                                                                            mock.SetupGet(r => r.User.Id).Returns(Pleasure.Generator.String());
                                                                                                            mock.SetupGet(r => r.CategoryAsClass).Returns(Pleasure.Generator.String());
                                                                                                        }));

                                      mockQuery = MockQuery<SearchStoreQuery, List<SearchStoreQuery.Response>>
                                              .When(query)
                                              .StubQuery(whereSpecification: new StoreByCategoryOptWhereSpec(query.Category)
                                                                 .And(new StoreByGenreOptWhereSpec(query.GenreId))
                                                                 .And(new StoreByIdsOptWhereSpec(query.StoreIds)),
                                                         fetchSpecification: new StoreWithUserFetchSpec(),
                                                         entities: expected.ToArray());
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result
                = () => mockQuery.ShouldBeIsResult(list => list.ShouldEqualWeakEach(expected,
                                                                                    (dsl, i) => dsl.ForwardToValue(r => r.UserId, expected[i].User.Id.ToString())
                                                                                                   .ForwardToValue(r => r.User, expected[i].User.FullName)));
    }
}