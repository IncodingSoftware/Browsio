namespace Browsio.UnitTest.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;


    [Subject(typeof(StoreByIdsOptWhereSpec))]
    public class When_store_by_ids_opt
    {
        #region Establish value

        static IQueryable<Store> fakeCollection;

        static List<Store> filterCollection;

        #endregion

        Establish establish = () =>
                                  {

                                      Func<string, Store> createEntity = (id) => Pleasure.MockAsObject<Store>(mock => mock.SetupGet(r => r.Id).Returns(id));

                                      idExist1 = Pleasure.Generator.String();
                                      idExist2 = Pleasure.Generator.String();

                                      fakeCollection = Pleasure.ToQueryable(createEntity(idExist1),
                                          createEntity(idExist2),
                                          createEntity(Pleasure.Generator.String()),
                                          createEntity(Pleasure.Generator.String()));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new StoreByIdsOptWhereSpec(new[] { idExist1, idExist2 }).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(2);
                                      filterCollection[0].Id.ShouldEqual(idExist1);
                                      filterCollection[1].Id.ShouldEqual(idExist2);
                                  };

        It should_be_opt = () => new StoreByIdsOptWhereSpec(null).IsSatisfiedBy().ShouldBeNull();

        static string idExist1;

        static string idExist2;
    }
}