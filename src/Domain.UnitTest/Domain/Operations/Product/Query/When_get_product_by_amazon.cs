namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using Browsio.Amazon;
    using Browsio.Domain;
    using Incoding.Block.IoC;
    using Incoding.Extensions;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(GetProductByAmazonQuery))]
    public class When_get_product_by_amazon
    {
        #region Establish value

        static MockMessage<GetProductByAmazonQuery, List<ProductAmazonVm>> mockQuery;

        static List<AmazonItem> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<GetProductByAmazonQuery>();

                                      var hasProduct = Pleasure.Generator.Invent<AmazonItem>();
                                      var notHasProduct = Pleasure.Generator.Invent<AmazonItem>();
                                      expected = Pleasure.ToList(notHasProduct, hasProduct);

                                      string category = Pleasure.Generator.String();

                                      var amazonService = Pleasure.MockAsObject<IAmazonService>(mock => mock.Setup(r => r.Fetch(query.Title, query.Page, category)).Returns(expected));
                                      IoCFactory.Instance.StubTryResolve(amazonService);

                                      mockQuery = MockQuery<GetProductByAmazonQuery, List<ProductAmazonVm>>
                                              .When(query)
                                              .StubGetById(query.StoreId, Pleasure.MockAsObject<Store>(mock => mock.SetupGet(r => r.CategoryAsAmazon).Returns(category)))
                                              .StubNotEmptyQuery(whereSpecification: new ProductByStoreWhereSpec(query.StoreId).And(new ProductByASINWhereSpec(hasProduct.ASIN)));
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result
                = () => mockQuery.ShouldBeIsResult(vms => vms.ShouldEqualWeakEach(expected, 
                                                                                  (dsl, i) => dsl.ForwardToAction(r => r.Exist, vm =>
                                                                                                                                    {
                                                                                                                                        if (i == 1)
                                                                                                                                            vm.Exist.ShouldBeTrue();
                                                                                                                                        else
                                                                                                                                            vm.Exist.ShouldBeFalse();
                                                                                                                                    })
                                                                                                 .ForwardToAction(r => r.IsLast, vm =>
                                                                                                                                     {
                                                                                                                                         if (i == 1)
                                                                                                                                             vm.IsLast.ShouldBeTrue();
                                                                                                                                         else
                                                                                                                                             vm.IsLast.ShouldBeFalse();
                                                                                                                                     })
                                                                                                 .ForwardToValue(r => r.Amount, expected[i].Price.ToString())));
    }
}