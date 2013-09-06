namespace Browsio.UnitTest.Wishlist
{
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;



    [Subject(typeof(WishlistController))]
    public class When_wishlist_controller_post_add_product_to_wishlist
    {
        #region Establish value

        static MockController<WishlistController> mockController;

        static ActionResult result;

        static AddProductToWishlistCommand input;

        #endregion

        Establish establish = () =>
                                  {
                                      input = Pleasure.Generator.Invent<AddProductToWishlistCommand>();

                                      mockController = MockController<WishlistController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.AddProductToWishList(input); };


        It should_be_success = () => result.ShouldBeIncodingSuccess();

        It should_be_push = () => mockController.ShouldBePush(input);
    }
}