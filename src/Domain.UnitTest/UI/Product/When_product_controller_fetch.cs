namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(ProductController))]
    public class When_product_controller_fetch
    {
        #region Establish value

        static MockController<ProductController> mockController;

        static ActionResult result;

        static GetProductByStoreQuery queryProduct;

        static ProductInWishlistQuery queryPw;

        static List<Product> expectedProducts;

        static Product product;

        static IList<ProductVm> expectedPwms;

                #endregion

        Establish establish = () =>
                                  {

                                      queryProduct = Pleasure.Generator.Invent<GetProductByStoreQuery>();
                                      product = Pleasure.Generator.Invent<Product>();
                                      expectedProducts = Pleasure.ToList(Pleasure.Generator.Invent<Product>());
                                      queryPw = Pleasure.Generator.Invent<ProductInWishlistQuery>();
                                      var expectedPwm = new ProductVm(product);
                                      expectedPwm.IsAddWishList = true;
                                      expectedPwms = new List<ProductVm>();
                                      expectedPwms.Add(expectedPwm);
                                      mockController = MockController<ProductController>
                                              .When()
                                              .StubQuery(queryProduct, expectedProducts)
                                              .StubQuery(queryPw, expectedPwms);
                                  };

        Because of = () => { result = mockController.Original.Fetch(queryProduct); };

        It should_be_result
                = () => result.ShouldBeIncodingData<List<ProductVm>>(vms => vms.ShouldEqualWeakEach(expectedPwms, 
                                                                                                    (dsl, i) => dsl.ForwardToValue(r => r.HasImage, true)
                                                                                                                   .ForwardToValue(r => r.Price, expectedPwms[i].Price.ToString())));
    }
}