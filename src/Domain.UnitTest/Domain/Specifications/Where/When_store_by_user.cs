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

    [Subject(typeof(StoreByUserWhereSpec))]
    public class When_store_by_user
    {
        #region Establish value

        static IQueryable<Store> fakeCollection;

        static List<Store> filterCollection;

        #endregion

        Establish establish = () =>
                                  {
                                      Func<string, Store> createEntity = (userId) => Pleasure.MockAsObject<Store>(mock => mock.SetupGet(r => r.User.Id).Returns(userId));

                                      fakeCollection = Pleasure.ToQueryable(createEntity(Pleasure.Generator.TheSameString()), createEntity(Pleasure.Generator.String()));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new StoreByUserWhereSpec(Pleasure.Generator.TheSameString()).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(1);
                                      filterCollection[0].User.Id.ShouldEqual(Pleasure.Generator.TheSameString());
                                  };
    }
}