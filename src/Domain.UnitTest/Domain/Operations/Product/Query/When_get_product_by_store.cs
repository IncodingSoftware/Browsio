namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using Browsio.Domain;
    using Incoding.Extensions;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(GetProductByStoreQuery))]
    public class When_get_product_by_store
    {
        #region Establish value

        static MockMessage<GetProductByStoreQuery, List<Product>> mockQuery;

        static List<Product> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<GetProductByStoreQuery>();
                                      expected = Pleasure.ToList(Pleasure.Generator.Invent<Product>());

                                      mockQuery = MockQuery<GetProductByStoreQuery, List<Product>>
                                              .When(query)
                                              .StubQuery(whereSpecification: new ProductByStoreWhereSpec(query.StoreId)
                                                                 .And(new ProductByNameOptWhereSpec(query.Content)), 
                                                         entities: expected.ToArray());
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result = () => mockQuery.ShouldBeIsResult(expected);
    }
}