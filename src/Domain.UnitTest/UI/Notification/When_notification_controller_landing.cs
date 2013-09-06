namespace Browsio.UnitTest.Notification
{
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;



    [Subject(typeof(NotificationController))]
    public class When_notification_controller_landing
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

        Because of = () => result = mockController.Original.Landing(notificationVm);

        It should_be_model = () => result.ShouldBeModel(notificationVm);

        It should_be_view = () => result.ShouldBeViewName("");
    }
}