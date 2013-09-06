namespace Browsio.UnitTest
{
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using System.Web.Mvc;


    [Subject(typeof(ProfileController))]
    public class When_profile_controller_get_user_login
    {
        #region Establish value

        static MockController<ProfileController> mockController;

        static ActionResult result;

        static GetUserLoginQuery query;

        static IncStructureResponse<string> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetUserLoginQuery>();

                                      expected = new IncStructureResponse<string>("Nikolya");

                                      mockController = MockController<ProfileController>
                                              .When()
                                              .StubQuery(query,expected);

                                  };

        Because of = () => result = mockController.Original.UserLogin(query);


        It should_be_result = () => result.ShouldBeIncodingData<IncStructureResponse<string>>(exp => exp.ShouldEqualWeak(expected));
    }
}