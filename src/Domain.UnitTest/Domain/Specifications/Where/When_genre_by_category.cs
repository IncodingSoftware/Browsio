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

    [Subject(typeof(GenreByCategoryOptWhereSpec))]
    public class When_genre_by_category
    {
        #region Establish value

        static IQueryable<Genre> fakeCollection;

        static List<Genre> filterCollection;

        static CategoryOfType exists;

        #endregion

        Establish establish = () =>
                                  {
                                      Func<CategoryOfType, Genre> createEntity = (category) => Pleasure.MockAsObject<Genre>(mock => mock.SetupGet(r => r.Category).Returns(category));

                                      exists = Pleasure.Generator.Enum<CategoryOfType>();
                                      fakeCollection = Pleasure.ToQueryable(createEntity(exists), createEntity(exists.Inverse<CategoryOfType>()));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new GenreByCategoryOptWhereSpec(exists).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(1);
                                      filterCollection[0].Category.ShouldEqual(exists);
                                  };

        It should_be_opt = () => new GenreByCategoryOptWhereSpec(null).IsSatisfiedBy().ShouldBeNull();
    }
}