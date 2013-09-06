namespace Browsio.UnitTest
{
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using System.Web.Mvc;


    [Subject(typeof(ProfileController))]
    public class When_profile_controller_post_followers
    {
        #region Establish value

        static MockController<ProfileController> mockController;

        static GetFollowersQuery query;

        static IncStructureResponse<int> expected;

        static ActionResult result;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetFollowersQuery>();

                                      expected = new IncStructureResponse<int>(10);

                                      mockController = MockController<ProfileController>
                                              .When()
                                              .StubQuery(query,expected);

                                  };

        Because of = () => result = mockController.Original.Followers(query);


        It should_be_result = () => result.ShouldBeIncodingData<IncStructureResponse<int>>(query => query.ShouldEqualWeak(expected));
    }
}