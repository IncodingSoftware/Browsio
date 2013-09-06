namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(SearchItemByQueryWhereSpec))]
    public class When_search_item_by_query
    {
        #region Establish value

        static IQueryable<SearchItem> fakeCollection;

        static List<SearchItem> filterCollection;

        static string fiftyFifty;

        static string fullQuery;

        #endregion

        Establish establish = () =>
                                  {
                                      Func<string, SearchItem> createEntity = (query) => Pleasure.MockAsObject<SearchItem>(mock => mock.SetupGet(r => r.Query).Returns(query));

                                      fullQuery = Pleasure.Generator.String();
                                      fiftyFifty = fullQuery.Substring(1, fullQuery.Length / 2);

                                      fakeCollection = Pleasure.ToQueryable(createEntity(fullQuery), createEntity(fiftyFifty), createEntity(Pleasure.Generator.String()));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new SearchItemByQueryWhereSpec(fiftyFifty).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(2);
                                      filterCollection[0].Query.ShouldEqual(fullQuery);
                                      filterCollection[1].Query.ShouldEqual(fiftyFifty);
                                  };
    }
}