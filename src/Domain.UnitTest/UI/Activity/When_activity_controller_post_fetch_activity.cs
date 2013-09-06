namespace Browsio.UnitTest
{
    using System.Collections.Generic;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using System.Web.Mvc;


    [Subject(typeof(ActivityController))]
    public class When_activity_controller_post_fetch_activity
    {
        #region Establish value

        static MockController<ActivityController> mockController;

        static GetActivityQuery query;

        static List<ActivityVm> expected;

        static ActionResult result;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetActivityQuery>();

                                      expected = Pleasure.ToList(Pleasure.Generator.Invent<ActivityVm>());

                                      mockController = MockController<ActivityController>
                                              .When()
                                              .StubQuery(query, expected);

                                  };

        Because of = () => result = mockController.Original.FetchActivity(query);


        It should_be_result = () => result.ShouldBeIncodingData(expected);
    }
}