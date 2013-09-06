namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(ProductController))]
    public class When_product_controller_get_edit
    {
        #region Establish value

        static MockController<ProductController> mockController;

        static ActionResult result;

        static GetEntityByIdQuery<Product> query;

        static Product expected;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetEntityByIdQuery<Product>>();
                                      expected = Pleasure.Generator.Invent<Product>();

                                      mockController = MockController<ProductController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => { result = mockController.Original.Edit(query); };

        It should_be_result = () => result.ShouldBeIncodingSuccess();

        It should_be_render = () => mockController.ShouldBeRenderModel<EditProductCommand>(command => command.ShouldEqualWeak(expected,dsl => dsl.IgnoreBecauseNotUse(r=>r.Image)));
    }
}