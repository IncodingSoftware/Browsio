namespace Browsio.UnitTest
{
    using System;
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;



    [Subject(typeof(AccountController))]
    public class When_account_controller_forgot_password_post
    {
        #region Establish value

        static MockController<AccountController> mockController;

        static ActionResult result;

        static NotificationVm notificationVm;

        static ForgotUserPasswordCommand input;

        #endregion

        Establish establish = () =>
                                  {
                                      input = Pleasure.Generator.Invent<ForgotUserPasswordCommand>();
                                      notificationVm = Pleasure.Generator.Invent<NotificationVm>();
                                      mockController = MockController<AccountController>
                                              .When()
                                              .StubRequestUrl(new Uri("http://localhost"))
                                              .StubUrlAction("http://localhost/Account/ChangePassword?ResetToken=" + input.Token);
                                  };

        Because of = () => { result = mockController.Original.ForgotPassword(input); };


        It should_be_success = () => result.ShouldBeRedirectToAction<NotificationController>(r => r.LandingAjax(notificationVm));

        It should_be_push = () => mockController.ShouldBePush(input);
    }
}