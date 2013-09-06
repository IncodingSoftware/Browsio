namespace Browsio.UnitTest
{
    using Browsio.Controllers;
    using Incoding.MSpecContrib;
    using Machine.Specifications;



    [Subject(typeof(ProfileController))]
    public class When_profile_controller_index
    {
        #region Establish value

        static MockController<ProfileController> mockController;

        #endregion

        Establish establish = () =>
                                  {
                                      mockController = MockController<ProfileController>
                                              .When();

                                  };

        Because of = () => mockController.Original.Index();

        It should_be_render = () => mockController.ShouldBeRenderView();
    }
}