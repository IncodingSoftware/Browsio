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
    public class When_product_controller_post_edit
    {
        #region Establish value

        static MockController<ProductController> mockController;

        static ActionResult result;

        static EditProductCommand input;

        #endregion

        Establish establish = () =>
                                  {
                                      input = Pleasure.Generator.Invent<EditProductCommand>();

                                      mockController = MockController<ProductController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.Edit(input); };

        It should_be_success = () => result.ShouldBeIncodingSuccess();

        It should_be_push = () => mockController.ShouldBePush(input);
    }
}