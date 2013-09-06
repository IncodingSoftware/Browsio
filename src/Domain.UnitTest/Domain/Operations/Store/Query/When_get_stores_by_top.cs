namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using Browsio.Domain;
    using Incoding.Data;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(GetStoresByTopQuery))]
    public class When_get_stores_by_top
    {
        #region Establish value

        static MockMessage<GetStoresByTopQuery, List<Store>> mockQuery;

        static List<Store> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<GetStoresByTopQuery>();
                                      expected = Pleasure.ToList(Pleasure.Generator.Invent<Store>());

                                      mockQuery = MockQuery<GetStoresByTopQuery, List<Store>>
                                              .When(query)
                                              .StubQuery(paginatedSpecification: new PaginatedSpecification(1, 10), 
                                                         fetchSpecification: new StoreWithUserFetchSpec(), 
                                                         entities: expected.ToArray());
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result = () => mockQuery.ShouldBeIsResult(expected);
    }
}