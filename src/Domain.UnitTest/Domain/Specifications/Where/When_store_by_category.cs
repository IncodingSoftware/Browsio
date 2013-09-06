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

    [Subject(typeof(StoreByCategoryOptWhereSpec))]
    public class When_store_by_category
    {
        #region Establish value

        static IQueryable<Store> fakeCollection;

        static List<Store> filterCollection;

        static CategoryOfType exist;

        #endregion

        Establish establish = () =>
                                  {
                                      Func<CategoryOfType, Store> createEntity = (category) => Pleasure.MockAsObject<Store>(mock => mock.SetupGet(r => r.Category).Returns(category));

                                      exist = Pleasure.Generator.Enum<CategoryOfType>();
                                      var notExist = exist.Inverse<CategoryOfType>();

                                      fakeCollection = Pleasure.ToQueryable(createEntity(exist), createEntity(notExist));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new StoreByCategoryOptWhereSpec(exist).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(1);
                                      filterCollection[0].Category.ShouldEqual(exist);
                                  };

        It should_be_opt = () => new StoreByCategoryOptWhereSpec(null).IsSatisfiedBy().ShouldBeNull();
    }
}