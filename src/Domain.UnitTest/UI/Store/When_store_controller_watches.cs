using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.UnitTest
{
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    [Subject(typeof(StoreController))]
    public class When_store_controller_watches
    {
        static CountWatchesQuery query;

        static IncStructureResponse<int> expected;

        static MockController<StoreController> mockController;

        static ActionResult result;

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<CountWatchesQuery>();
                                      expected = new IncStructureResponse<int>(Pleasure.Generator.PositiveNumber());

                                      mockController = MockController<StoreController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => result = mockController.Original.Watches(query);

        It shuold_be_result = () => result.ShouldBeIncodingData(expected);



    }
}
