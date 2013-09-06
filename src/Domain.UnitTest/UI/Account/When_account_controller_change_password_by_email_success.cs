namespace Browsio.UnitTest
{
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Incoding.MSpecContrib;
    using Machine.Specifications;



    [Subject(typeof(AccountController))]
    public class When_account_controller_change_password_by_email_success
    {
        #region Establish value

        static MockController<AccountController> mockController;

        static ActionResult result;

        #endregion

        Establish establish = () =>
                                  {
                                      mockController = MockController<AccountController>
                                              .When();

                                  };

        Because of = () => result = mockController.Original.ChangePasswordByEmailSuccess();

        It should_be_result = () => result.ShouldBeViewName("");
    }
}