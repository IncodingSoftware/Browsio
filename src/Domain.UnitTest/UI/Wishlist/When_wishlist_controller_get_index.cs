namespace Browsio.UnitTest.Wishlist
{
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Incoding.MSpecContrib;
    using Machine.Specifications;



    [Subject(typeof(WishlistController))]
    public class When_wishlist_controller_get_index
    {
        #region Establish value

        static MockController<WishlistController> mockController;

        #endregion

        Establish establish = () =>
                                  {
                                      mockController = MockController<WishlistController>
                                              .When();

                                  };

        Because of = () => mockController.Original.index();

        It should_be_render = () => mockController.ShouldBeRenderView("index");
    }
}