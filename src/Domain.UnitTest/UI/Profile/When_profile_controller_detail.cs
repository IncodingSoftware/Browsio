namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(ProfileController))]
    public class When_profile_controller_detail
    {
        #region Establish value

        static MockController<ProfileController> mockController;

        static ActionResult result;

        static GetUserDetailQuery query;

        static GetUserDetailQuery.Response expected;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetUserDetailQuery>();
                                      expected = Pleasure.Generator.Invent<GetUserDetailQuery.Response>();

                                      mockController = MockController<ProfileController>
                                              .When()
                                              .StubQuery(query, expected);


                                  };

        Because of = () => { result = mockController.Original.Detail(query); };

        It should_be_result = () => result.ShouldBeIncodingSuccess();

        It should_be_render = () => mockController.ShouldBeRenderModel(expected);
    }
}