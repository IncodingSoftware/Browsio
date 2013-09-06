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

    [Subject(typeof(ProductByASINWhereSpec))]
    public class When_product_by_asin
    {
        #region Establish value

        static IQueryable<Product> fakeCollection;

        static List<Product> filterCollection;

        #endregion

        Establish establish = () =>
                                  {
                                      Func<string, Product> createEntity = (asin) => Pleasure.MockAsObject<Product>(mock => mock.SetupGet(r => r.Asin).Returns(asin));

                                      fakeCollection = Pleasure.ToQueryable(createEntity(Pleasure.Generator.String()), createEntity(Pleasure.Generator.TheSameString()));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new ProductByASINWhereSpec(Pleasure.Generator.TheSameString()).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(1);
                                      filterCollection[0].Asin.ShouldBeTheSameString();
                                  };
    }
}