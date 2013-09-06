namespace Browsio.UnitTest
{
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Incoding.MSpecContrib;
    using Machine.Specifications;



    [Subject(typeof(AccountController))]
    public class When_account_controller_forgot_password
    {
        #region Establish value

        static MockController<AccountController> mockController;

        #endregion

        Establish establish = () =>
                                  {
                                      mockController = MockController<AccountController>
                                              .When();

                                  };

        Because of = () => mockController.Original.ForgotPassword();

        It should_result = () => mockController.ShouldBeRenderView();
    }
}