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

    [Subject(typeof(ProductByNameOptWhereSpec))]
    public class When_product_by_name_where
    {
        #region Establish value

        static IQueryable<Product> fakeCollection;

        static List<Product> filterCollection;

        static string fullWord;

        static string fiftyFifty;

        #endregion

        Establish establish = () =>
                                  {
                                      Func<string, Product> createEntity = (name) => Pleasure.MockAsObject<Product>(mock => mock.SetupGet(r => r.Name).Returns(name));

                                      fullWord = Pleasure.Generator.String();
                                      fiftyFifty = fullWord.Substring(0, fullWord.Length / 2);

                                      fakeCollection = Pleasure.ToQueryable(createEntity(fullWord), createEntity(fiftyFifty), createEntity(Pleasure.Generator.String()));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new ProductByNameOptWhereSpec(fiftyFifty).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(2);
                                      filterCollection[0].Name.ShouldEqual(fullWord);
                                      filterCollection[1].Name.ShouldEqual(fiftyFifty);
                                  };

        It should_be_opt = () => new ProductByNameOptWhereSpec(string.Empty).IsSatisfiedBy().ShouldBeNull();
    }
}