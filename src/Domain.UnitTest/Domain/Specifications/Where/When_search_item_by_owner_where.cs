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

    [Subject(typeof(SearchItemByOwnerWhereSpec))]
    public class When_search_item_by_owner_where
    {
        #region Establish value

        static IQueryable<SearchItem> fakeCollection;

        static List<SearchItem> filterCollection;

        #endregion

        Establish establish = () =>
                                  {
                                      Func<string, SearchItem> createEntity = (ownerId) => Pleasure.MockAsObject<SearchItem>(mock => mock.SetupGet(r => r.OwnerId).Returns(ownerId));

                                      fakeCollection = Pleasure.ToQueryable(createEntity(Pleasure.Generator.String()), createEntity(Pleasure.Generator.TheSameString()));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new SearchItemByOwnerWhereSpec(Pleasure.Generator.TheSameString()).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(1);
                                      filterCollection[0].OwnerId.ShouldBeTheSameString();
                                  };
    }
}