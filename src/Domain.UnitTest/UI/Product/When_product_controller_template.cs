namespace Browsio.UnitTest
{
    using Browsio.Controllers;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;



    [Subject(typeof(ProductController))]
    public class When_product_controller_template
    {
        #region Establish value

        static MockController<ProductController> mockController;

        static bool isOwner;

        #endregion

        Establish establish = () =>
                                  {
                                      isOwner = Pleasure.Generator.Bool();
                                      mockController = MockController<ProductController>
                                              .When();

                                  };

        Because of = () => mockController.Original.Template(isOwner);

        It should_by_result = () => mockController.ShouldBeRenderModel(new IncStructureResponse<bool>(isOwner), "Template");
    }
}