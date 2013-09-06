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

    [Subject(typeof(StoreByGenreOptWhereSpec))]
    public class When_store_by_genre_where
    {
        #region Establish value

        static IQueryable<Store> fakeCollection;

        static List<Store> filterCollection;

        #endregion

        Establish establish = () =>
                                  {
                                      Func<string, Store> createEntity = (genreId) => Pleasure.MockAsObject<Store>(mock => mock.SetupGet(r => r.Genre.Id).Returns(genreId));

                                      fakeCollection = Pleasure.ToQueryable(createEntity(Pleasure.Generator.String()), createEntity(Pleasure.Generator.TheSameString()));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new StoreByGenreOptWhereSpec(Pleasure.Generator.TheSameString()).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(1);
                                      filterCollection[0].Genre.Id.ShouldEqual(Pleasure.Generator.TheSameString());
                                  };

        It should_be_opt = () => new StoreByGenreOptWhereSpec(string.Empty).IsSatisfiedBy().ShouldBeNull();
    }
}