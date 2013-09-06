namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(ProductController))]
    public class When_product_controller_search_in_amazon
    {
        #region Establish value

        static MockController<ProductController> mockController;

        static ActionResult result;

        #endregion

        Establish establish = () =>
                                  {
                                      mockController = MockController<ProductController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.SearchInAmazon(); };

        It should_be_result = () => result.ShouldBeIncodingSuccess();

        It should_be_render = () => mockController.ShouldBeRenderModel(new GetProductByAmazonQuery
                                                                           {
                                                                                   Page = 1
                                                                           });
    }
}