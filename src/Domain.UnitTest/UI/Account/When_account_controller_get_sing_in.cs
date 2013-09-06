namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Incoding.MvcContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(AccountController))]
    public class When_account_controller_get_sing_in
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

        Because of = () => { result = mockController.Original.SignIn(); };

        It should_be_result = () => result.ShouldBeOfType<IncodingResult>();
    }
}