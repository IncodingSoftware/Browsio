namespace Browsio.UnitTest
{
    #region << Using >>

    using System;
    using System.Web;
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;
    using Incoding.Extensions;

    #endregion

    [Subject(typeof(AccountController))]
    public class When_account_controller_post_sign_up
    {
        #region Establish value

        static MockController<AccountController> mockController;

        static ActionResult result;

        static AddUserCommand input;

        static NotificationVm notificationVm;

        #endregion

        Establish establish = () =>
                                  {
                                      notificationVm = Pleasure.Generator.Invent<NotificationVm>();
                                      input = Pleasure.Generator.Invent<AddUserCommand>();
                                      scheme = "http";
                                      authority = "somedomain.com";
                                      accountConfirmUrl = "/Account/Confirm";

                                      mockController = MockController<AccountController>
                                              .When()
                                              .DisableAjax()
                                              .StubUrlAction(s => s.ShouldEqualWeak(accountConfirmUrl), accountConfirmUrl);

                                      Mock<HttpContextBase> mockHttpContext = mockController.TryGetValue("httpContext") as Mock<HttpContextBase>;
                                      mockHttpContext.SetupGet(r => r.Request)
                                                     .Returns(Pleasure.MockAsObject<HttpRequestBase>(
                                                     mock => mock
                                                         .SetupGet(r => r.Url).Returns(
                                                         new Uri(scheme + "://" + authority))
                                                                      ));
                                  };

        Because of = () => { result = mockController.Original.SignUp(input); };

        It should_be_redirect = () => result.ShouldBeRedirectToAction<NotificationController>(controller => controller.LandingAjax(notificationVm));

        It should_be_push = () => mockController.ShouldBePush(input);

        It should_be_set_path_result = () => input.PathResult.ShouldEqualWeak(scheme + "://" + authority + accountConfirmUrl);

        static string scheme;

        static string authority;

        static string accountConfirmUrl;
    }
}