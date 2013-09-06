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
    public class When_profile_controller_post_edit
    {
        #region Establish value

        static MockController<ProfileController> mockController;

        static ActionResult result;

        static EditUserCommand input;

        #endregion

        Establish establish = () =>
                                  {
                                      input = Pleasure.Generator.Invent<EditUserCommand>();

                                      mockController = MockController<ProfileController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.Edit(input); };

        It should_be_redirect = () => result.ShouldBeIncodingSuccess();

        It should_be_push = () => mockController.ShouldBePush(input);
    }
}