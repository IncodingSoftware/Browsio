namespace Browsio.UnitTest
{
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Incoding.MSpecContrib;
    using Machine.Specifications;



    [Subject(typeof(AccountController))]
    public class When_account_controller_confirm_success
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

        Because of = () => result = mockController.Original.ConfirmSuccess();

        It should_result = () => result.ShouldBeViewName("");
    }
}