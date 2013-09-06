namespace Browsio.UnitTest.Notification
{
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using System.Web.Mvc;


    [Subject(typeof(NotificationController))]
    public class When_notification_controller_lending_ajax
    {
        #region Establish value

        static MockController<NotificationController> mockController;

        static NotificationVm notificationVm;

        static ActionResult result;

        #endregion

        Establish establish = () =>
                                  {
                                      notificationVm = Pleasure.Generator.Invent<NotificationVm>();

                                      mockController = MockController<NotificationController>
                                              .When();

                                  };

        Because of = () => result = mockController.Original.LandingAjax(notificationVm);


        It should_be_result = () => mockController.ShouldBeRenderModel(notificationVm, "LandingPartial");
    }
}