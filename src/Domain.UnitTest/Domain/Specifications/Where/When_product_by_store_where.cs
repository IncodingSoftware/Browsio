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

    [Subject(typeof(ProductByStoreWhereSpec))]
    public class When_product_by_store_where
    {
        #region Establish value

        static IQueryable<Product> fakeCollection;

        static List<Product> filterCollection;

        #endregion

        Establish establish = () =>
                                  {
                                      Func<string, Product> createEntity = (storeId) => Pleasure.MockAsObject<Product>(mock => mock.SetupGet(r => r.Store.Id).Returns(storeId));

                                      fakeCollection = Pleasure.ToQueryable(createEntity(Pleasure.Generator.TheSameString()), createEntity(Pleasure.Generator.String()));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new ProductByStoreWhereSpec(Pleasure.Generator.TheSameString()).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(1);
                                      filterCollection[0].Store.Id.ShouldEqual(Pleasure.Generator.TheSameString());
                                  };
    }
}