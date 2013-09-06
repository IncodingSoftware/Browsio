namespace Browsio.UnitTest
{
    using Browsio.Controllers;
    using Incoding.MSpecContrib;
    using Machine.Specifications;



    [Subject(typeof(ActivityController))]
    public class When_activity_controller_get_index
    {
        #region Establish value

        static MockController<ActivityController> mockController;

        #endregion

        Establish establish = () =>
                                  {
                                      mockController = MockController<ActivityController>
                                              .When();

                                  };

        Because of = () => mockController.Original.Index();

        It should_be_render = () => mockController.ShouldBeRenderView("Index");
    }
}