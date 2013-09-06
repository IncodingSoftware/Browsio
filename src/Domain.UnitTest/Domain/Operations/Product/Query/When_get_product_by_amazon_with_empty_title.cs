namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(GetProductByAmazonQuery))]
    public class When_get_product_by_amazon_with_empty_title
    {
        #region Establish value

        static MockMessage<GetProductByAmazonQuery, List<ProductAmazonVm>> mockQuery;

        static List<ProductAmazonVm> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<GetProductByAmazonQuery>(dsl => dsl.Empty(r => r.Title));
                                      expected = new List<ProductAmazonVm>();

                                      mockQuery = MockQuery<GetProductByAmazonQuery, List<ProductAmazonVm>>
                                              .When(query);
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result = () => mockQuery.ShouldBeIsResult(expected);
    }
}