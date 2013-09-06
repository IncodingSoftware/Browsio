namespace Browsio.UnitTest
{
    using Browsio.Controllers;
    using Incoding.MSpecContrib;
    using Machine.Specifications;



    [Subject(typeof(AccountController))]
    public class When_account_controller_get_sign_up
    {
        #region Establish value

        static MockController<AccountController> mockController;

        #endregion

        Establish establish = () =>
                                  {
                                      mockController = MockController<AccountController>
                                              .When();

                                  };

        Because of = () =>  mockController.Original.SignUp();

        It should_be_result = () => mockController.ShouldBeRenderView("SignUp");
    }
}