using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.UnitTest
{
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using System.Web.Mvc;


    [Subject(typeof(StoreController))]
    public class When_store_controller_likes_this_store
    {
        #region Establish value

        static MockController<StoreController> mockController;

        static ActionResult result;

        static CountLikesQuery query;

        static IncStructureResponse<int> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<CountLikesQuery>();

                                      expected = new IncStructureResponse<int>(Pleasure.Generator.PositiveNumber());

                                      mockController = MockController<StoreController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => result = mockController.Original.LikesThisStore(query);


        It should_be_result = () => result.ShouldBeIncodingData(expected);
    }
}