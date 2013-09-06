namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(ProfileController))]
    public class When_profile_controller_get_edit
    {
        #region Establish value

        static MockController<ProfileController> mockController;

        static ActionResult result;

        static User expected;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<GetEntityByIdQuery<User>>(dsl => dsl.Tuning(r => r.Id, BrowsioPleasure.UserId));
                                      expected = Pleasure.Generator.Invent<User>(dsl => dsl.Tuning(r => r.Image, Pleasure.Generator.Bytes()));

                                      mockController = MockController<ProfileController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => { result = mockController.Original.Edit(); };

        It should_be_result = () => result.ShouldBeIncodingSuccess();

        It should_be_render = () => mockController.ShouldBeRenderModel<EditUserCommand>(command => command.ShouldEqualWeak(expected, dsl => dsl.ForwardToValue(r => r.HasImage, true)
                                                                                                                                               .IgnoreBecauseNotUse(r => r.DisplayPicture)));
    }
}