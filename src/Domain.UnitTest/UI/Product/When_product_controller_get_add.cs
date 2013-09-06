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
    public class When_product_controller_get_add
    {
        #region Establish value

        static MockController<ProductController> mockController;

        static ActionResult result;

        static string storeId;

        #endregion

        Establish establish = () =>
                                  {
                                      storeId = Pleasure.Generator.String();

                                      mockController = MockController<ProductController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.Add(storeId); };

        It should_be_result = () => result.ShouldBeIncodingSuccess();

        It should_be_render = () => mockController.ShouldBeRenderModel(new AddProductCommand { StoreId = storeId });
    }
}