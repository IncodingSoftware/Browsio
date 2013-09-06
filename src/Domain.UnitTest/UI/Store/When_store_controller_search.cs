namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Browsio.Domain.Search.Query;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(StoreController))]
    public class When_store_controller_search
    {
        #region Establish value

        static MockController<StoreController> mockController;

        static ActionResult result;

        static List<SearchStoreQuery.Response> expected;

        static SearchStoreQuery searchStoreQuery;

        #endregion

        Establish establish = () =>
                                  {                                      
                                      searchStoreQuery = Pleasure.Generator.Invent<SearchStoreQuery>();
                                      expected = Pleasure.ToList(Pleasure.Generator.Invent<SearchStoreQuery.Response>());
                                      
                                      searchIds = "id,id2,id3";
                                      searchIdsArray = Pleasure.ToArray(Pleasure.Generator.String());

                                      mockController = MockController<StoreController>
                                              .When()
                                              .StubQuery(searchStoreQuery, expected)
                                              .StubQuery(new GetStoreIdsBySearchIdsQuery()
                                                             {
                                                                     SearchIds = new[] { "id", "id2", "id3" }
                                                             }, searchIdsArray);
                                  };

        Because of = () => { result = mockController.Original.Search(searchStoreQuery,searchIds, friends: null); };

        It should_be_result = () => result.ShouldBeIncodingData(expected);

        static string searchIds;

        static string[] searchIdsArray;
    }
}