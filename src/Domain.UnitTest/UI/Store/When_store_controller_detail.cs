namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(StoreController))]
    public class When_store_controller_detail
    {
        #region Establish value

        static MockController<StoreController> mockController;

        static ActionResult result;

        static GetStoreDetailQuery query;

        static GetStoreDetailQuery.Response expected;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetStoreDetailQuery>();
                                      expected = Pleasure.Generator.Invent<GetStoreDetailQuery.Response>();

                                      mockController = MockController<StoreController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => { result = mockController.Original.Detail(query); };

        It should_be_result = () => result.ShouldBeIncodingData(expected);
    }
}