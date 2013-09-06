namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Browsio.Amazon;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(ProductController))]
    public class When_product_controller_fetch_by_amazon
    {
        #region Establish value

        static MockController<ProductController> mockController;

        static ActionResult result;

        static GetProductByAmazonQuery query;

        static List<ProductAmazonVm> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetProductByAmazonQuery>();
                                      expected = Pleasure.ToList(Pleasure.Generator.Invent<ProductAmazonVm>());

                                      mockController = MockController<ProductController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => { result = mockController.Original.FetchByAmazon(query); };

        It should_be_result = () => result.ShouldBeIncodingData(expected);
    }
}