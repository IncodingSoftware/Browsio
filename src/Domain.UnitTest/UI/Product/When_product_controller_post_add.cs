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
    public class When_product_controller_post_add
    {
        #region Establish value

        static MockController<ProductController> mockController;

        static ActionResult result;

        static AddProductCommand input;

        #endregion

        Establish establish = () =>
                                  {
                                      input = Pleasure.Generator.Invent<AddProductCommand>();

                                      mockController = MockController<ProductController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.Add(input,string.Empty); };

        It should_be_success = () => result.ShouldBeIncodingSuccess();

        It should_be_push = () => mockController.ShouldBePush(input);
    }
}