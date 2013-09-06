namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Collections.Generic;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(GetSearchItemsQuery))]
    public class When_get_search_items
    {
        #region Establish value

        static MockMessage<GetSearchItemsQuery, List<SearchItem>> mockQuery;

        static List<SearchItem> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<GetSearchItemsQuery>();
                                      expected = Pleasure.ToList(Pleasure.Generator.Invent<SearchItem>());

                                      mockQuery = MockQuery<GetSearchItemsQuery, List<SearchItem>>
                                              .When(query)
                                              .StubQuery(whereSpecification: new SearchItemByQueryWhereSpec(query.Keyword), 
                                                         entities: expected.ToArray());
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result = () => mockQuery.ShouldBeIsResult(expected);
    }
}