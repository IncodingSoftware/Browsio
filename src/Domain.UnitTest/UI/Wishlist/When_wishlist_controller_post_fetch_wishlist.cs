namespace Browsio.UnitTest.Wishlist
{
    using System.Collections.Generic;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using System.Web.Mvc;


    [Subject(typeof(WishlistController))]
    public class When_wishlist_controller_post_fetch_wishlist
    {
        #region Establish value

        static MockController<WishlistController> mockController;

        static GetProductInWishlist query;

        static IList<GetProductInWishlist.Response> expected;

        static ActionResult result;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetProductInWishlist>();

                                      expected = Pleasure.ToList(Pleasure.Generator.Invent<GetProductInWishlist.Response>());

                                      mockController = MockController<WishlistController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => result = mockController.Original.FetchWishlist(query);


        It should_be_result = () => result.ShouldBeIncodingData(expected);
    }
}