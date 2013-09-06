namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Incoding.MvcContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(ProductController))]
    public class When_product_controller_post_add_with_image_url
    {
        #region Establish value

        static MockController<ProductController> mockController;

        static ActionResult result;

        static AddProductCommand input;

        static string imageUrl;

        #endregion

        Establish establish = () =>
                                  {
                                      input = Pleasure.Generator.Invent<AddProductCommand>();
                                      imageUrl = "http://ecx.images-amazon.com/images/I/41A8SsxNQcL._SL160_.jpg";

                                      mockController = MockController<ProductController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.Add(input, imageUrl); };

        It should_be_success = () => result.ShouldBeIncodingSuccess();

        It should_be_push = () => mockController.ShouldBePush<AddProductCommand>(command => command.ShouldEqualWeak(input, dsl => dsl.IgnoreBecauseCalculate(r => r.Image)));

        It should_be_download_image = () => new HttpMemoryPostedFile(input.Image).ContentAsBytes.Length.ShouldEqual(3477);
    }
}