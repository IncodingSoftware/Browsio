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
    public class When_store_controller_unfollow_this_store
    {
        static DeleteFollowCommand command;

        static IncStructureResponse<int> expected;

        static MockController<StoreController> mockController;

        static ActionResult result;

        Establish establish = () =>
        {
            command = Pleasure.Generator.Invent<DeleteFollowCommand>();

            mockController = MockController<StoreController>
                    .When();
        };

        Because of = () => result = mockController.Original.UnFollowThisStore(command);

        It shuold_be_result = () => result.ShouldBeIncodingSuccess();

        It shuold_be_push = () => mockController.ShouldBePush(command);



    }
}